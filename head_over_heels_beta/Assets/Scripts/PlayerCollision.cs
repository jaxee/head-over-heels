using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

	public WorldManagerScript worldManager;
	private bool invincible;
	private bool flash;
	public bool solbuttonPressed;
	public bool lunabuttonPressed;
	GameObject lunaPillar;
	GameObject solButton;
	GameObject solPillar;
	GameObject lunaButton;
	GameObject sol;
	GameObject luna;

	AudioSource audioLoveToken;
	AudioSource audioStoryToken;
	AudioSource audioBox;

	Vector2 solButtonStartPosition;
	Vector2 solButtonEndPosition;
	Vector2 lunaPillarStartPosition;
	Vector2 lunaPillarEndPosition;

	Vector2 lunaButtonStartPosition;
	Vector2 lunaButtonEndPosition;
	Vector2 solPillarStartPosition;
	Vector2 solPillarEndPosition;

	// Use this for initialization
	void Start () {
		worldManager = GameObject.Find ("WorldManager").GetComponent<WorldManagerScript>();
		flash = true;
		solbuttonPressed = false;
		lunabuttonPressed = false;
	 	lunaPillar = GameObject.Find ("luna_pillar");
		sol = GameObject.Find ("SolCharacter");
		luna = GameObject.Find ("LunaCharacter");




		solButton = GameObject.Find ("sol_button");
		solPillar = GameObject.Find ("sol_pillar");
		lunaButton = GameObject.Find ("luna_button");
		GetComponent<Renderer> ().enabled = flash;
		solButtonStartPosition = new Vector2 (solButton.transform.position.x, solButton.transform.position.y );
		solButtonEndPosition = new Vector2 (solButton.transform.position.x, (-27.37f) );
		lunaPillarStartPosition = new Vector2 (lunaPillar.transform.position.x, lunaPillar.transform.position.y );
		lunaPillarEndPosition = new Vector2 (lunaPillar.transform.position.x, (lunaPillar.transform.position.y + 3f) );

		lunaButtonStartPosition = new Vector2 (lunaButton.transform.position.x, lunaButton.transform.position.y );
		lunaButtonEndPosition = new Vector2 (lunaButton.transform.position.x, (lunaButton.transform.position.y - 0.5f) );
		solPillarStartPosition = new Vector2 ( solPillar.transform.position.x,  solPillar.transform.position.y );
		solPillarEndPosition = new Vector2 ( solPillar.transform.position.x, ( solPillar.transform.position.y + 3f) );

	}
	
	// Update is called once per frame
	void Update () {

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

		//Moving Sol button down, and Luna pillar up
		if (col.gameObject.name == "sol_button" && solbuttonPressed == false) {


			if (solbuttonPressed == false) {
				StartCoroutine (MoveOverSeconds (lunaPillar, lunaPillarEndPosition, 3f));
				StartCoroutine (MoveOverSeconds (solButton, solButtonEndPosition, 1f));

			}

			solbuttonPressed = true;

		}
		//Else - Moving Sol button back up, and Luna pillar back down
		if ( col.gameObject.name != "sol_button" && solbuttonPressed == true) {
	
			if (solbuttonPressed == true) {	
				StartCoroutine (MoveOverSeconds (lunaPillar, lunaPillarStartPosition, 3f));
				StartCoroutine (MoveOverSeconds (solButton, solButtonStartPosition, 1f));

			}
			solbuttonPressed = false;

		}

		if (col.gameObject.name == "luna_button"  && lunabuttonPressed == false) {


			if (lunabuttonPressed == false) {
				StartCoroutine (MoveOverSeconds (solPillar, solPillarEndPosition, 3f));
				StartCoroutine (MoveOverSeconds (lunaButton, lunaButtonEndPosition, 1f));
			}
			lunabuttonPressed = true;


		}

		if (col.gameObject.name != "luna_button" && lunabuttonPressed == true) {


			if (lunabuttonPressed == true) {
				StartCoroutine (MoveOverSeconds (solPillar, solPillarStartPosition, 3f));
				StartCoroutine (MoveOverSeconds (lunaButton, lunaButtonStartPosition, 1f));

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
			audioLoveToken = col.gameObject.GetComponent<AudioSource> ();
			audioLoveToken.Play ();
			col.gameObject.GetComponent<Renderer> ().enabled = false;
			Destroy (col.gameObject, audioLoveToken.clip.length);
			worldManager.loveTokens++;
			PlayerPrefs.SetInt ("LoveTokens", worldManager.loveTokens);
			worldManager.setTokenText ();
		} else if (col.gameObject.tag == "StoryToken") {
			audioStoryToken = col.gameObject.GetComponent<AudioSource> ();
			audioStoryToken.Play ();
			col.gameObject.GetComponent<Renderer> ().enabled = false;
			Destroy (col.gameObject, audioStoryToken.clip.length);
			worldManager.storyTokens++;
			PlayerPrefs.SetInt ("StoryboardTokens", 1);
			worldManager.setStoryTokenText ();
		} else if (col.gameObject.tag == "Pit") {
			Destroy (col.gameObject);
			worldManager.playerLives = 0;

		} else if (col.gameObject.tag == "Box") {
			audioBox = col.gameObject.GetComponent<AudioSource> ();
			audioBox.Play ();
			//Destroy(col.gameObject, audioBox.clip.length);

		} else {
			audioBox.Stop ();
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
