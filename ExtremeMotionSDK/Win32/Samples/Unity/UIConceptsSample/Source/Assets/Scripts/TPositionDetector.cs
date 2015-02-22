using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xtr3D.Net.ExtremeMotion.Data;

public class TPositionDetector
{
	private const float MAX_DIFF_IN_Y_BETWEEN_PALM_TO_ORIGIN_ARM_LENGTH  = 0.3f;
	private const float MAX_DIFF_IN_Y_BETWEEN_ELBOW_TO_ORIGIN_IN_ARM_LENGTH = 0.3f;
	private const float MAX_DIFF_IN_Y_BETWEEN_HANDS_IN_ARM_LENGTH = 0.2f;
	private const float MIN_PRECENT_OF_ARM_LENGTH = 0.50f;

	public bool IsTPosition(JointCollection joints)
	{
		// checks if right arm is horizontal
		bool rightArmStraight = (Math.Abs(joints.HandRight.skeletonPoint.Y ) < MAX_DIFF_IN_Y_BETWEEN_PALM_TO_ORIGIN_ARM_LENGTH
							 && (Math.Abs(joints.ElbowRight.skeletonPoint.Y) < MAX_DIFF_IN_Y_BETWEEN_ELBOW_TO_ORIGIN_IN_ARM_LENGTH));
		// checks if left arm is horizontal
		bool leftArmStraight = (Math.Abs(joints.HandLeft.skeletonPoint.Y   ) < MAX_DIFF_IN_Y_BETWEEN_PALM_TO_ORIGIN_ARM_LENGTH
							 && (Math.Abs(joints.ElbowLeft.skeletonPoint.Y ) < MAX_DIFF_IN_Y_BETWEEN_ELBOW_TO_ORIGIN_IN_ARM_LENGTH));
		// checks if both arms are at the same angle
		bool armsAtSameLevel = (Math.Abs(joints.HandLeft.skeletonPoint.Y - joints.HandRight.skeletonPoint.Y) < MAX_DIFF_IN_Y_BETWEEN_HANDS_IN_ARM_LENGTH);
		// checks if arms are in the same plane as the body (e.g arms are not bent)
		bool longArmL = Math.Abs(joints.HandLeft.skeletonPoint.X  - joints.ShoulderLeft.skeletonPoint.X)  > MIN_PRECENT_OF_ARM_LENGTH;
		bool longArmR = Math.Abs(joints.HandRight.skeletonPoint.X - joints.ShoulderRight.skeletonPoint.X) > MIN_PRECENT_OF_ARM_LENGTH;

		if (!rightArmStraight || !leftArmStraight || !longArmL || !longArmR || !armsAtSameLevel)
		{
			return false;
		}
		else
		{
			return true;
		}
	}    
}

