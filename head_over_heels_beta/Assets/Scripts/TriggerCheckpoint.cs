using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheckpoint : MonoBehaviour {

	private bool triggered = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Player" && !triggered) {
			triggered = true;
			SpriteRenderer[] checkpoints = GetComponentsInChildren<SpriteRenderer> ();
			foreach (SpriteRenderer renderer in checkpoints) {
				renderer.enabled = !renderer.enabled;
			}
		}
	}
}
