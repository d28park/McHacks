using UnityEngine;
using System.Collections;

public class MyButton : MonoBehaviour {
	
	protected string m_idleBg = "button_bg_normal";
	protected string m_highlightBg = "button_bg_hover";
	protected string m_clickedBg = "button_bg_hover";
	protected string m_unavailableBg = "button_bg_hover";
	protected UISprite m_myBackground;
	protected UILabel m_myLabel;
	private bool m_isClicked = false;
	
	/// <summary>
	/// sets the button initial Position.
	/// </summary>
	/// <param name='myPosition'>
	/// the button position.
	/// </param>
	public virtual void Init (Vector3 myPosition) {
		
		this.transform.localScale = new Vector3(1,1,1);
		m_myBackground = GetComponentInChildren<UISprite>();
		m_myLabel = GetComponentInChildren<UILabel>();
		this.transform.localPosition = myPosition;
	}
	
	/// <summary>
	/// Highslight the button.
	/// </summary>
	/// <param name='highLight'>
	/// true for Highlight, false otherwise.
	/// </param>
	public virtual void HighLightState (bool highLight)
	{
		IsClicked = false;
		if(highLight){
			m_myBackground.spriteName = m_highlightBg;
		}
		else{
			m_myBackground.spriteName = m_idleBg;
		}
	}
	/// <summary>
	/// Sets the size of the button image.
	/// </summary>
	/// <param name='width'>
	/// Button Width.
	/// </param>
	/// <param name='height'>
	/// Button Height.
	/// </param>
	public void SetImageSize(float width, float height)
	{
		m_myBackground.transform.localScale = new Vector2(width,height);
	}
	
	/// <summary>
	/// Sets the button text label.
	/// </summary>
	/// <param name='label'>
	/// the text label.
	/// </param>
	public virtual void SetLabelPos(float xPos, float yPos)
	{
		if(m_myLabel != null)
			m_myLabel.transform.localPosition = new Vector3(xPos,yPos,-1);
	}
	
	public virtual void SetLabel(string label)
	{
		if(m_myLabel != null)
			m_myLabel.text = label;
	}
	
	public virtual void SetAvailability(bool available)
	{
		m_myBackground.enabled = available;
		m_myLabel.enabled = available;
	}
	
	/// <summary>
	/// Sets background to clicked
	/// </summary>
	public virtual void ClickedState ()
	{	
		IsClicked = true;
		m_myBackground.spriteName = m_clickedBg;
		
	}
	/// <summary>
	/// Sets the button background to unavailable
	/// </summary>
	public void UnavailableClicked()
	{
		IsClicked = true;
		m_myBackground.spriteName = m_unavailableBg;
	}

	public Vector3 GetPosition ()
	{
		return m_myBackground.transform.localPosition;
	}
	
	/// <summary>
	/// Gets or sets a value indicating whether the button is clicked.
	/// </summary>
	/// <value>
	/// <c>true</c> if this instance is clicked; otherwise, <c>false</c>.
	/// </value>
	public bool IsClicked
    {
          get{ return m_isClicked;  }
          set{ m_isClicked = value; }
    }
}
