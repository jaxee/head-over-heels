using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoroidFly : MonoBehaviour {

	public GameObject player;
	public float minDistance = 10;
	private EnemyMovement movementScript;

	// Use this for initialization
	void Start () {
		movementScript = GetComponent<EnemyMovement> ();

		if (!player) {
			if (tag == "SolEnemy") {
				player = GameObject.Find ("SolCharacter");
			} else {
				player = GameObject.Find ("LunaCharacter");
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (transform.position, player.transform.position) < minDistance && !movementScript.enabled) {
			movementScript.enabled = true;
		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		Destroy(gameObject);
	}
}
