using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Xtr3D.Net.ExtremeMotion.Data;
/// <summary>
/// Two Hands
/// </summary>
public class Concept4Controller : MonoBehaviour {
	
	private const int NUM_OF_BUTTONS = 5;
	private const int INVALID_VALUE = -1;
	private const float  BUTTON_VALIDATION_TIME = 0.2f;
	private const float TEXTURE_4X3_POS = 440f;
	
	//Regions
	private const float REGION_START_X = 0.5f;
	private const float REGION_START_Y = 1f;
	private const float BUTTON_REGION_HEIGHT = 0.4f;
	private const float REGION_WIDTH = 2f;
	private const float SCENE_BUTTON_HYSTE_X = 0.1f;
	private const float SCENE_BUTTON_HYSTE_Y = 0.1f;
	
	public SceneHeadline m_sceneHeadline;
	public SwitchTexture m_switchTexture;
	public SkeletonManager m_skeletonManager;
	public CalibrationManager m_calibrationManager;
	private GenericRegionsManager m_regionsManager;
	public VerticalItem[] m_buttons;
	
	private Dictionary<int, int> m_regionIdToButtonId = new Dictionary<int, int>();
	
	private string texturesPath = "Materials/";
	private string currentImageName = "Image_1152x648_";
	
	private int m_lastSelectedButtonID = INVALID_VALUE;
	private int m_lastSelectedRegionID = INVALID_VALUE;
	private float m_myTimer = 0;
	private float m_buttonValidationStartedTime = INVALID_VALUE;
	private static TrackingState m_lastEngineState = TrackingState.NotTracked;
	
	// Use this for initialization
	void Awake () {		
		CreateRegions();
	}
	void Start () {
		LoadTextures();
		
		m_sceneHeadline.SetText("Two Hands");
	}
	
	// Update is called once per frame
	void Update()
	{
		m_myTimer += Time.deltaTime; // updates game timer
		
		UpdateCalibrationState();
		UpdateActiveButtons();
	}

	private void LoadTextures ()
	{	
		if(ResolutionController.aspectRatio == ResolutionController.AspectRatios.Aspect_4x3){
			currentImageName = "Image_1265x945_";
			m_switchTexture.gameObject.transform.parent.transform.localPosition = new Vector3(TEXTURE_4X3_POS,0);
			m_switchTexture.SetTextureSize(640,480);
		}
		else
		{
			m_switchTexture.SetTextureSize(1152,648);
		}
		
		m_switchTexture.init(NUM_OF_BUTTONS);
		for (int i = 0; i < NUM_OF_BUTTONS; i++) {
			m_switchTexture.LoadTexture(texturesPath + currentImageName + (i+1).ToString(),i);
		}
	}
	
	private void CreateRegions () {
		
		int retRegionId = 0;
		m_regionIdToButtonId = new Dictionary<int, int>();
		m_regionsManager = new GenericRegionsManager();
		
		for(int i = 0; i < NUM_OF_BUTTONS; i++)
		{
			float regionStartX = REGION_START_X;
			retRegionId = m_regionsManager.AddRegion(regionStartX,1 - i*BUTTON_REGION_HEIGHT,REGION_WIDTH,BUTTON_REGION_HEIGHT,SCENE_BUTTON_HYSTE_X,SCENE_BUTTON_HYSTE_Y);
			m_regionIdToButtonId.Add(retRegionId,i);
		}
	}
	
	/// <summary>
	/// Updates the buttons state according to active region.
	/// </summary>
	private void UpdateActiveButtons()
	{
		if(m_lastEngineState.Equals(TrackingState.Tracked)) // if user is tracked
		{
			Vector2 palmPosition = m_skeletonManager.GetRightPalmPosition();
			Vector2 palmFixedPosition = new Vector2(Mathf.Clamp(palmPosition.x,0,REGION_WIDTH), palmPosition.y);
			//saving current active region id according to cursor position
			int selectedActiveRegionID = m_regionsManager.GetActiveRegion(palmFixedPosition);
			if(IsButtonSelectionValid(selectedActiveRegionID)) // checking if active region is selected for BUTTON_VALIDATION_TIME
			{	
				//checking if a new region is selected
				if(selectedActiveRegionID != m_lastSelectedRegionID)
				{
					int currentSelectedButtonID = m_regionIdToButtonId[selectedActiveRegionID];
					
					m_buttons[selectedActiveRegionID].HighLightState(true);
					m_switchTexture.Switch(currentSelectedButtonID);
					
					DeselectLastRegion();
					
					//update new selected button
					m_lastSelectedRegionID = selectedActiveRegionID;
					m_lastSelectedButtonID = currentSelectedButtonID;
				}
			}
		}
		else{ // user is not tracked
			InitRegionsState();
		}
	}
	
	/// <summary>
	/// Updates the state of the calibration screen
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
			if(m_buttonValidationStartedTime == INVALID_VALUE)
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
	
	/// <summary>
	/// Deselects the last region.
	/// </summary>
	private void DeselectLastRegion ()
	{
		//deselect last highlighted button if we have one
		if(m_lastSelectedRegionID != INVALID_VALUE)
		{
			m_buttons[m_lastSelectedButtonID].HighLightState(false);
		}
	}
	
	public bool IsItemSelected()
	{
		return (m_lastSelectedRegionID != INVALID_VALUE);
	}
	
	public int GetLastSelectedItem()
	{
		return m_lastSelectedRegionID;
	}
	
	/// <summary>
	/// Inits the state of the regions.
	/// </summary>
	private void InitRegionsState()
	{
		DeselectLastRegion();
		m_lastSelectedRegionID = INVALID_VALUE;
		m_lastSelectedButtonID = INVALID_VALUE;
	}
}
