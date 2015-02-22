using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xtr3D.Net.ExtremeMotion.Data;

class HoveringTranslator
{
    // configurable parameters
    private const float ROI_WIDTH_IN_ARM_UNITS                                  = 0.5f;  // The ROI width measured in number of arms units
    private const float ROI_HEIGHT_IN_ARM_UNITS                                 = 0.7f;   // The ROI height measured in number of arms units
    private const float ROI_TOP_LEFT_DISTANCE_X_FROM_SHOULDER_IN_ARM_UNITS      = 0.05f;  // The ROI horizontal distance from the right shoulder measured in number of arms units
    private const float ROI_TOP_LEFT_DISTANCE_Y_FROM_SHOULDER_IN_ARM_UNITS      = 0.8f;   // The ROI vertical distance from the right shoulder measured in number of arms units
    private const float OUTSIDE_SCREEN_DIST_THRESHOLD                           = 0.25f;  // Max distance of the palm from the bottom of the ROI that the output coordinates are still valid

    private const int   SMOOTH_DELAY                                            = 10;      // Number of frames delayed for smoothing the output coordinates

    private const float SHOULDER_DIFF_STATIC_THRESHOLD                          = 0.3f;   // Used to stabilize the shoulder location to get a stable ROI. Defines the minimal movement distance of shoulder from its current location
    private const float PROXIMITY_DIFF_STATIC_THRESHOLD                         = 0.3f;   // Used to change the arm length if the current proximity exceeds the last stable proximity by this threshold
    private const float SHOULDER_UPDATE_ALPHA                                   = 0.35f;  // Defines the coefficient for the linear interpolation between the previous shoulder and the current


    // General
    private const float INVALID_POINT_VAL = -1.0f;            // Invalid value (initialization value)
    
    // CheckIfLongerArm
    private const int HISTORY_SIZE_LONG_ARM_LEN = 10;         // The buffer size that keeps the informations of frames with longer arm
    private const int NUM_FRAMES_TO_DETECT_LONGER_ARM = 100;  // Maximal history length for which average arm length is computed
    private int       m_frameCounter;                         // Counter of frame number
    private float[]   m_longArmLenHist;                       // Arm length history array
    private float[]   m_longArmLenProximityHist;              // Proximity history array
    private int[]     m_longArmLenFramesHist;                 // Frames counter array
    private int       m_bufferLongArmLenInd;                  // Buffer index (cyclic)

    // StableShoulder
    private Point m_shoulderCurrPoint;                        // Current shoulder point
    private Point m_shoulderPrevPoint;                        // Previous shoulder point
    private float m_initialArmLength_Norm;                    // Initialized arm length
    private float m_currArmLength_Norm;                       // Current arm length
    private float m_initialProximty;                          // Initialized proximity
    private float m_prevProximty;                             // Previous proximity
    private float m_movementThresh;                           // Used to stabilize the shoulder location to get a stable ROI. Defines the minimal movement distance of shoulder from its current location
    private float m_proximityThreshold;                       // Used to change the arm length if the current proximity exceeds the last stable proximity by this threshold
    private float m_alpha;                                    // Defines the coefficient for the linear interpolation between the previous shoulder and the current
    private bool  m_isShoulderStatic;                         // Indicates if the shoulder is static

    // SetROI
    private float m_ROIwidthInArmUnits;                       // The ROI width measured in number of arms units
    private float m_ROIheightInArmUnits;                      // The ROI height measured in number of arms units   
    private float m_ROIdistFromShouldXArmUnits;               // The ROI horizontal distance from the right shoulder measured in number of arms units     
    private float m_ROIdistFromShouldYArmUnits;               // The ROI vertical distance from the right shoulder measured in number of arms units
    private float m_leftMostROI_Norm;                         // Upper val of the ROI    
    private float m_upperMostROI_Norm;                        // Left most val of the ROI
    private float m_ROIwidth_Norm;                            // The ROI width           
    private float m_ROIheight_Norm;                           // The ROI height          
    private bool  m_outsideScreen;                            // Max distance of the palm from the bottom of the ROI that the output coordinates are still valid
    private int   m_outsideScreenCounter;                     // Counts the number of frames in which the palm is outside the ROI
                                                                               
    // SmoothOutput
    private int   m_smoothDelay;                              // Number of frames delayed for smoothing the output coordinates
    private Point m_prevPalm;                                 // The output of the palm from the previous frame (after performing delay)
    private Point m_prevAlgPalm;                              // The palm from the previous frame without delay
    private int   m_identicalPalmLoactionCounter;             // Counts the number of frames that the palm is static

