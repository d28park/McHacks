using System;
using System.Collections.Generic;
using System.Windows;
using Xtr3D.Net;
using Xtr3D.Net.BaseTypes;
using Xtr3D.Net.ColorImage;
using Xtr3D.Net.AllFrames;
using Xtr3D.Net.ExtremeMotion;
using Xtr3D.Net.ExtremeMotion.Data;
using Xtr3D.Net.ExtremeMotion.Interop.Types;
using Xtr3D.Net.ExtremeMotion.Gesture;

namespace CSharpVisualSkeletonSample
{
    class GestureMessage
    {
        public GestureMessage(String _text)
        {
            timeToLiveCounter = 0;
            text = _text;
        }
        public int timeToLiveCounter;
        public String text;
    };

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SkeletonDrawer m_skeletonDrawer;
        private ColorImageDrawer m_colorImageDrawer;
        private ImageInfo m_imageInfo;

        private Dictionary<BaseGesture.GestureType, int> gestureTypeToDelay = new Dictionary<BaseGesture.GestureType, int>() 
        { 
            {BaseGesture.GestureType.STATIC_POSITION, 1},
            {BaseGesture.GestureType.HEAD_POSITION, 1},
            {BaseGesture.GestureType.SWIPE, 30},
            {BaseGesture.GestureType.WINGS, 1},
            {BaseGesture.GestureType.SEQUENCE, 30},
            {BaseGesture.GestureType.UP, 15},
            {BaseGesture.GestureType.DOWN, 15},
        };


        private Dictionary<int, GestureMessage> gestureMessages = new Dictionary<int, GestureMessage> { };

