/****************************************************************************
*																			*
*	  EmCUtils.h -- This module defines various C macros and helpers 		*
*	                for the Extreme Motion API.								*
*	  																		*
*	  Copyright (c) Extreme Reality Ltd. All rights reserved.				*
*																			*
*****************************************************************************/
#ifndef _EM_C_UTILITIES_H_
#define _EM_C_UTILITIES_H_

#include "EmCTypes.h"

/*********************************************
*				Bit operations				 *
**********************************************/
/** Determines whether one or more bit fields are set in the given value */
#define IS_FLAG_SET(val, flag)  ((val & flag) == flag)

/** Determines the number of set bits in a given 32-bit value */
inline unsigned int countSetBits(unsigned int i)
{	
	i = i - ((i >> 1) & 0x55555555);
	i = (i & 0x33333333) + ((i >> 2) & 0x33333333);
	return (((i + (i >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
} /* variable-precision SWAR algorithm */

#endif  // _EM_C_UTILITIES_H_
