using UnityEngine;
using System.Collections;

public class Region{
	
	private Rect m_regionRect;
	private Rect m_originalAreaRect;
	private bool m_isEnabled = true;
	private float m_hysteX;
	private float m_hysteY;
	/// <summary>
	/// Initializes a new instance of a region
	/// </summary>
	/// <param name='x'>
	/// top left x pos
	/// </param>
	/// <param name='y'>
	/// top left y pos
	/// </param>
	/// <param name='width'>
	/// region width.
	/// </param>
	/// <param name='height'>
	/// region height.
	/// </param>
	/// <param name='hysteX'>
	/// Horizontal (top&bottom) Hysteressis threshold.
	/// </param>
	/// <param name='hysteY'>
	/// Vertical (left&right) Hysteressis threshold.
	/// </param>
	public Region(float x, float y, float width, float height,float hysteX,float hysteY)
	{
		// initilize top area rect
		m_originalAreaRect = new Rect(x,y,width,height);
		m_regionRect = new Rect(m_originalAreaRect);
		m_hysteX = hysteX;
		m_hysteY = hysteY;
	}
	/// <summary>
	/// Determines whether the cursor instance is in bounderis of the clickable area
	/// </summary>
	public bool IsSelected (Vector2 cursorPosition)
	{
		if(m_isEnabled)
		{
			if(cursorPosition.x >= m_regionRect.x &&
				cursorPosition.x <= m_regionRect.x + m_regionRect.width &&
				cursorPosition.y <= m_regionRect.y &&
				cursorPosition.y >= m_regionRect.y - m_regionRect.height)
			{
				return true;
			}
		}
		return false;
	}
	
	public void IsEnabled(bool enabled)
	{
		m_isEnabled = enabled;
	}
	
	/// <summary>
	/// //checking if current active region is still active (still in region + hysteressis)
	/// </summary>
	/// <returns>
	/// <c>true</c> if current region is still active; otherwise, <c>false</c>.
	/// </returns>
	/// <param name='cursorPosition'>
	/// the position of the cursor
	/// </param>
	public bool IsStillActive (Vector2 cursorPosition)
	{
		if(m_isEnabled)
		{
			if(cursorPosition.x >= m_regionRect.x - m_hysteX &&
				cursorPosition.x <= m_regionRect.x + (m_regionRect.width + m_hysteX) &&
				cursorPosition.y <= m_regionRect.y + m_hysteY  &&
				cursorPosition.y >= m_regionRect.y - (m_regionRect.height + m_hysteY))
			{
				return true;
			}
		}
		return false;
	}
}
