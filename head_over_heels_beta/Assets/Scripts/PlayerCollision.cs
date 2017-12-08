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
	AudioSource audioGoal;
	public AudioSource audioHit;

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
				audioHit.Play ();
			}
		}

		if (col.gameObject.tag.Contains("Button")) {

			GameObject pillar = col.gameObject.GetComponent<PillarToMove>().Pillar;

			StartCoroutine (MoveOverSeconds (pillar, col.gameObject.GetComponent<PillarToMove>().pillarEndPosition, 1f));
			StartCoroutine (MoveOverSeconds (col.gameObject, col.gameObject.GetComponent<PillarToMove>().buttonEndPosition, 1f));

			Debug.Log ("Pressed button");
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
				audioHit.Play ();
			}
		}
	}

	void OnCollisionExit2D(Collision2D col) {
		if (col.gameObject.tag.Contains("Button")) {

			GameObject pillar = col.gameObject.GetComponent<PillarToMove>().Pillar;

			StartCoroutine (MoveOverSeconds (pillar, col.gameObject.GetComponent<PillarToMove>().startingPillarPosition, 1f));
			StartCoroutine (MoveOverSeconds (col.gameObject, col.gameObject.GetComponent<PillarToMove>().startingButtonPosition, 1f));

			Debug.Log ("Released button");

		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Goal") {
			audioGoal = col.gameObject.GetComponent<AudioSource> ();
			audioGoal.Play ();

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
			//audioBox.Stop ();
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
