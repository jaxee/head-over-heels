using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamaged : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag.Contains ("Obstacle")) {
			//Play dead animation
			//Destroy(gameObject);
			gameObject.SetActive(false);

		}
		if (col.gameObject.transform.childCount > 0) {
			if (col.gameObject.transform.GetChild (0).gameObject.tag.Contains ("Obstacle")) {
				//Destroy(gameObject);
				gameObject.SetActive(false);

			}
		}


	}
}
