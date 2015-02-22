using UnityEngine;
using System.Collections;

public class VerticalItem : MonoBehaviour {
	
	public UISprite m_buttonBg;
	public UILabel m_title;
	public UILabel m_subTitle;
	public UISprite m_iconImg;
	
	public void HighLightState(bool highlight)
	{
		m_buttonBg.enabled = highlight;
		
		if(highlight)
		{
			m_title.color = Color.white;
			m_subTitle.color = Color.white;
		}
		else
		{
			m_title.color = Color.black;
			m_subTitle.color = Color.black;
		}
			
	}
}
