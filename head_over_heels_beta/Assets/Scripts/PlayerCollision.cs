using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

	public WorldManagerScript worldManager;
	private bool invincible;
	private bool flash;
	public bool buttonPressed;

	GameObject sol;
	GameObject luna;

	AudioSource audioLoveToken;
	AudioSource audioStoryToken;
	AudioSource audioBox;

	Vector3 startingPillarPosition;
	Vector3 startingButtonPosition;


	// Use this for initialization
	void Start () {
		worldManager = GameObject.Find ("WorldManager").GetComponent<WorldManagerScript>();
		flash = true;
		sol = GameObject.Find ("SolCharacter");
		luna = GameObject.Find ("LunaCharacter");
	}
	
	// Update is called once per frame
	void Update () {
		if (invincible) {
			GetComponent<Renderer> ().enabled = flash;
		}

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

		if (col.gameObject.tag.Contains("Button") && buttonPressed == false) {

			buttonPressed = true;

			GameObject pillar = col.gameObject.GetComponent<PillarToMove>().Pillar;

			startingButtonPosition = col.gameObject.transform.position;
			startingPillarPosition = pillar.transform.position;

			Vector3 buttonEndPosition = new Vector3 (col.transform.position.x, col.transform.position.y - 0.5f, 0f );
			Vector3 pillarEndPosition = new Vector3 (pillar.transform.position.x, pillar.transform.position.y + 3f, 0f );

			StartCoroutine (MoveOverSeconds (pillar, pillarEndPosition, 3f));
			StartCoroutine (MoveOverSeconds (col.gameObject, buttonEndPosition, 1f));
		}
	}


void OnCollisionExit2D (Collision2D col) {
	
	if (col.gameObject.tag.Contains("Button") && buttonPressed == true) {

		buttonPressed = false;

		GameObject pillar = col.gameObject.GetComponent<PillarToMove>().Pillar;

		StartCoroutine (MoveOverSeconds (pillar, startingPillarPosition, 3f));
		StartCoroutine (MoveOverSeconds (col.gameObject, startingButtonPosition, 1f));
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
