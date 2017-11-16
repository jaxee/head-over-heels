using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	public GameObject player;
	public CharacterControl playerControls;
	public float maxDistance;
	private float playerPosition;
	private float verticalOffset;
	private float horizontalPosition;

	// Use this for initialization
	void Start () {
		verticalOffset = transform.position.y - player.transform.position.y;
		playerControls = player.GetComponent<CharacterControl> ();
		horizontalPosition = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {

		// Moving left
		if (playerControls.forwardForce < 0) {
			
			if (Mathf.Abs (player.transform.position.x - transform.position.x) > maxDistance) {
				if (player.transform.position.x + maxDistance < transform.position.x) {
					horizontalPosition = player.transform.position.x + maxDistance;
				}
			}
			else {
				horizontalPosition = transform.position.x;
			}

		}
		// Moving right
		if (playerControls.forwardForce > 0) {
			if (Mathf.Abs (player.transform.position.x - transform.position.x) > maxDistance) {
				if (player.transform.position.x - maxDistance > transform.position.x) {
					horizontalPosition = player.transform.position.x - maxDistance;
				}
			}
			else {
				horizontalPosition = transform.position.x;
			}
		}
			
		transform.position = new Vector3 (horizontalPosition, player.transform.position.y + verticalOffset, transform.position.z);

	}
}
