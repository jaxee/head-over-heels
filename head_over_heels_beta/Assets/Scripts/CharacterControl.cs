﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour {

	public float forwardForce;
	public Animator animator;
	public bool forwardForceToggle;
	public bool inAir;
	public int jumpCount;
	public float jumpSpeed;
	public float tapSpeed;
	private float lastTapTime;
	public Rigidbody2D rb2D;
	private float maxSpeed;

	public int maxJumps; //Maximum amount of jumps (i.e. 2 for double jumps)


	void Start () {
		maxJumps = 1;
		tapSpeed = 0.5f;
		lastTapTime = 0f;
		maxSpeed = 1f;
		jumpCount = maxJumps;
		rb2D = GetComponent<Rigidbody2D>();
		lastTapTime = 0f;
		jumpSpeed = 7f;
		//default movement in the right direction
		inAir = false;
		forwardForce = 5;
		forwardForceToggle = true;
		animator = this.GetComponent<Animator> ();
	}
	void Tackle () {
		//empty for now..
		animator.SetBool ("Tackle", true);
		Debug.Log ("Double tap");
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
			forwardForceToggle = true;

			animator.SetInteger ("Direction", 1);

			float currentTapTime = Time.time;

			float delta = (currentTapTime - lastTapTime);


			if (delta < tapSpeed) {
				Tackle ();
			} else {
				lastTapTime = currentTapTime;
		
			}


		}
		if (Input.GetKeyDown ("right")) {
			forwardForceToggle = true;
			forwardForce = 5f;
			animator.SetInteger ("Direction", 0);

		
			float currentTapTime = Time.time;
	
			float delta = (currentTapTime - lastTapTime);

			if (delta < tapSpeed) {

				Tackle ();
			} else {
				lastTapTime = currentTapTime;
			
			}
		

		}

	}


	void FixedUpdate()
	{

		//limiting velocities
		rb2D.velocity = new Vector2(Mathf.Clamp(rb2D.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rb2D.velocity.y, -maxSpeed, maxSpeed));

		//Moving right
		rb2D.AddForce(transform.right * forwardForce);

		//Jumping
		if (Input.GetKey ("space")) {
			animator.SetBool ("Jump", true);

			if (jumpCount > 0)
			{
				rb2D.velocity += jumpSpeed * Vector2.up;
				jumpCount -= 1;
				animator.SetBool ("Jump", false);
			}
		}

		//Debug.Log (jumpCount);


	}


	//handling collisions
	void OnCollisionEnter2D(Collision2D Col)
	{
		//used to only jump when the character is on the ground
		if (Col.gameObject.name == "Ground") {
		  jumpCount = maxJumps;
		}
	}




}