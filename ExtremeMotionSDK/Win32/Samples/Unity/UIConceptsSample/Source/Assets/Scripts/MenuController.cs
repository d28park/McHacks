using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Xtr3D.Net.ExtremeMotion.Data;

public class MenuController : MonoBehaviour {
	
	#region Defines
	private const float CLICK_TIME_IN_SEC = 1;
	private const float TIME_BEFORE_START_ANIMATION = 0.5f; // time is in seconds
	private const float  BUTTON_VALIDATION_TIME = 0.2f;
	private const float  BUTTON_READY_FOR_SLIDE_TIME = 0.5f;
	private const float SCENE_BUTTONS_ANIMATION_TIME = 1.5f;
	private const int INVALID_VALUE = -1;
	private const string DEFAULT_UI_SCENE_BUTTON_NAME = "UiConcept";
	private const float MAX_TIME_SLIDE_ACCEPTED = 1.5f;
	//Regions
	private const float REGION_START_X = 0.5f;
	private const float LOWER_REGIONS_START_X = 0.4f;
	private const float UP_ARROW_REGION_Y_POS = 1f;
	private const float DOWN_ARROW_REGION_Y_POS = -0.55f;
	private const float ARROW_REGION_HEIGHT = 0.65f;
	private const float LOWER_ARROW_REGION_HEIGHT = 0.35f;
	private const float BUTTON_REGION_HEIGHT = 0.3f;
	private const float REGION_WIDTH = 2f;
	private const float SCENE_BUTTON_HYSTE_X = 0.1f;
	private const float SCENE_BUTTON_HYSTE_Y = 0.1f;
	private const float ARROW_BUTTON_HYSTE_X = 0.1f;
	private const float ARROW_BUTTON_HYSTE_Y = 0.05f;
	// Scene buttons
	private const int NUM_OF_SCENE_BUTTONS = 5;
	private const float SCENE_BUTTON_WIDTH = 610;
	private const float SCENE_BUTTON_HEIGHT = 200;
	private const float SCENE_BUTTON_X_POS = -610;
	private const float SCENE_BUTTON_START_Y_POS = 290;
	private const int NUM_OF_BUTTONS_TO_SCROLL = 3;
	private const float BUTTON_Y_DIS_FROM_NEIGHBOR = 200; // button height + space
	private const float BUTTON_Y_DIS_TO_MOVE = BUTTON_Y_DIS_FROM_NEIGHBOR * NUM_OF_BUTTONS_TO_SCROLL;
	// Arrow buttons
	private const int NUM_OF_ARROWS = 2;
	private const float ARROW_BUTTON_WIDTH = 610;
	private const float ARROW_BUTTON_HEIGHT = 140;
	private const float TOP_ARROW_START_Y_POS = 0;
	private const float BOTTOM_ARROW_START_Y_POS = 140;
	private const float ARROW_START_X_POS = -610;
	
	private enum Buttons
	{
		UP_ARROW			= 0,
		DOWN_ARROW			= 1,
		THREE_TWO_GRID 		= 2,
		BUTTONS_OVER_VIDEO	= 3,
		HORIZONTAL_SCROLL 	= 4,
		X_POSITION 			= 5,
		TWO_HANDS_UI		= 6,
		NUM_OF_BUTTONS		= 7 // Indicating the enum size
	};
	
	private Dictionary<int, string> m_scenesNames = new Dictionary<int, string>
	{
		{(int)Buttons.THREE_TWO_GRID 	,"Grid Selection"},
		{(int)Buttons.BUTTONS_OVER_VIDEO	,"Buttons\nOver Video"},
		{(int)Buttons.HORIZONTAL_SCROLL 	,"Horizontal\nScroll"},
		{(int)Buttons.X_POSITION 		, "User\nPosition"},
		{(int)Buttons.TWO_HANDS_UI 		,"Two Hands"}
	};
	
	private List<int> m_sceneRegionsIds = new List<int>();
	private Dictionary<int, int> m_regionIdToButtonId = new Dictionary<int, int>();
	
