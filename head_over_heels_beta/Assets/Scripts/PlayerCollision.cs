using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

	public WorldManagerScript worldManager;
	private bool invincible;
	private bool flash;
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
				invincible = true;
				worldManager.playerLives--;
				Invoke ("resetInvulnerability", 2);
				InvokeRepeating ("hitEffect", 0, 0.25f);
			}
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Goal") {
			GetComponent<PlayerController> ().hasReachedGoal = true;
		} else if (col.gameObject.tag == "LoveToken") {
			Destroy (col.gameObject);
			worldManager.loveTokens++;
			worldManager.setTokenText ();
		}
		else if (col.gameObject.tag == "StoryToken") {
			Destroy (col.gameObject);
			worldManager.storyTokens++;
			worldManager.setTokenText ();
		}
		else if (col.gameObject.tag == "Pit") {
			Destroy (col.gameObject);
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

}
