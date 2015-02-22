using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Xtr3D.Net.ExtremeMotion.Data;
/// <summary>
/// Buttons over video.
/// </summary>
public class Concept1Controller : MonoBehaviour {
	
	private const float XTR_VIDEO_WIDTH = 640;
	private const float XTR_VIDEO_HEIGHT = 480;
	
	private const int INVALID_VALUE = -1;
	private const float CLICK_TIME_IN_SEC = 1f;
	private const int NUM_OF_BUTTONS = 4;
	
	// Scene buttons
	private const int BUTTONS_IN_COL = 2;
	private const int BUTTONS_IN_ROW = 2;
	private float BUTTON_START_X_POS = -400;
	private const float BUTTON_START_Y_POS = 324;
	
	private float BUTTON_WIDTH = 392;
	private float BUTTON_HEIGHT = 160;
	
	public HoveringManager m_hoveringManager;
	public CalibrationManager m_calibrationManager;
	public TimeBaseAnimation m_timeBaseAnimation;
	public SceneHeadline m_sceneHeadline;
	private GenericRegionsManager m_regionManager;
	public UIPanel m_videoPanel;
	public UISprite m_videoOverlay;
	
	//video
	private UITexture mainScreenTexture;
	public UITexture calibrationTexture;
	public GameObject m_videoHolder;
	public GameObject m_videoFrame;
	public GameObject m_videoScreen;
	private float videoWidth;
	private float videoHeight;
	
	private Vector2 m_rightPalmPosition;
	private Vector2 m_leftPalmPosition;
	
	// maps region id's to their current active button
	private Dictionary<int, int> m_regionIdToButtonId = new Dictionary<int, int>();
	// holds engine last skeleton tracking state
	private static TrackingState m_lastEngineState;
	
	private MyButton[] m_myButtons;
	
	private float m_myTimer = 0; // game timer
	private int m_currentSelectedButtonID = INVALID_VALUE;
	private int m_lastSelectedButtonID = INVALID_VALUE;
	private float m_buttonSelectedStartedTime = INVALID_VALUE;
	
	private Dictionary<int, string> m_buttonLabels = new Dictionary<int, string>
	{
		{0 		, "Log Out"},
		{1		, "Account"},
		{2		, "Settings"},
		{3 		, "Menu"}
	};
	
	void Awake()
	{
		videoWidth = XTR_VIDEO_WIDTH * m_videoHolder.transform.localScale.x;
		videoHeight = XTR_VIDEO_HEIGHT * m_videoHolder.transform.localScale.y;
		//Creating region manager
		m_regionManager = new GenericRegionsManager();
		//setting the screen title
		m_sceneHeadline.SetText("Buttons Over Video");
		
		m_lastEngineState = TrackingState.NotTracked;

		mainScreenTexture = m_videoHolder.GetComponentInChildren<UITexture>();
	}
	
	// Use this for initialization
	void Start () {
		CheckAspectRatio();
		CreateButtons();
	}
	/// <summary>
	/// Creates the buttons and placing them in scene.
	/// </summary>
	private void CreateButtons()
	{
		m_myButtons = new MyButton[NUM_OF_BUTTONS];
		GameObject myButtonPrefab = (GameObject) Resources.Load("Prefabs/MyButton"); // loading button prefab from resources
		
		//Building the buttons matrix
		for(int i = 0; i < BUTTONS_IN_ROW;i++)
		{
			for(int j = 0; j < BUTTONS_IN_COL;j++)
			{
				GameObject myButtonObject = (GameObject) Instantiate(myButtonPrefab,Vector3.zero,Quaternion.identity);
				m_myButtons[i*BUTTONS_IN_ROW+j] = myButtonObject.GetComponent<MyButton>();
				m_myButtons[i*BUTTONS_IN_ROW+j].transform.parent = m_videoPanel.transform;
				m_myButtons[i*BUTTONS_IN_ROW+j].Init(new Vector3(BUTTON_START_X_POS + (i*(BUTTON_WIDTH + BUTTON_WIDTH)),BUTTON_START_Y_POS - (j * (BUTTON_HEIGHT + BUTTON_HEIGHT)),-2));
				m_myButtons[i*BUTTONS_IN_ROW+j].SetImageSize(BUTTON_WIDTH,BUTTON_HEIGHT);
				m_myButtons[i*BUTTONS_IN_ROW+j].SetAvailability(false);
				m_myButtons[i*BUTTONS_IN_ROW+j].SetLabel(m_buttonLabels[i*BUTTONS_IN_ROW+j]);
				int regionID = m_regionManager.AddRegion(m_myButtons[i*BUTTONS_IN_ROW+j].transform.localPosition.x - m_videoScreen.transform.localPosition.x,m_myButtons[i*BUTTONS_IN_ROW+j].transform.localPosition.y,BUTTON_WIDTH,BUTTON_HEIGHT,0,0);
				m_regionIdToButtonId.Add(regionID,i);
			}
		}
	}

