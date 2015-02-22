using UnityEngine;
using System.Collections;
using Xtr3D.Net.ExtremeMotion;
using Xtr3D.Net.ExtremeMotion.Data;
using Xtr3D.Net.BaseTypes;

public class SlideDetectorManager : MonoBehaviour {

	private long m_lastFrameID 		= -1;
	private long m_currFrameID 		= -1;
	private bool m_slideDetected 	= false;
	private bool m_firstFrame 		= true;
	private Point m_rightPalmStartSlide;
	
	private SlideDetector m_slideDetector;
	
	public delegate void MySlideEventHandler();
	
	//This event can cause any method which conforms
	//to MyEventHandler to be called.
	public event MySlideEventHandler SlideOccured;
	
	void Start () {
		m_slideDetector = new SlideDetector();
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
					m_slideDetector.Init(skelProximity);
				}
				m_currFrameID = dataFrame.FrameKey.FrameNumberKey; // saves current frame id
				if (m_currFrameID <= m_lastFrameID) // checks if we have a "real" new data
					return;
				m_lastFrameID = m_currFrameID; // update current frame id
				
				JointCollection mySkeleton = dataFrame.Skeletons[0].Joints; // saves skeleton
				//checking skeleton state is tracked
				if (dataFrame.Skeletons[0].TrackingState.Equals(TrackingState.Tracked))
				{
                    m_slideDetected = m_slideDetector.IsSlide(mySkeleton, skelProximity, ref m_rightPalmStartSlide); // checking if slide received
					
					if(m_slideDetected){
						SlideOccured();
					}
				}
			}
		}
	}
	
	public Point GetSlideStartLocation()
	{
		return m_rightPalmStartSlide;
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
