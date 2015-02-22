using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xtr3D.Net.ExtremeMotion.Data;

public class SwipeDetector
{
    private const int INVALID_VALUE = -9999;
    private const int HISTORY_LEN = 30;
    private const float MAX_ALLOWED_MOVEMENT_OPPOSITE_DIRECTION     = 0.05f;	// in arm length units
    private const float  MAX_ALLOWED_CONSECUTIVE_VERTICAL_MOVEMENT  = 0.2f;
    private const float  MIN_X_MOVEMENT							    = 0.8f;	// in arm length units
    private const int    MIN_NUM_FRAMES_MOVEMENT					= 3;
    private const float  MIN_RIGHT_MOST_POS							= 0.75f;
    private const float  MAX_LEFT_MOST_POS							= 0.2f;
    private const float  MAX_ALLOWED_VERTICAL_DIST_FULL_MOTION		= 0.8f;
    private const float  MAX_OUTLIERS_RATIO                         = 0.25f;
    private const long   MIN_TIME_BETWEEN_OPPOSITE_SWIPES           = 1000;
    
    public enum SwipeType { NO_SWIPE = 0, SWIPED_LEFT = -1, SWIPED_RIGHT = 1 };

    private Point[] m_palmHistory;
    private int m_historyInd;
    private float[] m_deltaHistoryX;
    private float[] m_proximityHistory;
    private int m_lastSwipeType;
    private long m_lastSwipeTime;
	private SwipeType m_lastSwipeStatus;
    private long[] m_timeStampHistory;

    private float euclidDist(Point a, Point b)
    {
        return (float)(Math.Sqrt(Math.Pow((a.Y - b.Y), 2) + Math.Pow((a.X - b.X), 2)));
    }

    public void Init(float skelProximity)
    {
        m_palmHistory = new Point[HISTORY_LEN];
        m_deltaHistoryX = new float[HISTORY_LEN];
        m_proximityHistory = new float[HISTORY_LEN];
        m_timeStampHistory = new long[HISTORY_LEN];
        m_lastSwipeType = (int)SwipeType.NO_SWIPE;
        m_lastSwipeTime = INVALID_VALUE;
        for (int i = 0; i < HISTORY_LEN; i++)
        {
            m_timeStampHistory[i] = INVALID_VALUE;
        }
        clearSwipeHistory();
    }

    public void clearSwipeHistory()
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
	
	public SwipeType GetLastSwipeStatus()
	{
		return m_lastSwipeStatus;
	}
	
