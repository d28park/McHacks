#include <iostream>
#include <fstream>
#include <sstream>
#include <string>
#include <map>
#include <list>

// Windows
#include <windows.h>
#include <process.h>
#include "resource.h"

// OpenGL
#include "openglut.h"

// Extreme Motion C API
#include "EmCAPI.h"
#include "EmCUtils.h"

using namespace std;

///////////////////////////// Definitions //////////////////////////////

#define QUIT_COMMAND					        'q'
#define DEFAULT_COMMAND					        '\0'
#define ESCAPE_KEY						        27
#define POINT_SIZE						        10.0
#define BACKGROUND_LOCATION                     -0.3f
#define TIMEOUT_DURATION_MILLIS			        30000
#define CALIBRATION_IMAGE_WIDTH			        240
#define CALIBRATION_IMAGE_HEIGHT		        240
#define DEFAULT_WINDOW_POSITION_X		        900
#define DEFAULT_WINDOW_POSITION_Y		        100
#define STATE_STRING_POS_X 				        400
#define STATE_STRING_POS_Y				        50
#define WARNING_STRING_POS_X 			        400
#define WARNING_STRING_POS_Y			        70
#define WARNINGS_PRINT_Y_OFFSET                 20
#define MAX_WARNINGS                            10
#define GESTURE_STRING_POS_X					10
#define GESTURE_STRING_POS_Y					450
#define STATIC_GESTURE_TIME_TO_LIVE_FRAMES		1
#define HEAD_POSITION_GESTURE_TIME_TO_LIVE_FRAMES 1
#define SWIPE_GESTURE_TIME_TO_LIVE_FRAMES		30
#define WINGS_GESTURE_TIME_TO_LIVE_FRAMES		5
#define SEQUENCE_GESTURE_TIME_TO_LIVE_FRAMES	30
#define UP_GESTURE_TIME_TO_LIVE_FRAMES			10
#define DOWN_GESTURE_TIME_TO_LIVE_FRAMES		10
#define MAX_GESTURE_ID							14

#define LOG(__msg__) cout << __msg__  << endl;

#define EXPECT_OK_RETURN_ERROR_AND_SHUTDOWN(result, msg)\
{														\
    if(result != EM_STATUS_OK)							\
    {													\
        LOG( msg);									\
        emShutdown();									\
        CleanGlobals();									\
        return result;									\
    }													\
}

///////////////////////////// Structures //////////////////////////////

struct SImagePlanePoint
{
    double x, y;
};

struct SGestureMessage 
{
	string text;
	long timeToLiveCounter;
};

///////////////////////////// Global Variables //////////////////////////////

map<ESkeletonTrackingState,char*> g_stateToString;
map<EmWarning,char*> g_warningToString;

GLvoid *g_font_style = GLUT_BITMAP_HELVETICA_18;
char g_command = DEFAULT_COMMAND;			
unsigned char* g_imageData = NULL; 
unsigned char* g_calibrationImage = NULL;
bool g_isSkeletonAvailable = false;
SSkeletonFrame g_currentSkeletonFrame;
SWarningsFrame g_currentWarningsFrame;
SImagePlanePoint g_jointsToDraw[EM_JOINT_COUNT];
bool g_isCalibrating = false;
int g_openglWindowHandle = 0;
bool g_refreshDisplay = true;
bool g_isWindowClosed = false;
string g_detectedGestures;
SGestureMessage g_gestureMessages[MAX_GESTURE_ID];
ofstream logFile;
bool useLog = false;
///////////////////////////// Functions //////////////////////////////

//-------------------------------------------------------------------------
//	Scale a [0..1] value to image size
//-------------------------------------------------------------------------
SImagePlanePoint ConvertCoordinate(double x, double y)
{
    SImagePlanePoint r;
    r.x = x * (EM_DEFAULT_IMAGE_WIDTH);
    r.y = y * EM_DEFAULT_IMAGE_HEIGHT;
    return r ;
}

