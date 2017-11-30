using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleImage : MonoBehaviour {
	public WorldManagerScript worldManager;

	public Sprite musicOn;
	public Sprite musicOff;
	public Sprite soundOn;
	public Sprite soundOff;
	public Sprite rotationOn;
	public Sprite rotationOff;

	private Image musicImage;
	private Image soundImage;
	private Image manualRotationImage;

	// Use this for initialization
	void Start () {
		musicImage = GameObject.Find("Music_Btn").GetComponent<Image>();
		soundImage = GameObject.Find("Sound_Btn").GetComponent<Image> ();
		manualRotationImage = GameObject.Find("Rotate_Btn").GetComponent<Image> ();

		musicImage.sprite = musicOn;
		soundImage.sprite = soundOn;
		manualRotationImage.sprite = rotationOn;
	}

	public void switchMusicSprite() {
		if (worldManager.isMusicOn) {
			musicImage.sprite = musicOff;
		} else {
			musicImage.sprite = musicOn;
		}
	}

	public void switchSoundSprite() {
		if (worldManager.isSoundOn) {
			soundImage.sprite = soundOff;
		} else {
			soundImage.sprite = soundOn;
		}
	}

	public void switchRotationSprite() {
		if (worldManager.isManualRotationOn) {
			manualRotationImage.sprite = rotationOff;
		} else {
			manualRotationImage.sprite = rotationOn;
		}
	}
}
