/****************************************************************************
*																			*
*	  EmCWarning.h -- This module defines the various types used by the		*
*					 Extreme Motion warning stream API.						*
*	  																		*
*	  Copyright (c) Extreme Reality Ltd. All rights reserved.				*
*																			*
*****************************************************************************/
#ifndef _EM_WARNING_H_
#define _EM_WARNING_H_

#include "EmCTypes.h"

/**
 *  Warnings are 32 bit values laid out as follows:
 *
 *   3 3 2 2 2 2 2 2 2 2 2 2 1 1 1 1 1 1 1 1 1 1
 *   1 0 9 8 7 6 5 4 3 2 1 0 9 8 7 6 5 4 3 2 1 0 9 8 7 6 5 4 3 2 1 0
 *  +---------------+-----------------------------------------------+
 *  |   Category    |					Code			            |
 *  +---------------+-----------------------------------------------+
 *
 *  where
 *
 *      Category - is Extreme Motion component code
 *
 *      Code - is the per-category warnings content
 */
typedef int EmWarning;

/** Return the code */
#define EM_WARNING_CODE(w)		((w) & 0xFFFFFF)

/** Return the category */
#define EM_WARNING_CATEGORY(w)  (((w) >> 24) & 0xFF)

/** Create an EmWarning value from component pieces */
#define MAKE_EM_WARNING(fac, code) \
    ((EmWarning) (((unsigned int)(fac) << 24) | ((unsigned int)(code))))

/** Warning categories */
#define EM_WARNING_CATEGORY_SKELETON_FRAME_EDGE	0 /**< Warning relating user location in with respect to measured space */
#define EM_WARNING_CATEGORY_RAW_IMAGE			1 /**< Warning relating to single raw image data */

/** Skeleton frame edge warnings */
#define EM_WARNING_SKELETON_FRAME_EDGE_CLIPPED_RIGHT	MAKE_EM_WARNING(EM_WARNING_CATEGORY_SKELETON_FRAME_EDGE, 1)	 /**< User right side maybe clipped */
#define EM_WARNING_SKELETON_FRAME_EDGE_CLIPPED_LEFT	    MAKE_EM_WARNING(EM_WARNING_CATEGORY_SKELETON_FRAME_EDGE, 2)	 /**< User left edge side clipped */
#define EM_WARNING_SKELETON_FRAME_EDGE_CLIPPED_TOP		MAKE_EM_WARNING(EM_WARNING_CATEGORY_SKELETON_FRAME_EDGE, 4)	 /**< User head maybe clipped */
#define EM_WARNING_SKELETON_FRAME_EDGE_CLIPPED_BOTTOM	MAKE_EM_WARNING(EM_WARNING_CATEGORY_SKELETON_FRAME_EDGE, 8)	 /**< User feet maybe clipped */
#define EM_WARNING_SKELETON_FRAME_EDGE_CLIPPED_NEAR	    MAKE_EM_WARNING(EM_WARNING_CATEGORY_SKELETON_FRAME_EDGE, 16) /**< User too close */
#define EM_WARNING_SKELETON_FRAME_EDGE_CLIPPED_FAR      MAKE_EM_WARNING(EM_WARNING_CATEGORY_SKELETON_FRAME_EDGE, 32) /**< User too far */

/** Raw image warnings */
#define EM_WARNING_RAW_IMAGE_LIGHT_LOW				    MAKE_EM_WARNING(EM_WARNING_CATEGORY_RAW_IMAGE, 1) /**< Quality may be hindered due to low lighting conditions */
#define EM_WARNING_RAW_IMAGE_STRONG_BACKLIGHTING	    MAKE_EM_WARNING(EM_WARNING_CATEGORY_RAW_IMAGE, 2) /**< Quality may be hindered due to strong backlighting */
#define EM_WARNING_RAW_IMAGE_TOO_MANY_PEOPLE		    MAKE_EM_WARNING(EM_WARNING_CATEGORY_RAW_IMAGE, 4) /**< Quality may be hindered as too many people occupy the measured space */

/** Warning data */
typedef struct _SWarningsFrame
{
    SFrameHeader	header;			/**< Common frame data */
    int				numOfWarnings;
    EmWarning*		warnings;
}SWarningsFrame;

const SWarningsFrame S_WARNING_FRAME_INITIAL_VALUE = /**< Warning data initial value */
{
    S_FRAME_HEADER_INITIAL_VALUE,	/* header			*/
    0,								/* numOfWarnings	*/
    NULL							/* warnings			*/
}; 

#endif  // _EM_WARNING_H_