//-------------------------------------------------------------------------
//	Retrieve skeletal data from frame
//-------------------------------------------------------------------------
bool GetSkeletonData()
{
    for(int i = 0; i < EM_JOINT_COUNT; i++)
    {
        if(EM_JOINT_TRACKING_STATE_TRACKED == g_currentSkeletonFrame.skeletonData->joints[i].jointTrackingState) 
        {
            g_jointsToDraw[i] = ConvertCoordinate(
                g_currentSkeletonFrame.skeletonData->joints[i].skeletonPoint.imgCoordNormHorizontal,
                g_currentSkeletonFrame.skeletonData->joints[i].skeletonPoint.imgCoordNormVertical);
        }
        else
        {
            g_jointsToDraw[i].x = EM_JOINT_INVALID_IMAGE_COORDINATE;
            g_jointsToDraw[i].y = EM_JOINT_INVALID_IMAGE_COORDINATE;
        }
    }

    if(g_currentSkeletonFrame.skeletonData->state == EM_SKELETON_TRACKING_STATE_TRACKED)
    {
        return true;
    }

    return false;
}

//-------------------------------------------------------------------------
//	Initialize OpenGL_ThreadFunc
//-------------------------------------------------------------------------
void Init(void) 
{
    HBITMAP hbmPict = (HBITMAP)LoadImage(
         GetModuleHandle(NULL),
        MAKEINTRESOURCE(IDB_CALIBRATION_IMAGE),
        IMAGE_BITMAP,
        CALIBRATION_IMAGE_WIDTH ,
        CALIBRATION_IMAGE_HEIGHT,
        LR_CREATEDIBSECTION );
    
    GetBitmapBits( hbmPict, CALIBRATION_IMAGE_WIDTH * CALIBRATION_IMAGE_HEIGHT * (EM_DEFAULT_IMAGE_BITCOUNT / 8), g_calibrationImage );

    // Create a texture 
        unsigned char dummyImage[EM_DEFAULT_IMAGE_WIDTH*EM_DEFAULT_IMAGE_HEIGHT*3] = {'\0'}; //Create Dummy Empty Image
        //Create a texture for raw image from the camera and the calibration image
    glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB8, EM_DEFAULT_IMAGE_WIDTH, EM_DEFAULT_IMAGE_HEIGHT, 0, GL_RGB, GL_UNSIGNED_BYTE, (GLvoid*)dummyImage); 
    glTexImage2D(GL_TEXTURE_2D, 1, GL_RGB8, CALIBRATION_IMAGE_WIDTH, CALIBRATION_IMAGE_HEIGHT, 0, GL_RGB, GL_UNSIGNED_BYTE, (GLvoid*)g_calibrationImage);

    // Set up the texture
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP); 
	// Texture should replace color, not blend with it
	glTexEnvf(GL_TEXTURE_ENV, GL_TEXTURE_ENV_MODE, GL_REPLACE);

}

//-------------------------------------------------------------------------
//  Draws a string at the specified coordinates.
//-------------------------------------------------------------------------
void PrintStringToWindow (
    float x, float y, float z, 
    GLfloat red, GLfloat green, GLfloat blue, 
    char* format, ...)
{
    va_list args;   //  Variable argument list
    int len;        // String length
    int i;          //  Iterator
    char * text;    // Text
 
    //  Initialize a variable argument list
    va_start(args, format);
 
    //  Return the number of characters in the string referenced the list of arguments.
    // _vscprintf doesn't count terminating '\0' (that's why +1)
    len = _vscprintf(format, args) + 1;
 
    //  Allocate memory for a string of the specified size
    text = (char*)malloc(len * sizeof(char));
 
    //  Write formatted output using a pointer to the list of arguments
    vsprintf_s(text, len, format, args);
 
    //  End using variable argument list
    va_end(args);
 
    glColor3f(red, green, blue);
    //  Specify the raster position for pixel operations.
    glRasterPos3f (x, y, z);

    //  Draw the characters one by one
    for (i = 0; text[i] != '\0'; i++)
	{
		glutBitmapCharacter(g_font_style, text[i]);
	}
 
    //  Free the allocated memory for the string
    free(text);
    glColor3f(1.0, 1.0, 1.0);
}

//-------------------------------------------------------------------------
//  Convert ESkeletonTrackingState to display text
//-------------------------------------------------------------------------
const char* StateToString(ESkeletonTrackingState	state)
{
	return g_stateToString[state];
}

