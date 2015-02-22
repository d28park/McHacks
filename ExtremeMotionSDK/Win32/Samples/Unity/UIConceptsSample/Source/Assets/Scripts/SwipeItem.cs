using UnityEngine;
using System.Collections;

public class SwipeItem : MonoBehaviour {
	
	private const string IMAGE_NAME = "Image_470x294_";
	private const int HIGHLIGHTED_DEPTH = 3;
	private const int NORMAL_DEPTH = 1;
	
	private float depthFactor = 0.25f;
	private float currentDepth = 1;
	
	private TweenScale m_myScaleTween;
	private UISprite m_myBackground;
	private bool m_isHighlighted = false;
	
	// Use this for initialization
	void Awake () {
		m_myScaleTween = GetComponentInChildren<TweenScale>();
		m_myBackground = GetComponentInChildren<UISprite>();
	}

	public void SetImage (int id)
	{
		m_myBackground.spriteName = IMAGE_NAME + (id+1).ToString();
	}
	
	public TweenScale GetScaleTween()
	{
		return m_myScaleTween;
	}
	
	public void StartHighlighted ()
	{
		m_myScaleTween.duration = 0.001f;
		currentDepth = HIGHLIGHTED_DEPTH;
		Highlight(true);
	}

	public void Highlight (bool highlight)
	{
		m_isHighlighted = highlight;
		m_myScaleTween.Play(highlight);
	}
	
	void Update()
	{
		if(m_isHighlighted)
		{
			if(currentDepth < HIGHLIGHTED_DEPTH)
				currentDepth += depthFactor;
			else
				m_myBackground.depth = (int)currentDepth;
		}
		else
		{
			if(currentDepth > NORMAL_DEPTH)
				currentDepth -= depthFactor;
			else
				m_myBackground.depth = (int)currentDepth;
		}
	}
}
