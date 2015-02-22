using UnityEngine;
using System.Collections;

public class TargetUpdate : MonoBehaviour {

	private bool left;

	// Use this for initialization
	void Start () {
		left = true;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonUp ("Fire1")) {
			transform.Translate(0.5f, 0, 0);		
		}
	}

	void OnTriggerEnter(Collider other) {
		if ((other.gameObject.tag == "lefthand") && (left)) {
			transform.Translate(0.5f, 0, 0);
			left = false;
		}
		if ((other.gameObject.tag == "righthand") && (!left)) {
			transform.Translate(-0.5f, 0, 0);
			left = true;
		}
	}
}
