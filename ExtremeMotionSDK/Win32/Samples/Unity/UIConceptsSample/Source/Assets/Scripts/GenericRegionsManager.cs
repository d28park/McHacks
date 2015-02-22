using System.Collections.Generic;
using UnityEngine;

public class GenericRegionsManager{
	
	private List<Region> m_regions = new List<Region>();
	private int m_currentActiveRegionID = -1;
	private List<int>  m_currentActiveRegionsID = new List<int> { -1, -1 };
	/// <summary>
	/// Adds a new region.
	/// </summary>
	/// <returns>
	/// The new region id.
	/// </returns>
	/// <param name='topLeftX'>
	/// Top left x pos.
	/// </param>
	/// <param name='topLeftY'>
	/// Top left y pos..
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
	public int AddRegion(float topLeftX,float topLeftY, float width, float height,float hysteX, float hysteY)
	{	
		m_regions.Add(new Region(topLeftX,topLeftY,width, height,hysteX,hysteY));
		return m_regions.Count-1;
	}
	
	public void EnableRegion(int regionID,bool enable)
	{
		m_regions[regionID].IsEnabled(enable);
	}
	
	/// <summary>
	/// Gets the current active region include region hysteressis according to palm position.
	/// </summary>
	/// <returns>
	/// The active region.
	/// </returns>
	/// <param name='palmPosition'>
	/// Palm position.
	/// </param>
	public bool IsInRegionWithHyst(Vector2 palmPosition, int regionID)
	{
		return m_regions[regionID].IsStillActive(palmPosition);
	}
	
	/// <summary>
	/// Gets the current active region according to palm position.
	/// </summary>
	/// <returns>
	/// The active region.
	/// </returns>
	/// <param name='palmPosition'>
	/// Palm position.
	/// </param>
	public int GetActiveRegion(Vector2 palmPosition)
	{
		//checking if currentActiveRegionID is still active (still in region + hysteressis)
		//if not, searching for new active region
		if(!(m_currentActiveRegionID != -1 && m_regions[m_currentActiveRegionID].IsStillActive(palmPosition)))
		{
			m_currentActiveRegionID = -1;
			for (int i = 0; i < m_regions.Count; i++) {
				if(m_regions[i].IsSelected(palmPosition))
				{
					m_currentActiveRegionID = i;
					break;
				}
			}
		}
		return m_currentActiveRegionID;
	}
	
	/// <summary>
	/// Gets all active region according to palms position.
	/// </summary>
	/// <returns>
	/// The active regions.
	/// </returns>
	/// <param name='palmPosition'>
	/// Palm position.
	/// </param>
	public List<int> GetActiveRegions(Vector2[] palmsPosition)
	{
		
		//checking if currentActiveRegionID is still active (still in region + hysteressis)
		//if not, searching for new active region
		for (int i = 0; i < palmsPosition.Length; i++) {
			
			if(!(m_currentActiveRegionsID[i] != -1 && m_regions[m_currentActiveRegionsID[i]].IsStillActive(palmsPosition[i])))
			{
				m_currentActiveRegionsID[i] = -1;
				for (int j = 0; j < m_regions.Count; j++) {
					if(m_regions[j].IsSelected(palmsPosition[i]))
					{
						m_currentActiveRegionsID[i] = j;
						break;
					}
				}
			}
		}
		return m_currentActiveRegionsID;
	}
}
