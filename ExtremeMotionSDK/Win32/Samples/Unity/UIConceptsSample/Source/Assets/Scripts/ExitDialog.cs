using UnityEngine;
using System.Collections;

public class ExitDialog : MonoBehaviour {
	
	public GameObject exitDialog;
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			exitDialog.SetActive(true);
		}
	}
	
	void Hide()
	{
		exitDialog.SetActive(false);
	}
	
	void Quit()
	{
		Application.Quit();
	}
}	