//-------------------------------------------------------------------------
//  Update message string of detected gestures
//-------------------------------------------------------------------------
void UpdateDetectedGestureMessage()
{
	g_detectedGestures.clear();
	for(int i=0; i<MAX_GESTURE_ID; ++i)
	{
		if(g_gestureMessages[i].timeToLiveCounter>0)
		{
			(g_detectedGestures += g_gestureMessages[i].text) += " ";
			g_gestureMessages[i].timeToLiveCounter--;
		}
	}
}


//-------------------------------------------------------------------------
//  Output given text to window
//-------------------------------------------------------------------------
void PrintState(ESkeletonTrackingState	state)
{
    PrintStringToWindow(
        STATE_STRING_POS_X,
        STATE_STRING_POS_Y,
        0, 
        0.0, 1.0, 0.0,
        "Current State: %s",StateToString(state));
}

void PrintGestures(string detectedGestures)
{
    PrintStringToWindow(
        GESTURE_STRING_POS_X,
        GESTURE_STRING_POS_Y,
        0, 
        0.0, 1.0, 0.0,
        "Detected gestures: %s", 
		detectedGestures.c_str());
}

void WarningIdToStrings(EmWarning warning,std::list<std::string>& strings)
{
	typedef std::map<EmWarning, char*>::iterator EmWarningToCharIterator;
	for(EmWarningToCharIterator iterator = g_warningToString.begin(); iterator != g_warningToString.end(); iterator++) 
	{
		if(warning == iterator->first)
		{
			strings.push_back(iterator->second);
		}
	}	
}

//-------------------------------------------------------------------------
//  Extract warnings and output them to window
//-------------------------------------------------------------------------
int PrintWarning(EmWarning warning, float warningStrPosX, float warningStrPosY)
{
    int numWarningsPrinted = 0;
	std::list<std::string> warningsList;

	WarningIdToStrings(warning,warningsList);
	for (std::list<std::string>::const_iterator iterator = warningsList.begin(); iterator != warningsList.end(); ++iterator)
	{
		PrintStringToWindow(warningStrPosX, warningStrPosY + WARNINGS_PRINT_Y_OFFSET*numWarningsPrinted, 0, 1.0, 0.0, 0.0, "%s", (*iterator).c_str());
		numWarningsPrinted++;
	}

    return numWarningsPrinted;

#undef PRINT_WARNING
}

void PrintWarnings(SWarningsFrame	warnings)
{
    for (int i=0, numWarningsPrinted = 0; i<warnings.numOfWarnings; ++i)
    {
        numWarningsPrinted += PrintWarning(
            warnings.warnings[i], (float)WARNING_STRING_POS_X, (float)(WARNING_STRING_POS_Y + WARNINGS_PRINT_Y_OFFSET*numWarningsPrinted));
    }    
}


void DrawValidPoint(SImagePlanePoint point)
{
	if(point.x != EM_JOINT_INVALID_IMAGE_COORDINATE && point.y != EM_JOINT_INVALID_IMAGE_COORDINATE)
	{
		glVertex2d(point.x, point.y);
	}
}

