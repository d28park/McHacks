using UnityEngine;
using System.Collections;
using System;
using Xtr3D.Net;
using Xtr3D.Net.ExtremeMotion;
using Xtr3D.Net.ColorImage;
using Xtr3D.Net.ExtremeMotion.Data;

public class DisplayCameraRGB : MonoBehaviour {
	
	private UITexture m_myTexture;
	private byte[] m_newRgbImage;
	private object m_lockInstance = new object(); //used to sync between threads.
	private bool m_isDrawing = false;
	private Texture2D buffer;

	// Use this for initialization (called before start)
	void Awake () {
		// saves texture attached to game object
		if (m_myTexture == null)
			m_myTexture = GetComponent<UITexture>();

		buffer = new Texture2D(640,480,TextureFormat.RGB24,false);
	}
	// Use this for initialization
	void Start () {
		// register to XTR image event
		GeneratorSingleton.Instance.ColorImageFrameReady +=	new EventHandler<ColorImageFrameReadyEventArgs>(OnColorImageReceived);
	}
	
	private long m_lastFrameID = -1;
	private long m_currFrameID = -1;
	// called each time we recieve a new image from engine
	void OnColorImageReceived(object sender, ColorImageFrameReadyEventArgs e)
	{
		using (var colorImageFrame = e.OpenFrame() as ColorImageFrame)
		{	
			if (colorImageFrame != null)
            {
				m_currFrameID = colorImageFrame.FrameKey.FrameNumberKey; // saves current frame id
				if (m_currFrameID <= m_lastFrameID) // checks if we have a "real" new image
					return;
				// Unity allows to draw GUI only with dedicated threads, we use a lock to make sure this thread is synced with Unity 'update' method.
				// this will make sure we will display the new recieved image.
				lock (m_lockInstance) 
				{
					m_lastFrameID = m_currFrameID; // update current frame id
					m_newRgbImage = colorImageFrame.ColorImage.Image; // saves new image
				}
			}
		}
	}
	// Update is called once per frame
	void Update () {
		lock (m_lockInstance)
		{
			if(m_newRgbImage != null && m_isDrawing){
				// draws the new image on our texture
				buffer.LoadRawTextureData(m_newRgbImage);
				buffer.Apply();
				m_myTexture.mainTexture = buffer;
				m_newRgbImage = null; // clear after displayed to release memory
			}
		}
	}
	/// <summary>
	/// Determine if will draw RGB image over texture.
	/// </summary>
	/// <param name='draw'>
	/// true for drawing, false otherwise.
	/// </param>
	public void Draw(bool draw)
	{
		m_isDrawing = draw;
	}

	void OnApplicationPause(bool paused)
	{
		if(!paused){//restart the id count now that the generator was started again
			m_currFrameID = -1;
			m_lastFrameID = -1;
		}
	}
}