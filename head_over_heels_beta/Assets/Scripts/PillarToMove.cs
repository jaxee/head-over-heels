using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarToMove : MonoBehaviour {

	public GameObject Pillar;

	public Vector3 startingButtonPosition;
	public Vector3 startingPillarPosition;
	public Vector3 buttonEndPosition;
	public Vector3 pillarEndPosition;

	// Use this for initialization
	void Start () {
		startingButtonPosition = transform.position;
		startingPillarPosition = Pillar.transform.position;
		buttonEndPosition = new Vector3 (transform.position.x, transform.position.y - 0.5f, 0f );
		pillarEndPosition = new Vector3 (Pillar.transform.position.x, Pillar.transform.position.y + 3f, 0f );
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