//-------------------------------------------------------------------------
//  Draw display window, composited from raw image and skeletal data
//-------------------------------------------------------------------------
void DrawImage(void) 
{
   // Clear frame buffer
   glClearColor ( 0.0f, 0.0f, 0.0f, 0.5f );
   glClear(GL_COLOR_BUFFER_BIT);

   // Draw pixels to texture
   // Update Texture
   glTexSubImage2D(GL_TEXTURE_2D, 0 ,0, 0, EM_DEFAULT_IMAGE_WIDTH, EM_DEFAULT_IMAGE_HEIGHT, GL_RGB, GL_UNSIGNED_BYTE, (GLvoid*)g_imageData);

    if(g_isCalibrating)  
    {
        if( g_calibrationImage != NULL )
        {
            //update texture
            glTexSubImage2D(GL_TEXTURE_2D, 0 ,0, 0, CALIBRATION_IMAGE_WIDTH, CALIBRATION_IMAGE_HEIGHT, GL_RGB, GL_UNSIGNED_BYTE, (GLvoid*)g_calibrationImage);
        } 
    }
    
	// Enable textures
	glEnable(GL_TEXTURE_2D);

    glBegin( GL_QUADS ); //Begin Drawing Textures
        glTexCoord2d(0.0, 0.0);		glVertex2d(0.0,			  0.0);
        glTexCoord2d(1.0, 0.0); 	glVertex2d(EM_DEFAULT_IMAGE_WIDTH, 0.0);
        glTexCoord2d(1.0, 1.0); 	glVertex2d(EM_DEFAULT_IMAGE_WIDTH, EM_DEFAULT_IMAGE_HEIGHT);
        glTexCoord2d(0.0, 1.0); 	glVertex2d(0.0,			  EM_DEFAULT_IMAGE_HEIGHT);
    glEnd();

	// Disable textures
	glDisable(GL_TEXTURE_2D);

    if(g_isSkeletonAvailable)
    {
        //Draw Tracking State
        PrintState(g_currentSkeletonFrame.skeletonData->state);
        PrintWarnings(g_currentWarningsFrame);		
		PrintGestures(g_detectedGestures);
        GetSkeletonData();

        if(g_currentSkeletonFrame.skeletonData->state == EM_SKELETON_TRACKING_STATE_CALIBRATING)
        {
            g_isCalibrating = true;
        }
        else
        {
            g_isCalibrating = false;
        }
            

        //Drawing Settings for skeleton joints
        glPointSize(POINT_SIZE);

        //Begin Drawing Skeleton

        // Horizontal lines
        glBegin(GL_LINE_STRIP); 
		glColor4f(0.0, 1.0, 0.0,1.0);  
		DrawValidPoint(g_jointsToDraw[EM_JOINT_HAND_RIGHT]);
		DrawValidPoint(g_jointsToDraw[EM_JOINT_ELBOW_RIGHT]);
		DrawValidPoint(g_jointsToDraw[EM_JOINT_SHOULDER_RIGHT]);
		DrawValidPoint(g_jointsToDraw[EM_JOINT_SPINE]);
		DrawValidPoint(g_jointsToDraw[EM_JOINT_SHOULDER_LEFT]);
		DrawValidPoint(g_jointsToDraw[EM_JOINT_ELBOW_LEFT]);
		DrawValidPoint(g_jointsToDraw[EM_JOINT_HAND_LEFT]);
        glEnd();

        // Vertical lines
        glBegin(GL_LINE_STRIP);
		glColor4f(0.0, 1.0, 0.0,1.0);
		DrawValidPoint(g_jointsToDraw[EM_JOINT_HEAD]);
		DrawValidPoint(g_jointsToDraw[EM_JOINT_SHOULDER_CENTER]);
		DrawValidPoint(g_jointsToDraw[EM_JOINT_SPINE]);
		DrawValidPoint(g_jointsToDraw[EM_JOINT_HIP_CENTER]);
        glEnd();

        glBegin(GL_POINTS);
		glColor4f(0.0, 1.0, 0.0,1.0);
		DrawValidPoint(g_jointsToDraw[EM_JOINT_HAND_RIGHT]);
		DrawValidPoint(g_jointsToDraw[EM_JOINT_ELBOW_RIGHT]);
		DrawValidPoint(g_jointsToDraw[EM_JOINT_SHOULDER_RIGHT]);
		DrawValidPoint(g_jointsToDraw[EM_JOINT_SPINE]);
		DrawValidPoint(g_jointsToDraw[EM_JOINT_SHOULDER_CENTER]);
		DrawValidPoint(g_jointsToDraw[EM_JOINT_HIP_CENTER]);
		DrawValidPoint(g_jointsToDraw[EM_JOINT_HEAD]);
		DrawValidPoint(g_jointsToDraw[EM_JOINT_SHOULDER_LEFT]);
		DrawValidPoint(g_jointsToDraw[EM_JOINT_ELBOW_LEFT]);
		DrawValidPoint(g_jointsToDraw[EM_JOINT_HAND_LEFT]);
        glEnd();
    }

    // Swap buffers (due to double buffering).
    glutSwapBuffers();  
} 


//-------------------------------------------------------------------------
//  This thread is used to listen to the keyboard and quit the 
//	application when "q" is pressed.
//-------------------------------------------------------------------------
unsigned int __stdcall KeyboardInputProc(void*)
{
    while(g_command != QUIT_COMMAND)
    {
        g_command = getchar();
    }
    return 0;
}

