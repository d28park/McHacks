using UnityEngine;
using System.Collections;

public class GridItem : MyButton {
	
	private const string ICON_SPRITE_NAME = "Icon";
	private const string TEXT_BG_SPRITE_NAME = "TextBG";
	private const string OVERLAY_SPRITE_NAME = "Overlay";
	
	private string m_iconSpriteName = "Image_367x211_";
	private UISprite m_textBgSprite;
	private UISprite m_overlaySprite;
	private TweenScale m_myTween;
	public UISprite m_buttonTimeFinished;
	
	void Awake () {
		// sets the images for arrow button
		m_idleBg = "WhiteTileBorder";
		m_highlightBg = "item_hover_bg_tile";
		m_myTween = GetComponent<TweenScale>();
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
			if(sprites[i].name.Equals(TEXT_BG_SPRITE_NAME))
			{
				m_textBgSprite = sprites[i];
			}
			else if(sprites[i].name.Equals(OVERLAY_SPRITE_NAME)) 
			{
				m_overlaySprite = sprites[i];
			}
			else{ // sprite is BG
				m_myBackground = sprites[i];
				m_myBackground.MakePixelPerfect();
			}
		}
		
		this.transform.localPosition = myPosition;
		this.transform.localScale = new Vector3(1,1,1);
		
		m_myLabel = GetComponentInChildren<UILabel>();
	}
	
	public override void HighLightState (bool highLight)
	{
		if(highLight){
			m_textBgSprite.spriteName = m_highlightBg;
			m_overlaySprite.enabled = true;
			m_myTween.Play(true);
			m_myLabel.color = Color.white;
			m_myBackground.depth = 4;
			m_textBgSprite.depth = 5;
			m_overlaySprite.depth = 6;
		}
		else{
			m_textBgSprite.spriteName = m_idleBg;
			m_overlaySprite.enabled = false;
			m_myTween.Play(false);
			m_myLabel.color = Color.grey;
			m_overlaySprite.depth = 2;
			m_textBgSprite.depth = 1;
			m_myBackground.depth = 0;
			
			if(IsClicked)
			{
				IsClicked = false;
				m_buttonTimeFinished.enabled = false;
			}
		}
	}

	public void SetImage (int imageID)
	{
		imageID++;
		m_myBackground.spriteName = m_iconSpriteName + imageID.ToString(); // icon sptire name looks like "Image_367x211_1.png" etc.
	}
	
	public override void ClickedState ()
	{	
		IsClicked = true;
		m_buttonTimeFinished.enabled = true;
	}
}
