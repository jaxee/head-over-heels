using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSwitch : MonoBehaviour {

	private bool shouldRotate;
	private bool shouldSlide;
	private bool switching;
	public GameObject worldPivot;
	public GameObject worldGroup;
	public GameObject solCharacter;
	public GameObject lunaCharacter;
	bool solActive = true;

	void Start() {
		worldPivot = GameObject.Find ("WorldPivot");
		worldGroup = GameObject.Find ("WorldGroup");
		solCharacter = GameObject.Find ("SolCharacter");
		lunaCharacter = GameObject.Find ("LunaCharacter");
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

			Debug.Log (worldPivot.transform.rotation.eulerAngles.z);
		}
		if (shouldSlide) 
		{
			if (solActive) {
				
				worldGroup.transform.Translate (new Vector3 (0, 1 * Time.deltaTime * 10, 0));

				if (worldGroup.transform.position.y <= -1.8f) {
					worldGroup.transform.position = new Vector3 (0, -1.8f, 0);
					shouldSlide = false;
					switchActiveCharacter ();
					switching = false;
				}
			} 
			else {
				worldGroup.transform.Translate (new Vector3 (0, -1 * Time.deltaTime * 10, 0));

				if (worldGroup.transform.position.y <= -1.8f) {
					worldGroup.transform.position = new Vector3 (0, -1.8f, 0);
					shouldSlide = false;
					switchActiveCharacter ();
					switching = false;
				}
			}
		}
	}

	public void OnPress()
	{
		if (!switching) {
			shouldRotate = !shouldRotate;
		}
	}

	public void switchActiveCharacter()
	{
		solActive = !solActive;

		CharacterControl solControlScript = solCharacter.GetComponent<CharacterControl> ();
		CharacterControl lunaControlScript = lunaCharacter.GetComponent<CharacterControl> ();

		if (solActive) {
			lunaControlScript.forwardForce = 0;
			solControlScript.forwardForce = 5;
		} else {
			solControlScript.forwardForce = 0;
			lunaControlScript.forwardForce = 5;
		}
	}
}
