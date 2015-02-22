using UnityEngine;
using System.Collections;
using Xtr3D.Net.ExtremeMotion;
using Xtr3D.Net.ExtremeMotion.Data;
using Xtr3D.Net.BaseTypes;

public class BackRecognitionManager : MonoBehaviour {
	
	private const int NUM_NON_BACK_GESTURE_CONSECUTIVE_UPDATES_THRESHOLD = 10;
	private const string MAIN_MENU_SCENE_NAME = "Main";
	private const float BACK_GESTURE_CLICK_TIME = 1f;
	
	private long m_lastFrameID 		= -1;
	private long m_currFrameID 		= -1;
	private float 		m_backGestureStartTime = -1;
	private float 		m_myTimer = 0;
	private int 		m_consecutiveNoneBackGesture = 5;
	private bool m_backGestureDetected 	= false;
	
	public TimeBaseAnimation m_myAnimation;
	private DetectDownDiagonalPosition m_backGestureDetector;
	
	void Start () {
		m_backGestureDetector = new DetectDownDiagonalPosition();
		m_backGestureDetector.Init();
		
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
					if(m_backGestureDetector.IsDownDiagonalPoision(mySkeleton,m_lastFrameID)) // checking if user performing back gesture
					{
						m_backGestureDetected = true;
					}
					else{
						m_backGestureDetected = false;
					}
				}
			}
		}
	}
	
	void Update()
	{
		m_myTimer += Time.deltaTime; // updating game timer
		
		// user is pose
		if (m_backGestureDetected)
		{
			if(m_backGestureStartTime == -1) // checks if time based click didn't start yet
			{
				//saves animation starting time
				m_backGestureStartTime = m_myTimer;
				m_myAnimation.PlayAnimation(); // starts animation
				m_consecutiveNoneBackGesture = 0;
			}
			else
			{
				// checking if time from m_tPoseStartTime passed click time threshold
				if(m_myTimer - m_backGestureStartTime > BACK_GESTURE_CLICK_TIME)
				{
					// back gesture click occured
					m_backGestureStartTime = -1;
					Application.LoadLevel(MAIN_MENU_SCENE_NAME);
				}
			}
		}else{ // user is not in pose
			if (m_consecutiveNoneBackGesture > NUM_NON_BACK_GESTURE_CONSECUTIVE_UPDATES_THRESHOLD)
			{
				m_backGestureStartTime = -1;
				m_myAnimation.HideAnimation();
			}
			else{
				m_consecutiveNoneBackGesture++;
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