	public SkeletonManager m_skeletonManager;
	public CalibrationManager m_calibrationManager;
	public TimeBaseAnimation m_timeBaseAnimation;

	public SlideDetectorManager m_slideDetectorManager;
	public UIPanel m_scrollPanel;
	public UIPanel m_topScrollPanel;
	public UIPanel m_bottomScrollPanel;
	
	private GenericRegionsManager m_regionsManager;
	
	private MyButton[] m_buttons;
	private int m_numberOfPagesToScroll;
	private int m_currentPage = 1;
	
	private int m_firstSceneButtonIdOnPage = 0;
	
	private float m_myTimer = 0;
	private int m_currentSelectedRegionID = INVALID_VALUE;
	private int m_lastKnownActiveRegionID = INVALID_VALUE;
	private float m_lastKnownActiveRegionTime = INVALID_VALUE;
	private int m_lastSelectedRegionID = INVALID_VALUE;
	private float m_buttonSelectedStartedTime = INVALID_VALUE;
	private float m_buttonValidationStartedTime = INVALID_VALUE;
	private float m_readyForSlideTime = INVALID_VALUE;
	private bool m_isSlideDetected = false;
	private bool m_isSlideStarted = false;
	private float m_sceneButtonAnimStartedTime = INVALID_VALUE;
	private bool  m_waitWithAnimation = false;
	private int m_currentSelectedButtonID = INVALID_VALUE;
	private int m_lastSelectedButtonID = INVALID_VALUE;
	private List<int> m_disabledRegions = new List<int>(NUM_OF_SCENE_BUTTONS);
	private static TrackingState m_lastEngineState = TrackingState.NotTracked;
	#endregion Defines
	#region Initialize Scene
	void Awake () {
		CreateRegions();
		m_numberOfPagesToScroll = Mathf.CeilToInt((float)NUM_OF_SCENE_BUTTONS/(float)NUM_OF_BUTTONS_TO_SCROLL);
	}
	
	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start () {
		CreateButtons();
		
		//Register to slide event
		m_slideDetectorManager.SlideOccured += SlideDetected;
		InitArrowButtonsVisibility();
	}
	
