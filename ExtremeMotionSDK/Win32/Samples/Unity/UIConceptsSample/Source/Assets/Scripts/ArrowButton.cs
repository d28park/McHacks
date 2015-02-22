using UnityEngine;
using System.Collections;

public class ArrowButton : MyButton {
	
	private UISprite m_arrowSprite;
	private const string ARROW_SPRITE_NAME = "ArrowItem";
	private const string BG_SPRITE_NAME = "Background";
	
	void Awake () {
		// sets the images for arrow button
		m_idleBg = "arow_up_normal";
		m_highlightBg = "arow_up_hover";
		m_unavailableBg = "arow_up_disable";
	}
	
	/// <summary>
	/// sets the button initial Position.
	/// </summary>
	/// <param name='myPosition'>
	/// the button position.
	/// </param>
	public override void Init (Vector3 myPosition) {
		
		UISprite[] sprites = GetComponentsInChildren<UISprite>();
		for (int i = 0; i < sprites.Length; i++) {
			if(sprites[i].name.Equals(ARROW_SPRITE_NAME))
			{
				m_arrowSprite = sprites[i];
				m_arrowSprite.MakePixelPerfect();
			}
			else // sprite is BG
			{
				m_myBackground = sprites[i];
				m_myBackground.MakePixelPerfect();
			}
		}
		
		this.transform.localPosition = myPosition;
		this.transform.localScale = new Vector3(1,1,1);
	}
	
	public override void HighLightState (bool highLight)
	{
		m_myBackground.enabled = highLight;
		
		if(highLight){
			m_arrowSprite.spriteName = m_highlightBg;
		}
		else{
			m_arrowSprite.spriteName = m_idleBg;
		}
	}
	
	public void RotateArrow(Quaternion rotation)
	{
		m_arrowSprite.transform.localRotation = rotation;
		//fixing arrow y position after rotation
		m_arrowSprite.transform.localPosition = new Vector3(m_arrowSprite.transform.localPosition.x,m_arrowSprite.transform.localPosition.y - m_arrowSprite.transform.localScale.y);
	}
	
	public override void SetAvailability(bool available)
	{
		if(!available){
			m_arrowSprite.enabled = false;
		}
		else{ // arrow available
			if(!m_arrowSprite.spriteName.Equals(m_highlightBg))
			{
				// change to idle if we are not on highlight state
				m_arrowSprite.spriteName = m_idleBg;
				m_arrowSprite.enabled = true;
			}
		}
	}
	
	public void SetArrowItemSize(float width,float height)
	{
		m_arrowSprite.transform.localScale = new Vector3(width,height);
	}
}
