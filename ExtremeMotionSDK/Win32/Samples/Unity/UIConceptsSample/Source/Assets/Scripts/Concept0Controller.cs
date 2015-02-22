using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Xtr3D.Net.ExtremeMotion.Data;
/// <summary>
/// MxN grid,time based click
/// </summary>
public class Concept0Controller : MonoBehaviour {
	
	private const float CLICK_TIME_IN_SEC = 1;
	private const int INVALID_VALUE = -1;
	private const int BUTTONS_IN_ROW = 3;
	private const int BUTTONS_IN_COL = 2;
	private const int NUM_OF_BUTTONS = BUTTONS_IN_ROW * BUTTONS_IN_COL;
	private const float  BUTTON_VALIDATION_TIME = 0.1f;
	private const float  TIME_BASED_ANIMATION_START_DELAY = 1f;
	
	private const float SKELETON_MAX_X_POS = 1.15f;
	private const float SKELETON_MIN_X_POS = 0.31f;
	private const float REGION_START_X = 0.30f;
	private const float REGION_START_Y = 0.8f;
	private const float REGION_WIDTH = 0.30f;
	private const float REGION_HEIGHT = 0.45f;
	
	private const float SCENE_BUTTON_HYSTE_X = 0.05f;
	private const float SCENE_BUTTON_HYSTE_Y = 0.05f;
	
	// buttons params
	private const float BUTTON_WIDTH = 367;
	private const float BUTTON_HEIGHT = 211;
	private const float BUTTON_START_X_POS = -370;
	private const float BUTTON_START_Y_POS = 240;
	private const float BUTTON_X_ROW_SPACE = 20;
	private const float BUTTON_Y_ROW_SPACE = 140;
	
	public CalibrationManager m_calibrationManager;
	private GenericRegionsManager m_regionManager;
	public SkeletonManager m_skeletonManager;
	public TimeBaseAnimation m_timeBaseAnimation;
	public SceneHeadline m_myHeadline;
	
	private float m_myTimer = 0; // game timer
	private int m_currentSelectedRegionID = INVALID_VALUE;
	private int m_lastSelectedRegionID = INVALID_VALUE;
	private float m_buttonSelectedStartedTime = INVALID_VALUE;
	private float m_buttonValidationStartedTime = INVALID_VALUE;
	private float m_lastRegionValidationID = INVALID_VALUE;
	private float m_timeBasedClickDelay = INVALID_VALUE;
	
	private int m_currentSelectedButtonID = INVALID_VALUE;
	private int m_lastSelectedButtonID = INVALID_VALUE;
	
	private GridItem[] m_gridItems;
	// maps region id's to their current active button
	private Dictionary<int, int> m_regionIdToButtonId = new Dictionary<int, int>();
	
	private static TrackingState m_lastEngineState = TrackingState.NotTracked;
	
	void Awake()
	{
		//Creating region manager
		m_regionManager = new GenericRegionsManager();
	}
	void Start () {
		CreateButtons();
		CreateRegions();
		
		m_myHeadline.SetText("Grid Selection");
	}
	
	void Update () {
		
		//updating scene timer
		m_myTimer += Time.deltaTime;
		UpdateCalibrationState();
		// We want to allow navigate throw menu only if T pose geture is not performed
		UpdateActiveButton();
	}
	
	/// <summary>
	/// Updates the state of the calibration screen.
	/// </summary>
	private void UpdateCalibrationState ()
	{
		if(!m_lastEngineState.Equals(m_skeletonManager.GetTrackingState()))
		{
			m_lastEngineState = m_skeletonManager.GetTrackingState();
		
			switch (m_lastEngineState) {
			case TrackingState.Calibrating:
				m_calibrationManager.ShowCalibration(true);
			break;
				
			case TrackingState.Tracked:
				m_calibrationManager.ShowCalibration(false);
			break;
			default:
			break;
			}
		}
	}
	
