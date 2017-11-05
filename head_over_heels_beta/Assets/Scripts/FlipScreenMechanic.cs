using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipScreenMechanic : MonoBehaviour {

	public Button flipScreen;
	public Camera lunaCam;
	public Camera solCam;
	bool isLunaCam = true;

	// Use this for initialization
	void Start () {
		Button btn = flipScreen.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}
	
	void TaskOnClick() {
		Debug.Log("FLIP THE DAMN SCREEN!");

		if (isLunaCam) {

			isLunaCam = false;
		} else {

			isLunaCam = true;
		}
	}
}