	/// <summary>
	/// Determines whether a left\right swipe is detected
	/// </summary>
	/// <returns>
	/// <c>int</c> see Enum - SwipeType
	/// </returns>
	/// <param name='joints'>
	/// Joints.
	/// </param>
	/// <param name='lastSkeletonProximity'>
	/// Last skeleton proximity.
	/// </param>
	/// <param name='timeStamp'>
	/// Time stamp.
	/// </param>
    public int IsSwipe(JointCollection joints, float lastSkeletonProximity, long timeStamp)
    {
        Point palmR = joints.HandRight.skeletonPoint;

        // Update history
        m_palmHistory[m_historyInd % HISTORY_LEN] = palmR;
        m_deltaHistoryX[m_historyInd % HISTORY_LEN] = palmR.X - m_palmHistory[(m_historyInd - 1 + HISTORY_LEN) % HISTORY_LEN].X;
        m_timeStampHistory[m_historyInd % HISTORY_LEN] = timeStamp;
        m_proximityHistory[m_historyInd % HISTORY_LEN] = lastSkeletonProximity;
        m_historyInd++;

        int lastInd = (m_historyInd - 1 + HISTORY_LEN) % HISTORY_LEN;
        float lastX = m_palmHistory[lastInd].X;
        float lastY = m_palmHistory[lastInd].Y;
        float cumDiffX = 0;
        int numOutlierLeft = 0;
        int numOutlierRight = 0;
        int numMovementFramesLeft = 1;
        int numMovementFramesRight = 1;
        for (int i = 1; i < HISTORY_LEN; i++)
        {
            int currInd = (m_historyInd - 1 - i + HISTORY_LEN) % HISTORY_LEN;
            float currdiffX = m_deltaHistoryX[currInd];
            float currdiffX2 = m_deltaHistoryX[(currInd - 1 + HISTORY_LEN) % HISTORY_LEN];
            float currdiffY = Math.Abs(m_palmHistory[(m_historyInd - 1 - i + 2 * HISTORY_LEN) % HISTORY_LEN].Y - m_palmHistory[(m_historyInd - 2 - i + 2 * HISTORY_LEN) % HISTORY_LEN].Y);
            float currdiffY2 = Math.Abs(m_palmHistory[(m_historyInd - 3 - i + 2 * HISTORY_LEN) % HISTORY_LEN].Y - m_palmHistory[(m_historyInd - 2 - i + 2 * HISTORY_LEN) % HISTORY_LEN].Y);
            if (m_palmHistory[(m_historyInd - 1 - i + 2 * HISTORY_LEN) % HISTORY_LEN].Y == INVALID_VALUE || m_palmHistory[(m_historyInd - 2 - i + 2 * HISTORY_LEN) % HISTORY_LEN].Y == INVALID_VALUE ||
                m_palmHistory[(m_historyInd - 3 - i + 2 * HISTORY_LEN) % HISTORY_LEN].Y == INVALID_VALUE)
            {
                continue;
            }
            bool isOutlierLeftFrame = false;
            bool isOutlierRightFrame = false;
            if (currdiffY > MAX_ALLOWED_CONSECUTIVE_VERTICAL_MOVEMENT && currdiffY2 < MAX_ALLOWED_CONSECUTIVE_VERTICAL_MOVEMENT)
            {
                numOutlierLeft++;
                numOutlierRight++;
                isOutlierLeftFrame = true;
                isOutlierRightFrame = true;
                continue;
            }

            if ((currdiffX > MAX_ALLOWED_MOVEMENT_OPPOSITE_DIRECTION && currdiffX2 < MAX_ALLOWED_MOVEMENT_OPPOSITE_DIRECTION))
            {
                numOutlierLeft++;
                isOutlierLeftFrame = true;
            }
            else
                numMovementFramesLeft++;
            if ((currdiffX < -MAX_ALLOWED_MOVEMENT_OPPOSITE_DIRECTION && currdiffX2 > -MAX_ALLOWED_MOVEMENT_OPPOSITE_DIRECTION))
            {
                numOutlierRight++;
                isOutlierRightFrame = true;
            }
            else
                numMovementFramesRight++;

            cumDiffX = m_palmHistory[currInd].X - lastX;
            float diffY = Math.Abs(m_palmHistory[currInd].Y - lastY);
            float slope = Math.Abs(diffY / cumDiffX);
            int swipeStatus = (int)SwipeType.NO_SWIPE;
            if (cumDiffX > MIN_X_MOVEMENT && numOutlierLeft <= MAX_OUTLIERS_RATIO * numMovementFramesLeft && numMovementFramesLeft >= MIN_NUM_FRAMES_MOVEMENT &&
                slope < MAX_ALLOWED_VERTICAL_DIST_FULL_MOTION && lastX < MAX_LEFT_MOST_POS && m_palmHistory[currInd].X > MIN_RIGHT_MOST_POS && !isOutlierLeftFrame)
            {
                swipeStatus = (int)SwipeType.SWIPED_LEFT;
            }
            if (cumDiffX < -MIN_X_MOVEMENT && numOutlierRight <= MAX_OUTLIERS_RATIO * numMovementFramesRight && numMovementFramesRight >= MIN_NUM_FRAMES_MOVEMENT &&
                slope < MAX_ALLOWED_VERTICAL_DIST_FULL_MOTION && lastX > MIN_RIGHT_MOST_POS && m_palmHistory[currInd].X < MAX_LEFT_MOST_POS && !isOutlierRightFrame)
            {
                swipeStatus = (int)SwipeType.SWIPED_RIGHT;
            }
            if (swipeStatus != (int)SwipeType.NO_SWIPE)
            {
                bool swipeOK = true;
                // Check that there wasn't a swipe in the opposite direction a short time ago
                if (m_lastSwipeType != swipeStatus)
                {
                    if (!IsBlockingTimeFinished(m_timeStampHistory[currInd]))
                        swipeOK = false;
					else
						m_lastSwipeStatus = SwipeType.NO_SWIPE;
                }
				else
				{
					if(IsBlockingTimeFinished(m_timeStampHistory[currInd]))
						m_lastSwipeStatus = SwipeType.NO_SWIPE;
				}
                if (swipeOK)
                {
                    clearSwipeHistory();
					m_lastSwipeStatus = (SwipeType) swipeStatus;
                    m_lastSwipeType = swipeStatus;
                    m_lastSwipeTime = timeStamp;
                    return swipeStatus;
                }
            }
			else
			{
				if(m_lastSwipeStatus!= SwipeType.NO_SWIPE && IsBlockingTimeFinished(m_timeStampHistory[currInd]))
					m_lastSwipeStatus = SwipeType.NO_SWIPE;
			}
        }

        return (int)SwipeType.NO_SWIPE;
    }
	
	private bool IsBlockingTimeFinished(long currentTimeStamp)
	{
		if ((currentTimeStamp - m_lastSwipeTime) < MIN_TIME_BETWEEN_OPPOSITE_SWIPES)
			return false;
		
		return true;
	}
}