	/// <summary>
	/// Creates the buttons and placing them in scene.
	/// </summary>
	private void CreateButtons()
	{
		m_gridItems = new GridItem[NUM_OF_BUTTONS];
		GameObject gridItemPrefab = (GameObject) Resources.Load("Prefabs/GridItem"); // loading grid item prefab from resources
		//Building the buttons matrix
		
		for(int i = 0; i < BUTTONS_IN_ROW;i++)
		{
			for(int j = 0; j < BUTTONS_IN_COL;j++)
			{
				GameObject myItemObject = (GameObject) Instantiate(gridItemPrefab,Vector3.zero,Quaternion.identity);
				m_gridItems[j*BUTTONS_IN_ROW+i] = myItemObject.GetComponent<GridItem>();
				m_gridItems[j*BUTTONS_IN_ROW+i].transform.parent = this.transform;
				m_gridItems[j*BUTTONS_IN_ROW+i].Init(new Vector3(BUTTON_START_X_POS + (i*(BUTTON_WIDTH + BUTTON_X_ROW_SPACE)),BUTTON_START_Y_POS - (j * (BUTTON_HEIGHT + BUTTON_Y_ROW_SPACE)),0));
				((GridItem)m_gridItems[j*BUTTONS_IN_ROW+i]).SetImage(j*BUTTONS_IN_ROW+i);
				m_gridItems[j*BUTTONS_IN_ROW+i].SetImageSize(BUTTON_WIDTH,BUTTON_HEIGHT);
			}
		}
	}
	
	/// <summary>
	/// Creates the regions.
	/// </summary>
	private void CreateRegions ()
	{
		for(int i = 0; i < BUTTONS_IN_ROW;i++)
		{
			for(int j = 0; j < BUTTONS_IN_COL;j++)
			{
				int regionID = m_regionManager.AddRegion(REGION_START_X + (i * REGION_WIDTH),REGION_START_Y - (j * REGION_HEIGHT),REGION_WIDTH,REGION_HEIGHT,SCENE_BUTTON_HYSTE_X,SCENE_BUTTON_HYSTE_Y);
				m_regionIdToButtonId.Add(regionID,j*BUTTONS_IN_ROW+i);
			}
			
		}
	}
	
	/// <summary>
	/// Updates the active buttons.
	/// </summary>
	private void UpdateActiveButton ()
	{	
		if(m_lastEngineState.Equals(TrackingState.Tracked)) // if user is tracked
		{
			// limits palm x pos by SKELETON_MAX_X_POS
			Vector2 palmPosition = m_skeletonManager.GetRightPalmPosition();
			Vector2 palmFixedPosition = new Vector2(Mathf.Clamp(palmPosition.x,SKELETON_MIN_X_POS,SKELETON_MAX_X_POS), palmPosition.y);
			//saving current active region according to cursor position
			int currentRegion = m_regionManager.GetActiveRegion(palmFixedPosition);
			
			if(IsButtonSelectionValid(currentRegion)) // checking if active region is selected for BUTTON_VALIDATION_TIME
			{
				m_currentSelectedRegionID = currentRegion;
				//checking if a new region is selected
				if(m_currentSelectedRegionID != m_lastSelectedRegionID)
				{
					m_currentSelectedButtonID = m_regionIdToButtonId[m_currentSelectedRegionID];
					m_gridItems[m_currentSelectedButtonID].HighLightState(true);
					
					DeselectLastRegion();
					//update new selected button
					m_lastSelectedRegionID = m_currentSelectedRegionID;
					m_lastSelectedButtonID = m_currentSelectedButtonID;
					
					// if we have a new button we want starting a new time based click
					InitClickState();
				}
				UpdateTimeBaseClick();
			}
		}
		else // user is not tracked
		{
			InitClickState();
		}
	}
	
