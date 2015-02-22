using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Xtr3D.Net.ExtremeMotion;
using Xtr3D.Net.ColorImage;
using Xtr3D.Net.ExtremeMotion.Data;
using Xtr3D.Net.ExtremeMotion.Interop.Types;

public class JointsUpdate : MonoBehaviour {
	
	private const float HEIGHT_MULTIPLIER = 2; // Value should be 2 when using ortographic camera in unity
	private const int DEPTH_CONSTANT = 2; // Value should be 2 when using ortographic camera in unity
	
	private float textureXPos;
	private float textureYPos;
	private float textureDimensionX;
	private float textureDimensionY;
	
	public GameObject myCube;
	public GUIText guiTextComponent;
	public LineRenderer lineRenderer;
	
	private FrameRateCalc frameRateCalc;

	public GameObject joint;
	public GameObject LeftHand;
	public GameObject LeftElbow;
	public GameObject LeftShoulder;
	public GameObject RightHand;
	public GameObject RightElbow;
	public GameObject RightShoulder;
	
	// current target sphere locations
	private GameObject handTarget, elbowTarget;
	private SphereCollider handTargetCollider, elbowTargetCollider;
	private int currentLocIndex = 1;
	private float[, ,] targetLoc = new float[,,] {
		{ { 1.0f, 1.5f, -6.0f }, { -2.0f, 1.5f, -6.0f } }, 
		{ { -0.5f, 1.5f, -6.0f }, { -0.5f, 0.1f, -6.0f } },
		{ { -1.5f, 1.0f, -4.5f }, { -1.5f, 1.0f, -4.5f } }
	};

	private Dictionary<JointType, GameObject> jointsGameObjects = new Dictionary<JointType, GameObject>();
	private Dictionary<JointType, Xtr3D.Net.ExtremeMotion.Data.Joint> typesToJoints = new Dictionary<JointType, Xtr3D.Net.ExtremeMotion.Data.Joint>();

	private JointType[] jointTypesArray = new JointType[] { 
		JointType.Head,
		JointType.ShoulderCenter,
		JointType.Spine,
		JointType.HipCenter,
		JointType.ShoulderLeft,
		JointType.ShoulderRight,
		JointType.ElbowLeft,
		JointType.ElbowRight,
		JointType.HandRight,
		JointType.HandLeft 
	};
	
	long lastFrameID = -1;
	long currFrameID = -1;
	
	
	void Start () {
		CalculateTextureParams();
		CreateJoints();
        Xtr3dGeneratorStateManager.RegisterDataCallback(MyDataFrameReady);
		// init frameRateCalc for calculating avarage running fps in the last given frames
		frameRateCalc = new FrameRateCalc(50);
		
		// init targets
		handTarget = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		handTarget.transform.position = new Vector3(1.0f, 1.5f, -6.0f);
		handTargetCollider = handTarget.gameObject.AddComponent<SphereCollider>();
		handTargetCollider.center = Vector3.zero;
		handTargetCollider.radius = 1.0f;

		elbowTarget = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		elbowTarget.transform.position = new Vector3((float)-2.0, (float)(1.5), (float)(-6.0));
		elbowTargetCollider = elbowTarget.gameObject.AddComponent<SphereCollider>();
		elbowTargetCollider.center = Vector3.zero;
		elbowTargetCollider.radius = 1.0f;
	}
	
	private void CalculateTextureParams()
	{
		float heightMeasure = Camera.main.orthographicSize * HEIGHT_MULTIPLIER; // Calculating the current world view height measure
		textureDimensionX = Math.Abs(myCube.transform.localScale.x * (Screen.height/heightMeasure)); // calculating current cube size accroding to current screen resolution
		textureDimensionY = Math.Abs(myCube.transform.localScale.y * (Screen.height/heightMeasure)); // calculating current cube size accroding to current screen resolution
		Vector3 cubePos = Camera.main.WorldToScreenPoint(myCube.transform.position);
		textureXPos = cubePos.x - textureDimensionX/2;
		textureYPos = cubePos.y + textureDimensionY/2;	
	}
	
	private void CreateJoints()
	{
		foreach (JointType type in jointTypesArray)
			jointsGameObjects[type] = (GameObject) Instantiate(joint,new Vector3(0f,0f,-5),Quaternion.identity);
	}
	
	
	private void MyDataFrameReady(object sender, Xtr3D.Net.ExtremeMotion.Data.DataFrameReadyEventArgs e)
	{
        using (var dataFrame = e.OpenFrame() as DataFrame)
        {
			if (dataFrame!=null)
			{
				currFrameID = dataFrame.FrameKey.FrameNumberKey;
				Debug.Log ("Skeleton frame: " + currFrameID + ", state: " + dataFrame.Skeletons[0].TrackingState + ", proximity: " + dataFrame.Skeletons[0].Proximity.SkeletonProximity);
				if (currFrameID <= lastFrameID  && currFrameID!=1) //currFrameId=1 on reset/resume!!!
					return;
				
				lastFrameID = currFrameID;
				
				//update frameRateCalc, we need to call this every frame as we are calculating avarage fps in the last x frames.
				frameRateCalc.UpdateAvgFps();
				JointCollection skl = dataFrame.Skeletons[0].Joints;

				//use a copy of the Joints data structure as the dataFrame values can change.
				typesToJoints[JointType.ShoulderCenter] = skl.ShoulderCenter;
				typesToJoints[JointType.Spine] 			= skl.Spine;
				typesToJoints[JointType.HipCenter] 		= skl.HipCenter;
				typesToJoints[JointType.ShoulderLeft] 	= skl.ShoulderLeft;
				typesToJoints[JointType.ShoulderRight]	= skl.ShoulderRight;
				typesToJoints[JointType.ElbowLeft] 		= skl.ElbowLeft;
				typesToJoints[JointType.ElbowRight] 	= skl.ElbowRight;
				typesToJoints[JointType.HandRight]		= skl.HandRight;
				typesToJoints[JointType.HandLeft] 		= skl.HandLeft;
				typesToJoints[JointType.Head] 			= skl.Head;
			}
		}
		
	}

