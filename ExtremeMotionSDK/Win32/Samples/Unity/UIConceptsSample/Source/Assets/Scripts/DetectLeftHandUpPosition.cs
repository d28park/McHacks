using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xtr3D.Net.ExtremeMotion.Data;
using UnityEngine;

public class DetectLeftHandUpPosition
{
    private const int HISTORY_LEN = 10;
    private const float MIN_RATIO_DETECTION = 0.6f;
    private const float MIN_PRECENT_OF_ARM_LENGTH = 0.75f;
    private const float ANGLE_TOLERANCE_ELBOW_PALM_UP = 1.73f;         // tan(240)=1.73; 
    private const float ANGLE_TOLERANCE_ELBOW_PALM_DOWN = -1.73f;       // tan(120)=-1.73;
    private const float ANGLE_TOLERANCE_SHOULDER_ELBOW_UP = 100f;     // cotan(0.01)=100; 
    private const float ANGLE_TOLERANCE_SHOULDER_ELBOW_DOWN = -0.58f;   // cotan(120)=-0.58;  

    private bool[] m_detectionHistory;
    private int historyInd = 0;

    private float euclidDist(Point a, Point b)
    {
        return (float)(Math.Sqrt(Math.Pow((a.Y - b.Y), 2) + Math.Pow((a.X - b.X), 2)));
    }

    public void Init()
    {
        m_detectionHistory = new bool[HISTORY_LEN];
        for (int i = 0; i < HISTORY_LEN; i++)
            m_detectionHistory[i] = false;
    }

    public bool IsLeftHandUpPoision(JointCollection joints, long frameID)
    {
        Point shoulderL = joints.ShoulderLeft.skeletonPoint;
        Point elbowL = joints.ElbowLeft.skeletonPoint;
        Point palmL = joints.HandLeft.skeletonPoint;

        float fullArmLength = euclidDist(shoulderL, palmL);
        bool isLeftArmStraight = fullArmLength > MIN_PRECENT_OF_ARM_LENGTH;

        float elbowPalmYDiff = elbowL.Y - palmL.Y;
        float elbowPalmXDiff = elbowL.X - palmL.X;
        float shoulderElbowYDiff = shoulderL.Y - elbowL.Y;
        float shoulderElbowXDiff = shoulderL.X - elbowL.X;
        bool isArmUp = elbowPalmYDiff < 0;
		
        float tanAngleElbowPalm = elbowPalmXDiff / elbowPalmYDiff;
        float cotanAngleShoulderElbow = shoulderElbowYDiff / shoulderElbowXDiff;
		
        bool isAngle240DegElbowPalmUp = tanAngleElbowPalm < ANGLE_TOLERANCE_ELBOW_PALM_UP;
        bool isAngle120DegElbowPalmDown = tanAngleElbowPalm > ANGLE_TOLERANCE_ELBOW_PALM_DOWN;
        bool isAngle0DegShoulderElbowUp = cotanAngleShoulderElbow < ANGLE_TOLERANCE_SHOULDER_ELBOW_UP;
        bool isAngle120DegShoulderElbowDown = cotanAngleShoulderElbow > ANGLE_TOLERANCE_SHOULDER_ELBOW_DOWN;
		
        bool res = (!isLeftArmStraight && isAngle240DegElbowPalmUp && isAngle120DegElbowPalmDown && isAngle0DegShoulderElbowUp && isAngle120DegShoulderElbowDown && isArmUp);

        m_detectionHistory[historyInd%HISTORY_LEN] = res;
        historyInd++;
        int numTrueHist = 0;
        for (int i = 0; i < HISTORY_LEN; i++)
        {
            if (m_detectionHistory[i] == true)
                numTrueHist++;
        }
        bool isLeftHandUp = (numTrueHist >= MIN_RATIO_DETECTION * HISTORY_LEN);
        return isLeftHandUp;
    }                 
}