using UnityEngine;
using System.Collections;
using Xtr3D.Net.BaseTypes;

public class PlatformChecker
{
	public static PlatformType getPlatformType()
	{
		//set default platform to Windows
		PlatformType platform = PlatformType.WINDOWS; 
		
		if (Application.platform == RuntimePlatform.Android)
			platform = PlatformType.ANDROID;
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
			platform = PlatformType.IOS;
		else if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
			platform = PlatformType.MAC;
		#if NETFX_CORE
		else if (Application.platform==RuntimePlatform.MetroPlayerX86 || Application.platform==RuntimePlatform.MetroPlayerX64)
			platform = PlatformType.WINDOWS_STORE;
		#endif
		return platform;
	}
}
