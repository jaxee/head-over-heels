using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour {

	public float forwardForce;
	public bool forwardForceToggle;
	public bool inAir;
	public int jumpCount;
	public float jumpSpeed;
	public float tapSpeed = 0.01f; //in seconds
	private float lastTapTime = 0f;
	public Rigidbody2D rb2D;
	public int maxJumps = 1; //Maximum amount of jumps (i.e. 2 for double jumps)


	void Start () {
		jumpCount = maxJumps;
		rb2D = GetComponent<Rigidbody2D>();
		lastTapTime = 0f;
		jumpSpeed = 7f;
		//default movement in the right direction
		inAir = false;
		forwardForce = 5;
		forwardForceToggle = true;

	}
	
	// Update is called once per frame
	void Update () {

		//Pause movement
		if (Input.GetKeyDown ("up")) {
			if (forwardForceToggle == true) {	
				forwardForce = 0;
				forwardForceToggle = false;
			} else if (forwardForceToggle == false) {
				forwardForce = 5;
				forwardForceToggle = true;
			}
		}

		if (Input.GetKeyDown ("left")) {
			forwardForce = -5f;

			float currentTapTime = Time.time;
			if ((currentTapTime - lastTapTime) < tapSpeed) {
				Debug.Log ("Double tap");
				Tackle ();
			} else {
				lastTapTime = currentTapTime;
			}


		}
		if (Input.GetKeyDown ("right")) {
			forwardForce = 5f;
		
			float currentTapTime = Time.time;
			if ((currentTapTime - lastTapTime) < tapSpeed) {
				Debug.Log ("Double tap");
				Tackle ();
			} else {
				lastTapTime = currentTapTime;
			
			}
		

		}

	}


	void FixedUpdate()
	{
		//Moving right
		rb2D.AddForce(transform.right * forwardForce);

		//Jumping
		if (Input.GetKey ("space")) {
			if (jumpCount > 0)
			{
				rb2D.velocity += jumpSpeed * Vector2.up;
				jumpCount -= 1;
			}
		}


	}


	//handling collisions
	void OnCollisionEnter2D(Collision2D Col)
	{
		//used to only jump when the character is on the ground
		if (Col.gameObject.name == "ground") {
		  jumpCount = maxJumps;
		}
	}

	void Tackle () {
		//empty for now..
	}


}
