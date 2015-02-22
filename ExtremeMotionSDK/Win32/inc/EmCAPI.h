/****************************************************************************
*																			*
*	  EmCAPI.h -- This module defines the APIs for the Extreme Motion 		*
*	              image, skeleton, warnings and gestures and services.								*
*	  																		*
*	  Copyright (c) Extreme Reality Ltd. All rights reserved.				*
*																			*
*****************************************************************************/
#ifndef _EM_C_API_H_
#define _EM_C_API_H_

#include "EmPlatform.h"
#include "EmCTypes.h"
#include "EmCRawImage.h"
#include "EmCSkeleton.h"
#include "EmCWarning.h"
#include "EmCGesture.h"

/**
 * Initializes the Generator. If the Generator is already initialized, an error is returned. 
 * To reconfigure call emShutdown.
 *
 * @param imageDescriptor 
 * An array of SImageDescriptor-s depicting the various 2D image/map 
 * (e.g. raw image) data format to be used. The descriptor order 
 * corresponds to the compacted stream bit-order. If no image
 * streams are used, this may be set to NULL.
 * 
 * @param streamFlags 
 * Bitwise-OR combination of EM_STREAM constants.
 *
 * @param renderer 
 * Reserved, may be set to NULL.
 *
 * @retval EM_STATUS_OK					if successful
		   EM_STATUS_OUT_OF_FLOW		if the Extreme Motion SDK has already been initialized
		   EM_STATUS_INVALID_LICENSE	if the Extreme Motion license file is invalid or missing
		   EM_STATUS_BAD_ARGUMENT		if an invalid argument is passed
 *		   EM_STATUS_ERROR				otherwise
 */
EM_C_API EEmStatus emInitialize(
	_In_    SImageDescriptor* imageDescriptor,
    _In_	StreamType streamFlags,
	_In_	IUnknown* renderer
    );

/**
 * Shuts-down this generator instance, releasing all resources allocated by 
 * it.
 */
EM_C_API void emShutdown();

/**
 * Configures specific stream parameters. Calling this must follow a call to
 * emInitialize specifying stream initialization. 
 *
 * @param streamFlag 
 * The EM_STREAM constant of the stream to be configured.
 *
 * @param paramName 
 * The parameter name to be configured
 *
 * @param paramValue 
 * The parameter value to be set.
 *
 * @retval EM_STATUS_OK				if successful 
 *         EM_STATUS_NOT_SUPPORTED	if paramName was invalid 
 *		   EM_STATUS_BAD_ARGUMENT	if paramValue was invalid 
 *		   EM_STATUS_OUT_OF_FLOW	if the Extreme Motion SDK was not initialized
 *		   EM_STATUS_ERROR			if the parameter could not be set
 */
EM_C_API EEmStatus emSetStreamParam(
    _In_	StreamType streamFlag,
    _In_	const wchar_t* paramName,
    _In_	const wchar_t* paramValue
    );

/**
 * Starts the specified streams. If a stream is already running, its start 
 * command will be ignored.
 * Streams will be started with their default configuration unless otherwise
 * specified by a call to emSetStreamParam.
 *
 * @param streamFlags 
 * Bitwise-OR combination of EM_STREAM constants.
 * 
 * @retval EM_STATUS_OK				if successful
 *		   EM_STATUS_OUT_OF_FLOW	if the Extreme Motion SDK has not been initialized
 *		   EM_STATUS_BAD_ARGUMENT	if the streamFlags argument is invalid
 *		   EM_STATUS_ERROR			otherwise	   
 */
EM_C_API EEmStatus emStartStreams(
    _In_	StreamType streamFlags
    );

/**
 * Stop dispatching frames for the specified streams. 
 * If a stream is already stopped, its stop command will be ignored.
 *
 * @param streamFlags 
 * Bitwise-OR combination of EM_STREAM constants.
 * 
 * @retval EM_STATUS_OK				if successful
 *		   EM_STATUS_OUT_OF_FLOW	if the Extreme Motion SDK has not been initialized
 *		   EM_STATUS_BAD_ARGUMENT	if streamFlags is invalid
 *		   EM_STATUS_ERROR			otherwise
 */
EM_C_API EEmStatus emStopStreams(
    _In_	StreamType streamFlags
    );