    public HoveringTranslator()
    {
        m_prevPalm.ImgCoordNormHorizontal = INVALID_POINT_VAL;
        m_prevPalm.ImgCoordNormVertical = INVALID_POINT_VAL;
        m_prevAlgPalm.ImgCoordNormHorizontal = INVALID_POINT_VAL;
        m_prevAlgPalm.ImgCoordNormVertical = INVALID_POINT_VAL;
        m_identicalPalmLoactionCounter = 0;
        m_outsideScreenCounter = 0;
    }

    public void Init(float skeletonProximty, JointCollection joints)
    {

        LoadParameters();

        float shoulderX_Norm = joints.ShoulderRight.skeletonPoint.ImgCoordNormHorizontal;
        float shoulderY_Norm = joints.ShoulderRight.skeletonPoint.ImgCoordNormVertical;
		
		m_initialArmLength_Norm = CalcArmLengthFromJoints(joints);
		
        m_currArmLength_Norm = m_initialArmLength_Norm;
        m_initialProximty = skeletonProximty;
        m_prevProximty = skeletonProximty;
        
        m_shoulderCurrPoint.ImgCoordNormHorizontal = shoulderX_Norm;
        m_shoulderCurrPoint.ImgCoordNormVertical = shoulderY_Norm;
        m_shoulderPrevPoint.ImgCoordNormHorizontal = shoulderX_Norm;
        m_shoulderPrevPoint.ImgCoordNormVertical = shoulderY_Norm;

        m_isShoulderStatic = true;

        m_frameCounter = 0;
        m_bufferLongArmLenInd = 0;
        m_longArmLenHist = new float[HISTORY_SIZE_LONG_ARM_LEN];
        m_longArmLenProximityHist = new float[HISTORY_SIZE_LONG_ARM_LEN];
        m_longArmLenFramesHist = new int[HISTORY_SIZE_LONG_ARM_LEN];
        for (int i = 0; i < HISTORY_SIZE_LONG_ARM_LEN; ++i)
        {
            m_longArmLenHist[i] = m_initialArmLength_Norm;
            m_longArmLenProximityHist[i] = m_initialProximty;
            m_longArmLenFramesHist[i] = -1000;
        }
    }

    /// <summary>
    /// The function translates x (ImgCoordNormHorizontal) and y (ImgCoordNormVertical) coordinates of the skeleton to fit full screen resolution according to a ROI.
    /// The ROI is created according the users location and distance from the camera.
    /// </summary>
    /// <param name="x"> is passed as a refernece. 
    /// In value: float between 0 and 1 describing the location of the X axis of the palm in the RGB image.
    /// Out Value: float between 0 and 1 describing the location of the X axis of the palm full screen mode.</param>
    /// <param name="y">is passed as a refernce. 
    /// In value: float between 0 and 1 describing the location of the Y axis of the palm in the RGB image.
    /// Out Value: float between 0 and 1 describing the location of the Y axis of the palm full screen mode.</param>
    /// <param name="skeletonProximty">Indicates the distance from the camera. Received from the SDK.</param>
    /// <param name="joints">A JointCollection of a certain the skeleton</param>
    public void ImgNormXYCoordsToScreenXY(ref float x, ref float y, float skeletonProximty, JointCollection joints)
    {

        // Check if the length of the arm is longer that in calibration
        CheckIfLongerArm(joints, skeletonProximty);

        // Check For impossible positions (elbow above palm)
        OutlierCorrector(ref x, ref y, joints);

        // Update the arm (according to the proximity) and stable the shoulder
        StableShoulder(skeletonProximty, joints);

        // Set the ROI using the stable shoulder
        SetROI(m_shoulderCurrPoint.ImgCoordNormHorizontal, m_shoulderCurrPoint.ImgCoordNormVertical);

        // Change the palm location coordinates to fit the ROI
        NormalizeToROI(ref x, ref y);

        // Add a linear interpolation (moving 1/m_smoothDelay of the distance each frame)
        SmoothOutput(ref x, ref y);

        // If palm is located outside the bottom of the ROI then x and y are get INVALID_POINT_VAL values.
        InvalidateOutsideROIOutput(ref x, ref y);

        return;
    }