//-------------------------------------------------------------------------
//	Listen to quit command on window
//-------------------------------------------------------------------------
void KeyboardFuncOGL(unsigned char key, int x, int y)
{
    if(key == ESCAPE_KEY)
    {
        g_command = QUIT_COMMAND;
    }
}

//-------------------------------------------------------------------------
//	Callback used to refresh the the display even when out of focus
//-------------------------------------------------------------------------
void OnIdle()
{
    if(g_refreshDisplay)
    {
        glutPostRedisplay();
    }
}

//-------------------------------------------------------------------------
//	Reshape the window
//-------------------------------------------------------------------------
void ReshapeWindow(GLsizei w, GLsizei h)
{
    glClearColor(0.0f, 0.0f, 0.5f, 0.0f);
    glMatrixMode(GL_PROJECTION);
    glLoadIdentity();
    gluOrtho2D(0, w, h, 0);        
    glMatrixMode(GL_MODELVIEW);
    glViewport(0, 0, w, h);
}

void closeWindow()
{
	g_isWindowClosed =  true;
}

//-------------------------------------------------------------------------
//	Setup the window
//-------------------------------------------------------------------------
unsigned int __stdcall OpenGL_ThreadFunc(void*)
{
    glutInitWindowSize(EM_DEFAULT_IMAGE_WIDTH, EM_DEFAULT_IMAGE_HEIGHT);
    glutInitWindowPosition( DEFAULT_WINDOW_POSITION_X, DEFAULT_WINDOW_POSITION_Y ); 
    glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGB); //Allow double buffering.
    g_openglWindowHandle = glutCreateWindow("CConsoleSample"); 
	glutReshapeWindow(EM_DEFAULT_IMAGE_WIDTH, EM_DEFAULT_IMAGE_HEIGHT);
    glutKeyboardFunc(KeyboardFuncOGL);
    glutIdleFunc(OnIdle);
    glutReshapeFunc(ReshapeWindow);
	glutCloseFunc(closeWindow);
	glutSetOption(GLUT_ACTION_ON_WINDOW_CLOSE, GLUT_ACTION_GLUTMAINLOOP_RETURNS);
    Init(); 
    glutDisplayFunc(DrawImage);
    glutMainLoop();		
    return 0;
}

//-------------------------------------------------------------------------
//	Initialize global variables
//-------------------------------------------------------------------------
void InitGlobals()
{
	g_stateToString[EM_SKELETON_TRACKING_STATE_INITIALIZING] ="Initializing";
	g_stateToString[EM_SKELETON_TRACKING_STATE_NOT_TRACKED ] ="Not Tracked" ;
	g_stateToString[EM_SKELETON_TRACKING_STATE_CALIBRATING ] ="Calibrating" ;
	g_stateToString[EM_SKELETON_TRACKING_STATE_TRACKED	   ] ="Tracked"     ;

	g_warningToString[EM_WARNING_SKELETON_FRAME_EDGE_CLIPPED_RIGHT  ]= "Too right";
	g_warningToString[EM_WARNING_SKELETON_FRAME_EDGE_CLIPPED_LEFT	]= "Too left";
	g_warningToString[EM_WARNING_SKELETON_FRAME_EDGE_CLIPPED_TOP	]= "Too high";
	g_warningToString[EM_WARNING_SKELETON_FRAME_EDGE_CLIPPED_BOTTOM ]= "Too low";
	g_warningToString[EM_WARNING_SKELETON_FRAME_EDGE_CLIPPED_NEAR	]= "Too close";
	g_warningToString[EM_WARNING_SKELETON_FRAME_EDGE_CLIPPED_FAR    ]= "Too far";
	g_warningToString[EM_WARNING_RAW_IMAGE_LIGHT_LOW				]= "Low lighting";
	g_warningToString[EM_WARNING_RAW_IMAGE_STRONG_BACKLIGHTING	    ]= "Strong backlighting";
	g_warningToString[EM_WARNING_RAW_IMAGE_TOO_MANY_PEOPLE		    ]= "Too many people";

  


    g_imageData = (unsigned char*) malloc(EM_DEFAULT_IMAGE_SIZE ); 
    g_calibrationImage = (unsigned char*) malloc(CALIBRATION_IMAGE_WIDTH * CALIBRATION_IMAGE_HEIGHT * (EM_DEFAULT_IMAGE_BITCOUNT / 8));

    g_currentSkeletonFrame.skeletonData = (SSkeleton*)malloc(sizeof(SSkeleton));

    g_currentWarningsFrame.warnings = (EmWarning*)malloc(MAX_WARNINGS * sizeof(EmWarning));

	for(int i=0; i<MAX_GESTURE_ID; ++i)
	{
			g_gestureMessages[i].timeToLiveCounter = 0;
	}
}

