using UnityEngine;
using System.Collections;
using Xtr3D.Net.ExtremeMotion;
using Xtr3D.Net.ExtremeMotion.Data;
using Xtr3D.Net.BaseTypes;

public class SwipeDetectorManager : MonoBehaviour {

	private long m_lastFrameID 		= -1;
	private long m_currFrameID 		= -1;
	private int m_swipeType 		= 0;
	private bool m_firstFrame 		= true;
	
	private TrackingState m_skeletonState = TrackingState.NotTracked;
	public SwipeIndicator swipeIndicator;
	private SwipeDetector.SwipeType m_swipeStatus;
	
	private SwipeDetector m_swipeDetector;
	
	public delegate void MySwipeEventHandler(int swipeType);
	
	//This event can cause any method which conforms
	//to MyEventHandler to be called.
	public event MySwipeEventHandler SwipeOccured;
	
	void Start () {
		m_swipeDetector = new SwipeDetector();
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
				float skelProximity = dataFrame.Skeletons[0].Proximity.SkeletonProximity;
				if (m_firstFrame)
				{
					m_firstFrame = false;
					m_swipeDetector.Init(skelProximity);
				}
				m_currFrameID = dataFrame.FrameKey.FrameNumberKey; // saves current frame id
				m_skeletonState = dataFrame.Skeletons[0].TrackingState; // saves skeleton current tracking state
				if (m_currFrameID <= m_lastFrameID) // checks if we have a "real" new data
					return;
				m_lastFrameID = m_currFrameID; // update current frame id
				
				JointCollection mySkeleton = dataFrame.Skeletons[0].Joints; // saves skeleton
				//checking skeleton state is tracked
				if (dataFrame.Skeletons[0].TrackingState.Equals(TrackingState.Tracked))
				{
                    //m_swipeDetected = m_swipeDetector.IsSlide(mySkeleton, skelProximity, ref m_rightPalmStartSlide); // checking if slide received
					m_swipeStatus = m_swipeDetector.GetLastSwipeStatus();
					m_swipeType = m_swipeDetector.IsSwipe(mySkeleton,skelProximity,dataFrame.Timestamp);
					SwipeOccured(m_swipeType);
				}
			}
		}
	}
	
	void Update()
	{
		if(m_swipeStatus == SwipeDetector.SwipeType.NO_SWIPE)
			swipeIndicator.Clear();
		else
			UpdateSwipeStatus();
	}
	
	void UpdateSwipeStatus ()
	{
		swipeIndicator.ShowArrow(m_swipeStatus);
	}
	
	/// <summary>
	/// Gets the skeleton tracking state.
	/// </summary>
	/// <returns>
	/// The tracking state.
	/// </returns>
	public TrackingState GetTrackingState()
	{
		return m_skeletonState;
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
