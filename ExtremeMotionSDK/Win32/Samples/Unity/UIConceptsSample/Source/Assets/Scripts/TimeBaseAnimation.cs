using UnityEngine;
using System.Collections.Generic;
using System;

// This class taken from NGUI scripts (see NGUI\UI\UISpriteAnimation for referance)

public class TimeBaseAnimation : MonoBehaviour {

	[SerializeField] int mFPS = 30; // in our case we have 30 frames so we want total animation time will be 1 sec
	[SerializeField] string mPrefix = ""; // the sprite animation file name. (in our case "ButtonTime0XX")
	[SerializeField] bool mLoop = true;
	[SerializeField] bool mPixelPerfect = true;
	
	UISprite mSprite;
	float mDelta = 0f;
	long m_startTime;
	long m_delayTime;
	int mIndex = 0;
	bool mActive = false;
	List<string> mSpriteNames = new List<string>();

	/// <summary>
	/// Number of frames in the animation.
	/// </summary>

	public int frames { get { return mSpriteNames.Count; } }

	/// <summary>
	/// Animation framerate.
	/// </summary>

	public int framesPerSecond { get { return mFPS; } set { mFPS = value; } }

	/// <summary>
	/// Set the name prefix used to filter sprites from the atlas.
	/// </summary>

	public string namePrefix { get { return mPrefix; } set { if (mPrefix != value) { mPrefix = value; RebuildSpriteList(); } } }

	/// <summary>
	/// Set the animation to be looping or not
	/// </summary>
	
	public bool isPixelPerfect { get { return mPixelPerfect; } set { mPixelPerfect = value; } }
	
	public bool loop { get { return mLoop; } set { mLoop = value; } }

	/// <summary>
	/// Returns is the animation is still playing or not
	/// </summary>

	public bool isPlaying { get { return mActive; } }

	/// <summary>
	/// Rebuild the sprite list first thing.
	/// </summary>
	
	void Start () { RebuildSpriteList(); HideAnimation();}

	/// <summary>
	/// Advance the sprite animation process.
	/// </summary>

	void Update ()
	{
		
		if (mActive && mSpriteNames.Count > 1 && Application.isPlaying && mFPS > 0f)
		{
			if((System.DateTime.Now.Ticks  / TimeSpan.TicksPerMillisecond) - m_startTime > m_delayTime)
			{
				if(m_delayTime > 0) // if we play animation with delay we want to show animtaion only after delay was finished
					mSprite.enabled = true;
				
				mDelta += Time.deltaTime;
				float rate = 1f / mFPS;
		
				if (rate < mDelta)
				{
					
					mDelta = (rate > 0f) ? mDelta - rate : 0f;
					if (++mIndex >= mSpriteNames.Count)
					{
						mIndex = 0;
						mActive = loop;
					}
		
					if (mActive)
					{
						mSprite.spriteName = mSpriteNames[mIndex];
						
						if(isPixelPerfect)
							mSprite.MakePixelPerfect();
					}
				}
			}
		}
	}
	
	/// <summary>
	/// Sets the position of the animated sprite
	/// </summary>
	public void SetPosition(Vector3 pos)
	{
		mSprite.transform.localPosition = pos;
	}
	/// <summary>
	/// Hides the animation.
	/// </summary>
	public void HideAnimation()
	{
		if(mSprite != null && mSprite.enabled)
		{
			mSprite.enabled = false;
			mActive = false;
		}
	}
	/// <summary>
	/// Plays the animation.
	/// </summary>
	public void PlayAnimation()
	{
		if(!mActive)
		{
			ResetAnimation();
			mSprite.enabled = true;
		}
	}
	/// <summary>
	/// Plays the animation after given delay.
	/// </summary>
	public void PlayAnimationWithDelay (long delayTime)
	{
		if(!mActive)
		{
			ResetAnimation();
			m_delayTime = delayTime;
			m_startTime = System.DateTime.Now.Ticks  / TimeSpan.TicksPerMillisecond;
			mSprite.enabled = false;
		}
	}

	/// <summary>
	/// Rebuild the sprite list after changing the sprite name.
	/// </summary>

	void RebuildSpriteList ()
	{
		if (mSprite == null) mSprite = this.GetComponent<UISprite>();
		mSpriteNames.Clear();

		if (mSprite != null && mSprite.atlas != null)
		{
			List<UIAtlas.Sprite> sprites = mSprite.atlas.spriteList;

			for (int i = 0, imax = sprites.Count; i < imax; ++i)
			{
				UIAtlas.Sprite sprite = sprites[i];

				if (string.IsNullOrEmpty(mPrefix) || sprite.name.StartsWith(mPrefix))
				{
					mSpriteNames.Add(sprite.name);
				}
			}
			mSpriteNames.Sort();
		}
	}
	
	/// <summary>
	/// Reset the animation to frame 0 and activate it.
	/// </summary>
	
	public void ResetAnimation()
	{
		mActive = true;
		mIndex = 0;
		m_delayTime = 0;
		m_startTime = 0;
		
		mSprite.spriteName = mPrefix + mIndex.ToString() + (mIndex+1).ToString(); // sets the sprite to the first image in sequence (starts with "01")
		if(isPixelPerfect){ mSprite.MakePixelPerfect(); }
	}
}
