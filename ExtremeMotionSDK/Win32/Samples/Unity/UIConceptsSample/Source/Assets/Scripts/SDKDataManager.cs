using UnityEngine;
using System.Collections;
using System;
using Xtr3D.Net;
using Xtr3D.Net.BaseTypes;
using Xtr3D.Net.ExtremeMotion;

public class SDKDataManager : MonoBehaviour {
	// Use this for initialization
	private bool isEngineStarted = false;
	
	void Awake()
	{	
		//Retrieving the platform we are running on
		PlatformType platformType = PlatformChecker.getPlatformType();
		//Initializing engine with current platform
		try
		{
			if(!GeneratorSingleton.Instance.IsInitialized)
			{
				if(platformType == PlatformType.IOS)
					GeneratorSingleton.Instance.Initialize(platformType,new ImageInfo(ImageResolution.Resolution640x480,ImageInfo.ImageFormat.RGB888));
				else
					GeneratorSingleton.Instance.Initialize(platformType);
			}
		}
		catch(Exception e)
		{
			Debug.LogError(e.ToString() + " " + e.Message);
		}		
	}
	
	void Update()
	{
		if(!isEngineStarted)
		{
			isEngineStarted = true;
			//Starting the engine
			try
			{
	   			GeneratorSingleton.Instance.Start();
			}	
			catch (Exception e)
			{
				Debug.LogError(e.ToString() + " " + e.Message);
			}
		}
	}
	
	/// <summary>
	/// Closing the engine on applicaton quit
	/// </summary>
	void OnApplicationQuit()
	{
		Debug.Log("Application Quit");
		GeneratorSingleton.Instance.Stop();
		GeneratorSingleton.Instance.Shutdown();
		// Due to Mono issue, a standalone application which uses the engine can get non-responsive, instead of quiting.
		// to solve this, we kill the prcoess instead.
		#if UNITY_STANDALONE_WIN 
		if(! Application.isEditor && Application.platform == RuntimePlatform.WindowsPlayer) // only relevant for windows 8
			System.Diagnostics.Process.GetCurrentProcess().Kill();
		#endif
	}
	/// <summary>
	/// Handles on application pause event on mobile devices
	/// </summary>
	void OnApplicationPause(bool paused)
	{
		if(paused){
			Debug.Log("Application in background");
			Screen.sleepTimeout = SleepTimeout.SystemSetting; 
			GeneratorSingleton.Instance.Stop();
		}
		else{
			Debug.Log("Application in foreground");
			Screen.sleepTimeout = SleepTimeout.NeverSleep; // prevents from the screen to dimm
			GeneratorSingleton.Instance.Start();
		}
	}
}