	/// <summary>
	/// Checks if the current button is selected for BUTTON_VALIDATION_TIME
	/// </summary>
	/// <returns>
	/// <c>true</c> if BUTTON_VALIDATION_TIME passed ; otherwise, <c>false</c>.
	/// </returns>
	private bool IsButtonSelectionValid (int activeRegionId)
	{
		bool valid = false;
		if(activeRegionId != INVALID_VALUE)
		{
			if(activeRegionId != m_lastRegionValidationID)
			{
				// we have a new region to check validation
				m_buttonValidationStartedTime = INVALID_VALUE;
				m_lastRegionValidationID = activeRegionId;
			}
			else if(m_buttonValidationStartedTime == INVALID_VALUE)
			{
				m_buttonValidationStartedTime = m_myTimer;
			}
			else
			{
				if(m_myTimer - m_buttonValidationStartedTime > BUTTON_VALIDATION_TIME)
				{
					m_buttonValidationStartedTime = INVALID_VALUE;
					valid = true;
				}
			}
		}
		else{
			InitRegionsState(); //we are on the empty region!
		}
		
		return valid;
	}
	
	private bool IsTimeBasedAnimationDelayFinished ()
	{
		bool delayFinished = false;
		
		if(m_timeBasedClickDelay == INVALID_VALUE)
		{
			m_timeBasedClickDelay = m_myTimer;
		}
		else
		{
			if(m_myTimer - m_timeBasedClickDelay > TIME_BASED_ANIMATION_START_DELAY)
			{
				delayFinished = true;
			}
		}
		
		return delayFinished;
	}
	
	/// <summary>
	/// Play time base animation and updates click time.
	/// </summary>
	private void UpdateTimeBaseClick ()
	{
		if(IsTimeBasedAnimationDelayFinished())
		{
			if(m_buttonSelectedStartedTime == INVALID_VALUE) // checks if time based click didn't start yet
			{
				UpdateClickAnimationPosition();
				m_buttonSelectedStartedTime = m_myTimer;
				m_timeBaseAnimation.PlayAnimation(); // starts time based click animation 
			}
			else // time based click already started
			{
				//checking if time from m_buttonSelectedStartedTime is larger than click time threshold
				if(m_myTimer - m_buttonSelectedStartedTime > CLICK_TIME_IN_SEC)
				{
					// if button not already on click state
					if(!m_gridItems[m_currentSelectedButtonID].IsClicked)
					{
						//button clicked
						m_gridItems[m_currentSelectedButtonID].ClickedState();
						m_timeBaseAnimation.HideAnimation();
					}
				}
			}
		}
	}
	
	void UpdateClickAnimationPosition ()
	{
		m_timeBaseAnimation.transform.localPosition = m_gridItems[m_regionIdToButtonId[m_currentSelectedRegionID]].transform.localPosition;
	}
	
	/// <summary>
	/// Inits the state of the regions.
	/// </summary>
	private void InitRegionsState()
	{
		DeselectLastRegion();
		m_currentSelectedRegionID = INVALID_VALUE;
		m_currentSelectedButtonID = INVALID_VALUE;
		m_lastSelectedRegionID = INVALID_VALUE;
		m_lastSelectedButtonID = INVALID_VALUE;
		m_lastRegionValidationID = INVALID_VALUE;
		m_timeBaseAnimation.HideAnimation();
	}
	
	/// <summary>
	/// Initilizes the state.
	/// </summary>
	private void InitClickState()
	{
		m_timeBasedClickDelay = INVALID_VALUE;
		m_buttonSelectedStartedTime = INVALID_VALUE;
		m_timeBaseAnimation.HideAnimation();
	}
	/// <summary>
	/// Deslects the last button.
	/// </summary>
	private void DeselectLastRegion ()
	{
		//deselect last highlighted button if we have one
		if(m_lastSelectedRegionID != INVALID_VALUE)
		{
			m_gridItems[m_lastSelectedButtonID].HighLightState(false);
		}
	}
}
