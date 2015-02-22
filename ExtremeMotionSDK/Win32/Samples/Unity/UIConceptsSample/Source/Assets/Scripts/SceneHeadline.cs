using UnityEngine;
using System.Collections;

public class SceneHeadline : MonoBehaviour {
	
	private UILabel headline;
	public UILabel description;
	private float fontSize = 36;
	
	void Awake()
	{
		headline = GetComponent<UILabel>();
	}
	
	void Start()
	{
		transform.localScale = new Vector3(fontSize,fontSize,0);
	}
	/// <summary>
	/// Sets the position of the headline.
	/// </summary>
	/// <param name='newPosition'>
	/// The new position.
	/// </param>
	public void SetPosition(Vector3 newPosition)
	{
		this.transform.localPosition = newPosition;
	}
	/// <summary>
	/// Sets the headline text.
	/// </summary>
	/// <param name='newText'>
	/// the new text.
	/// </param>
	public void SetText(string newText)
	{
		if(headline != null){
			headline.text = newText;
		}
	}
	
	public void ChangeLayout()
	{
		if(ResolutionController.aspectRatio == ResolutionController.AspectRatios.Aspect_4x3)
		{
			description.transform.localPosition = new Vector3(300,-100,-1);
			description.text = "Reach to 'touch' an item and wait for a click";
			headline.transform.localPosition = new Vector3(300,-50,-1);
		}
	}
}
