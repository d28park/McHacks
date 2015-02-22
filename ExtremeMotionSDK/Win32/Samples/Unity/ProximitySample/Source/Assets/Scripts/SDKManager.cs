using UnityEngine;
using System.Collections.Generic;
using System;
using Xtr3D.Net.BaseTypes;
using Xtr3D.Net.ExtremeMotion;
using Xtr3D.Net;

public class SDKManager : MonoBehaviour {
	
	/// <summary>
	/// Translates the currently supported platform types into the SDK's platform types
	/// </summary>
	private static PlatformType getPlatformType()
	{
		Dictionary<RuntimePlatform, PlatformType> platforms = new Dictionary<RuntimePlatform, PlatformType>()
		{
			{RuntimePlatform.Android, PlatformType.ANDROID},
			{RuntimePlatform.IPhonePlayer, PlatformType.IOS},
			{RuntimePlatform.WindowsPlayer, PlatformType.WINDOWS},
			{RuntimePlatform.WindowsEditor, PlatformType.WINDOWS},
			{RuntimePlatform.OSXEditor, PlatformType.MAC},
			{RuntimePlatform.OSXPlayer, PlatformType.MAC}
		};		

		return platforms[Application.platform];
	}
	
	void Awake ()
	{
		try
		{
			// Initializing the SDK with the current platform type
			if(getPlatformType() == PlatformType.IOS)
				GeneratorSingleton.Instance.Initialize(getPlatformType(),new ImageInfo(ImageResolution.Resolution640x480,ImageInfo.ImageFormat.RGB888));
			else
				GeneratorSingleton.Instance.Initialize(getPlatformType());
		
			// Starting the sdk so we can receive events
			GeneratorSingleton.Instance.Start();
		}
		catch(Exception e)
		{
			Debug.LogError(e.ToString() + " " + e.Message);
		}
		
	}	
	
	void OnApplicationPause(bool paused)
	{
		if(paused){
			Debug.Log("Application in background");
			GeneratorSingleton.Instance.Stop();
		}
		else{
			Debug.Log("Application in foreground");
			GeneratorSingleton.Instance.Start();
		}
	}
	
	void OnApplicationQuit()
	{
		Debug.Log("Application Quit");
		GeneratorSingleton.Instance.Stop();
		GeneratorSingleton.Instance.Shutdown();
		
		// Due to Unity3d issue, a standalone application which uses the engine can get non-responsive, instead of quiting.
		// to solve this, we kill the prcoess instead.
		if(!Application.isEditor && Application.platform == RuntimePlatform.WindowsPlayer)
			System.Diagnostics.Process.GetCurrentProcess().Kill();
	}
}