	// Update is called once per frame
	void OnGUI () {
		// Display in our text component the avg fps
		guiTextComponent.text = System.String.Format("{0:F2} Skeleton FPS",frameRateCalc.GetAvgFps());
	}
	
	void Update()
	{
		// Don't do anything if no info was passed yet
		if (!typesToJoints.ContainsKey(JointType.Head))
			return;
		
		// Loop through each JointType
		foreach (JointType type in jointTypesArray)
		{
			if (typesToJoints[JointType.HandLeft].jointTrackingState == JointTrackingState.Tracked)
			{
				float x = textureXPos + typesToJoints[JointType.HandLeft].skeletonPoint.ImgCoordNormHorizontal * textureDimensionX;
				float y = textureYPos + -1* typesToJoints[JointType.HandLeft].skeletonPoint.ImgCoordNormVertical * textureDimensionY;
				float z = DEPTH_CONSTANT + typesToJoints[JointType.HandLeft].skeletonPoint.Z;
				LeftHand.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x , y, z));
			}
			if (typesToJoints[JointType.ElbowLeft].jointTrackingState == JointTrackingState.Tracked)
			{
				float x = textureXPos + typesToJoints[JointType.ElbowLeft].skeletonPoint.ImgCoordNormHorizontal * textureDimensionX;
				float y = textureYPos + -1* typesToJoints[JointType.ElbowLeft].skeletonPoint.ImgCoordNormVertical * textureDimensionY;
				float z = DEPTH_CONSTANT + typesToJoints[JointType.ElbowLeft].skeletonPoint.Z;
				LeftElbow.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x , y, z));
			}
			if (typesToJoints[JointType.ShoulderLeft].jointTrackingState == JointTrackingState.Tracked)
			{
				float x = textureXPos + typesToJoints[JointType.ShoulderLeft].skeletonPoint.ImgCoordNormHorizontal * textureDimensionX;
				float y = textureYPos + -1* typesToJoints[JointType.ShoulderLeft].skeletonPoint.ImgCoordNormVertical * textureDimensionY;
				float z = DEPTH_CONSTANT + typesToJoints[JointType.ShoulderLeft].skeletonPoint.Z;
				LeftShoulder.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x , y, z));
			}
			if (typesToJoints[JointType.HandRight].jointTrackingState == JointTrackingState.Tracked)
			{
				float x = textureXPos + typesToJoints[JointType.HandRight].skeletonPoint.ImgCoordNormHorizontal * textureDimensionX;
				float y = textureYPos + -1* typesToJoints[JointType.HandRight].skeletonPoint.ImgCoordNormVertical * textureDimensionY;
				float z = DEPTH_CONSTANT + typesToJoints[JointType.HandRight].skeletonPoint.Z;
				RightHand.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x , y, z));
			}
			if (typesToJoints[JointType.ElbowRight].jointTrackingState == JointTrackingState.Tracked)
			{
				float x = textureXPos + typesToJoints[JointType.ElbowRight].skeletonPoint.ImgCoordNormHorizontal * textureDimensionX;
				float y = textureYPos + -1* typesToJoints[JointType.ElbowRight].skeletonPoint.ImgCoordNormVertical * textureDimensionY;
				float z = DEPTH_CONSTANT + typesToJoints[JointType.ElbowRight].skeletonPoint.Z;
				RightElbow.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x , y, z));
			}
			if (typesToJoints[JointType.ElbowRight].jointTrackingState == JointTrackingState.Tracked)
			{
				float x = textureXPos + typesToJoints[JointType.ShoulderRight].skeletonPoint.ImgCoordNormHorizontal * textureDimensionX;
				float y = textureYPos + -1* typesToJoints[JointType.ShoulderRight].skeletonPoint.ImgCoordNormVertical * textureDimensionY;
				float z = DEPTH_CONSTANT + typesToJoints[JointType.ShoulderRight].skeletonPoint.Z;
				RightShoulder.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x , y, z));
			}
			// Update positions if tracked
			/*if (typesToJoints [type].jointTrackingState == JointTrackingState.Tracked)
			{
				float x = textureXPos + typesToJoints[type].skeletonPoint.ImgCoordNormHorizontal * textureDimensionX;
				float y = textureYPos + -1* typesToJoints[type].skeletonPoint.ImgCoordNormVertical * textureDimensionY;
				// TODO: determine proportional DEPTH_MODIFIER to more closely emulate real life
				float z = DEPTH_CONSTANT + typesToJoints[type].skeletonPoint.Z;
				
				// (re-)activate joint and update position
				jointsGameObjects[type].SetActive(true);
				jointsGameObjects[type].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x , y, z));*/

				// TODO:
				//		check for joint collision
				if (Input.GetKeyDown (KeyCode.N))
				{
					print ("N pressed");
					handTarget.transform.position = new Vector3(targetLoc[currentLocIndex,0,0], targetLoc[currentLocIndex,0,1], targetLoc[currentLocIndex,0,2]);
					elbowTarget.transform.position = new Vector3(targetLoc[currentLocIndex,1,0], targetLoc[currentLocIndex,1,1], targetLoc[currentLocIndex,1,2]);
					currentLocIndex++;
					if (currentLocIndex > 2)
					{
						currentLocIndex = 0;
					}
				}
			}
		}
	}
