/****************************************************************************
*																			*
*	  EmPlatform.h -- This module defines the platform definitions for		*
*					  the Extreme Motion skeleton and image services.		*
*	  																		*
*	  Copyright (c) Extreme Reality Ltd. All rights reserved.				*
*																			*
*****************************************************************************/
#ifndef _EM_PLATFORM_H_
#define _EM_PLATFORM_H_

#include "EmImageDefinitions.h"

//
// Supported platforms
//

#define EM_PLATFORM_WIN32 1
#define EM_PLATFORM_WIN_RT 2
#define EM_PLATFORM_LINUX_X86 3
#define EM_PLATFORM_LINUX_ARM 4
#define EM_PLATFORM_MACOSX 5
#define EM_PLATFORM_ANDROID_ARM 6

#if (defined _WIN32) //Common to both Desktop and WinRT
#	include "EmPlatformWin32.h"
#endif
#if defined(WINAPI_FAMILY) && WINAPI_FAMILY==WINAPI_FAMILY_APP
#	include "EmPlatformWinRT.h"
#elif (defined (OS_ANDROID) || defined(ANDROID)) && defined (__arm__)
#	include "EmPlatformAndroid-Arm.h"
#elif (__APPLE__)
#   include "TargetConditionals.h"
#   if TARGET_OS_IPHONE || TARGET_OS_IPAD || TARGET_OS_SIMULATOR
#       include "EmPlatformIOS.h"
#   else
#       include "EmPlatformMacOSX.h"
#   endif
#endif

#ifdef __cplusplus
#	define EM_C extern "C"
#	define EM_C_API_EXPORT EM_C  EM_API_EXPORT
#	define EM_C_API_IMPORT EM_C  EM_API_IMPORT
#	define EM_CPP_API_EXPORT  EM_API_EXPORT
#	define EM_CPP_API_IMPORT  EM_API_IMPORT
#else // __cplusplus
#	define EM_C_API_EXPORT  EM_API_EXPORT
#	define EM_C_API_IMPORT  EM_API_IMPORT
#endif  // __cplusplus

#ifdef EXTREME_MOTION_EXPORTS
#	define EM_C_API EM_C_API_EXPORT
#	define EM_CPP_API EM_CPP_API_EXPORT
#else // EXTREME_MOTION_EXPORTS
#	define EM_C_API EM_C_API_IMPORT
#	define EM_CPP_API EM_CPP_API_IMPORT
#endif // EXTREME_MOTION_EXPORTS

#endif // _EM_PLATFORM_H_
