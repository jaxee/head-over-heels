using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour {

	public Button levelOne;
	public Button startLevelOne;
	public Button backToHome;
	public Button goToHome;

	// Use this for initialization
	void Start () {
		if (levelOne != null) {
			levelOne.onClick.AddListener(StartAnimaticScene);
		}

		if (startLevelOne != null) {
			startLevelOne.onClick.AddListener(StartLevelOne);
		}

		if (backToHome != null) {
			backToHome.onClick.AddListener(ReturnToHome);
		}

		if (goToHome != null) {
			goToHome.onClick.AddListener(GoToHome);
		}
	}
	
	void StartAnimaticScene () {
		Debug.Log("Begin cinematic!");
		SceneManager.LoadScene("cinematic", LoadSceneMode.Single);
	}

	void StartLevelOne () {
		Debug.Log("Begin level one!");
		SceneManager.LoadScene("LevelOne_FINAL", LoadSceneMode.Single);
	}

	void ReturnToHome () {
		Debug.Log ("Return to home screen!");
		SceneManager.LoadScene ("interface", LoadSceneMode.Single);
	}

	void GoToHome () {
		Debug.Log ("Go to home screen!");
		PlayerPrefs.DeleteAll();
		SceneManager.LoadScene ("interface", LoadSceneMode.Single);
	}
}





