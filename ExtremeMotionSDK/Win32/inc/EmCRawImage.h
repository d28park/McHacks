/****************************************************************************
*																			*
*	  EmCRawImage.h -- This module defines the types used by the			*
*	                Extreme Motion raw image stream API.					*
*	  																		*
*	  Copyright (c) Extreme Reality Ltd. All rights reserved.				*
*																			*
*****************************************************************************/
#ifndef _EM_C_RAW_IMAGE_H_
#define _EM_C_RAW_IMAGE_H_

#include "EmPlatform.h"
#include "EmCTypes.h"
#include "EmCWarning.h"

#define EM_DEFAULT_IMAGE_WIDTH			  640
#define EM_DEFAULT_IMAGE_HEIGHT			  480 
#define EM_DEFAULT_IMAGE_STRIDE           (EM_DEFAULT_IMAGE_WIDTH * EM_DEFAULT_IMAGE_BITCOUNT / 8)
#define EM_DEFAULT_IMAGE_SIZE             (EM_DEFAULT_IMAGE_HEIGHT * EM_DEFAULT_IMAGE_STRIDE)

/* Image Descriptor Data */
typedef struct _SImageDescriptor
{
	unsigned int	width;
	unsigned int	height;
	int				stride;
	unsigned short  bitCount;
	unsigned int	fccFormat;
	unsigned int	sizeImage;
} SImageDescriptor;

const SImageDescriptor S_IMAGE_DESCRIPTOR_INITIAL_VALUE = /**< image descriptor data initial value */
{
	EM_DEFAULT_IMAGE_WIDTH,		      /* width			*/	
	EM_DEFAULT_IMAGE_HEIGHT,	      /* height			*/	
	EM_DEFAULT_IMAGE_STRIDE,	      /* stride			*/	
	EM_DEFAULT_IMAGE_BITCOUNT,	      /* bit count		*/	
	EM_DEFAULT_IMAGE_FORMAT,	      /* fccFormat		*/	
	EM_DEFAULT_IMAGE_SIZE		      /* sizeImage		*/	
};

/** Raw camera image data */
typedef struct _SRawImageFrame
{
	SFrameHeader	   header;			/**< Common frame data */
	SImageDescriptor   imageDescriptor;
	unsigned char*	   imageData;
}SRawImageFrame;

const SRawImageFrame S_RAW_IMAGE_FRAME_INITIAL_VALUE = /**< Raw camera image data initial value */
{
	S_FRAME_HEADER_INITIAL_VALUE,	  /* header				*/	
	S_IMAGE_DESCRIPTOR_INITIAL_VALUE, /* imageDescriptor	*/
	NULL							  /* imageData			*/	
};


#endif  // _EM_C_RAW_IMAGE_H_