    /// <summary>
    /// set activation area position
    /// </summary>
    private void SetROI(float shoulderX_Norm, float shoulderY_Norm)
    {
        m_leftMostROI_Norm  = shoulderX_Norm + (m_ROIdistFromShouldXArmUnits * m_currArmLength_Norm); // leftmost point of ROI
        m_upperMostROI_Norm = shoulderY_Norm - (m_ROIdistFromShouldYArmUnits * m_currArmLength_Norm); // uppermost point of ROI
        m_ROIwidth_Norm     = m_currArmLength_Norm * m_ROIwidthInArmUnits;
        m_ROIheight_Norm    = m_currArmLength_Norm * m_ROIheightInArmUnits;
    }

    /// <summary>
    /// This function checks if the length of the arm is longer than in calibration and updates the actual size. 
    /// </summary>
    private void CheckIfLongerArm(JointCollection joints, float skeletonProximty)
    {
        // Increase the frame counter
        m_frameCounter++;
        // Compute the current arm length
        float tmpArmLen = CalcArmLengthFromJoints(joints);
        // Normalized the initial arm length (from calibration) to fit the current distance from camera 
        float initialArmLength_NormToProximity = m_initialArmLength_Norm  * (m_initialProximty / skeletonProximty);
        // Check if the current length is longer (but not too long to avoid outliers)
        if (tmpArmLen > initialArmLength_NormToProximity && tmpArmLen < 1.4 * initialArmLength_NormToProximity)
        {
            // Compute the new index in the cyclic array
            m_bufferLongArmLenInd = (m_bufferLongArmLenInd - 1 + HISTORY_SIZE_LONG_ARM_LEN) % HISTORY_SIZE_LONG_ARM_LEN;
            // Keep the relevant information of this frame in different arrays
            m_longArmLenHist[m_bufferLongArmLenInd] = tmpArmLen;
            m_longArmLenProximityHist[m_bufferLongArmLenInd] = skeletonProximty;
            m_longArmLenFramesHist[m_bufferLongArmLenInd] = m_frameCounter;
        }

        int numFramesInHistory = m_longArmLenFramesHist[m_bufferLongArmLenInd] - m_longArmLenFramesHist[(m_bufferLongArmLenInd - 1 + HISTORY_SIZE_LONG_ARM_LEN) % HISTORY_SIZE_LONG_ARM_LEN];
        // Only updates the arm length if not more than NUM_FRAMES_TO_DETECT_LONGER_ARM frames have passed since the first longer arm was detected.
        if (numFramesInHistory < NUM_FRAMES_TO_DETECT_LONGER_ARM && numFramesInHistory != 0)
        {
            // Average the information of the longer arms array
            float meanLen = 0.0f;
            float meanProximity = 0.0f;
            for (int i = 0; i < HISTORY_SIZE_LONG_ARM_LEN; i++)
            {
                meanLen += m_longArmLenHist[i];
                meanProximity += m_longArmLenProximityHist[i];
                m_longArmLenFramesHist[i] = -1000;
            }
            // Update the base information of the arm length and proximity
            m_initialArmLength_Norm = meanLen / HISTORY_SIZE_LONG_ARM_LEN;
            m_initialProximty = meanProximity / HISTORY_SIZE_LONG_ARM_LEN;
            m_longArmLenFramesHist[m_bufferLongArmLenInd] = -1000;

            m_currArmLength_Norm = m_initialArmLength_Norm;
        }
    }

    private float CalcArmLengthFromJoints(JointCollection joints)
    {
        return euclidDist(joints.ShoulderRight.skeletonPoint, joints.ElbowRight.skeletonPoint) + euclidDist(joints.ElbowRight.skeletonPoint, joints.HandRight.skeletonPoint);
    }
    
