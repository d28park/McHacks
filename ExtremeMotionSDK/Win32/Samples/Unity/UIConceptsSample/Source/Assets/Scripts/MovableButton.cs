using UnityEngine;
using System.Collections;

public class MovableButton : MyButton {
	
	private const float EDGE_MOVEMENT = 20;
	private const int HIGHLIGHT_POSITION = 0; // this is the position that will be highlited in buttons colum
	private const string ICON_OBJECT_NAME = "ButtonIcon";
	private const string DEFAULT_ICON_SPRITE_NAME = "ButtonIcon_0";
	private const string BG_OBJECT_NAME = "Background";
	
	private float m_startyPos;
	private float m_startxPos;
	private float m_yDisToMove;
	private float m_yDisFromNeighbor;
	private float m_currentyPos;
	private float m_tweenDuration;
	private int m_currentPositionID;
	private int m_positionsToMove;
	private bool m_isEnabled = false;
	private bool m_isReadyForSlide = false;
	private string m_idleIconSpriteName;
	private TweenPosition m_myTween;
	private UISprite m_myIcon;
	private ButtonSlider m_buttonSlider;
	private string m_levelToLoad;
	
	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Awake () {
		
		m_myTween = GetComponent<TweenPosition>();
		m_myTween.duration = m_tweenDuration;
		m_myTween.eventReceiver = this.gameObject;
		m_myTween.enabled = false;
		
		m_buttonSlider = GetComponentInChildren<ButtonSlider>();
		
		ShowSlider(false);
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
			if(sprites[i].name.Equals(ICON_OBJECT_NAME))
			{
				m_myIcon = sprites[i];
				m_myIcon.MakePixelPerfect();
			}
			else if(sprites[i].name.Equals(BG_OBJECT_NAME))
			{
				m_myBackground = sprites[i];
				m_myBackground.MakePixelPerfect();
			}
		}
		
		this.transform.localPosition = myPosition;
		this.transform.localScale = new Vector3(1,1,1);
		
		m_currentyPos = myPosition.y;
		m_startxPos = myPosition.x;
		
		m_myLabel = GetComponentInChildren<UILabel>();
	}
	
	public void SetLevelToLoad(string levelToLoad)
	{
		m_levelToLoad = levelToLoad;	
	}
	
	public override void HighLightState (bool highLight)
	{
		IsClicked = false;
		m_myBackground.enabled = highLight;
		
		ChangeIcon(highLight);
	}
	
	public void SetIcon (int id)
	{
		m_idleIconSpriteName = DEFAULT_ICON_SPRITE_NAME + id.ToString();
		m_myIcon.spriteName = m_idleIconSpriteName;
		m_myIcon.MakePixelPerfect();
	}
	
	public void ChangeIcon (bool highLight)
	{
		if(highLight)
			m_myIcon.spriteName = m_idleIconSpriteName + "_hover";
		else
			m_myIcon.spriteName = m_idleIconSpriteName;
		
		m_myIcon.MakePixelPerfect();
	}
	
	/// <summary>
	/// Inits the tween position in Y axis.
	/// </summary>
	/// </param>
	/// <param name='myID'>
	/// The ID of the button.
	/// </param>
	/// <param name='startyPos'>
	/// Starting Y position.
	/// </param>
	/// <param name='yDisToMove'>
	/// (button height + small space between buttons) used for the distance movement.
	/// </param>
	public void InitTweenYpos(int myID, float startyPos, float yDisToMove,float yDisFromNeighbor,float tweenDuration)
	{
		m_currentPositionID = myID;
		
		m_startyPos = startyPos;
		m_yDisFromNeighbor = yDisFromNeighbor;
		SetYdistanceToMove(yDisToMove);
		
		m_tweenDuration = tweenDuration;
	}
	
	/// <summary>
	/// Moves up in buttons colum.
	/// </summary>
	/// <param name='isEdge'>
	/// Is colume in screen edge.
	/// </param>
	public void MoveUp(bool isEdge)
	{
		MoveYaxis(true,isEdge);
	}
	/// <summary>
	/// Moves down in buttons colum.
	/// </summary>
	/// <param name='isEdge'>
	/// Is colume in screen edge.
	/// </param>
	public void MoveDown(bool isEdge)
	{
		MoveYaxis(false,isEdge);
	}
	
	public void SetYdistanceToMove(float newDistance)
	{
		m_yDisToMove = newDistance;
		
		m_positionsToMove = (int) (m_yDisToMove / m_yDisFromNeighbor);
	}
	
	/// <summary>
	/// Initilize tween parameters for current movement
	/// </summary>
	/// <param name='isUp'>
	/// Move up or down
	/// </param>
	/// <param name='isEdge'>
	/// Is in edge or not.
	/// </param>
	private void MoveYaxis(bool isUp,bool isEdge)
	{
		if(!m_isEnabled)
			m_myTween.enabled = true;
		// if we are in the edge of the colume
		if(isEdge)
		{	
			// we are faking movement according to current edge (up/down)
			float moveToY = isUp ? m_currentyPos - EDGE_MOVEMENT : m_currentyPos + EDGE_MOVEMENT;
			// fake movement will move slightly up or down so from and to vectors are opposite
			m_myTween.from = new Vector3(m_startxPos,moveToY,0);
			m_myTween.to = new Vector3(m_startxPos,m_currentyPos,0);
		}
		else // init normal movement parameters
		{
			// we initilize movement parameter according to current movement direction
			float moveToY = isUp ? m_currentyPos + m_yDisToMove : m_currentyPos - m_yDisToMove;
			// updates button current position in colum (we use it in order to fix button position after tween)
			m_currentPositionID = isUp ? m_currentPositionID + m_positionsToMove : m_currentPositionID - m_positionsToMove;
			
			m_myTween.from = new Vector3(m_startxPos,m_currentyPos,0);
			m_myTween.to = new Vector3(m_startxPos,moveToY,0);
		}
		m_myTween.Play(true); // starts tween
	}

	/// <summary>
	/// Button tween position finished.
	/// </summary>
	private void TweenFinished()
	{
		// "fixing" button position
		m_currentyPos = m_startyPos + (m_currentPositionID * m_yDisFromNeighbor);
		
		m_myTween.enabled = false; // disabling tween for resetting
		m_myTween.Reset(); // reset tween state to begining
		// updating new fixed button position
		this.transform.localPosition = new Vector3(m_startxPos,m_currentyPos,0);
	}
	
	public void ReadyForSlide()
	{
		if(!m_isReadyForSlide)
		{
			m_isReadyForSlide = true;
			ShowSlider(true);
		}
	}
	
	public void ShowSlider (bool show)
	{
		m_isReadyForSlide = show;
		m_buttonSlider.gameObject.SetActive(show);
	}

	public void Slide ()
	{
		if(!m_buttonSlider.IsSliding){
			m_buttonSlider.IsSliding = true;
			m_buttonSlider.StartSlide();
		}
	}
	
	private void SlideFinished()
	{
		Application.LoadLevel(m_levelToLoad);
	}
}
