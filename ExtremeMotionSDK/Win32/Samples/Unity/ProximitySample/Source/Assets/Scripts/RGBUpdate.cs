using UnityEngine;
using Xtr3D.Net;
using Xtr3D.Net.ExtremeMotion;
using Xtr3D.Net.ColorImage;

public class RGBUpdate : MonoBehaviour {
	
	public UITexture RGBTexture;
	private byte[] _rgbImage;
	private long _currentFrameKey = -1;
	private long _lastFrameKey = -1;
	private int _imageHeight;
	private int _imageWidth;
	private Texture2D buffer;
	void Start () 
	{
		// Registering to the SDK's RGB/ColorImage event
		GeneratorSingleton.Instance.ColorImageFrameReady += OnImageFrame;
		buffer = new Texture2D(640,480,TextureFormat.RGB24,false);
	}
	
	void OnImageFrame (object sender, ColorImageFrameReadyEventArgs e)
	{
		using (ColorImageFrame colorImageFrame = e.OpenFrame() as ColorImageFrame)
		{
			if (colorImageFrame == null)
			{
				return;
			}
			
			_rgbImage = colorImageFrame.ColorImage.Image;
			_currentFrameKey = colorImageFrame.FrameKey.FrameNumberKey;
			_imageWidth = colorImageFrame.Width;
			_imageHeight = colorImageFrame.Height;
		}
	}
			
	void Update () 
	{
		// Updating the display only if we have what to display,
		// And it's not the same frame-data as the last one we displayed
		if ((_lastFrameKey == _currentFrameKey)	|| (_rgbImage == null))
		{
			return;
		}
		
		_lastFrameKey = _currentFrameKey;
		
		buffer.LoadRawTextureData(_rgbImage);
		buffer.Apply();
		RGBTexture.mainTexture = buffer;
	}
}
