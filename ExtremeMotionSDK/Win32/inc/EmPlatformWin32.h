#ifndef _EM_PLATFORM_WIN32_H_
#define _EM_PLATFORM_WIN32_H_

//---------------------------------------------------------------------------
// Prerequisites
//---------------------------------------------------------------------------
#ifndef WINVER						// Allow use of features specific to Windows XP or later
	#define WINVER 0x0501
#endif
#ifndef _WIN32_WINNT				// Allow use of features specific to Windows XP or later
	#define _WIN32_WINNT 0x0602
#endif						
#ifndef _WIN32_WINDOWS				// Allow use of features specific to Windows 98 or later
	#define _WIN32_WINDOWS 0x0410
#endif
#ifndef _WIN32_IE					// Allow use of features specific to IE 6.0 or later
	#define _WIN32_IE 0x0600
#endif
#define WIN32_LEAN_AND_MEAN			// Exclude rarely-used stuff from Windows headers

// Undeprecate CRT functions
#ifndef _CRT_SECURE_NO_DEPRECATE 
	#define _CRT_SECURE_NO_DEPRECATE 1
#endif

//---------------------------------------------------------------------------
// Includes
//---------------------------------------------------------------------------
#if !defined(WINAPI_FAMILY) || WINAPI_FAMILY!=WINAPI_FAMILY_APP
#include <Unknwn.h>
#else
#include <unknwnbase.h>
#endif //#if !defined(WINAPI_FAMILY) || WINAPI_FAMILY!=WINAPI_FAMILY_APP

#include <windows.h>
#include <stdlib.h>
#include <stdio.h>
#include <malloc.h>
#include <io.h>
#include <time.h>
#include <assert.h>
#include <float.h>
#include <crtdbg.h>
#include <sal.h>
#if _MSC_VER < 1600 // Visual Studio 2008 and older doesn't have stdint.h...
typedef __int64 int64_t;
typedef unsigned __int64 uint64_t;
#else
#include <stdint.h>
#endif

//---------------------------------------------------------------------------
// Platform Basic Definition
//---------------------------------------------------------------------------
#define EM_PLATFORM EM_PLATFORM_WIN32
#define EM_PLATFORM_STRING "Win32"

//---------------------------------------------------------------------------
// Platform Capabilities
//---------------------------------------------------------------------------
#define EM_PLATFORM_ENDIAN_TYPE EM_PLATFORM_IS_LITTLE_ENDIAN

#define EM_PLATFORM_SUPPORTS_DYNAMIC_LIBS 1

//---------------------------------------------------------------------------
// Memory
//---------------------------------------------------------------------------
/** The default memory alignment. */ 
#define EM_DEFAULT_MEM_ALIGN 16

/** The thread static declarator (using TLS). */
#define EM_THREAD_STATIC __declspec(thread)

//---------------------------------------------------------------------------
// Files
//---------------------------------------------------------------------------
/** The maximum allowed file path size (in bytes). */ 
#define EM_FILE_MAX_PATH MAX_PATH

//---------------------------------------------------------------------------
// Call backs
//---------------------------------------------------------------------------
/** The std call type. */ 
#define EM_STDCALL __stdcall

/** The call back calling convention. */ 
#define EM_CALLBACK_TYPE EM_STDCALL

/** The C and C++ calling convension. */
#define EM_C_DECL __cdecl

//---------------------------------------------------------------------------
// Macros
//---------------------------------------------------------------------------
/** Returns the date and time at compile time. */ 
#define EM_TIMESTAMP __DATE__ " " __TIME__

/** Converts n into a pre-processor string.  */ 
#define EM_STRINGIFY(n) EM_STRINGIFY_HELPER(n)
#define EM_STRINGIFY_HELPER(n) #n

//---------------------------------------------------------------------------
// API Export/Import Macros
//---------------------------------------------------------------------------
/** Indicates an exported shared library function. */ 
#define EM_API_EXPORT __declspec(dllexport)

/** Indicates an imported shared library function. */ 
#define EM_API_IMPORT __declspec(dllimport)

/** Indicates a deprecated function */
#if _MSC_VER < 1400 // Before VS2005 there was no support for declspec deprecated...
	#define EM_API_DEPRECATED(msg)
#else
	#define EM_API_DEPRECATED(msg) __declspec(deprecated(msg))
#endif

/** Image defaults */
#define EM_DEFAULT_IMAGE_BITCOUNT         EM_RGB888_BITCOUNT	     
#define EM_DEFAULT_IMAGE_FORMAT		      EM_RGB888_FORMAT             

#endif //_EM_PLATFORM_WIN32_H_