    /// <summary>
    /// This function updates the arm length if the proximity has significantly changed and stables the shoulder.
    /// </summary>
    private void StableShoulder(float skeletonProximty, JointCollection joints)
    {
        // Changed the arm length only if the difference in the proximity from calibration exceeded threshold
        if (Math.Abs(skeletonProximty - m_prevProximty) > m_proximityThreshold)
        {
            m_currArmLength_Norm = m_initialArmLength_Norm * (m_initialProximty / skeletonProximty);
            m_prevProximty = skeletonProximty;
        }            

        // The movement threshold is proportional to arm length
        float currMovementThresh = m_movementThresh * m_currArmLength_Norm;
		float distToPrevShoulder = euclidDist(joints.ShoulderRight.skeletonPoint, m_shoulderPrevPoint);
        // Update the right shoulder point like "hysteresis".  
        if (distToPrevShoulder > currMovementThresh || !m_isShoulderStatic)
        {
            m_isShoulderStatic = false;
            // If got here, than the shoulder is not static and therefore also small movement can move the shoulder 
            if (distToPrevShoulder < currMovementThresh / 10)
            {
                // Back to static mode. The shoulder keeps its value from previous frame 
                m_shoulderCurrPoint = m_shoulderPrevPoint;
                m_isShoulderStatic = true;
            }
            else
            {
                m_shoulderCurrPoint.ImgCoordNormHorizontal = m_shoulderPrevPoint.ImgCoordNormHorizontal * (1 - m_alpha) + joints.ShoulderRight.skeletonPoint.ImgCoordNormHorizontal * m_alpha;
                m_shoulderCurrPoint.ImgCoordNormVertical = m_shoulderPrevPoint.ImgCoordNormVertical * (1 - m_alpha) + joints.ShoulderRight.skeletonPoint.ImgCoordNormVertical * m_alpha;
            }
        }
        else
        {
            // The shoulder keeps its value from previous frame
            m_shoulderCurrPoint = m_shoulderPrevPoint;
        }
		m_shoulderPrevPoint = m_shoulderCurrPoint;
    }

    /// <summary>
    /// Check For impossible positions for example elbow above hand is short.
    /// </summary>
    private void OutlierCorrector(ref float x, ref float y, JointCollection joints )
    {
        // x and y are the palm joints
        if (y > joints.ElbowRight.skeletonPoint.ImgCoordNormVertical + 0.05 && CalcArmLengthFromJoints(joints) < m_initialArmLength_Norm*0.7)
        {
            x = joints.ElbowRight.skeletonPoint.ImgCoordNormHorizontal;
            y = joints.ElbowRight.skeletonPoint.ImgCoordNormVertical;
        }
    }

    
    /// <summary>
    /// Currently load parameters uses hard coded values. Optionally can change so the values would be loaded from a configuration file.
    /// </summary>
    private void LoadParameters()
    {
        m_smoothDelay                = SMOOTH_DELAY;
        m_ROIwidthInArmUnits         = ROI_WIDTH_IN_ARM_UNITS; // percentage from arm length
        m_ROIheightInArmUnits        = ROI_HEIGHT_IN_ARM_UNITS; // percentage from arm length
        m_ROIdistFromShouldXArmUnits = ROI_TOP_LEFT_DISTANCE_X_FROM_SHOULDER_IN_ARM_UNITS; // distance from shoulder to leftmost point of ROI
        m_ROIdistFromShouldYArmUnits = ROI_TOP_LEFT_DISTANCE_Y_FROM_SHOULDER_IN_ARM_UNITS; // distance from shoulder to uppermost point of ROI

        m_movementThresh     = SHOULDER_DIFF_STATIC_THRESHOLD;
        m_proximityThreshold = PROXIMITY_DIFF_STATIC_THRESHOLD;
        m_alpha              = SHOULDER_UPDATE_ALPHA;
      }
	/// <summary>
	/// returns the Euclidian distance between two points.
	/// </summary>
	/// <returns>
	/// The Euclidian distance.
	/// </returns>
	/// <param name='a'>
	/// Point A.
	/// </param>
	/// <param name='b'>
	/// Point B.
	/// </param>
    private float euclidDist(Point a, Point b)
    {
        return (float)(Math.Sqrt(Math.Pow(a.ImgCoordNormVertical-b.ImgCoordNormVertical,2)+Math.Pow(a.ImgCoordNormHorizontal-b.ImgCoordNormHorizontal,2)));
    }

