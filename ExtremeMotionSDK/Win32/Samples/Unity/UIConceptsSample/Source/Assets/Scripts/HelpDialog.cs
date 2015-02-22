using UnityEngine;
using System.Collections;

public class HelpDialog : MonoBehaviour {
	
	private bool m_isOn = false;
	private TweenPosition m_myTween;
	private UILabel m_myText;
	
	// Use this for initialization
	void Awake () {
		m_myTween = GetComponent<TweenPosition>();
		m_myText = GetComponent<UILabel>();
	}
	
	public void SetHelpText(string helpText)
	{
		m_myText.text = helpText;
	}
	
	public void ShowDialog()
	{
		if(!m_isOn)
		{
			m_myTween.Play(true);
			m_isOn = true;
		}
	}
	
	public void HideDialog()
	{
		m_myTween.Play(false);
		m_isOn = false;
	}
	
	public bool IsOn()
	{
		return m_isOn;
	}
}
