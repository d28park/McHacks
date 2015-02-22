using UnityEngine;
using System.Collections;
using Xtr3D.Net.ExtremeMotion;
using Xtr3D.Net.ExtremeMotion.Data;
using Xtr3D.Net.ColorImage;
using Xtr3D.Net.BaseTypes;
/// <summary>
/// Hovering manager 
/// </summary>
public class HoveringManager : MonoBehaviour {
	
	private const int INVALID_VALUE = -1;
	//XTR Parameters filled every data frame
	private TrackingState m_skeletonState = TrackingState.Initializing;
	private float m_skeletonProximity;
	
	private HoveringTranslator m_hoveringTranslator;
	private bool m_first_calibrate = false;
	
	private long lastFrameID = INVALID_VALUE;
	private long currFrameID = INVALID_VALUE;
	
	private Vector2 m_translatedPalmPosition;
	private Vector2 m_rightPalmPosition;
	private Vector2 m_leftPalmPosition;
	
	private bool m_cursorValid = false;
	private Object m_lockInstance = new Object(); // used to sync between threads
	
	// Use this for initialization
	void Start () {
		m_hoveringTranslator = new HoveringTranslator();
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
				currFrameID = dataFrame.FrameKey.FrameNumberKey; // saves current frame id
				if (currFrameID <= lastFrameID) // checks if we have a "real" new data
					return;
				lastFrameID = currFrameID; // update current frame id
				
				JointCollection mySkeleton = dataFrame.Skeletons[0].Joints; // saves skeleton
				
				float cursor_X = INVALID_VALUE;
				float cursor_Y = INVALID_VALUE;
				if (m_skeletonState.Equals(TrackingState.Tracked))
				{
					
					//Hovering translator
					if(!m_first_calibrate)
					{
						m_first_calibrate = true;
						m_hoveringTranslator.Init(m_skeletonProximity,mySkeleton);
					}
					cursor_X = mySkeleton.HandRight.skeletonPoint.ImgCoordNormHorizontal;
					cursor_Y = mySkeleton.HandRight.skeletonPoint.ImgCoordNormVertical;
					m_rightPalmPosition = new Vector2(cursor_X,cursor_Y);
					m_leftPalmPosition = new Vector2(mySkeleton.HandLeft.skeletonPoint.ImgCoordNormHorizontal,mySkeleton.HandLeft.skeletonPoint.ImgCoordNormVertical);
					
					//cursor will be controled with right hand, we save cursor coordinates (X,Y) as represented inside XTR RGB image (640X480)
					m_hoveringTranslator.ImgNormXYCoordsToScreenXY(ref cursor_X,ref cursor_Y,m_skeletonProximity,mySkeleton);
				}
				lock (m_lockInstance)
				{	
					m_skeletonState = dataFrame.Skeletons[0].TrackingState; // saves skeleton tracking state
					m_skeletonProximity = dataFrame.Skeletons[0].Proximity.SkeletonProximity; // saves skeleton proximity (distance from cam)
					m_translatedPalmPosition = new Vector2(cursor_X,cursor_Y);
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
		lock (m_lockInstance)
		{
			if(m_skeletonState.Equals(TrackingState.Tracked))
			{
				// check if point values are valid
				if(m_translatedPalmPosition.x > INVALID_VALUE && m_translatedPalmPosition.y > INVALID_VALUE)
					CursorValid = true;
				else
					CursorValid = false;
			}
			else // skeleton is not in tracking state
			{
				CursorValid = false;
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
	/// Gets or sets a value indicating whether the cursor is valid.
	/// </summary>
	/// <value>
	/// <c>true</c> if cursor valid; otherwise, <c>false</c>.
	/// </value>
	public bool CursorValid
    {
          get{ return m_cursorValid;  }
          set{ m_cursorValid = value; }
    }
	
	/// <summary>
	/// Gets the right palm position translated to current screen resolution.
	/// </summary>
	public Vector2 GetTranslatedRightPalmPosition()
	{
		// we adapt cursor position to unity with nGui by matching both systems
		return new Vector2(((m_translatedPalmPosition.x) * Screen.width)/2 ,(0.5f - m_translatedPalmPosition.y) * Screen.height);
	}
	
	/// <summary>
	/// Gets the right palm position in xtr3d color image frame.
	/// </summary>
	public Vector2 GetRightPalmPosition()
	{
		return new Vector2(m_rightPalmPosition.x,-m_rightPalmPosition.y);
	}
	/// <summary>
	/// Gets the left palm position in xtr3d color image frame.
	/// </summary>
	public Vector2 GetLeftPalmPosition()
	{
		return new Vector2(m_leftPalmPosition.x,-m_leftPalmPosition.y);
	}

	void OnApplicationPause(bool pause)
	{
		if(pause){
			lastFrameID = -1;
			currFrameID = -1;
			m_skeletonState = TrackingState.NotTracked;
			GeneratorSingleton.Instance.DataFrameReady -= MyDataFrameReady;
		}
		else{
			GeneratorSingleton.Instance.DataFrameReady += MyDataFrameReady;
		}
	}
}
