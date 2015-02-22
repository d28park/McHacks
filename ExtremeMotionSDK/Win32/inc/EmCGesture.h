/****************************************************************************
*																			*
*	  EmCGestures.h -- This module defines the types used by the			*
*	                Extreme Motion static gestures stream API.				*
*	  																		*
*	  Copyright (c) Extreme Reality Ltd. All rights reserved.				*
*																			*
*****************************************************************************/
#ifndef _EM_C_GESTURES_H_
#define _EM_C_GESTURES_H_

#include "EmPlatform.h"
#include "EmCTypes.h"

typedef enum _EGestureType
{
	EM_GESTURE_TYPE_START,
	EM_GESTURE_TYPE_STATIC_POSITION = EM_GESTURE_TYPE_START,
	EM_GESTURE_TYPE_HEAD_POSITION,
	EM_GESTURE_TYPE_SWIPE,
	EM_GESTURE_TYPE_WINGS,
	EM_GESTURE_TYPE_SEQUENCE,
	EM_GESTURE_TYPE_UP,
	EM_GESTURE_TYPE_DOWN,
	EM_GESTURE_TYPE_COUNT
}EGestureType;

/* Base gesture*/
typedef struct _SBaseGesture
{
	EGestureType		gestureType;
	GestureId			gestureId;
    char*               description;
}SBaseGesture;

/* Gestures frame */
typedef struct _SGesturesFrame
{
	SFrameHeader	    header;			/**< Common frame data */
	int					numOfGestures;
	SBaseGesture**		gestures;
}SGesturesFrame;

/* Static gestures data */
typedef struct _SStaticGesture
{
	SBaseGesture	gestureHeader;
}SStaticGesture;

/* Head Position gesture data */
typedef struct _SHeadPositionGesture
{
	SBaseGesture	gestureHeader;
	int				regionIndex;
}SHeadPositionGesture;

/*Swipe gesture data */
typedef struct _SSwipeGesture
{
	SBaseGesture	gestureHeader;	
}SSwipeGesture;

/*Wings gesture data */
typedef struct _SWingsGesture
{
	SBaseGesture	gestureHeader;
	int				armsAngle; /* the angle between arms and the body. From -90 to 90. */
}SWingsGesture;

/*Sequence gesture data */
typedef struct _SSequenceGesture
{
	SBaseGesture	gestureHeader;
}SSequenceGesture;

/*Up gesture data */
typedef struct _SUpGesture
{
	SBaseGesture	gestureHeader;
}SUpGesture;

/*Down gesture data */
typedef struct _SDownGesture
{
	SBaseGesture	gestureHeader;
}SDownGesture;

const SGesturesFrame S_GESTURES_FRAME_INITIAL_VALUE = /**< Raw camera image data initial value */
{
	S_FRAME_HEADER_INITIAL_VALUE,	  /* header				*/	
	0,								  /* number of gestures	*/	
	NULL							  /* gestures array		*/
};

#endif  // _EM_C_GESTURES_H_