    /// <summary>
    /// This function delays the output in order to generate a smoother output by preforming a linear interpolation (moving 1/m_smoothDelay of the distance each frame)
    /// </summary>
    private void SmoothOutput(ref float x, ref float y)
    {
        
        // Happens only in first frame. m_prevPalm (x and y) are initialized to INVALID_POINT_VAL
        if (m_prevPalm.ImgCoordNormHorizontal == INVALID_POINT_VAL)
        {
            m_prevPalm.ImgCoordNormHorizontal = x;
            m_prevPalm.ImgCoordNormVertical = y;
        }
        Point currAlgPalm;
        Point currDelta;
        currAlgPalm = new Point();
        currDelta = new Point(); //currDelta point defines delta vector from previous location.
        currAlgPalm.ImgCoordNormHorizontal = x;
        currAlgPalm.ImgCoordNormVertical = y;
        
        // Adding a linear interpolation. Moving 1/m_smoothDelay of the distance each frame.
        currDelta.ImgCoordNormHorizontal = (x - m_prevPalm.ImgCoordNormHorizontal) / m_smoothDelay;
        currDelta.ImgCoordNormVertical = (y - m_prevPalm.ImgCoordNormVertical) / m_smoothDelay;

        // If alg position is static for m_smoothDelay*3 frames then set it to the static location.
        if (m_prevAlgPalm.ImgCoordNormHorizontal == x && m_prevAlgPalm.ImgCoordNormVertical == y)
        {
            if (m_identicalPalmLoactionCounter > m_smoothDelay*3)
            {
                currDelta.ImgCoordNormHorizontal = (x - m_prevPalm.ImgCoordNormHorizontal);
                currDelta.ImgCoordNormVertical = (y - m_prevPalm.ImgCoordNormVertical);
                m_identicalPalmLoactionCounter = 0;
            }
            else
            {
                ++m_identicalPalmLoactionCounter;
            }
        }
        else
        {
            m_identicalPalmLoactionCounter = 0;
        }

        // Update the previous input
        m_prevAlgPalm.ImgCoordNormHorizontal = x;
        m_prevAlgPalm.ImgCoordNormVertical = y;

        // Sets ref x and ref y to last courser location + the smoothed delta.
        x = m_prevPalm.ImgCoordNormHorizontal + currDelta.ImgCoordNormHorizontal;
        y = m_prevPalm.ImgCoordNormVertical + currDelta.ImgCoordNormVertical;

        // Update the previous output
        m_prevPalm.ImgCoordNormHorizontal = x;
        m_prevPalm.ImgCoordNormVertical = y;

    }

    /// <summary>
    /// This function changes the palm location coordinates to fit the ROI
    /// </summary>
    private void NormalizeToROI(ref float x, ref float y)
    {
        // Reduce the ROI when approach the boundaries of the image
        const float marginFromEdges = 0.05f;
        // The ROI shouldn't reach the left and upper edges of the image.
        m_leftMostROI_Norm = Math.Max(m_leftMostROI_Norm, marginFromEdges);
        m_upperMostROI_Norm = Math.Max(m_upperMostROI_Norm, marginFromEdges);

        // Subtract (1 - marginFromEdges) - <m_leftMostROI_Norm | m_upperMostROI_Norm> so ROI won't reach the right and bottom edges of the image.
        m_ROIwidth_Norm =  Math.Max(Math.Min(m_ROIwidth_Norm,  (1 - marginFromEdges) - m_leftMostROI_Norm),  0);
        m_ROIheight_Norm = Math.Max(Math.Min(m_ROIheight_Norm, (1 - marginFromEdges) - m_upperMostROI_Norm), 0);

        // Compute the new palm values in the ROI
        // 0.001f to make sure we don't divide by zero.
        x = (x - Math.Max(m_leftMostROI_Norm,  0)) / Math.Max(m_ROIwidth_Norm, 0.001f);
        y = (y - Math.Max(m_upperMostROI_Norm, 0)) / Math.Max(m_ROIheight_Norm, 0.001f);

        //Checking if palm is outside the bottom of the ROI in more than OUTSIDE_SCREEN_DIST_THRESHOLD
        if ( y > 1 + OUTSIDE_SCREEN_DIST_THRESHOLD)
        {
            ++m_outsideScreenCounter;
            //Only if the palm is outside the valid area for a a while then the output is considered invalid.
            if (m_outsideScreenCounter > m_smoothDelay * 2)
            {
                m_outsideScreen = true;
            }
        }
        else
        {
            m_outsideScreenCounter = 0;
            m_outsideScreen = false;
        }
		#if UNITY_STANDALONE_WIN 
        System.Console.WriteLine(y);
		#endif

        // Add protection
        x = Math.Min(Math.Max(0, x), 1);
        y = Math.Min(Math.Max(0, y), 1);
    }

    private void InvalidateOutsideROIOutput(ref float x, ref float y)
    {
        if (m_outsideScreen == true)
        {
            x  = INVALID_POINT_VAL;
            y  = INVALID_POINT_VAL;
        }
    }
  
}