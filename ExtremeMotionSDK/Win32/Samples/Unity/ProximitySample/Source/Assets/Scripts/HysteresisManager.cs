using System;

/// <summary>
/// Class that divides the Proximity-range into bars.
/// There is smoothening - a bar changes only after a certain number of 
/// values was received in the same direction, either higher or lower.
/// There is also a safety range, an "epsilon" around each bar limit.
/// </summary>
public class HysteresisManager
{
    readonly float[] r_lowThresh; 
    readonly float[] r_highThresh;
    
    readonly float[] r_innerBinBounds;
    
    const int v_NumOfBins = 5,
    			v_NumUpdatesToChangeBin = 5,
    			v_NumOppositeUpdates = 2;       

    int m_numHigherThanCurBin = 0;
    int m_numLowerThanCurBin = 0;	            
  
    public HysteresisManager()	         
	{
		const float k_MarginPercent = 0.2f, 
					k_Closest = 1.64f, // Closest proximity before "too close"
					k_Farthest = 3.11f; // Farthest proximity before "too far"
				
		float binSize = (k_Farthest - k_Closest) / (v_NumOfBins - 2); 	// The range divided by number of bins, disregarding
																		// The too close [0-1.65) and too far (3.11+) areas
		
		r_innerBinBounds = new float[v_NumOfBins - 1];
		
		// Building the inner bounds by going in bin-sized "steps"
		for (int i = 0; i < r_innerBinBounds.Length; i++)
        {
            r_innerBinBounds[i] = k_Closest + i * binSize;
        }		

        r_lowThresh = new float[v_NumOfBins];
        r_highThresh = new float[v_NumOfBins];

        r_lowThresh[0] = 0;
        r_highThresh[v_NumOfBins - 1] = float.MaxValue;

        for (int i = 0; i < r_innerBinBounds.Length; i++)
        {
	        r_lowThresh[i + 1] = r_innerBinBounds[i] - k_MarginPercent * binSize;
	        r_highThresh[i] = r_innerBinBounds[i] + k_MarginPercent * binSize;
        }			
    }

    public void SetCurrentBinIndexFromValue(float value)
    {           
        BinIndex = v_NumOfBins - 1;
        for (int i = 0; i < r_innerBinBounds.Length; i++)
        {
            if (value <= r_innerBinBounds[i])
            {
                SetCurrentBinIndex(i);
                return;
            }                
        }
    }

    public void SetCurrentBinIndex(int binIndex)
    {	        
        BinIndex = binIndex;
        m_numHigherThanCurBin = 0;
        m_numLowerThanCurBin = 0;
    }

    public void UpdateValue(float newProximity)
    {	     
		// If we went beyond the current bin's lower threshhold
        if (newProximity <= r_lowThresh[BinIndex])
        {
	        m_numLowerThanCurBin++;
        } 
		// If we went beyond the current bin's upper threshhold
        else if (newProximity >= r_highThresh[BinIndex])
        {
	        m_numHigherThanCurBin++;
        }
		
		// Checking the number of times we went in each direction to ascertain if 
		// we have a consistent pattern
		
		// If we went lower enough times to update, and didn't go higher enough times to cancel
        if (m_numLowerThanCurBin >= v_NumUpdatesToChangeBin && m_numHigherThanCurBin < v_NumOppositeUpdates) 
        {
            BinIndex = BinIndex <= 0 ? 0 : BinIndex - 1;
	        m_numLowerThanCurBin = 0;
	        m_numHigherThanCurBin = 0;
        }
		// If we went higher enough times to update, and didn't go lower enough times to cancel
        else if (m_numLowerThanCurBin < v_NumOppositeUpdates && m_numHigherThanCurBin >= v_NumUpdatesToChangeBin)
        {
	        BinIndex = BinIndex < v_NumOfBins - 1 ? BinIndex + 1 : v_NumOfBins - 1;
	        m_numLowerThanCurBin = 0;
	        m_numHigherThanCurBin = 0;
        }
		// If we went in both directions enough times to cancel either change. Panic mode.
        else if (m_numLowerThanCurBin >= v_NumOppositeUpdates && m_numHigherThanCurBin >= v_NumOppositeUpdates) 
        {
	        m_numLowerThanCurBin = 0;
	        m_numHigherThanCurBin = 0;
        }	
    }

    public int BinIndex
    {
		get; protected set;
    }
}    


