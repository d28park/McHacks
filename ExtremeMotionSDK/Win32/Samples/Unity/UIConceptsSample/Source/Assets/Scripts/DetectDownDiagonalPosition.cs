using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xtr3D.Net.ExtremeMotion.Data;

public class DetectDownDiagonalPosition
{
    private const int HISTORY_LEN = 10;
    private const float MIN_RATIO_DETECTION = 0.6f;
    private const float MIN_PRECENT_OF_ARM_LENGTH = 0.75f;
    private const float ANGLE_TOLERANCE_ELBOW_PALM_UP = 1.43f;         // tan(55)=1.43; 
    private const float ANGLE_TOLERANCE_ELBOW_PALM_DOWN = 0.57f;       // tan(30)=0.58;   tan(25)=0.47;
    private const float ANGLE_TOLERANCE_SHOULDER_ELBOW_UP = 1.19f;     // tan(50)=1.19; 
    private const float ANGLE_TOLERANCE_SHOULDER_ELBOW_DOWN = 0.36f;   // tan(20)=0.36;  

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

    public bool IsDownDiagonalPoision(JointCollection joints, long frameID)
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
        bool isArmDown = elbowPalmYDiff > 0;
        float tanAngleElbowPalm = elbowPalmXDiff / elbowPalmYDiff;
        float tanAngleShoulderElbow = shoulderElbowXDiff / shoulderElbowYDiff;
        bool isAngle45DegElbowPalmUp = tanAngleElbowPalm < ANGLE_TOLERANCE_ELBOW_PALM_UP;
        bool isAngle45DegElbowPalmDown = tanAngleElbowPalm > ANGLE_TOLERANCE_ELBOW_PALM_DOWN;
        bool isAngle45DegShoulderElbowUp = tanAngleShoulderElbow < ANGLE_TOLERANCE_SHOULDER_ELBOW_UP;
        bool isAngle45DegShoulderElbowDown = tanAngleShoulderElbow > ANGLE_TOLERANCE_SHOULDER_ELBOW_DOWN;

        bool res = (isLeftArmStraight && isAngle45DegElbowPalmUp && isAngle45DegElbowPalmDown && isAngle45DegShoulderElbowUp && isAngle45DegShoulderElbowDown && isArmDown);

        m_detectionHistory[historyInd%HISTORY_LEN] = res;
        historyInd++;
        int numTrueHist = 0;
        for (int i = 0; i < HISTORY_LEN; i++)
        {
            if (m_detectionHistory[i] == true)
                numTrueHist++;
        }
        bool isDownDiag = (numTrueHist >= MIN_RATIO_DETECTION * HISTORY_LEN);
        return isDownDiag;
    }                 
}