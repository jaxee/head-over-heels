using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSwitch : MonoBehaviour {

	private bool shouldRotate;
	AudioSource buttonSound;
	private bool shouldSlide;
	private bool switching;
	public GameObject worldPivot;
	public GameObject worldGroup;
	public WorldManagerScript worldManager;
	bool solActive = true;

	void Start() {
		worldPivot = GameObject.Find ("WorldPivot");
		worldGroup = GameObject.Find ("WorldGroup");
		worldManager = GameObject.Find ("WorldManager").GetComponent<WorldManagerScript>();
	}
	
	// Update is called once per frame
	void Update () {

		if(shouldRotate)
		{
			switching = true;

			if (solActive) {
				worldPivot.transform.Rotate (new Vector3 (0, 0, -1 * Time.deltaTime * 200));

				if (worldPivot.transform.rotation.eulerAngles.z <= 180) {
					worldPivot.transform.eulerAngles = new Vector3 (0, 0, 180);
					shouldRotate = false;
					shouldSlide = true;
				}
			} 
			else {
				worldPivot.transform.Rotate (new Vector3 (0, 0, -1 * Time.deltaTime * 200));

				if (worldPivot.transform.rotation.eulerAngles.z <= 1 || worldPivot.transform.rotation.eulerAngles.z >= 350) {
					worldPivot.transform.eulerAngles = new Vector3 (0, 0, 0);
					shouldRotate = false;
					shouldSlide = true;
				}
			}
		}
		if (shouldSlide) 
		{
			if (solActive) {
				
				worldGroup.transform.Translate (new Vector3 (0, 1 * Time.deltaTime * 10, 0));

				if (worldGroup.transform.position.y <= -1.8f) {
					worldGroup.transform.position = new Vector3 (0, -1.8f, 0);
					shouldSlide = false;
					switching = false;
					worldManager.SwitchActiveWorld (solActive);
					solActive = !solActive;
				}
			} 
			else {
				worldGroup.transform.Translate (new Vector3 (0, -1 * Time.deltaTime * 10, 0));

				if (worldGroup.transform.position.y <= -1.8f) {
					worldGroup.transform.position = new Vector3 (0, -1.8f, 0);
					shouldSlide = false;
					switching = false;
					worldManager.SwitchActiveWorld (solActive);
					solActive = !solActive;
				}
			}
		}
	}

	public void OnClick()
	{
		Debug.Log ("Play");
		buttonSound = gameObject.GetComponent<AudioSource> ();
		buttonSound.Play ();

		if (!switching) {
			shouldRotate = !shouldRotate;
		}
	}
}