	/// <summary>
	/// Creates the buttons and placing them in scene.
	/// </summary>
	private void CreateButtons ()
	{
		m_buttons = new MyButton[NUM_OF_SCENE_BUTTONS + NUM_OF_ARROWS];
		
		GameObject arrowButtonPrefab = (GameObject) Resources.Load("Prefabs/ArrowButton"); // loading arrow button prefab from resources
		GameObject sceneButtonPrefab = (GameObject) Resources.Load("Prefabs/MovableBtn"); // loading movable button prefab from resources
		//adding arrow buttons to scene
		for(int i=0;i < NUM_OF_ARROWS; i++)
		{
			GameObject arrowObject = (GameObject) Instantiate(arrowButtonPrefab,Vector3.zero,Quaternion.identity);
			m_buttons[i] = arrowObject.GetComponent<ArrowButton>();
			m_buttons[i].transform.parent = (i == (int) Buttons.UP_ARROW) ? m_topScrollPanel.transform : m_bottomScrollPanel.transform;
			m_buttons[i].Init(new Vector3(ARROW_START_X_POS,(i == (int) Buttons.UP_ARROW) ? TOP_ARROW_START_Y_POS : BOTTOM_ARROW_START_Y_POS,-1
				));
			if(i == (int) Buttons.DOWN_ARROW)
			{
				((ArrowButton)m_buttons[i]).RotateArrow(new Quaternion(180,0,0,0));
			}
			m_buttons[i].SetImageSize(ARROW_BUTTON_WIDTH,ARROW_BUTTON_HEIGHT);
			
			if(ResolutionController.aspectRatio == ResolutionController.AspectRatios.Aspect_4x3)
			{
				((ArrowButton)m_buttons[i]).SetArrowItemSize(67,30);
			}
		}
		//Building the buttons column
		for(int i = 0; i < NUM_OF_SCENE_BUTTONS;i++)
		{
			int sceneIndex = i + NUM_OF_ARROWS;
			GameObject movableBtnObject = (GameObject) Instantiate(sceneButtonPrefab,Vector3.zero,Quaternion.identity);
			m_buttons[sceneIndex] = movableBtnObject.GetComponent<MovableButton>();
			m_buttons[sceneIndex].transform.parent = m_scrollPanel.transform;
			m_buttons[sceneIndex].Init(new Vector3(SCENE_BUTTON_X_POS,(i*-BUTTON_Y_DIS_FROM_NEIGHBOR) + SCENE_BUTTON_START_Y_POS,0));
			m_buttons[sceneIndex].SetImageSize(SCENE_BUTTON_WIDTH,SCENE_BUTTON_HEIGHT);
			//sets tween
			((MovableButton)m_buttons[sceneIndex]).InitTweenYpos(-i,SCENE_BUTTON_START_Y_POS,BUTTON_Y_DIS_TO_MOVE,BUTTON_Y_DIS_FROM_NEIGHBOR,SCENE_BUTTONS_ANIMATION_TIME);
			//sets button label
			((MovableButton)m_buttons[sceneIndex]).SetLabel(m_scenesNames[sceneIndex]);
			((MovableButton)m_buttons[sceneIndex]).SetIcon(i);
			((MovableButton)m_buttons[sceneIndex]).SetLevelToLoad(DEFAULT_UI_SCENE_BUTTON_NAME + i.ToString());
		}
	}
	/// <summary>
	/// Creates the regions
	/// </summary>
	private void CreateRegions ()
	{
		int retRegionId = 0;
		m_regionIdToButtonId = new Dictionary<int, int>();
		m_regionsManager = new GenericRegionsManager();
		//adding upper arrow region
		retRegionId = m_regionsManager.AddRegion(REGION_START_X,UP_ARROW_REGION_Y_POS,REGION_WIDTH,ARROW_REGION_HEIGHT,ARROW_BUTTON_HYSTE_X,ARROW_BUTTON_HYSTE_Y);
		m_regionIdToButtonId.Add(retRegionId,(int)Buttons.UP_ARROW);
		//adding lower arrow region
		retRegionId = m_regionsManager.AddRegion(LOWER_REGIONS_START_X,DOWN_ARROW_REGION_Y_POS,REGION_WIDTH,LOWER_ARROW_REGION_HEIGHT,ARROW_BUTTON_HYSTE_X,ARROW_BUTTON_HYSTE_Y);
		m_regionIdToButtonId.Add(retRegionId,(int)Buttons.DOWN_ARROW);
		// adding regions for scene buttons
		for(int i = 0; i < NUM_OF_BUTTONS_TO_SCROLL; i++)
		{
			float regionStartX = REGION_START_X;
			if(i>=1){ // setting lower regions to be more to the right of the user body
				regionStartX = LOWER_REGIONS_START_X;
			}
			retRegionId = m_regionsManager.AddRegion(regionStartX,1 - (ARROW_REGION_HEIGHT) - i*BUTTON_REGION_HEIGHT,REGION_WIDTH,BUTTON_REGION_HEIGHT,SCENE_BUTTON_HYSTE_X,SCENE_BUTTON_HYSTE_Y);
			m_regionIdToButtonId.Add(retRegionId,i + (int)Buttons.THREE_TWO_GRID);
			m_sceneRegionsIds.Add(retRegionId);
		}
	}
	#endregion Initialize Scene
	