//-------------------------------------------------------------------------
//	Reset global variables
//-------------------------------------------------------------------------
void CleanGlobals()
{
	if (!g_isWindowClosed)
	{
		glutDestroyWindow( g_openglWindowHandle ); 
	}
    if(g_imageData)
    {
        free(g_imageData);
        g_imageData = NULL;
    }
    if(g_calibrationImage)
    {
        free(g_calibrationImage);
        g_calibrationImage = NULL;	
    }
    if (g_currentSkeletonFrame.skeletonData)
    {
        free(g_currentSkeletonFrame.skeletonData);
        g_currentSkeletonFrame.skeletonData = NULL;
    }
    if (g_currentWarningsFrame.warnings)
    {
        free(g_currentWarningsFrame.warnings);
        g_currentWarningsFrame.warnings = NULL;
    }
}

//-------------------------------------------------------------------------
//	Callback to close the application
//-------------------------------------------------------------------------
BOOL WINAPI CtrlHandler(DWORD dwType) 
{
    emShutdown();	
    CleanGlobals();
    return TRUE;
}

//--------------------------------------------------------------------------------------------------------
//	Copying the captured image frame
//--------------------------------------------------------------------------------------------------------
void CopyRawImageFrame(SRawImageFrame* imageFrame)
{
	memcpy(g_imageData,imageFrame->imageData,imageFrame->imageDescriptor.sizeImage);  
	LOG("Raw image frame: " << imageFrame->header.frameId);

}

//--------------------------------------------------------------------------------------------------------
//	Copying the captured skeleton frame aside in order to draw the skeleton and release the captured frame
//--------------------------------------------------------------------------------------------------------
void CopySkeletonFrame(SSkeletonFrame* skeletonFrame)
{
    g_currentSkeletonFrame.header = skeletonFrame->header;
    g_currentSkeletonFrame.numOfSkeletons = skeletonFrame->numOfSkeletons;
    g_currentSkeletonFrame.skeletonData->skeletonId = skeletonFrame->skeletonData->skeletonId;
    g_currentSkeletonFrame.skeletonData->skeletonProximity = skeletonFrame->skeletonData->skeletonProximity;
    g_currentSkeletonFrame.skeletonData->state = skeletonFrame->skeletonData->state;
    memcpy(g_currentSkeletonFrame.skeletonData->joints, skeletonFrame->skeletonData->joints, EM_JOINT_COUNT*sizeof(SJoint));
	LOG("Skeleton frame: " << skeletonFrame->header.frameId << 
		", state: " << StateToString(skeletonFrame->skeletonData->state) << 
		", proximity: " << skeletonFrame->skeletonData->skeletonProximity);
}

//-----------------------------------------------------------------------------------------------------------
//	Copying the captured warnings frame aside in order to display the warnings and release the captured frame
//-----------------------------------------------------------------------------------------------------------
void CopyWarningsFrame(SWarningsFrame* warningsFrame)
{
    g_currentWarningsFrame.header = warningsFrame->header;
    g_currentWarningsFrame.numOfWarnings = warningsFrame->numOfWarnings;
    memcpy(g_currentWarningsFrame.warnings,warningsFrame->warnings,warningsFrame->numOfWarnings*sizeof(EmWarning));
	std::list<std::string> allWarningsList;

	// Accumulate all warning strings
	for(int i = 0; i < warningsFrame->numOfWarnings;i++)
	{
		std::list<std::string> warningsList;
		WarningIdToStrings(warningsFrame->warnings[i],warningsList);
		allWarningsList.insert(allWarningsList.end(),warningsList.begin(),warningsList.end());
	}

	// Log warnings frame including warning strings
	LOG( "Warnings frame: " << warningsFrame->header.frameId << ", contains " << allWarningsList.size() << " Warnings:" );
	for (std::list<std::string>::const_iterator iterator = allWarningsList.begin(); iterator != allWarningsList.end(); ++iterator)
	{
		LOG(" - " << (*iterator).c_str() );
	}

}

