using System;
using UnityEngine;

public class VersionManager : MonoBehaviour {

	private const int TEXT_SIZE = 25;  // remove when updating NGUI !!!
	private UILabel myText;
	
	void Start () {
		myText = GetComponent<UILabel>();
   		Version assemblyVersion = Xtr3D.Net.HelperMethods.GetAssemblyVersion();
		myText.text = "Version: 1.0." + assemblyVersion.Revision;
		myText.gameObject.transform.localScale = new Vector3(TEXT_SIZE, TEXT_SIZE);
	}
}