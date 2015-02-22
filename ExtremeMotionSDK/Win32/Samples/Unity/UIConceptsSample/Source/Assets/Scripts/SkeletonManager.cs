using UnityEngine;
using System.Collections;
using Xtr3D.Net.ExtremeMotion;
using Xtr3D.Net.ExtremeMotion.Data;

public class SkeletonManager : MonoBehaviour {
	
	private TrackingState m_skeletonState = TrackingState.NotTracked;
	private Vector2 m_rightPalmPosition;
	private Vector2 m_leftPalmPosition;
	private Vector2 m_headPosition;
	
	// Use this for initialization
	void Start () {
		// register to XTR skeleton event
		GeneratorSingleton.Instance.DataFrameReady += MyDataFrameReady;
	}
	
	// Called each time we recieve a new data from engine
	private void MyDataFrameReady(object sender, Xtr3D.Net.ExtremeMotion.Data.DataFrameReadyEventArgs e)
	{
        using (var dataFrame = e.OpenFrame() as DataFrame)
        {
			if (dataFrame!=null)
			{
				JointCollection mySkeleton = dataFrame.Skeletons[0].Joints; // saves skeleton
				m_skeletonState = dataFrame.Skeletons[0].TrackingState; // saves skeleton current tracking state
				
				if (m_skeletonState.Equals(TrackingState.Tracked))
				{
					m_rightPalmPosition = new Vector2(mySkeleton.HandRight.skeletonPoint.X,mySkeleton.HandRight.skeletonPoint.Y);
					m_leftPalmPosition = new Vector2(mySkeleton.HandLeft.skeletonPoint.X,mySkeleton.HandLeft.skeletonPoint.Y);
					m_headPosition = new Vector2(mySkeleton.Head.skeletonPoint.ImgCoordNormHorizontal,mySkeleton.Head.skeletonPoint.ImgCoordNormVertical);
				}
			}
		}
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
	
	/// <summary>
	/// Gets the right palm position.
	/// </summary>
	public Vector2 GetRightPalmPosition()
	{
		return  new Vector2(Mathf.Max(m_rightPalmPosition.x,-1),Mathf.Max(m_rightPalmPosition.y,-1));
	}
	
	/// <summary>
	/// Gets the left palm position.
	/// </summary>
	public Vector2 GetLeftPalmPosition()
	{
		return  new Vector2(Mathf.Max(m_leftPalmPosition.x,-1),Mathf.Max(m_leftPalmPosition.y,-1));
	}
	/// <summary>
	/// Gets the head's position.
	/// </summary>
	public Vector2 GetHeadPosition()
	{
		return  new Vector2(Mathf.Max(m_headPosition.x,-1),Mathf.Max(m_headPosition.y,-1));
	}
}