/**
 * Waits for any of the given streams to have a new frame or until the timeout 
 * interval elapses.
 * 
 * @param streamFlags 
 * Bitwise-OR combination of EM_STREAM constants.
 * 
 * @param timeoutMillis 
 * The time-out interval, in milliseconds. If a nonzero value is specified, 
 * the function waits until any of the specified streams has a frame or the 
 * interval elapses; otherwise, the function does not enter a wait state if 
 * any of the specified streams has a new frame, it always returns immediately.
 *
 * @param resultStream 
 * The EM_STREAM constant of the stream that has a frame.
 *
 * @param framesAddress A pre-allocated array of pointer-size to which
 * a reference to the new frame will be outputted.
 * The frame must be released using emUnlockFrames.
 *
 * @retval EM_STATUS_OK				if any of the specified streams has a new frame
 *		   EM_STATUS_TIME_OUT		if the specified interval has elapsed
 *		   EM_STATUS_OUT_OF_FLOW	if the Extreme Motion SDK was not initialized successfully
 *									and/or no streams have been started
 *		   EM_STATUS_BAD_ARGUMENT	if streamstoWaitOn is invalid
 *		   EM_STATUS_ERROR			otherwise
 */
EM_C_API EEmStatus emWaitForAndLockAnyFrame(
	_In_	StreamType streamstoWaitOn, 
	_In_	TimeType timeoutMillis,
	_Out_	StreamType* resultStream, 
	_Out_	void** frameAddress
	);

/**
 * Waits for all of the given streams to have a new frame or until the timeout
 * interval elapses, this provides a mean to synchronize streams' output.
 * 
 * @param streamFlags 
 * Bitwise-OR combination of EM_STREAM constants.
 *
 * @param timeoutMillis 
 * The time-out interval, in milliseconds. If a nonzero value is specified, 
 * the function waits until any of the specified streams has a frame or the 
 * interval elapses.
 *
 * @param framesAddresses A pre-allocated array of size matching the number of
 * initialized streams, of references to frames. The frame order corresponds 
 * to the compacted stream bit-order. Each frame must be released using 
 * emUnlockFrames.
 *
 * @retval EM_STATUS_OK				if all of the specified streams have a new frame
 *		   EM_STATUS_TIME_OUT		if the specified interval has elapsed
 *		   EM_STATUS_OUT_OF_FLOW	if the Extreme Motion SDK was not initialized successfully
 *									and/or no streams have been started
 *		   EM_STATUS_BAD_ARGUMENT	if streamstoWaitOn is invalid
 *		   EM_STATUS_NOT_SUPPORTED	if the pair-wise synchronization of any of the specified
									streams is unsupported
 *		   EM_STATUS_ERROR			otherwise
 */
EM_C_API EEmStatus emWaitForAndLockAllFrames(
	_In_	StreamType streamstoWaitOn,
	_In_	TimeType timeoutMillis,
	_Out_	void** framesAddresses
	);

/**
 * Returns the last frame the specified stream has produced. 
 * The function is non-blocking.
 * 
 * @param streams 
 * Bitwise-OR combination of EM_STREAM constants.
 *
 * @param framesAddresses A pre-allocated array of pointer-size times 
 * number of requested streams, of references to frames. 
 * The frame order corresponds to the compacted stream bit-order. 
 * Each frame must be released using emUnlockFrames.
 *
 * @retval EM_STATUS_OK				if a frame was retrieved successfully
 *   	   EM_STATUS_OUT_OF_FLOW	if the Extreme Motion SDK was not initialized successfully
 *									and/or one or more of the requested streams was not started
 *		   EM_STATUS_BAD_ARGUMENT	if streamFlags is invalid
 *		   EM_STATUS_ERROR			otherwise
 */
EM_C_API EEmStatus emLockCurrentFrames(
    _In_	StreamType streamFlags, 
    _Out_	void** frameAddresses
    );

/**
 * Releases the specified frames. Once released the frame's data is invalid.
 * 
 * @param frameAddress 
 * A reference to a frame pointers as received by any locking call.
 *
 * @retval EM_STATUS_OK				if all frames were released
 *		   EM_STATUS_BAD_ARGUMENT	if streamFlags is invalid
 *		   EM_STATUS_ERROR			otherwise
 */
EM_C_API EEmStatus emUnlockFrames(
    _In_	const StreamType streamFlags, 
    _In_	const void** frameAddresses
    );

/**
 * Sets the gesture file name.
 * 
 * @param gestureFileName
 * Full path to gesture file name
 *
 * @retval EM_STATUS_OK				if gesture file was set
 *		   EM_STATUS_ERROR			otherwise
 */
EM_C_API EEmStatus emSetGesturesFile(
    _In_	const char* gestureFileName
    );

/**
 * Injects an image to Extreme Motion SDK.
 * Make sure configuration file is set to use external image acquisition
 * 
 * @param timeStampMiliSeconds
 * Image timestamp in milliseconds
 *
 * @param data
 * Actual image in 640 x 480 RGB format 
 *
 * @retval EM_STATUS_OK				if gesture file was set
 *		   EM_STATUS_ERROR			otherwise
 */
EM_C_API EEmStatus emHandleImage(
    _In_ TimeType timeStampMiliSeconds,
    _In_ unsigned char* data);
#endif // _EM_C_API_H_