	/// <summary>
	/// Moves the scene buttons down.
	/// </summary>
	private void MoveMenuDown()
	{
		bool isEdge = false;
		int buttonsLeft = 0;
		
		float distanceToMove = BUTTON_Y_DIS_FROM_NEIGHBOR * NUM_OF_BUTTONS_TO_SCROLL; // setting default menu movement
		int firstPage = 1;
		
		if(m_currentPage > firstPage) // if we are not on the first page
		{
		
			UpdateArrowButtonsVisibility();
			if(m_currentPage - 1 == firstPage) // checking if next movement is first page
			{
				buttonsLeft = m_firstSceneButtonIdOnPage % NUM_OF_BUTTONS_TO_SCROLL; // calculating the the number of buttons left on the first page
				if(buttonsLeft > 0) // if we have buttons left we need to calculate the distance for moving to initial state
					distanceToMove = buttonsLeft * BUTTON_Y_DIS_FROM_NEIGHBOR; // calculating the smaller distance we need to move according to the number of buttons left
				
				//we are hiding upper arrow
				HideArrow(Buttons.UP_ARROW);
			}
			
			UpdateFirstButtonOnPageID(buttonsLeft,false);
			
			m_currentPage--;
		}
		else{// we are in the upper edge
			isEdge = true;
		}
		
		for(int i = (int)Buttons.THREE_TWO_GRID; i < (int)Buttons.NUM_OF_BUTTONS;i++) // moves all scenes buttons down
		{
			((MovableButton)m_buttons[i]).SetYdistanceToMove(distanceToMove);
			((MovableButton)m_buttons[i]).MoveDown(isEdge);
		}
	}
	/// <summary>
	/// Moves the scene buttons up.
	/// </summary>
	private void MoveMenuUp()
	{
		bool isEdge = false;
		int remainder = 0;
		float distanceToMove = BUTTON_Y_DIS_FROM_NEIGHBOR * NUM_OF_BUTTONS_TO_SCROLL;
		
		// checking if we are not in the last page
		if(m_currentPage < m_numberOfPagesToScroll)
		{
			UpdateArrowButtonsVisibility();
			
			if(m_currentPage + 1 == m_numberOfPagesToScroll) // checking if next movement is last page
			{
				remainder = (NUM_OF_SCENE_BUTTONS - NUM_OF_BUTTONS_TO_SCROLL) - m_firstSceneButtonIdOnPage; // calculating the the number of buttons left on the last page
				if(remainder > 0){
					distanceToMove = remainder * BUTTON_Y_DIS_FROM_NEIGHBOR; // calculating the smaller distance we need to move according to the number of buttons left
				}
				//we are hiding bottom arrow
				HideArrow(Buttons.DOWN_ARROW);
			}
				
			UpdateFirstButtonOnPageID(remainder,true);
			
			m_currentPage++;
		}
		else{ // we are in the bottom edge
			isEdge = true;
		}
		
		for(int i = (int)Buttons.THREE_TWO_GRID; i < (int)Buttons.NUM_OF_BUTTONS;i++) // moves all scenes buttons up
		{	
			((MovableButton)m_buttons[i]).SetYdistanceToMove(distanceToMove);
			((MovableButton)m_buttons[i]).MoveUp(isEdge);
		}
	}
	/// <summary>
	/// Updates the first button ID on current page.
	/// </summary>
	/// <param name='buttonsRemainder'>
	/// the buttons left to scroll on the next/prev page.
	/// </param>
	/// <param name='movingUp'>
	/// if movement was up/down.
	/// </param>
	void UpdateFirstButtonOnPageID(int buttonsRemainder, bool movingUp)
	{
		//If we are here then a remainder of 0 means we have NUM_OF_BUTTONS_TO_SCROLL buttons to scroll.
		if (buttonsRemainder == 0)
		{
			buttonsRemainder = NUM_OF_BUTTONS_TO_SCROLL;
		}
		//If going down the direction is set to negative.
		int direction  = movingUp ? 1 : -1;
		int positionsToMove = direction*buttonsRemainder;
		//Updating buttons for each region according to positionsToMove
		foreach(int regionId in m_sceneRegionsIds)
		{
			m_regionIdToButtonId[regionId] = m_regionIdToButtonId[regionId] + positionsToMove;
		}
		//Seting the first scene button to be shown.
		m_firstSceneButtonIdOnPage += positionsToMove;
	}

	public void SlideDetected ()
	{
		m_isSlideDetected = true;
	}
	
