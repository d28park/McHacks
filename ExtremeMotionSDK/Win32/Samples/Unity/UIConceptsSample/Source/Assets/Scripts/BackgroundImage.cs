using UnityEngine;
using System.Collections;

public class BackgroundImage : MonoBehaviour {
	
	
	private string Image_4x3 = "bg_2048x1536";
	private string Image_16x9 = "bg_1920x1080";
	// Use this for initialization
	void Awake () {
		
		if(ResolutionController.aspectRatio == ResolutionController.AspectRatios.Aspect_16x9)
		{
			GetComponent<UITexture>().material.mainTexture = Resources.Load("Materials/" + Image_16x9) as Texture;
		}
		else
		{
			GetComponent<UITexture>().material.mainTexture = Resources.Load("Materials/" + Image_4x3) as Texture;
		}	
	}
	void Start()
	{
	}
}
