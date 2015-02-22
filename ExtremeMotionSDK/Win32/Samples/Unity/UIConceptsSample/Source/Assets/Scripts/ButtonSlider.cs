using UnityEngine;
using System.Collections;

public class ButtonSlider : MonoBehaviour {
	
	public UISprite m_sliderBg;
	public UISprite m_sliderFill;
	public UISprite m_sliderKnob;
	
	private bool m_isSliding;
	
	private TweenPosition m_SliderKnobTween;
	private TweenScale m_SliderFillTween;
	
	// Use this for initialization
	void Awake () {
		m_SliderKnobTween = GetComponentInChildren<TweenPosition>();
		m_SliderFillTween = GetComponentInChildren<TweenScale>();
		
		InitTweens();
	}
	
	public bool IsSliding
	{
		set {m_isSliding = value; }
		get {return m_isSliding; }
	}
	
	private void InitTweens ()
	{
		m_SliderFillTween.duration = 0.5f;
		m_SliderKnobTween.duration = 0.5f;
		
		m_SliderFillTween.eventReceiver = this.transform.parent.gameObject;
		m_SliderFillTween.callWhenFinished = "SlideFinished";
	}
	
	public void StartSlide()
	{
		m_SliderFillTween.Play(true);
		m_SliderKnobTween.Play(true);
	}
}