	private void ShowMenu (bool show)
	{
		for (int i = 0; i < m_myButtons.Length; i++) {
			m_myButtons[i].SetAvailability(show);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//updating game time
		m_myTimer += Time.deltaTime;
		UpdateTexture();
		UpdateCalibrationState();
		UpdatePalmsPosition();
		UpdateActiveButton();
	}

	void UpdateTexture ()
	{
		if(mainScreenTexture != null && calibrationTexture.mainTexture != null)
			mainScreenTexture.mainTexture = calibrationTexture.mainTexture;
	}

	/// <summary>
	/// Updates the palms position.
	/// </summary>
	void UpdatePalmsPosition ()
	{
		if(m_hoveringManager.GetTrackingState().Equals(TrackingState.Tracked)) // if user is tracked
		{
			m_rightPalmPosition = m_hoveringManager.GetRightPalmPosition();
			m_rightPalmPosition = new Vector2((m_rightPalmPosition.x * videoWidth) - videoWidth/2, (m_rightPalmPosition.y * videoHeight) + videoHeight/2);
			
			m_leftPalmPosition = m_hoveringManager.GetLeftPalmPosition();
			m_leftPalmPosition = new Vector2((m_leftPalmPosition.x * videoWidth) - videoWidth/2, (m_leftPalmPosition.y * videoHeight) + videoHeight/2);
		}
	}
	/// <summary>
	/// Updates the active buttons.
	/// </summary>
	private void UpdateActiveButton ()
	{	
		if(m_hoveringManager.GetTrackingState().Equals(TrackingState.Tracked)) // if user is tracked
		{
			//saving current active region according to cursor position
			List<int> currentRegions = m_regionManager.GetActiveRegions(new Vector2[] { m_rightPalmPosition , m_leftPalmPosition});
			if(!(m_currentSelectedButtonID != INVALID_VALUE && currentRegions.Contains(m_currentSelectedButtonID))){
				for (int i = 0; i < currentRegions.Count; i++) {
					
					if(currentRegions[i] != INVALID_VALUE)
					{
						m_currentSelectedButtonID = currentRegions[i];
						break;
					}
					m_currentSelectedButtonID = INVALID_VALUE;
				}
			}
			if(m_currentSelectedButtonID != INVALID_VALUE) // checking if the current region is valid
			{
				if(m_currentSelectedButtonID != m_lastSelectedButtonID) // checking if the current region is not already selected
				{
					//new region received. initilize click and animation
					InitState();
					m_lastSelectedButtonID = m_currentSelectedButtonID;
				
					// update click animation position
					UpdateClickAnimationPosition();
				}
				//updates the time based click
				UpdateTimeBaseClick();
			}
			else
			{
				InitState();
			}
		}
		else // user is not tracked
		{
			InitState();
		}
	}
	void UpdateClickAnimationPosition ()
	{
		Vector3 currentButtonPosition = m_myButtons[m_currentSelectedButtonID].transform.localPosition;
		m_timeBaseAnimation.SetPosition(new Vector3(currentButtonPosition.x + 60,currentButtonPosition.y - BUTTON_HEIGHT/2));
	}
	/// <summary>
	/// Initilizes the state.
	/// </summary>
	private void InitState()
	{
		DeslectLastButton();
		m_buttonSelectedStartedTime = INVALID_VALUE;
		m_timeBaseAnimation.HideAnimation();
	}
	/// <summary>
	/// Deslects the last button.
	/// </summary>
	private void DeslectLastButton()
	{
		if(m_lastSelectedButtonID != INVALID_VALUE)
		{
			m_myButtons[m_lastSelectedButtonID].HighLightState(false);
			m_lastSelectedButtonID = INVALID_VALUE;
		}
	}
	
	/// <summary>
	/// Updates the state of the calibration screen.
	/// </summary>
	private void UpdateCalibrationState ()
	{
		if(!m_lastEngineState.Equals(m_hoveringManager.GetTrackingState()))
		{
			m_lastEngineState = m_hoveringManager.GetTrackingState();
		
			switch (m_lastEngineState) {
			case TrackingState.Calibrating:
				m_calibrationManager.ShowCalibration(true);
				ShowMenu(false);
			break;
				
			case TrackingState.Tracked:
				m_calibrationManager.ShowCalibration(false);
				m_calibrationManager.EnableDrawing(true);
				ShowMenu(true);
			break;
			default:
			break;
			}
		}
	}
	
	/// <summary>
	/// Play time base animation and updates click time.
	/// </summary>
	private void UpdateTimeBaseClick ()
	{
		if(m_buttonSelectedStartedTime == INVALID_VALUE) // checks if time based click didn't start yet
		{
			m_buttonSelectedStartedTime = m_myTimer;
			m_timeBaseAnimation.PlayAnimation(); // starts time based click animation 
		}
		else // time based click already started
		{
			//checking if time from m_buttonSelectedStartedTime is larger than click time threshold
			if(m_myTimer - m_buttonSelectedStartedTime > CLICK_TIME_IN_SEC)
			{
				// if button not already on click state
				if(!m_myButtons[m_currentSelectedButtonID].IsClicked)
				{
					//button clicked
					m_myButtons[m_currentSelectedButtonID].HighLightState(true);
					m_timeBaseAnimation.HideAnimation();
				}
			}
		}
	}

	void CheckAspectRatio ()
	{
		if(ResolutionController.aspectRatio == ResolutionController.AspectRatios.Aspect_4x3)
		{
			BUTTON_START_X_POS = -600;
			m_videoOverlay.enabled = true;
			m_videoOverlay.transform.localScale = new Vector3(ResolutionController.screenWidth,m_videoOverlay.transform.localScale.y);
			m_videoFrame.SetActive(false);
			m_videoHolder.transform.localScale = new Vector3(2.25f,2.25f);
			m_videoScreen.transform.localPosition = Vector3.zero;
			m_sceneHeadline.ChangeLayout();
		}
	}

	void OnApplicationPause(bool pause)
	{
		if(pause){
			m_lastEngineState = TrackingState.NotTracked;
		}
	}
}

