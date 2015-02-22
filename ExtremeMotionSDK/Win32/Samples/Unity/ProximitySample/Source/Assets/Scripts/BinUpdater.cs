using UnityEngine;
using System.Collections;
using Xtr3D.Net.ExtremeMotion;
using Xtr3D.Net.ExtremeMotion.Data;

public class BinUpdater : MonoBehaviour {
	
	private readonly HysteresisManager m_HysteresisManager = new HysteresisManager();
	private TrackingState m_PreviousState = TrackingState.Initializing;
	private UISprite m_Texture;
	private const string k_TextureBaseName = "distance_";
	private string m_CurrentTextureName = k_TextureBaseName;
	
	void Start () {
		GeneratorSingleton.Instance.DataFrameReady += OnDataFrame;
		
		m_Texture = GetComponent<UISprite>();
	}
	
	/// <summary>
	/// Proximity is given through the Data Frames. We thus register to that event
	/// </summary>	
	void OnDataFrame (object sender, Xtr3D.Net.ExtremeMotion.Data.DataFrameReadyEventArgs e)
	{
		using (DataFrame dataFrame = e.OpenFrame() as DataFrame)
		{
			if (dataFrame == null)
			{
				return;
			}
			
			Skeleton skl = dataFrame.Skeletons[0]; // Currently providing only one skeleton
			float skeletonProximity = skl.Proximity.SkeletonProximity;
			
			// While we are tracked, we inspect the Proximity's behavior patterns
			if (TrackingState.Tracked.Equals(skl.TrackingState))
			{	
				// If this is the first frame we're tracked on, calling an init function
				if (!TrackingState.Tracked.Equals(m_PreviousState))
				{
					m_HysteresisManager.SetCurrentBinIndexFromValue(skeletonProximity);
					Debug.Log("Calibrated");
				}
				// If this is after we began tracking, we update the bin number
				else
				{
					m_HysteresisManager.UpdateValue(skeletonProximity);
					Debug.Log(string.Format("Received Bin #{0}", m_HysteresisManager.BinIndex));
				}
				
				// Updating the presentation information - the images go by bin number, 0 to 4
				m_CurrentTextureName = k_TextureBaseName + m_HysteresisManager.BinIndex.ToString();
			}
			// We are not tracked, but we were tracked in the previous frame - resetting the hysteresis
			else
			{
				// show original distance meter state
				m_CurrentTextureName = k_TextureBaseName;
				
				if (TrackingState.Tracked.Equals(m_PreviousState))
				{
					m_HysteresisManager.SetCurrentBinIndex(-1);
					Debug.Log("Resetting Bin Hysteresis");
				}
				// We continue being not tracked
				else if(TrackingState.NotTracked.Equals(skl.TrackingState)) 
				{					
					Debug.Log("Not tracked");
				}
			}
		
			m_PreviousState = skl.TrackingState;
		}
	}
	
	void Update () {
	
		if (!m_Texture.spriteName.Equals(m_CurrentTextureName))
		{
			m_Texture.spriteName = m_CurrentTextureName;
		}
	}
}
