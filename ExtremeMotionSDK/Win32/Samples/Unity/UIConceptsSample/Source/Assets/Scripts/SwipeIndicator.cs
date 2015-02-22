using UnityEngine;
using System.Collections;

public class SwipeIndicator : MonoBehaviour {
	
	public UITexture leftArrowTexture;
	public UITexture rightArrowTexture;
	
	private Texture blackArrow;
	private Texture orangeArrow;
	private SwipeDetector.SwipeType currentType;
	
	void Awake () {
		currentType = SwipeDetector.SwipeType.NO_SWIPE;
	}
	
	// Use this for initialization
	void Start () {
		blackArrow = Resources.Load("Materials/arrow") as Texture;
		orangeArrow = Resources.Load("Materials/orangeArrow") as Texture;
		
		leftArrowTexture.mainTexture = blackArrow;
		rightArrowTexture.mainTexture = blackArrow;
		
		leftArrowTexture.gameObject.transform.localScale = new Vector3(150,150);
		rightArrowTexture.gameObject.transform.localScale = new Vector3(150,150);
	}
	
	public void ShowArrow (SwipeDetector.SwipeType m_swipeType)
	{
		if(currentType != m_swipeType)
		{
			currentType = m_swipeType;
			
			if(m_swipeType.Equals(SwipeDetector.SwipeType.SWIPED_RIGHT))
				rightArrowTexture.mainTexture = orangeArrow;
			else
				leftArrowTexture.mainTexture = orangeArrow;
		}
	}
	public void Clear()
	{
		if(currentType != SwipeDetector.SwipeType.NO_SWIPE)
		{			
			if(currentType.Equals(SwipeDetector.SwipeType.SWIPED_RIGHT))
				rightArrowTexture.mainTexture = blackArrow;
			else
				leftArrowTexture.mainTexture = blackArrow;
			
			currentType = SwipeDetector.SwipeType.NO_SWIPE;
		}
	}
}
