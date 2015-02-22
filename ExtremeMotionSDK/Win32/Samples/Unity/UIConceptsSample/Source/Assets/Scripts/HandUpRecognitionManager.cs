using UnityEngine;
using System.Collections;
using Xtr3D.Net.ExtremeMotion;
using Xtr3D.Net.ExtremeMotion.Data;
using Xtr3D.Net.BaseTypes;

public class HandUpRecognitionManager : MonoBehaviour {
	
	private const int INVALID_VALUE = -1;
	private const int NUM_NON_BACK_GESTURE_CONSECUTIVE_UPDATES_THRESHOLD = 10;
	private const string MAIN_MENU_SCENE_NAME = "Main";
	private const float HAND_UP_DETECT_TIME = 1f;
	private const float TIME_BETWEEN_EVENTS = 5f;
	
	private long m_lastFrameID 		= INVALID_VALUE;
	private long m_currFrameID 		= INVALID_VALUE;
	private float 		m_handUpStartTime = INVALID_VALUE;
	private float 		m_myTimer = 0;
	private int 		m_consecutiveNoneBackGesture = 5;
	private bool m_handUpGestureDetected 	= false;
	private bool m_isLeftHandInPosition 	= false;
	
	public TimeBaseAnimation m_timeBaseAnimation;
	private DetectLeftHandUpPosition m_handUpGestureDetector;
	private Concept4Controller m_controller;
	public UISprite m_animationFinishedIcon;
	private int m_currentSelectedButton = INVALID_VALUE;
	
	private Object m_lockInstance = new Object(); // used to sync between threads
	
	void Start () {
		m_controller = this.transform.parent.gameObject.GetComponent<Concept4Controller>();
		
		m_handUpGestureDetector = new DetectLeftHandUpPosition();
		m_handUpGestureDetector.Init();
		// register to XTR skeleton event
		GeneratorSingleton.Instance.DataFrameReady += MyDataFrameReady;
	}
	/// <summary>
	/// skeleton data frame ready.
	/// </summary>
	/// <param name='sender'>
	/// Sender.
	/// </param>
	/// <param name='e'>
	/// event params
	/// </param>
	private void MyDataFrameReady(object sender, Xtr3D.Net.ExtremeMotion.Data.DataFrameReadyEventArgs e)
	{
		using (var dataFrame = e.OpenFrame() as DataFrame)
        {
			if (dataFrame!=null)
			{
				m_currFrameID = dataFrame.FrameKey.FrameNumberKey; // saves current frame id
				if (m_currFrameID <= m_lastFrameID) // checks if we have a "real" new data
					return;
				m_lastFrameID = m_currFrameID; // update current frame id
				
				JointCollection mySkeleton = dataFrame.Skeletons[0].Joints; // saves skeleton
				//checking skeleton state is tracked
				if (dataFrame.Skeletons[0].TrackingState.Equals(TrackingState.Tracked))
				{
					lock (m_lockInstance)
					{	
						if(m_handUpGestureDetector.IsLeftHandUpPoision(mySkeleton,m_lastFrameID)) // checking if user performing back gesture
						{
							m_isLeftHandInPosition = true;
						}
						else{
							m_isLeftHandInPosition = false;
						}
					}
				}
			}
		}
	}
	
	void Update()
	{
		m_myTimer += Time.deltaTime; // updating game timer
		
		lock (m_lockInstance)
		{
			// user is pose
			if (m_isLeftHandInPosition)
			{
				if(m_controller.IsItemSelected())
				{
					if(m_controller.GetLastSelectedItem() != m_currentSelectedButton)
					{
						m_currentSelectedButton = m_controller.GetLastSelectedItem();
						m_handUpStartTime = INVALID_VALUE;
						m_animationFinishedIcon.enabled = false;
						m_handUpGestureDetected = false;
						m_timeBaseAnimation.HideAnimation();
					}
					
					if(m_handUpStartTime == INVALID_VALUE) // checks if time based click didn't start yet
					{
						//saves animation starting time
						m_handUpStartTime = m_myTimer;
						m_timeBaseAnimation.PlayAnimation(); // starts animation
						m_consecutiveNoneBackGesture = 0;
					}
					else
					{
						if(!m_handUpGestureDetected)
						{
							// checking if time from m_backGestureStartTime passed click time threshold
							if(m_myTimer - m_handUpStartTime > HAND_UP_DETECT_TIME)
							{
								// back gesture click occured
								m_handUpGestureDetected = true;
								m_timeBaseAnimation.HideAnimation();
								m_animationFinishedIcon.enabled = true;
							}
						}
					}
				}
				else
				{
					Debug.Log("no item selected");
					m_currentSelectedButton = INVALID_VALUE;
					m_timeBaseAnimation.HideAnimation();
				}
			}
				else{ // user is not in pose
				
				m_animationFinishedIcon.enabled = false;
				
				if (m_consecutiveNoneBackGesture > NUM_NON_BACK_GESTURE_CONSECUTIVE_UPDATES_THRESHOLD)
				{
					m_handUpStartTime = INVALID_VALUE;
					m_handUpGestureDetected = false;
					m_timeBaseAnimation.HideAnimation();
				}
				else{
					m_consecutiveNoneBackGesture++;
				}
			}
		}
	}

	void OnApplicationPause(bool pause)
	{
		if(pause){
			m_lastFrameID = -1;
			m_currFrameID = -1;
			GeneratorSingleton.Instance.DataFrameReady -= MyDataFrameReady;
		}
		else{
			GeneratorSingleton.Instance.DataFrameReady += MyDataFrameReady;
		}
	}
}
