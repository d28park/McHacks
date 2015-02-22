using UnityEngine;
using System.Collections;

public class CalibrationManager : MonoBehaviour {
	
	private TweenScale m_myTeen;
	public DisplayCameraRGB m_displayRGB;
	private UISprite m_raiseHandsImage;
	
	// Use this for initialization
	void Awake () {
		m_myTeen = GetComponent<TweenScale>();
		m_raiseHandsImage = GetComponentInChildren<UISprite>();
		m_myTeen.enabled = false;
		m_displayRGB = GetComponentInChildren<DisplayCameraRGB>();
	}
	/// <summary>
	/// Shows/Hides the calibration screen with scale animation.
	/// </summary>
	/// <param name='show'>
	/// Show/Hide calibration screen.
	/// </param>
	public void ShowCalibration(bool show)
	{
		if(!m_myTeen.enabled){
			m_myTeen.enabled = true;	
		}
		EnableDrawing(show);
		m_myTeen.Play(show);
	}
	/// <summary>
	/// Calibration screen tween scale finished.
	/// called by teen scale component
	/// </summary>
	private void TweenFinished()
	{
		m_myTeen.enabled = false;
	}
	
	public void EnableDrawing(bool draw)
	{
		m_displayRGB.Draw(draw);
	}
	
	public void ShowRaiseHandsImage(bool show)
	{
		if(m_raiseHandsImage.enabled != show)
			m_raiseHandsImage.enabled = show;
	}
}
