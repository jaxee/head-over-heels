using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour {

	public WorldManagerScript worldManager;
	private bool invincible;
	private bool flash;
	private Rigidbody2D rigidbody;

	// Use this for initialization
	void Start () {
		worldManager = GameObject.Find ("WorldManager").GetComponent<WorldManagerScript>();
		flash = true;
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Renderer> ().enabled = flash;
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (!invincible) {
			if (col.gameObject.tag.Contains ("Obstacle") || col.gameObject.tag.Contains ("Enemy")) {
				invincible = true;
				worldManager.playerLives--;
				Invoke ("resetInvulnerability", 2);
				InvokeRepeating ("hitEffect", 0, 0.25f);
			}
		}

		if (col.gameObject.name.Contains("box") || col.gameObject.name.Contains("Box")) {
			//If tackling, also get tackle force
			/* Remove for now due to bugs
			if (GetComponent<Animator> ().GetBool ("Tackle")) {
				Rigidbody2D rigidbody = col.gameObject.GetComponent<Rigidbody2D> ();
				rigidbody.bodyType = RigidbodyType2D.Dynamic;
			}
			*/
		}
	}

	void OnCollisionStay2D (Collision2D col)
	{
		if (!invincible) {
			if (col.gameObject.tag.Contains ("Obstacle") || col.gameObject.tag.Contains ("Enemy")) {
				Debug.Log ("hit");
				invincible = true;
				worldManager.playerLives--;
				Invoke ("resetInvulnerability", 2);
				InvokeRepeating ("hitEffect", 0, 0.25f);
			}
		}

		if (col.gameObject.name.Contains("box") || col.gameObject.name.Contains("Box")) {
			// If tackling
			/* Remove for now due to bugs
			if (GetComponent<Animator> ().GetBool ("Tackle")) {
				rigidbody = col.gameObject.GetComponent<Rigidbody2D> ();
				rigidbody.bodyType = RigidbodyType2D.Dynamic;
				Invoke ("stopMovingBox", 1.0f);
			}
			*/
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Goal") {
			GetComponent<CharacterControl> ().hasReachedGoal = true;
			Debug.Log ("Goal reached");
		} else if (col.gameObject.tag == "LoveToken") {
			Destroy (col.gameObject);
			worldManager.loveTokens++;
			worldManager.setTokenText ();
			Debug.Log ("Loooooove Token, Baby");
			Debug.Log (worldManager.loveTokens);
		}
	}

	void resetInvulnerability()
	{
		invincible = false;
		CancelInvoke ();
		flash = true;
	}

	void hitEffect()
	{
		flash = !flash;
	}

	void stopMovingBox()
	{
		rigidbody.bodyType = RigidbodyType2D.Static;
		GetComponent<CharacterControl> ().forwardForce = 10;
	}

}
