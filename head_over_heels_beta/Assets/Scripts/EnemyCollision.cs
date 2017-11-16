using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour {

	public WorldManagerScript worldManager;
	private bool invincible;
	private bool flash;

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
				Debug.Log ("hit");
				invincible = true;
				worldManager.playerLives--;
				Invoke ("resetInvulnerability", 2);
				InvokeRepeating ("hitEffect", 0, 0.25f);
			}
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

}
