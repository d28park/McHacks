using UnityEngine;
using System.Collections;
using Xtr3D.Net.ExtremeMotion;
using Xtr3D.Net.ExtremeMotion.Data;
using Xtr3D.Net.BaseTypes;

public class TPoseRecognitionManager : MonoBehaviour {
	
	private const int NUM_NON_T_POSE_CONSECUTIVE_UPDATES_THRESHOLD = 10;
	private const float T_POSE_CLICK_TIME = 1f;
	
	private long m_lastFrameID 		= -1;
	private long m_currFrameID 		= -1;
	private float 		m_tPoseStartTime = -1;
	private float 		m_myTimer = 0;
	private int 		m_consecutiveNoneTPoses = 5;
	private bool m_tPoseDetected 	= false;
	
	public TimeBaseAnimation m_myAnimation;
	//private UISprite m_TPoseSprite;
	private HelpDialog m_helpDialog;
	private TPositionDetector m_TposeDetector;
	
	void Start () {
		m_TposeDetector = new TPositionDetector();
		
		m_myAnimation.gameObject.transform.localPosition = this.transform.localPosition;
		
		//m_TPoseSprite = GetComponent<UISprite>();
		
		m_helpDialog = GameObject.FindGameObjectWithTag("Help").GetComponent<HelpDialog>();
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
					if(m_TposeDetector.IsTPosition(mySkeleton)) // checking if user is in T pose
					{
						m_tPoseDetected = true;
					}
					else{
						m_tPoseDetected = false;
					}
				}
			}
		}
	}
	
	public bool IsEnabled()
	{
		return m_myAnimation.isPlaying;
	}
	
	private void Init()
	{
		m_tPoseStartTime = -1;
		m_myAnimation.HideAnimation();
	}
	
	void Update()
	{
		m_myTimer += Time.deltaTime; // updating game timer
		
		if(!m_helpDialog.IsOn())
		{
			// user is in T pose
			if (m_tPoseDetected)
			{			
				if(m_tPoseStartTime == -1) // checks if time based click didn't start yet
				{
					//saves animation starting time
					m_tPoseStartTime = m_myTimer;
					m_myAnimation.PlayAnimation(); // starts animation
					m_consecutiveNoneTPoses = 0;
				}
				else
				{
					// checking if time from m_tPoseStartTime passed click time threshold
					if(m_myTimer - m_tPoseStartTime > T_POSE_CLICK_TIME)
					{
						//T pose click occured
						m_tPoseStartTime = -1;
						
						if(m_helpDialog != null){
							Init();
							m_helpDialog.ShowDialog();
						}
						else{
							Debug.LogError("T pose position detected, Help Screen not found!");
						}
					}
				}
			}else{ // user is not in T pose
				if (m_consecutiveNoneTPoses > NUM_NON_T_POSE_CONSECUTIVE_UPDATES_THRESHOLD)
				{
					Init();
				}
				else{
					m_consecutiveNoneTPoses++;
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
