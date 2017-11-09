using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSwitch : MonoBehaviour {

	private bool shouldRotate;
	private bool shouldSlide;
	public GameObject worldPivot;
	public GameObject worldGroup;

	void Start() {

		worldPivot = GameObject.Find ("WorldPivot");
		worldGroup = GameObject.Find ("WorldGroup");
	}
	
	// Update is called once per frame
	void Update () {

		if(shouldRotate)
		{
			worldPivot.transform.Rotate (new Vector3 (0, 0, -1 * Time.deltaTime * 200));
			//worldPivot.transform.Rotate(Vector3.back * Time.deltaTime *40, Space.World);

			if (worldPivot.transform.rotation.eulerAngles.z <= 180) {
				worldPivot.transform.eulerAngles = new Vector3(0, 0, 180);
				shouldRotate = false;
				shouldSlide = true;
			}
		}
		if (shouldSlide) 
		{
			worldGroup.transform.Translate(new Vector3 (0, 1 * Time.deltaTime * 10, 0));

			Debug.Log (worldGroup.transform.position.y);

			if (worldGroup.transform.position.y <= -1.8f) {
				worldGroup.transform.position = new Vector3(0, -1.8f, 0);
				shouldSlide = false;
			}
		}
	}

	public void OnPress()
	{
		shouldRotate = !shouldRotate;
	}
}
