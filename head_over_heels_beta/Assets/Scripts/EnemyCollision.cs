using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour {

	public WorldManagerScript worldManager;
	private bool invincible;
	private bool flash;
	private Rigidbody2D rigidbody;
	public bool solbuttonPressed;
	public bool lunabuttonPressed;

	// Use this for initialization
	void Start () {
		worldManager = GameObject.Find ("WorldManager").GetComponent<WorldManagerScript>();
		flash = true;
		solbuttonPressed = false;
		lunabuttonPressed = false;
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

		if (col.gameObject.name == "sol_button" && solbuttonPressed == false) {

			GameObject pillar = GameObject.Find ("luna_pillar");
			GameObject button = GameObject.Find ("sol_button");

			Vector2 buttonEndPosition = new Vector2 (button.transform.position.x, (button.transform.position.y - 0.5f) );

			Vector2 endPosition = new Vector2 (pillar.transform.position.x, (pillar.transform.position.y + 3) );
			if (solbuttonPressed == false) {	
				StartCoroutine (MoveOverSeconds (pillar, endPosition, 3f));
				StartCoroutine (MoveOverSeconds (button, buttonEndPosition, 1f));

			}
			solbuttonPressed = true;



		}
		if (col.gameObject.name != "sol_button" && solbuttonPressed == true) {
			GameObject pillar = GameObject.Find ("luna_pillar");
			GameObject button = GameObject.Find ("sol_button");

			Vector2 buttonEndPosition = new Vector2 (button.transform.position.x, (button.transform.position.y + 0.5f) );

			Vector2 endPosition = new Vector2 (pillar.transform.position.x, (pillar.transform.position.y - 3) );
			if (solbuttonPressed == true) {	
				StartCoroutine (MoveOverSeconds (pillar, endPosition, 3f));
				StartCoroutine (MoveOverSeconds (button, buttonEndPosition, 1f));

			}
			solbuttonPressed = false;

		}

		if (col.gameObject.name == "luna_button"  && lunabuttonPressed == false) {

			GameObject pillar = GameObject.Find ("sol_pillar");
			GameObject button = GameObject.Find ("luna_button");
			Vector2 endPosition = new Vector2 (pillar.transform.position.x, (pillar.transform.position.y + 3) );
			Vector2 buttonEndPosition = new Vector2 (button.transform.position.x, (button.transform.position.y - 0.5f) );

			if (lunabuttonPressed == false) {
				StartCoroutine (MoveOverSeconds (pillar, endPosition, 3f));
				StartCoroutine (MoveOverSeconds (button, buttonEndPosition, 1f));

			}
			lunabuttonPressed = true;


		}

		if (col.gameObject.name != "luna_button" && lunabuttonPressed == true) {

			GameObject pillar = GameObject.Find ("sol_pillar");
			GameObject button = GameObject.Find ("luna_button");

			Vector2 buttonEndPosition = new Vector2 (button.transform.position.x, (button.transform.position.y + 0.5f) );

			Vector2 endPosition = new Vector2 (pillar.transform.position.x, (pillar.transform.position.y - 3) );
			if (lunabuttonPressed == true) {	
				StartCoroutine (MoveOverSeconds (pillar, endPosition, 3f));
				StartCoroutine (MoveOverSeconds (button, buttonEndPosition, 1f));

			}
			lunabuttonPressed = false;

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
		else if (col.gameObject.tag == "Pit") {
			Destroy (col.gameObject);
			Debug.Log ("Falling into the pit");
			worldManager.playerLives = 0;

		}

	}




	public IEnumerator MoveOverSeconds (GameObject objectToMove, Vector3 end, float seconds)
	{
		float elapsedTime = 0;
		Vector3 startingPos = objectToMove.transform.position;
		while (elapsedTime < seconds)
		{
			objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		objectToMove.transform.position = end;
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