void HandleGestures(SGesturesFrame* gestureFrame)
{	
	LOG("Gestures frame: " << gestureFrame->header.frameId << ", contains " << gestureFrame->numOfGestures << " gestures");
	char str[32];
	for(int i = 0 ; i < gestureFrame->numOfGestures; i++)
	{
		const int gestureId = gestureFrame->gestures[i]->gestureId;
		if(gestureId < 0 || gestureId >= MAX_GESTURE_ID)
			continue;		

        std::stringstream gestureIdAsStream;
        gestureIdAsStream << gestureId;
        g_gestureMessages[gestureId].text = ((gestureIdAsStream.str() + " - ") + gestureFrame->gestures[i]->description);
		// Update messages for gesture
		switch( gestureFrame->gestures[i]->gestureType)
		{
		case EM_GESTURE_TYPE_STATIC_POSITION:
			g_gestureMessages[gestureId].timeToLiveCounter = STATIC_GESTURE_TIME_TO_LIVE_FRAMES;
			break;
		case EM_GESTURE_TYPE_HEAD_POSITION:
			{
				char str[32];
				SHeadPositionGesture* headPositionGesture = 
					reinterpret_cast<SHeadPositionGesture*>(gestureFrame->gestures[i]);
				sprintf_s(str, "%d - %s (%d)", gestureId, gestureFrame->gestures[i]->description, headPositionGesture->regionIndex);
				g_gestureMessages[gestureId].text = str;
				g_gestureMessages[gestureId].timeToLiveCounter = HEAD_POSITION_GESTURE_TIME_TO_LIVE_FRAMES;
				break;
			}
		case EM_GESTURE_TYPE_SWIPE:
			g_gestureMessages[gestureId].timeToLiveCounter = SWIPE_GESTURE_TIME_TO_LIVE_FRAMES;
			break;
		case EM_GESTURE_TYPE_WINGS:
			{
				SWingsGesture* wingsGesture = 
					reinterpret_cast<SWingsGesture*>(gestureFrame->gestures[i]);
				sprintf_s(str, "%d - %s (%d)", gestureId, gestureFrame->gestures[i]->description, wingsGesture->armsAngle);
				g_gestureMessages[gestureId].timeToLiveCounter = WINGS_GESTURE_TIME_TO_LIVE_FRAMES;
				g_gestureMessages[gestureId].text = str;
				break;
			}
		case EM_GESTURE_TYPE_SEQUENCE:
			g_gestureMessages[gestureId].timeToLiveCounter = SEQUENCE_GESTURE_TIME_TO_LIVE_FRAMES;
			break;
		case EM_GESTURE_TYPE_DOWN:
			{
				SDownGesture* downGesture = 
					reinterpret_cast<SDownGesture*>(gestureFrame->gestures[i]);
				sprintf_s(str, "%d - %s", gestureId, gestureFrame->gestures[i]->description);
				g_gestureMessages[gestureId].timeToLiveCounter = DOWN_GESTURE_TIME_TO_LIVE_FRAMES;
				g_gestureMessages[gestureId].text = str;
				break;
			}
		case EM_GESTURE_TYPE_UP:
			g_gestureMessages[gestureId].timeToLiveCounter = UP_GESTURE_TIME_TO_LIVE_FRAMES;
			break;
		}	
		LOG( i << ". Gesture id: "<< g_gestureMessages[gestureId].text);

	}
}

