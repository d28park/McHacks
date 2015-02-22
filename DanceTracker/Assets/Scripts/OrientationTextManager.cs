using UnityEngine;
using System.Collections;

public class OrientationTextManager : MonoBehaviour {
	
	public TextMesh MessageText;
	public TextMesh MessageTextUpsideDown;
	
	void Awake () {
		MessageText.renderer.material.color = Color.red;
		MessageTextUpsideDown.renderer.material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
		MessageText.renderer.enabled = Input.deviceOrientation == DeviceOrientation.Portrait;
		MessageTextUpsideDown.renderer.enabled = Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown;
	}
}
