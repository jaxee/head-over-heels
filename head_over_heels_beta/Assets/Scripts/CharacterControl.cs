using System.Collections;
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
	public bool active;

	public int maxJumps; //Maximum amount of jumps (i.e. 2 for double jumps)

	void Start () {
		maxJumps = 1;
		tapSpeed = 0.5f;
		lastTapTime = 0f;
		maxSpeed = 1f;
		jumpCount = maxJumps;
		rb2D = GetComponent<Rigidbody2D>();
		lastTapTime = 0f;
		jumpSpeed = 500f;
		//default movement in the right direction
		inAir = false;
		forwardForce = 10;
		forwardForceToggle = true;
	}
	void Tackle () {
		//empty for now..
		animator.SetBool ("Tackle", true);
		Debug.Log ("Double tap");
	}
	
	// Update is called once per frame
	void Update () {

		if (active) {
			//Pause movement
			if (Input.GetKeyDown ("up")) {
				if (forwardForceToggle == true) {	
					forwardForce = 0;
					forwardForceToggle = false;
				} else if (forwardForceToggle == false) {
					forwardForce = 10;
					forwardForceToggle = true;
				}
			}
	
			if (Input.GetKeyDown ("left")) {
				rb2D.velocity = new Vector2 (0,0);
				forwardForce = -10f;
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
				rb2D.velocity = new Vector2 (0,0);
				forwardForceToggle = true;
				forwardForce = 10f;
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

	}


	void FixedUpdate()
	{
		if (active) {
			//limiting velocities

			//Moving right

			rb2D.AddForce(transform.right * forwardForce);
			rb2D.velocity = new Vector2 (Mathf.Clamp (rb2D.velocity.x, -maxSpeed, maxSpeed), rb2D.velocity.y);

			//Jumping
			if (Input.GetKey ("space")) {
				animator.SetBool ("Jump", true);

				if (jumpCount > 0) {
					//rb2D.velocity += jumpSpeed * Vector2.up;
					//rb2D.velocity += jumpSpeed * Vector2.right;
					animator.SetBool ("Jump", true);
					rb2D.AddForce(transform.up * jumpSpeed);
					if (forwardForce > 0) { //positive going right
						animator.enabled = false;

						forwardForce = 120;
					} else if (forwardForce < 0) { //negative going left
						forwardForce = -120;
						animator.enabled = false;
					}
					jumpCount -= 1;
					animator.SetBool ("Jump", false);

				}
			}
		}

	}


	//handling collisions
	void OnCollisionEnter2D(Collision2D Col)
	{
		//used to only jump when the character is on the ground
		if (Col.gameObject.tag == "Ground") {
			animator.enabled = true;

			if (forwardForce > 0) { //positive going right
				forwardForce = 10;
			} else if (forwardForce < 0) { //negative going left
				forwardForce = -10;
			}
		  jumpCount = maxJumps;
		}

		//if collision with a wall
		if (Col.gameObject.name == "Wall") {
			forwardForce *= -1;
		}
	}




}
