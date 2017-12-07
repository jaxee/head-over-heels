using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudEnemyScript : MonoBehaviour {

	public GameObject player;
	public float minDistance = 8;
	public float hoverSpeed = 0.1f;
	public float hoverHeight = 5f;
	public float flyAwayDistance = 10f;
	private EnemyMovement movementScript;
	private Vector3 exitPosition;
	private bool patrolling = true;
	public GameObject particle;

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

		if (Vector3.Distance (transform.position, player.transform.position) < minDistance && movementScript.enabled && patrolling) {
			movementScript.enabled = false;
			exitPosition = transform.position;
			patrolling = false;
			InvokeRepeating ("MakeItRain", 1f, 0.75f);
		}

		if (!movementScript.enabled) {
			MoveOverPlayer ();

			if (Vector3.Distance (transform.position, exitPosition) > flyAwayDistance) {
				movementScript.enabled = true;
				Invoke ("resetPatrol", 3f);
			}
		}
	}

	void MoveOverPlayer () {
		transform.position = Vector3.Lerp(transform.position, new Vector2(player.transform.position.x, player.transform.position.y + hoverHeight), hoverSpeed);
	}

	void resetPatrol () {
		patrolling = true;
		CancelInvoke ();
	}

	void MakeItRain() {
		Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(-1.0f, 1.0f), transform.position.y - Random.Range(1.25f, 2.0f), 0.0f);

		Instantiate (particle, spawnPosition, Quaternion.identity);
	}
}
