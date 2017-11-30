﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Rigidbody2D playerRigidBody;
	SpriteRenderer playerRenderer;
	Animator playerAnimator;

	// Movement
	public float maxSpeed;
	private float speed;
	public bool moveRight = true;
	public bool shouldMove = true;
	private int direction = 1;

	// Jumping
	bool grounded = false;
	float groundCheckRadius = 0.2f;
	public LayerMask groundLayer;
	public Transform groundCheck;
	public float jumpPower;

	// Tackling
	bool isTackling = false;

	// Active player
	public bool isActive;

	// Game events
	public bool hasReachedGoal;


	// Use this for initialization
	void Start () {
		playerRigidBody = GetComponent<Rigidbody2D>();
		playerRenderer = GetComponent<SpriteRenderer>();
		playerAnimator = GetComponent<Animator> ();
		speed = maxSpeed;
	}
	
	// Update is called once per frame
	void Update () {

		if (isActive) {

			if (Input.GetKeyDown (KeyCode.L) && !isTackling) {
				isTackling = true;
			}

			if (grounded && Input.GetAxis ("Jump") > 0) {
				playerAnimator.enabled = false;
				playerRigidBody.velocity = new Vector2 (playerRigidBody.velocity.x, 0f);
				playerRigidBody.AddForce (new Vector2 (0f, jumpPower), ForceMode2D.Impulse);
				grounded = false;
			}

			grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, groundLayer);

			if (grounded) {
				playerAnimator.enabled = true;
			}
				
			float move = Input.GetAxis ("Horizontal");

			if (move > 0 && !moveRight) {
				Flip ();
			} else if (move < 0 && moveRight) {
				Flip ();
			} else if (move != 0 && !shouldMove) {
				shouldMove = true;
				speed = maxSpeed;
			}

			direction = moveRight ? 1 : -1;

			// Eventually will need touch input, or we make a new input mapping
			if (Input.GetKeyDown (KeyCode.UpArrow) && grounded) {
				shouldMove = !shouldMove;

				if (shouldMove) {
					speed = maxSpeed;
				} else {
					speed = 0;
				}
			}

			playerRigidBody.velocity = new Vector2 (direction * speed, playerRigidBody.velocity.y);

			playerAnimator.SetBool ("ShouldMove", shouldMove);

			// Play Tackle animation
			playerAnimator.SetBool ("ShouldTackle", isTackling);

		} else {
			// Finish detecting when the player lands
			grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, groundLayer);
			if (grounded) {
				playerAnimator.enabled = true;
			}

			// Stop all forward movement
			playerRigidBody.velocity = new Vector2 (0f, playerRigidBody.velocity.y);

			// Play Idle animation
			playerAnimator.SetBool ("ShouldMove", false);

		}

	}

	void Flip() {
		moveRight = !moveRight;
		playerRenderer.flipX = !playerRenderer.flipX;

		if (!shouldMove) {
			shouldMove = true;
			speed = maxSpeed;
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {

		// Prevents sticking onto objects. Don't perform when tackling.
		if (((groundLayer & 1 << collider.gameObject.layer) == 1 << collider.gameObject.layer) && !isTackling) {
			Flip ();
		}
	}

	// Called by the tackle animation
	void EndTackle() {
		isTackling = false;

		// Stop Tackle animation
		playerAnimator.SetBool ("ShouldTackle", isTackling);
	}
}