        public MainWindow()
        {
            InitializeComponent();
            m_imageInfo = new ImageInfo(ImageResolution.Resolution640x480, Xtr3D.Net.ImageInfo.ImageFormat.RGB888);
            m_skeletonDrawer = new SkeletonDrawer(m_imageInfo);
            SkeletonDisplay.Source = m_skeletonDrawer.ImageSource;

            m_colorImageDrawer = new ColorImageDrawer(m_imageInfo);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Conditionally initialize the singleton instance
            GeneratorSingleton.Instance.Initialize(PlatformType.WINDOWS, m_imageInfo);
            GeneratorSingleton.Instance.SetGestureRecognitionFile("SamplePoses.xml");

            //Register to the AllFramesReady event with our event handler, which will synchronize between camera and skeleton and draw them on the screen.
            //In case displaying the skeleton is not needed (e.g. calibration stage), using only GeneratorSingleton.Instance.ColorImageFrameReady results in better performance.
            //For that matter, see MyAllFramesReadyEventHandler on how to easily allow switching between waiting for synchronized frames, 
            //coming from all streams (via GeneratorSingleton.Instance.AllFramesReady) 
            //and waiting separately for each stream (GeneratorSingleton.Instance.DataFrameReady and GeneratorSingleton.Instance.ColorImageFrameReady).
            GeneratorSingleton.Instance.AllFramesReady +=
                new EventHandler<AllFramesReadyEventArgs>(MyAllFramesReadyEventHandler);

            // Start event pumping
            GeneratorSingleton.Instance.Start();
            if (CSharpVisualSkeletonSample.Properties.Settings.Default.EnableSmoothing)
            {
                GeneratorSingleton.Instance.DataStream.Enable(new TransformSmoothParameters()
                {
                    Smoothing = CSharpVisualSkeletonSample.Properties.Settings.Default.SmoothingCoeff,
                    Correction = CSharpVisualSkeletonSample.Properties.Settings.Default.CorrectionCoeff,
                    OutlierRemovalSensitivity = CSharpVisualSkeletonSample.Properties.Settings.Default.OutlierRemovalSensitivity,
                    MaxNumberOfConsecutiveRemovals = CSharpVisualSkeletonSample.Properties.Settings.Default.MaxConsecutiveRemovals
                });
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Stop pumping
            GeneratorSingleton.Instance.Stop();

            GeneratorSingleton.Instance.AllFramesReady -= MyAllFramesReadyEventHandler;

            // Conditionally shutdown the service
            GeneratorSingleton.Instance.Shutdown();
        }

        #region Framework Event Handling

        //This is the handler to be given to GeneratorSingleton.Instance.AllFramesReady.
        //In order to easily switch to waiting separately for each stream 
        //(GeneratorSingleton.Instance.DataFrameReady and GeneratorSingleton.Instance.ColorImageFrameReady), 
        //The issue the following, instead of MyAllFramesReadyEventHandler:
        // GeneratorSingleton.Instance.DataFrameReady += JointsUpdate.MyDataFrameReady;
        // GeneratorSingleton.Instance.ColorImageFrameReady += RgbTextureUpdate.MyColorImageFrameReadyEventHandler;
        private void MyAllFramesReadyEventHandler(object sender, AllFramesReadyEventArgs e)
        {
            using (var allFrames = e.OpenFrame() as AllFramesFrame)
            {
                if (allFrames != null)
                {
                    foreach (var evtArgs in allFrames.FramesReadyEventArgs)
                    {
                        var colorImageFrameReady = evtArgs as ColorImageFrameReadyEventArgs;
                        if (null != colorImageFrameReady)
                        {
                            this.MyColorImageFrameReadyEventHandler(sender, colorImageFrameReady);
                            continue;
                        }
                        var dataFrameReady = evtArgs as DataFrameReadyEventArgs;
                        if (null != dataFrameReady)
                        {
                            this.MyDataFrameReady(sender, dataFrameReady);
                            continue;
                        }
                        var gestureFrameReady = evtArgs as GesturesFrameReadyEventArgs;
                        if (null != gestureFrameReady)
                        {
                            this.MyRecognitionFrameReadyEventHandler(sender, gestureFrameReady);
                            continue;
                        }
                    }
                }
            }
        }

        private void MyRecognitionFrameReadyEventHandler(object sender, GesturesFrameReadyEventArgs e)
        {
            // Opening the received frame
            using (var gesturesFrame = e.OpenFrame() as GesturesFrame)
            {
                GesturesText.Text = "";
                if (gesturesFrame != null)
                {
                    foreach (BaseGesture gesture in gesturesFrame.FirstSkeletonGestures())
                    {
                        // Update messages for gesture
                        if (!gestureMessages.ContainsKey(gesture.ID))
                        {
                            gestureMessages.Add(gesture.ID, new GestureMessage(gesture.Description));
                        }
                        gestureMessages[gesture.ID].timeToLiveCounter = gestureTypeToDelay[gesture.Type];
                        switch (gesture.Type)
                        {
                            case BaseGesture.GestureType.HEAD_POSITION:
                                {
                                    HeadPositionGesture headPositionGesture = gesture as HeadPositionGesture;
                                    gestureMessages[gesture.ID].text = gesture.Description + " " + headPositionGesture.RegionIndex;
                                    break;
                                }
                            case BaseGesture.GestureType.WINGS:
                                {
                                    WingsGesture wingsGesture = gesture as WingsGesture;
                                    gestureMessages[gesture.ID].text = gesture.Description + " " + wingsGesture.ArmsAngle;
                                    break;
                                }
                        }
                    }
                }

                // Generate gestures text
                GesturesText.Text = "";
                foreach (int id in gestureMessages.Keys)
                {
                    if (gestureMessages[id].timeToLiveCounter > 0)
                    {
                        GesturesText.Text += id + " - " + gestureMessages[id].text;
                        GesturesText.Text += "\r\n";
                        gestureMessages[id].timeToLiveCounter--;
                    }
                }
            }
        }

        private void MyColorImageFrameReadyEventHandler(object sender, ColorImageFrameReadyEventArgs e)
        {
            // Opening the received frame
            using (var colorImageFrame = e.OpenFrame() as ColorImageFrame)
            {
                if (colorImageFrame != null) // Making sure it's really ColorImageFrame
                {
                    m_colorImageDrawer.DrawColorImage(colorImageFrame.ColorImage.Image); // Reading the ColorImage data
                    ColorImageDisplay.Source = m_colorImageDrawer.ImageSource;

                    var colorImageStream = colorImageFrame.Stream as ImageStreamBase<FrameKey, ColorImage>;
                    UpdateImageAndVideoWarnings(colorImageFrame.Warnings, colorImageStream.Warnings); // Reading both Image and ImageStream warnings
                }
            }
        }

        void MyDataFrameReady(object sender, Xtr3D.Net.ExtremeMotion.Data.DataFrameReadyEventArgs e)
        {
            // Opening the received frame
            using (var dataFrame = e.OpenFrame() as DataFrame)
            {

                if (dataFrame != null) // Making sure it's really DataFrame
                {
                    var joints = dataFrame.Skeletons[0].Joints; // Possibly several Skeletons, we'll use the first
                    if (dataFrame.Skeletons[0] != null)
                    {
                        TrackingState state = dataFrame.Skeletons[0].TrackingState;
                        SkeletonTrackingState.Text = state.ToString();
                        SetCalibrationIconVisibility(state);
                    }
                    // We only want to display a tracked Skeleton
                    if (joints.Head.jointTrackingState == JointTrackingState.Tracked)
                    {
                        m_skeletonDrawer.DrawSkeleton(joints);
                    }
                    else
                    {
                        m_skeletonDrawer.WipeSkeleton();
                    }

                    UpdateFrameEdges(dataFrame.Skeletons[0].ClippedEdges); // Reading the Skeleton Edge warnings
                }
            }
        }

        private void SetCalibrationIconVisibility(TrackingState state)
        {
            if (state == Xtr3D.Net.ExtremeMotion.Data.TrackingState.Calibrating)
            {
                CalibrationIcon.Visibility = Visibility.Visible;
            }
            else
            {
                CalibrationIcon.Visibility = Visibility.Hidden;
            }
        }
        #endregion

        #region Warnings Update
        private void UpdateImageAndVideoWarnings(ImageWarnings imageWarnings, ImageStreamWarnings imageStreamWarnings)
        {
            // Using .NET Enum functions to see what Warnings we received
            LightLowBox.IsChecked = imageWarnings.HasFlag(ImageWarnings.LightLow);
            StrongBacklightingBox.IsChecked = imageWarnings.HasFlag(ImageWarnings.StrongBacklighting);
            TooManyPeople.IsChecked = imageWarnings.HasFlag(ImageWarnings.TooManyPeople);
        }

        private void UpdateFrameEdges(FrameEdges edges)
        {
            // Using .NET Enum functions to see what Warnings we received                        
            Right.IsChecked = edges.HasFlag(FrameEdges.Right);
            Near.IsChecked = edges.HasFlag(FrameEdges.Near);
            Left.IsChecked = edges.HasFlag(FrameEdges.Left);
            Far.IsChecked = edges.HasFlag(FrameEdges.Far);
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Reset engine
            GeneratorSingleton.Instance.Reset();
            // Reset display
            ResetDisplay();
        }

        private void ResetDisplay()
        {

            UpdateFrameEdges(FrameEdges.None);
            UpdateImageAndVideoWarnings(ImageWarnings.None, ImageStreamWarnings.None);
        }
    }


}