///////////////////////////// Main //////////////////////////////
int main(int argc, char* argv[])
{
	int iii = EM_WARNING_SKELETON_FRAME_EDGE_CLIPPED_TOP;
    InitGlobals();
    unsigned int GL_ThreadID;

    glutInit(&argc, argv);  
    HANDLE OpenGL_ThreadFunc_Thread = (HANDLE)_beginthreadex(NULL, 0, OpenGL_ThreadFunc, NULL, 0, &GL_ThreadID);

	StreamType inputStreams = EM_STREAM_RAW_IMAGE|EM_STREAM_SKELETON|EM_STREAM_WARNING|EM_STREAM_GESTURES; // List of the streams to initialize and start.
    const int NUM_OF_STREAMS_TO_WAIT_FOR = 4;
    unsigned int keyboardInputThreadId;

    SetConsoleCtrlHandler(CtrlHandler, TRUE);
    SImageDescriptor imageDescriptor(S_IMAGE_DESCRIPTOR_INITIAL_VALUE);
    
    //Initialize Extreme Motion SDK
    EEmStatus result = emInitialize(&imageDescriptor, inputStreams, NULL); 
    EXPECT_OK_RETURN_ERROR_AND_SHUTDOWN(result, "Initialize failed with error: " << result << endl);
    LOG( "Initialize successful" );
    
    //Set gestures file (make sure SamplePoses.xml is present where the .exe is located!)
	result = emSetGesturesFile("SamplePoses.xml"); 
    if (result != EM_STATUS_OK)
    {
        LOG( "Warning: emSetGesturesFile failed with result: " << result << ". Filename was: " << "SamplePoses.xml" << ". No gestures shall be detected..." );
    }
    else
    {
        LOG( "Setting gesture file completed successful" );
    }
    

    //Start the streams
    result =  emStartStreams(inputStreams); 
    EXPECT_OK_RETURN_ERROR_AND_SHUTDOWN(result, "Failed starting the streams. Error: " << result << endl)
    LOG( "Successfully started the RawImage, Skeleton and Warnings streams" );

    //Spin up the thread that listens to keyboard
    HANDLE inputThread = (HANDLE)_beginthreadex(NULL, 0, KeyboardInputProc, NULL, 0, &keyboardInputThreadId);  
    if (NULL == inputThread)
    {
        LOG( "Failed to create thread");
        return EM_STATUS_ERROR;
    }

    while(g_command != QUIT_COMMAND)
    {
		if (g_isWindowClosed)			
		{
			break;
		}
		
        void* capturedFrames[NUM_OF_STREAMS_TO_WAIT_FOR];
        

        // Wait for synchronized frames from streams defined in inputStreams. 
        // Once all frames are received, they are locked until released.
        // Note that in case the skeleton is not needed to be drawn (e.g. calibration stage), 
        // using emWaitForAndLockAnyFrame will result in better performance.
        result =  emWaitForAndLockAllFrames(inputStreams, TIMEOUT_DURATION_MILLIS, capturedFrames);  
        if(result != EM_STATUS_OK)
        {
            LOG( "Failed capturing frame. Error: " << result );
            continue;
        }

		LOG( "Received frame: " << ((SRawImageFrame*)capturedFrames[0])->header.frameId);
        SRawImageFrame* imageFrame = (SRawImageFrame*)capturedFrames[0];
		CopyRawImageFrame(imageFrame);

        SSkeletonFrame* skeletonFrame = (SSkeletonFrame*)capturedFrames[1];	
        CopySkeletonFrame(skeletonFrame);                
        g_isSkeletonAvailable = true;
		
        // Separately check for any warnings received. 
		SWarningsFrame* warningsFrame = (SWarningsFrame*)capturedFrames[2];
        CopyWarningsFrame(warningsFrame);                                

		SGesturesFrame* gestureFrame = (SGesturesFrame* )capturedFrames[3];
		HandleGestures(gestureFrame);
		UpdateDetectedGestureMessage();

		if (!g_isWindowClosed)			
		{
			glutPostRedisplay(); 
		}
		
        // Releasing frames
        result = emUnlockFrames(inputStreams, (const void**)capturedFrames); 
        if(result != EM_STATUS_OK)
        {
            LOG( "Failed capturing frame. Error: " << result );
            continue;
        }
    }

    g_refreshDisplay = false;
    if(!CloseHandle(inputThread))
    {
        LOG( "Failed to close the keyboard input thread");
    }

    emShutdown(); // Shutting the system down
    CleanGlobals();
    return 0;

}