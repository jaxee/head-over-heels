using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour {

	public float forwardForce;
	private float savedForce;
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
	private int currentDirection;

	public bool hasReachedGoal;

	public int maxJumps; //Maximum amount of jumps (i.e. 2 for double jumps)

	void Start () {

		animator.SetInteger ("Direction", 0);
		animator.enabled = true;

		maxJumps = 1;
		tapSpeed = 0.5f;
		lastTapTime = 0f;
		maxSpeed = 2f;
		jumpCount = maxJumps;
		rb2D = GetComponent<Rigidbody2D>();
		lastTapTime = 0f;
		jumpSpeed = 150f;
		//default movement in the right direction
		inAir = false;
		forwardForce = 80;
		forwardForceToggle = true;
	}
	void Tackle () {
		//empty for now..
		//animator.SetBool ("Tackle", true);
		/* Removing for now due to bugs
		Invoke ("EndTackle", 1.0f);
		if (forwardForce == 0) {
			forwardForce = 10;
		}
		savedForce = forwardForce;
		forwardForce = forwardForce * 2;
		*/
	}

	void EndTackle() {
		animator.SetBool ("Tackle", false);
		forwardForce = savedForce;
	}

	// Update is called once per frame
	void Update () {


		Debug.Log (animator.enabled);
		if (active) {

			//Debug.Log (jumpCount);

			//Pause movement
			if (Input.GetKeyDown ("up")) {

				currentDirection = animator.GetInteger ("Direction");

				if (forwardForceToggle == true) {	
					animator.SetBool ("Idle", true);
					forwardForce = 0;
					forwardForceToggle = false;
				} else if (forwardForceToggle == false) {
					animator.SetBool ("Idle", false);
					//Debug.Log (currentDirection);
					if (currentDirection == 1) {
						forwardForce = -10;
					} else if (currentDirection == 0) {
						forwardForce = 10;
					}

					forwardForceToggle = true;
				}
			}

			if (Input.GetKeyDown ("left")) {
				rb2D.velocity = new Vector2 (0, 0);
				forwardForce = -80f;
				forwardForceToggle = true;
				animator.SetBool ("Idle", false);

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
				rb2D.velocity = new Vector2 (0, 0);
				forwardForceToggle = true;
				forwardForce = 80f;
				animator.SetInteger ("Direction", 0);

				animator.SetBool ("Idle", false);
				float currentTapTime = Time.time;

				float delta = (currentTapTime - lastTapTime);

				if (delta < tapSpeed) {

					Tackle ();
				} else {
					lastTapTime = currentTapTime;

				}


			}
		} else {
			animator.SetBool ("Idle", true);
		}

	}


	void FixedUpdate()
	{
		if (active) {
			//limiting velocities

			//Moving right

			rb2D.AddForce(transform.right * forwardForce);
			rb2D.velocity = new Vector2 (Mathf.Clamp (rb2D.velocity.x, -maxSpeed, maxSpeed), rb2D.velocity.y);

			//Debug.Log (forwardForce);
			//Jumping
			if (Input.GetKey ("space")) {
				animator.SetBool ("Jump", true);

				if (jumpCount > 0) {
					//rb2D.velocity += jumpSpeed * Vector2.up;
					//rb2D.velocity += jumpSpeed * Vector2.right;
					animator.SetBool ("Idle", false);
					animator.SetBool ("Jump", true);
					rb2D.AddForce(transform.up * jumpSpeed);
					if (forwardForce > 0) { //positive going right
						//animator.enabled = false;

						//forwardForce = 80;
					} else if (forwardForce < 0) { //negative going left
						//forwardForce = -80;
						//animator.enabled = false;
					}
					jumpCount -= 1;
					animator.SetBool ("Jump", false);
					//Debug.Log ("in the prev" + jumpCount);


				}
			}


		}

	}


	//handling collisions
	void OnCollisionStay2D(Collision2D Col)
	{


		//used to only jump when the character is on the ground
		if (Col.gameObject.tag == "Ground" && jumpCount == 0) {
			//Debug.Log ("Coll with ground");

			rb2D.AddForce(transform.right * -1.0f);
			jumpCount = maxJumps;
		}


	}










}