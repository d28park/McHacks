/****************************************************************************
*																			*
*	  EmCTypes.h -- This module defines the various types used by the		*
*	                Extreme Motion API.										*
*	  																		*
*	  Copyright (c) Extreme Reality Ltd. All rights reserved.				*
*																			*
*****************************************************************************/
#ifndef _EM_C_TYPES_H_
#define  _EM_C_TYPES_H_

#include <limits.h>
#include "ExtremeRealityTypes.h"

/*****************************************************************************
*								Generator		                             *
*****************************************************************************/

#define EM_STREAM_NONE		0x00000000  /**< No streams */
#define EM_STREAM_RAW_IMAGE	0x00000001	/**< Raw camera image stream */
#define EM_STREAM_SKELETON	0x00000002	/**< Skeletal data stream */
#define EM_STREAM_WARNING	0x00000004 	/**< System state warning stream */
#define EM_STREAM_GESTURES	0x00000008 	/**< Gestures stream */
#define EM_STREAM_INVALID	0x80000000

#define EM_STREAM_COUNT		4

#define EM_FRAME_INVALID_ID			UINT_MAX
#define EM_FRAME_INVALID_TIMESTAMP	UINT_MAX
typedef unsigned int StreamType;
typedef unsigned int GestureId;

/** Possible status values */
typedef enum _EEmStatus
{
	EM_STATUS_OK = 0,
	EM_STATUS_ERROR,
	EM_STATUS_CAMERA_UNAVAILABLE,
	EM_STATUS_NOT_IMPLEMENTED,
	EM_STATUS_NOT_SUPPORTED,
	EM_STATUS_BAD_ARGUMENT,
	EM_STATUS_OUT_OF_FLOW,
	EM_STATUS_TIMEOUT,	
	EM_STATUS_INVALID_LICENSE
}EEmStatus;

/** Common frame data */
typedef struct _SFrameHeader
{
	StreamType		type;		/**< EM_STREAM source of frame */
	FrameId			frameId;	/**< EM_STREAM-unique, monotonically increasing, frame id */
	TimeType		timestamp;	/**< System time when the frame was authored */
}SFrameHeader;

/** Common frame data initial value */
const SFrameHeader S_FRAME_HEADER_INITIAL_VALUE = 
{
	EM_STREAM_INVALID,			/* type			*/
	EM_FRAME_INVALID_ID,		/* frameId		*/
	EM_FRAME_INVALID_TIMESTAMP	/* timestamp	*/
};

#endif  // _EM_C_TYPES_H_
