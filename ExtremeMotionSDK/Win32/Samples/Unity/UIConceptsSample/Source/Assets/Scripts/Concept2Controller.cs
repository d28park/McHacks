using UnityEngine;
using System.Collections;
using Xtr3D.Net.ExtremeMotion.Data;
/// <summary>
/// Horizontal Scroll
/// </summary>
public class Concept2Controller : MonoBehaviour {
	
	private const string IMAGES_CONTAINER_OBJECT_NAME = "Images";
	private const int NUM_OF_IMAGES = 5;
	private const int IMAGE_WIDTH = 470;
	private const int ITEM_GAP = 20;
	private const int FAKE_MOVEMENT_DISTANCE = 80;
	private const float SCALE_ANIMATION_DURATION = 0.6f;
	private int m_currentImageId = 2;
	private SwipeDetector.SwipeType m_swipeType = SwipeDetector.SwipeType.NO_SWIPE;
	
	public SwipeDetectorManager m_swipeDetectorManager;
	private SwipeItem[] m_images;
	private TweenPosition m_imagesTweenPosition;
	private GameObject ImagesCont;
	public CalibrationManager m_calibrationManager;
	private bool m_imagesTweenFinished = true;
	private bool firstSwipe = true;
	
	private static TrackingState m_lastEngineState = TrackingState.NotTracked;
	
	// Use this for initialization
	void Start () {
		//Register to swipe event
		ImagesCont = GameObject.Find(IMAGES_CONTAINER_OBJECT_NAME);
		m_imagesTweenPosition = ImagesCont.GetComponent<TweenPosition>();
		
		m_swipeDetectorManager.SwipeOccured += SwipeOccured;
		CreateButtons();
		InitTween();
		HighlightStartingImage();
		
		// due to an issue on ipad 2 with panel clipping, we remove it.
		if(ResolutionController.aspectRatio == ResolutionController.AspectRatios.Aspect_4x3)
		{
			GetComponent<UIPanel>().clipping = UIDrawCall.Clipping.None;
		}
	}
	
	private void CreateButtons()
	{
		m_images = new SwipeItem[NUM_OF_IMAGES];
		
		GameObject swipeItemPrefab = (GameObject) Resources.Load("Prefabs/SwipeItem"); // loading "swipe item" prefab from resources
		
		//Building the buttons column
		for(int i = 0; i < NUM_OF_IMAGES;i++)
		{
			GameObject swipeItemObject = (GameObject) Instantiate(swipeItemPrefab,Vector3.zero,Quaternion.identity);
			m_images[i] = swipeItemObject.GetComponent<SwipeItem>();
			m_images[i].gameObject.transform.parent = ImagesCont.transform;
			m_images[i].gameObject.transform.localScale = new Vector3(1,1,1);
			m_images[i].gameObject.transform.localPosition = new Vector3(-i*(IMAGE_WIDTH+ITEM_GAP),0,0);
			m_images[i].SetImage(i);
		}	
	}
	
	// Update is called once per frame
	void Update () {
		UpdateCalibrationState();
		
		if(!m_swipeType.Equals(SwipeDetector.SwipeType.NO_SWIPE))
		{
			if(m_imagesTweenFinished)
			{
				if(m_swipeType.Equals(SwipeDetector.SwipeType.SWIPED_RIGHT)){
					if(m_currentImageId < NUM_OF_IMAGES-1)
					{
						MoveImages(1);
						m_images[m_currentImageId-1].Highlight(false);
					}
					else // fake movement
					{
						FakeMoveImages(FAKE_MOVEMENT_DISTANCE);
					}
				}
				else{ // Swipe Left
					if(m_currentImageId > 0)
					{
						MoveImages(-1);
						m_images[m_currentImageId+1].Highlight(false);
					}
					else // fake movement
					{
						FakeMoveImages(FAKE_MOVEMENT_DISTANCE*-1);
					}
				}
				m_images[m_currentImageId].Highlight(true);
			}
			m_swipeType = SwipeDetector.SwipeType.NO_SWIPE;
		}
	}

	void HighlightStartingImage ()
	{
		m_images[m_currentImageId].StartHighlighted();
	}
	
	/// <summary>
	/// Updates the state of the calibration screen.
	/// </summary>
	private void UpdateCalibrationState ()
	{
		if(!m_lastEngineState.Equals(m_swipeDetectorManager.GetTrackingState()))
		{
			m_lastEngineState = m_swipeDetectorManager.GetTrackingState();
		
			switch (m_lastEngineState) {
			case TrackingState.Calibrating:
				m_calibrationManager.ShowCalibration(true);
			break;
				
			case TrackingState.Tracked:
				m_calibrationManager.ShowCalibration(false);
			break;
			default:
			break;
			}
		}
	}
	
	void InitTween ()
	{
		m_imagesTweenPosition.eventReceiver = this.gameObject;
		m_imagesTweenPosition.callWhenFinished = "TweenFinished";
	}
	
	private void TweenFinished()
	{
		m_imagesTweenFinished = true;
		m_imagesTweenPosition.enabled = false;
		m_imagesTweenPosition.Reset();
		ImagesCont.transform.localPosition = new Vector3(m_currentImageId*(IMAGE_WIDTH+ITEM_GAP),0);
	}

	private void SwipeOccured (int swipeType)
	{
		if(firstSwipe)
		{
			m_images[m_currentImageId].GetScaleTween().duration = SCALE_ANIMATION_DURATION;
			firstSwipe = false;
		}
			
		m_swipeType = (SwipeDetector.SwipeType)swipeType;
	}
	
	void MoveImages(int direction)
	{
		m_imagesTweenFinished = false;
		m_imagesTweenPosition.enabled = true;
		m_imagesTweenPosition.method = UITweener.Method.Linear;
		m_imagesTweenPosition.from = new Vector3(m_currentImageId*(IMAGE_WIDTH+ITEM_GAP),0);
		m_imagesTweenPosition.to = new Vector3((m_currentImageId + direction)*(IMAGE_WIDTH+ITEM_GAP),0);
		m_imagesTweenPosition.duration = 0.6f;
		m_imagesTweenPosition.Play(true);
		m_currentImageId += direction;
	}
	
	void FakeMoveImages(int distance)
	{
		m_imagesTweenFinished = false;
		m_imagesTweenPosition.enabled = true;
		m_imagesTweenPosition.method = UITweener.Method.Linear;
		m_imagesTweenPosition.from = new Vector3(m_currentImageId*(IMAGE_WIDTH+ITEM_GAP),0);
		m_imagesTweenPosition.to = new Vector3((m_currentImageId*(IMAGE_WIDTH+ITEM_GAP)) + distance,0);
		m_imagesTweenPosition.duration = 0.3f;
		m_imagesTweenPosition.Play(true);
	}
}
