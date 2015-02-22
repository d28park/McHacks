using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Xtr3D.Net.ExtremeMotion.Data;
/// <summary>
/// X position
/// </summary>
public class Concept3Controller : MonoBehaviour {
	
	private const int INVALID_VALUE = -1;
	private const int NUM_OF_BUTTONS = 3;
	const int NUM_OF_TEXTURES = 3;
	
	private string defaultTextureName = "City_1920x1080_0";
	private string texturesPath = "Materials/";
	
	// Scene buttons
	private const float BUTTON_HEIGHT = 110;
	private const float BUTTON_START_Y_POS = 0;
	private const float BUTTON_X_ROW_SPACE = 3;
	private float buttonStartXpos = 0;
	private float buttonWidth = 0;
	private float regionXshift = 0;
	
	private float regionStartX;
	private float regionWidth;
	
	public CalibrationManager m_calibrationManager;
	public SceneHeadline m_myHeadline;
	private GenericRegionsManager m_regionManager;
	public SkeletonManager m_skeletonManager;
	public SwitchTexture m_switchTexture;
	public UISprite darkTile;
	
	// maps region id's to their current active button
	private Dictionary<int, int> m_regionIdToButtonId = new Dictionary<int, int>();
	// holds engine last skeleton tracking state
	private static TrackingState m_lastEngineState = TrackingState.NotTracked;
	
	private XPosButton[] m_myButtons;
	
	private int m_currentSelectedButtonID = INVALID_VALUE;
	
	private Dictionary<int, string> m_buttonsNames = new Dictionary<int, string>
	{
		{0 	,"London"},
		{1	,"Paris"},
		{2 ,"New York"},
	};
	
	void Awake()
	{
		//Creating region manager
		m_regionManager = new GenericRegionsManager();
	}
	// Use this for initialization
	void Start () {
		
		regionXshift = ResolutionController.screenWidth /(NUM_OF_BUTTONS+1);
		buttonStartXpos = -ResolutionController.screenWidth/2;
		buttonWidth = (ResolutionController.screenWidth - (BUTTON_X_ROW_SPACE*(NUM_OF_BUTTONS-1))) / NUM_OF_BUTTONS;
		
		LoadTextures();	
		//setting the screen title
		m_myHeadline.SetText("User Position");
		CreateButtons();
		CreateRegions();
	}
	
	private void LoadTextures()
	{
		// setting the textures according to screen aspect ratio
		if(ResolutionController.aspectRatio == ResolutionController.AspectRatios.Aspect_4x3){
			defaultTextureName = "City_2056x1536_0";
		}
		
		m_switchTexture.init(NUM_OF_TEXTURES);
		
		for (int i = 0; i < NUM_OF_TEXTURES; i++) {
			m_switchTexture.LoadTexture(texturesPath + defaultTextureName + (i+1).ToString(),i);
		}
	}

	/// <summary>
	/// Creates the buttons and placing them in scene.
	/// </summary>
	private void CreateButtons()
	{
		m_myButtons = new XPosButton[NUM_OF_BUTTONS];
		GameObject myButtonPrefab = (GameObject) Resources.Load("Prefabs/XposButton"); // loading button prefab from resources
		//Building the buttons matrix
		for(int i = 0; i < NUM_OF_BUTTONS;i++)
		{
			GameObject myButtonObject = (GameObject) Instantiate(myButtonPrefab,Vector3.zero,Quaternion.identity);
			m_myButtons[i] = myButtonObject.GetComponent<XPosButton>();
			m_myButtons[i].transform.parent = this.transform;
			m_myButtons[i].Init(new Vector3(buttonStartXpos + (i*(buttonWidth + BUTTON_X_ROW_SPACE)),BUTTON_START_Y_POS,0));
			m_myButtons[i].SetImageSize(buttonWidth,BUTTON_HEIGHT);
			m_myButtons[i].SetLabel(m_buttonsNames[i]);
			m_myButtons[i].SetLabelPos(buttonWidth/2,-BUTTON_HEIGHT/2);
		}
	}
	/// <summary>
	/// Creates the regions.
	/// </summary>
	private void CreateRegions ()
	{
		regionStartX = buttonStartXpos + regionXshift;
		regionWidth = (ResolutionController.screenWidth - regionXshift) / NUM_OF_BUTTONS;
		for (int i = 0; i < m_myButtons.Length; i++) {
			int regionID = m_regionManager.AddRegion(regionStartX + (i*regionWidth),BUTTON_START_Y_POS,regionWidth,BUTTON_HEIGHT,0,0);
			m_regionIdToButtonId.Add(regionID,i);
		}
	}
	// Update is called once per frame
	void Update () {
		UpdateCalibrationState();
		UpdateActiveButton();
	}
	/// <summary>
	/// Updates the active buttons.
	/// </summary>
	private void UpdateActiveButton ()
	{	
		if(m_lastEngineState.Equals(TrackingState.Tracked)) // if user is tracked
		{
			//saving current active region according to cursor position
			int currentRegion = m_regionManager.GetActiveRegion(new Vector2((m_skeletonManager.GetHeadPosition().x * ResolutionController.screenWidth) - ResolutionController.screenWidth/2,-BUTTON_HEIGHT/2));
			
			if(currentRegion != INVALID_VALUE) // checking if current region is valid
			{
				ApplyDarkOverlay(false);
				if(currentRegion != m_currentSelectedButtonID) // cheking if the current region is not already selected
				{
					//new region received. initilize click and animation
					InitState();
					m_currentSelectedButtonID = currentRegion;
					
				}
				//updates the time based click
				m_myButtons[m_currentSelectedButtonID].HighLightState(true);
				m_switchTexture.Switch(m_currentSelectedButtonID);
			}
			else
			{
				InitState();
				ApplyDarkOverlay(true);
			}
		}
		else // user is not tracked
		{
			InitState();
			ApplyDarkOverlay(true);
		}
	}
	/// <summary>
	/// Initilizes the state.
	/// </summary>
	private void InitState()
	{
		DeslectLastButton();
	}

	void ApplyDarkOverlay (bool apply)
	{
		if(darkTile.enabled != apply)
			darkTile.enabled = apply;
	}
	
	/// <summary>
	/// Deslects the last button.
	/// </summary>
	private void DeslectLastButton()
	{
		if(m_currentSelectedButtonID != INVALID_VALUE)
		{
			m_myButtons[m_currentSelectedButtonID].HighLightState(false);
		}
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
}
