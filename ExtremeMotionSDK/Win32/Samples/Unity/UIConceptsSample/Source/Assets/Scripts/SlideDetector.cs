using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xtr3D.Net.ExtremeMotion.Data;

public class SlideDetector
{
	private const int INVALID_VALUE = -999;
    private const int HISTORY_LEN = 30; //40
    private const int NUM_FRAMES_FOR_PALM_STABILITY = 5;
    private const float SLIDE_HISTORY_LEN = (HISTORY_LEN - NUM_FRAMES_FOR_PALM_STABILITY);
    private const float MAX_VERTICAL_PROP = 0.3f;//0.7f   // Max proportion of vertical movement (out of the total X-movement) ***with respect to the average height***.
    private const float MAX_ALLOWED_MOVEMENT_OPPOSITE_DIRECTION = 0.05f;	// in arm length units
    private const float MAX_ALLOWED_CONSECUTIVE_VERTICAL_MOVEMENT   = 0.2f;
    private const float MIN_HORIZONTAL_MOVEMENT_FOR_SLIDE = 0.6f; //0.35	// in arm length units. !!!!!! Consider using 0.6 if the slide is not in the hips area !!!!!!
    private const int   MIN_NUM_FRAMES_MOVEMENT                     = 3;
    private const float   MAX_OUTLIERS_RATIO                        = 0.25f;
    private const float MAX_ALLOWED_PALM_MOVE_DURING_STABLE = 0.03f; // in arm length units
 
    private const float MAX_ALLOWED_DEPTH_DIFF                      = 0.25f; 

    private Point[] m_palmHistory;
    private int m_historyInd;
    private float[] m_deltaHistoryX;

    private float[] m_proximityHistory;

    private float euclidDist(Point a, Point b)
    {
        return (float)(Math.Sqrt(Math.Pow((a.Y - b.Y), 2) + Math.Pow((a.X - b.X), 2)));
    }

        public void Init(float skelProximity)
        {
            m_palmHistory = new Point[HISTORY_LEN];
            m_deltaHistoryX = new float[HISTORY_LEN];
            m_proximityHistory = new float[HISTORY_LEN];
            clearSlideHistory();
        }

        public void clearSlideHistory()
        {
            for (int i = 0; i < HISTORY_LEN; i++)
            {
                m_palmHistory[i].Y = INVALID_VALUE; 
                m_palmHistory[i].X = INVALID_VALUE; 
                m_deltaHistoryX[i] = 0.0f;
                m_proximityHistory[i] = -1.0f;
            }
            m_historyInd = 0;
        }

        public bool IsSlide(JointCollection joints, float lastSkeletonProximity, ref Point palmRstartSlide)
        {
            Point palmR = joints.HandRight.skeletonPoint;
          
            // Update history
            m_palmHistory[m_historyInd % HISTORY_LEN] = palmR;
            m_deltaHistoryX[m_historyInd % HISTORY_LEN] = palmR.X - m_palmHistory[(m_historyInd - 1 + HISTORY_LEN) % HISTORY_LEN].X;
            m_proximityHistory[m_historyInd % HISTORY_LEN] = lastSkeletonProximity;
            m_historyInd++;

            int lastInd = (m_historyInd - 1 + HISTORY_LEN) % HISTORY_LEN;
            float lastX = m_palmHistory[lastInd].X;
            float lastY = m_palmHistory[lastInd].Y;
            float cumDiffX = 0;
            int numOutlier = 0;
            float cumY = lastY;
            int numMovementFrames = 1;
            for (int i = 1; i < SLIDE_HISTORY_LEN; i++)
            {

                int currInd = (m_historyInd - 1 - i + HISTORY_LEN) % HISTORY_LEN;
                float currdiffX = m_deltaHistoryX[currInd];
                float currdiffX2 = m_deltaHistoryX[(currInd - 1 + HISTORY_LEN) % HISTORY_LEN];
                float currdiffY = Math.Abs(m_palmHistory[(m_historyInd - 1 - i + HISTORY_LEN) % HISTORY_LEN].Y - m_palmHistory[(m_historyInd - 2 - i + HISTORY_LEN) % HISTORY_LEN].Y);
                float currdiffY2 = Math.Abs(m_palmHistory[(m_historyInd - 3 - i + HISTORY_LEN) % HISTORY_LEN].Y - m_palmHistory[(m_historyInd - 2 - i + HISTORY_LEN) % HISTORY_LEN].Y);

                if ((currdiffX > MAX_ALLOWED_MOVEMENT_OPPOSITE_DIRECTION && currdiffX2 < MAX_ALLOWED_MOVEMENT_OPPOSITE_DIRECTION) ||
                    (currdiffY > MAX_ALLOWED_CONSECUTIVE_VERTICAL_MOVEMENT && currdiffY2 < MAX_ALLOWED_CONSECUTIVE_VERTICAL_MOVEMENT))
                {
                    numOutlier++;
                    continue;
                }

                cumY = cumY + m_palmHistory[currInd].Y;
                numMovementFrames++;
            
            cumDiffX = m_palmHistory[currInd].X - lastX;

            if (cumDiffX > MIN_HORIZONTAL_MOVEMENT_FOR_SLIDE && numOutlier <= MAX_OUTLIERS_RATIO*numMovementFrames && numMovementFrames >= MIN_NUM_FRAMES_MOVEMENT)
            {
                // Check palm stability from currInd (and backwards)
                Point startPalmStability = m_palmHistory[currInd];
                bool isPalmStillStable = true;
                int indStartPalmStability = i + 1;
                for (int j = i + 1; j < i + NUM_FRAMES_FOR_PALM_STABILITY; j++)
                {
                    int currIndPalmStabilityCheck = (m_historyInd - 1 - j + HISTORY_LEN) % HISTORY_LEN;
                    if (euclidDist(startPalmStability, m_palmHistory[currIndPalmStabilityCheck]) > MAX_ALLOWED_PALM_MOVE_DURING_STABLE)
                    {
                        isPalmStillStable = false;
                        break;
                    }
                    indStartPalmStability = j;
                }
                if (!isPalmStillStable)
                    continue;

                // Check outliers in Y (if we got here then palm was stable at currInd)
                float aveY = (float)cumY / numMovementFrames;
                int numOutlierY = 0;
                for (int j = 0; j <= i; j++)
                {
                    int currIndYStabilityCheck = (m_historyInd - 1 - j + HISTORY_LEN) % HISTORY_LEN;
                    float currY = m_palmHistory[currIndYStabilityCheck].Y;
                    if (Math.Abs(currY - aveY) > MAX_VERTICAL_PROP * cumDiffX)
                        numOutlierY++;
                }
                if (numOutlierY > MAX_OUTLIERS_RATIO*numMovementFrames)
                    return false;

                // Check if head is stable
                for (int j = 0; j <= indStartPalmStability; j++)
                {
                    int currIndHeadStability = (m_historyInd - 1 - j + HISTORY_LEN) % HISTORY_LEN;
                    float currProximityDiff = Math.Abs(m_proximityHistory[currIndHeadStability] - lastSkeletonProximity);
                    //Checking if user moved towards or away from the camera.
                    if (currProximityDiff / lastSkeletonProximity > MAX_ALLOWED_DEPTH_DIFF)
                        return false;
                }
				int currIndPalmRstartSlide = (m_historyInd - 1 - indStartPalmStability + HISTORY_LEN) % HISTORY_LEN;
                palmRstartSlide = m_palmHistory[currIndPalmRstartSlide];
                clearSlideHistory();
                return true;
            }
        }
        return false;
    }
}