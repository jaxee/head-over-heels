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
	public bool buttonPressed;

	public int maxJumps; //Maximum amount of jumps (i.e. 2 for double jumps)

	void Start () {
		buttonPressed = false;
		maxJumps = 1;
		tapSpeed = 0.5f;
		lastTapTime = 0f;
		maxSpeed = 2f;
		jumpCount = maxJumps;
		rb2D = GetComponent<Rigidbody2D>();
		lastTapTime = 0f;
		jumpSpeed = 380f;
		//default movement in the right direction
		inAir = false;
		forwardForce = 10;
		forwardForceToggle = true;
	}
	void Tackle () {
		//empty for now..
		animator.SetBool ("Tackle", true);
		Invoke ("EndTackle", 1.0f);
		if (forwardForce == 0) {
			forwardForce = 10;
		}
		savedForce = forwardForce;
		forwardForce = forwardForce * 2;
	}

	void EndTackle() {
		animator.SetBool ("Tackle", false);
		forwardForce = savedForce;
	}

	// Update is called once per frame
	void Update () {



		if (active) {



			//Pause movement
			if (Input.GetKeyDown ("up")) {

				currentDirection = animator.GetInteger ("Direction");

				if (forwardForceToggle == true) {	
					animator.SetBool ("Idle", true);
					forwardForce = 0;
					forwardForceToggle = false;
				} else if (forwardForceToggle == false) {
					animator.SetBool ("Idle", false);
					Debug.Log (currentDirection);
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
				forwardForce = -10f;
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
				forwardForce = 10f;
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
						animator.enabled = false;

						forwardForce = 80;
					} else if (forwardForce < 0) { //negative going left
						forwardForce = -80;
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

		//collision with button to lift pillar

		//collision with button to lift pillar
		if (Col.gameObject.name == "luna_pillar") {
			forwardForce = 0;
		}

		if (Col.gameObject.name == "sol_button") {
			buttonPressed = true;
			GameObject pillar = GameObject.Find ("luna_pillar");
			Vector2 endPosition = new Vector2 (pillar.transform.position.x, (pillar.transform.position.y + 5) );
			StartCoroutine (MoveOverSeconds (pillar, endPosition, 3f));


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








}