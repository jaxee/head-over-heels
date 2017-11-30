using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	public GameObject player;
	public PlayerController playerController;
	public float maxHorizontalDistance;
	public float minVerticalDistance;
	public float maxVerticalDistance;
	private float playerPosition;
	private float verticalOffset;
	private float horizontalPosition;
	private float verticalPosition;

	// Use this for initialization
	void Start () {
		verticalOffset = transform.position.y - player.transform.position.y;
		playerController = player.GetComponent<PlayerController> ();
		horizontalPosition = transform.position.x;
		verticalPosition = player.transform.position.y + verticalOffset;
	}
	
	// Update is called once per frame
	void Update () {

		// Jumping up
		if (playerController.playerRigidBody.velocity.y > 0) {
			if (Mathf.Abs (player.transform.position.y - verticalPosition) < minVerticalDistance) {
				verticalPosition = player.transform.position.y + minVerticalDistance;
			}
		}

		// Falling down
		if (playerController.playerRigidBody.velocity.y < 0) {
			if (Mathf.Abs (player.transform.position.y - verticalPosition) > maxVerticalDistance) {
				verticalPosition = player.transform.position.y + maxVerticalDistance;
			}
		}

		// Moving left
		if (playerController.playerRigidBody.velocity.x < 0) {
			if (Mathf.Abs (player.transform.position.x - transform.position.x) > maxHorizontalDistance) {
				if (player.transform.position.x + maxHorizontalDistance < transform.position.x) {
					horizontalPosition = player.transform.position.x + maxHorizontalDistance;
				}
			}
			else {
				horizontalPosition = transform.position.x;
			}
		}
		// Moving right
		if (playerController.playerRigidBody.velocity.x > 0) {
			if (Mathf.Abs (player.transform.position.x - transform.position.x) > maxHorizontalDistance) {
				if (player.transform.position.x - maxHorizontalDistance > transform.position.x) {
					horizontalPosition = player.transform.position.x - maxHorizontalDistance;
				}
			}
			else {
				horizontalPosition = transform.position.x;
			}
		}


			
		transform.position = new Vector3 (horizontalPosition, verticalPosition, transform.position.z);

	}
}
