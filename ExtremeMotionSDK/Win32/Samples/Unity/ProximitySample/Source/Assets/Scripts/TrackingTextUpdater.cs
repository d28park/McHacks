using UnityEngine;
using System.Collections.Generic;
using Xtr3D.Net.ExtremeMotion;
using Xtr3D.Net.ExtremeMotion.Data;
using System;

public class TrackingTextUpdater : MonoBehaviour
{
	private TrackingState trackingState;
	private const string basicTrackingText = "Tracking State:";
	private Dictionary<TrackingState, string> m_StateTextDictionary = new Dictionary<TrackingState, string>() { 
		{TrackingState.Initializing, "Initializing"},
		{TrackingState.Calibrating, "Calibrating"},
		{TrackingState.NotTracked, "Not Tracked"},
		{TrackingState.Tracked, "Tracked"}
	};
	private UILabel TrackingText;
	public UISprite CalibrationTexture;
	
	
	void Awake()
	{
		TrackingText = GetComponent<UILabel>();
	}
	
	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start() {	
		GeneratorSingleton.Instance.DataFrameReady += OnDataFrameReceived;
	}

	void OnDataFrameReceived(object sender, DataFrameReadyEventArgs e)
	{
		using (DataFrame dataFrame = e.OpenFrame() as DataFrame)
		{
			
			string text = String.Empty;
			if (!m_StateTextDictionary.TryGetValue(trackingState, out text))
			{
				text = "UNRECOGNIZED STATE";
			}
			trackingState = dataFrame.Skeletons[0].TrackingState;
			TrackingText.text = basicTrackingText + System.Environment.NewLine + text;
		}
	}
	
    // Update is called once per frame
	void Update () {
		// If we're in calibration mode - show the Calibration Image
		CalibrationTexture.enabled = trackingState.Equals(TrackingState.Calibrating);
	}
}