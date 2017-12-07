using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoroidFly : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("ok");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		Destroy(gameObject);
	}
}
