using UnityEngine;
using System.Collections;

public class ResolutionController : MonoBehaviour {
    
	private float Resolution4X3Cutoff = 1.65f;
    public UIAtlas ReferenceAtlas;
    private UIAtlas ReplacementAtlas;
	
	private string Atlas4x3Name = "4x3_Atlas";
	private string Atlas16x9Name = "16x9_Atlas";

	public enum AspectRatios
	{
		Aspect_16x9 = 0,
		Aspect_4x3 = 1
	};
	public static AspectRatios aspectRatio = AspectRatios.Aspect_16x9;
	public static float screenWidth;
    // Use this for initialization
    void Awake () {
		
		CalculateScreenSize();
		
		UICamera cam = GetComponentInChildren<UICamera>();
		float aspect = cam.camera.aspect;
		
		if(aspect >= Resolution4X3Cutoff){
			aspectRatio = AspectRatios.Aspect_16x9;
			ReplacementAtlas =  Resources.Load("GUI/16x9/Atlas/" + Atlas16x9Name, typeof(UIAtlas)) as UIAtlas;
		 }
		else
		{
			aspectRatio = AspectRatios.Aspect_4x3;
			ReplacementAtlas =  Resources.Load("GUI/4x3/Atlas/" + Atlas4x3Name,typeof(UIAtlas)) as UIAtlas;
       		
		}
		ReferenceAtlas.replacement = ReplacementAtlas;
		NGUITools.Broadcast("MakePixelPerfect");
    }
	
	private void CalculateScreenSize(){
 
		float ratio = (float)GetComponent<UIRoot>().activeHeight / Screen.height;
 
		screenWidth = Mathf.Ceil(Screen.width * ratio);
	}
}