	void Update()
	{
		m_myTimer += Time.deltaTime; // updates game timer
		
		UpdateCalibrationState();
		
		if(m_isSlideDetected && m_lastKnownActiveRegionID > 1) // slide detected and current region is NOT an arrow button
		{
			Point slideStartLocation = m_slideDetectorManager.GetSlideStartLocation(); // getting slide start location
			//checking if we started slide inside our last selected region at the last accepted period of time (MAX_TIME_SLIDE_ACCEPTED)
			if(m_myTimer - m_lastKnownActiveRegionTime <= MAX_TIME_SLIDE_ACCEPTED &&
				m_regionsManager.IsInRegionWithHyst(new Vector2(slideStartLocation.X,slideStartLocation.Y),m_lastKnownActiveRegionID))
			{
				MenuItemClicked();
			}
			else{ // illegal slide
				m_isSlideDetected = false;
			}
		}
		else{
			if(!m_isSlideStarted)
				UpdateActiveButtons();
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
				m_currentSelectedRegionID = selectedActiveRegionID;
				
				//checking if a new region is selected
				if(m_currentSelectedRegionID != m_lastSelectedRegionID)
				{
					//Debug.Log(m_currentSelectedRegionID);
					// checking if the current region is not disabled
					if(IsCurrentRegionDisabled())
					{
						GetNearestActiveRegion();
					}
					
					if(m_currentSelectedButtonID != m_regionIdToButtonId[m_currentSelectedRegionID])
					{
						m_currentSelectedButtonID = m_regionIdToButtonId[m_currentSelectedRegionID];
						
						DeselectLastRegion();
						
						m_buttons[m_currentSelectedButtonID].HighLightState(true);
						
						//update new selected button
						m_lastSelectedRegionID = m_currentSelectedRegionID;
						m_lastSelectedButtonID = m_currentSelectedButtonID;
						
						// if we have a new button we want starting a new time based click
						InitClickState();
					}
					
				}
			}
			//Only after we passed BUTTON_VALIDATION_TIME time and current selected button is activated
			if(m_currentSelectedButtonID != INVALID_VALUE)
			{
				// one of our scene buttons is selected, we start check if the button ready for slide
				if((m_currentSelectedButtonID != (int)Buttons.DOWN_ARROW) && (m_currentSelectedButtonID != (int)Buttons.UP_ARROW))
				{
					UpdateButtonReadyForSlide();
				}
				else{ // one of our arrows is selected, we start the time base click
					UpdateTimeBasedClick();
				}
			}
		}
		else{ // user is not tracked
			InitRegionsState();
		}
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
			
