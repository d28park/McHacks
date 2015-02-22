/****************************************************************************
*																			*
*	  EmCSkeleton.h -- This module defines the types used by the			*
*	                Extreme Motion skeleton stream API.						*
*	  																		*
*	  Copyright (c) Extreme Reality Ltd. All rights reserved.				*
*																			*
*****************************************************************************/
#ifndef _EM_C_SKELETON_H_
#define _EM_C_SKELETON_H_

#include "EmCTypes.h"
#include "EmCWarning.h"

#define EM_JOINT_INVALID_COORDINATE			-9999.0f
#define EM_JOINT_INVALID_IMAGE_COORDINATE	-1.0f
#define EM_SKELETON_INVALID_ID				-1
#define EM_SKELETON_INVALID_PROXIMITY		-1.0f

/** Available joints */
typedef enum _EJointType
{
	EM_JOINT_FIRST_JOINT_INDEX = 0,
	/*0*/  EM_JOINT_HIP_CENTER = EM_JOINT_FIRST_JOINT_INDEX,
	/*1*/  EM_JOINT_SPINE,
	/*2*/  EM_JOINT_SHOULDER_CENTER,
	/*3*/  EM_JOINT_HEAD,
	/*4*/  EM_JOINT_SHOULDER_LEFT,
	/*5*/  EM_JOINT_ELBOW_LEFT,
	/*6*/  EM_JOINT_WRIST_LEFT 				/**<Not supported.*/,
	/*7*/  EM_JOINT_HAND_LEFT,
	/*8*/  EM_JOINT_SHOULDER_RIGHT,
	/*9*/  EM_JOINT_ELBOW_RIGHT,
	/*10*/ EM_JOINT_WRIST_RIGHT 			/**<Not supported.*/,
	/*11*/ EM_JOINT_HAND_RIGHT,
	/*12*/ EM_JOINT_HIP_LEFT 				/**<Not supported.*/,
	/*13*/ EM_JOINT_KNEE_LEFT 				/**<Not supported.*/,
	/*14*/ EM_JOINT_ANKLE_LEFT				/**<Not supported.*/,
	/*15*/ EM_JOINT_FOOT_LEFT 				/**<Not supported.*/,
	/*16*/ EM_JOINT_HIP_RIGHT 				/**<Not supported.*/,
	/*17*/ EM_JOINT_KNEE_RIGHT 				/**<Not supported.*/,
	/*18*/ EM_JOINT_ANKLE_RIGHT 			/**<Not supported.*/,
	/*19*/ EM_JOINT_FOOT_RIGHT				/**<Not supported.*/,	
	/*20*/ EM_JOINT_COUNT /**< size of per-skeleton Joint array */
}EJointType;

/** The nature of the detected joint */
typedef enum _EJointTrackingState
{
	EM_JOINT_TRACKING_STATE_NOT_TRACKED,	/**< joint not detected in current frame */
	EM_JOINT_TRACKING_STATE_INFERRED, 		/**< joint approximated due to hiding or noisy conditions */
	EM_JOINT_TRACKING_STATE_TRACKED			/**< joint detected in current frame */
}EJointTrackingState;

/** Skeletal processing states */
typedef enum _ESkeletonTrackingState
{
	EM_SKELETON_TRACKING_STATE_INITIALIZING,
	EM_SKELETON_TRACKING_STATE_NOT_TRACKED,
	EM_SKELETON_TRACKING_STATE_CALIBRATING,
	EM_SKELETON_TRACKING_STATE_TRACKED
}ESkeletonTrackingState;

/** Joint location */
typedef struct _SSkeletonPoint
{
	float x;
	float y;
	float z;
	float imgCoordNormHorizontal;	/**< [0, 1] from leftmost side of image plane */
	float imgCoordNormVertical;		/**< [0, 1] from topmost side of image plane */
}SSkeletonPoint;

const SSkeletonPoint S_SKELETON_POINT_INITIAL_VALUE = /**< Joint location initial value */
{
	EM_JOINT_INVALID_COORDINATE,		/* x */
	EM_JOINT_INVALID_COORDINATE,		/* y */
	EM_JOINT_INVALID_COORDINATE,		/* z */
	EM_JOINT_INVALID_IMAGE_COORDINATE,	/* imgCoordNormHorizontal */
	EM_JOINT_INVALID_IMAGE_COORDINATE	/* imgCoordNormVertical */
};

/** Joint data */
typedef struct _SJoint
{
	SSkeletonPoint skeletonPoint;
	EJointType jointType;
	EJointTrackingState jointTrackingState;
}SJoint;

const SJoint S_JOINT_INITIAL_VALUE = /**< Joint data initial value */
{
	S_SKELETON_POINT_INITIAL_VALUE,		/* skeletonPoint		*/
	EM_JOINT_COUNT,						/* jointType			*/
	EM_JOINT_TRACKING_STATE_NOT_TRACKED	/* jointTrackingState	*/
};

/** Single skeleton data */
typedef struct _SSkeleton
{
	ESkeletonTrackingState	state;
	int						skeletonId;
    float					skeletonProximity;
	SJoint					joints[EM_JOINT_COUNT];
}SSkeleton;

const SSkeleton S_SKELETON_INITIAL_VALUE = /**< Single skeleton data initial value */
{
	EM_SKELETON_TRACKING_STATE_INITIALIZING,	/* ESkeletonTrackingState	*/
	EM_SKELETON_INVALID_ID,						/* skeletonId				*/
	EM_SKELETON_INVALID_PROXIMITY,				/* skeletonProximity		*/
	S_JOINT_INITIAL_VALUE						/* joints					*/
};

/** Skeletal data */
typedef struct _SSkeletonFrame
{
	SFrameHeader	header;			/**< Common frame data */
	int				numOfSkeletons;
	SSkeleton*		skeletonData;	
}SSkeletonFrame;

const SSkeletonFrame S_SKELETON_FRAME_INITIAL_VALUE = /**< Skeletal data initial value */
{
	S_FRAME_HEADER_INITIAL_VALUE,	/* header */
	0,		/* numOfSkeletons	*/
	NULL	/* SSkeleton		*/
};


#endif  // _EM_C_SKELETON_H_