			if((m_lastSelectedButtonID != (int)Buttons.DOWN_ARROW) && (m_lastSelectedButtonID != (int)Buttons.UP_ARROW))
			{
				((MovableButton)m_buttons[m_lastSelectedButtonID]).ShowSlider(false);
			}
		}
	}
	
	private bool IsCurrentRegionDisabled()
	{
		bool disabled = false;
		
		foreach (int region in m_disabledRegions)
		{
			if(region == m_currentSelectedRegionID)
			{
				disabled = true;
				break;
			}
		}
		
		return disabled;
	}

	private void GetNearestActiveRegion ()
	{
		if(m_currentSelectedRegionID == (int) Buttons.DOWN_ARROW)
			m_currentSelectedRegionID = (int) Buttons.HORIZONTAL_SCROLL;
		else if(m_currentSelectedRegionID == (int) Buttons.UP_ARROW)
			m_currentSelectedRegionID = (int) Buttons.THREE_TWO_GRID;
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
	/// Checks if the current button is selected for BUTTON_VALIDATION_TIME
	/// </summary>
	/// <returns>
	/// <c>true</c> if BUTTON_VALIDATION_TIME passed ; otherwise, <c>false</c>.
	/// </returns>
	private void UpdateButtonReadyForSlide ()
	{
		
		if(m_readyForSlideTime == INVALID_VALUE)
		{
			m_readyForSlideTime = m_myTimer;
		}
		else
		{
			if(m_myTimer - m_readyForSlideTime > BUTTON_READY_FOR_SLIDE_TIME)
			{
				//show slider
				((MovableButton)m_buttons[m_currentSelectedButtonID]).ReadyForSlide();
				
				// saves the region ID and time stamp of the last button ready for slide
				m_lastKnownActiveRegionTime = m_myTimer;
				m_lastKnownActiveRegionID = m_currentSelectedRegionID;
			}
		}
	}
	
	/// <summary>
	/// Inits the state of click time based click.
	/// </summary>
	private void InitClickState()
	{	
		m_readyForSlideTime = INVALID_VALUE;
		m_buttonSelectedStartedTime = INVALID_VALUE;
		m_sceneButtonAnimStartedTime = INVALID_VALUE;
		m_timeBaseAnimation.HideAnimation();
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
		m_timeBaseAnimation.HideAnimation();
	}
		
	/// <summary>
	/// Updates the arrows time based click when one of the buttons is selected
	/// </summary>
	private void UpdateTimeBasedClick ()
	{
		if(m_lastSelectedRegionID != INVALID_VALUE) // one of our valid region is selected
		{
			if(m_buttonSelectedStartedTime == INVALID_VALUE) // save current time if not started already
			{
				UpdateClickAnimationPosition();
				m_buttonSelectedStartedTime = m_myTimer;
				m_waitWithAnimation = false;
			}
			else // click animation started
			{
				//Waiting TIME_BEFORE_START_ANIMATION so animation wouldn't start immediatly when just hovered over a button.
				if(m_myTimer - m_buttonSelectedStartedTime > TIME_BEFORE_START_ANIMATION)
				{
					// Start playing animation only if it isn't playing already and Scene moving animation has finsihed.
					if(!m_timeBaseAnimation.isPlaying && !m_waitWithAnimation)
					{
						m_timeBaseAnimation.PlayAnimation();
						m_waitWithAnimation = true;
					}
				}
				// checks if we need to trigger click
				// since we have a time based click and we want it to be synced with click animation
				// we will have to sync between "CLICK_TIME_IN_SEC" and our actual animation time (num of frames / fps - see TimeBaseAnimation)
				if(m_myTimer - m_buttonSelectedStartedTime > CLICK_TIME_IN_SEC + TIME_BEFORE_START_ANIMATION)
				{
					//if we need to trigger recurring clicks, we want to start time base click again only after scene buttons movement animation finished
					if(m_sceneButtonAnimStartedTime == INVALID_VALUE) // save starting time of scene buttons animation if not started
					{
						//trigger click - start scene buttons movement
						m_sceneButtonAnimStartedTime = m_myTimer;
						ArrowItemClicked();
						m_timeBaseAnimation.HideAnimation();
						
					}
					else // scene buttons movement animation started
					{
						// checking if scene buttons animation finished - SCENE_BUTTONS_ANIMATION_TIME
						if(m_myTimer - m_sceneButtonAnimStartedTime > (SCENE_BUTTONS_ANIMATION_TIME))
						{
							// animation finished, initiliaze timers for starting click again
							//UpdateArrowButtonsVisibility();
							m_buttonSelectedStartedTime = INVALID_VALUE;
							m_sceneButtonAnimStartedTime = INVALID_VALUE;
							m_waitWithAnimation = false;
						}
					}
				}				
			}
		}
		else // we are currently on an invalid region
		{
			if(m_timeBaseAnimation.isPlaying)
				m_timeBaseAnimation.HideAnimation();
		}
	}
	
	/// <summary>
	/// inits the arrow buttons state (enabled/disabled) according to scroll view size.
	/// </summary>
	private void InitArrowButtonsVisibility ()
	{	
		//m_regionsManager.EnableRegion(m_regionIdToButtonId[(int)Buttons.UP_ARROW],false);
		((ArrowButton)m_buttons[(int)Buttons.UP_ARROW]).SetAvailability(false);
		m_disabledRegions.Add((int)Buttons.UP_ARROW);
		
		if(m_numberOfPagesToScroll == 1)
		{
			//m_regionsManager.EnableRegion(m_regionIdToButtonId[(int)Buttons.DOWN_ARROW],false);
			((ArrowButton)m_buttons[(int)Buttons.DOWN_ARROW]).SetAvailability(false);
		}
	}
	/// <summary>
	/// Updates the arrow buttons state (enabled/disabled) according to scroll view position.
	/// </summary>
	private void UpdateArrowButtonsVisibility ()
	{
		if(m_numberOfPagesToScroll > 1)
		{
			//m_regionsManager.EnableRegion(m_regionIdToButtonId[(int)Buttons.UP_ARROW],true);
			((ArrowButton)m_buttons[(int)Buttons.UP_ARROW]).SetAvailability(true);
			if(m_disabledRegions.Contains((int)Buttons.UP_ARROW))
				m_disabledRegions.Remove((int)Buttons.UP_ARROW);
			
			//m_regionsManager.EnableRegion(m_regionIdToButtonId[(int)Buttons.DOWN_ARROW],true);
			((ArrowButton)m_buttons[(int)Buttons.DOWN_ARROW]).SetAvailability(true);
			if(m_disabledRegions.Contains((int)Buttons.DOWN_ARROW))
				m_disabledRegions.Remove((int)Buttons.DOWN_ARROW);
		}
	}

	void HideArrow (Buttons arrowId)
	{
		//m_regionsManager.EnableRegion(m_regionIdToButtonId[(int)arrowId],false);
		m_buttons[(int)arrowId].SetAvailability(false);
		m_disabledRegions.Add((int)arrowId);
		m_buttons[m_regionIdToButtonId[(int)arrowId]].HighLightState(false);
		m_lastSelectedRegionID = INVALID_VALUE;
	}
	
	/// <summary>
	/// Updates the click animation position according to current selected arrow.
	/// </summary>
	void UpdateClickAnimationPosition ()
	{
		m_timeBaseAnimation.transform.parent = (m_currentSelectedButtonID == (int) Buttons.UP_ARROW) ? m_topScrollPanel.transform : m_bottomScrollPanel.transform;
		Vector3 arrowButtonPos = m_buttons[m_currentSelectedButtonID].transform.localPosition;
		int arrowPositionOffset = 10;
		int direction = (m_currentSelectedButtonID == (int) Buttons.UP_ARROW) ? 1 : -1;
		m_timeBaseAnimation.transform.localPosition = new Vector3(arrowButtonPos.x + ARROW_BUTTON_WIDTH/2,arrowButtonPos.y - m_timeBaseAnimation.transform.localScale.y + (direction * arrowPositionOffset));
	}
	
	/// <summary>
	/// Starts the slide button's animation.
	/// </summary>
	private void MenuItemClicked()
	{
		// making sure slide will be in focus
		if(!m_isSlideStarted)
		{
			m_isSlideStarted = true;
			// turn off last button in case last known ready for slide button was activated
			if(m_lastSelectedRegionID != INVALID_VALUE && m_lastSelectedRegionID != m_lastKnownActiveRegionID)
				m_buttons[m_regionIdToButtonId[m_lastSelectedRegionID]].HighLightState(false);
			// force highlight last known ready for slide button when activated
			m_buttons[m_regionIdToButtonId[m_lastKnownActiveRegionID]].HighLightState(true);
			((MovableButton)m_buttons[m_regionIdToButtonId[m_lastKnownActiveRegionID]]).ShowSlider(true);
			// Start slide
			((MovableButton)m_buttons[m_regionIdToButtonId[m_lastKnownActiveRegionID]]).Slide();
		}
	}
	
	private void ArrowItemClicked()
	{
		switch (m_regionIdToButtonId[m_currentSelectedRegionID]) {
			case (int)Buttons.UP_ARROW:
				MoveMenuDown();
				break;
			case (int)Buttons.DOWN_ARROW:
				MoveMenuUp();
				break;
		}
	}
	
}