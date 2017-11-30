using UnityEngine;
using System.Collections;

public class AnimateBounce : MonoBehaviour {

	public bool Move = true;
	public Vector3 MoveVector = Vector3.up;
	public float MoveRange;
	public float MoveSpeed;

	private AnimateBounce bounceObject;
	Vector3 startPosition;

	void Start() { 

		bounceObject = this;
		startPosition = bounceObject.transform.position;

	}

	void Update() {

		if(Move)
			bounceObject.transform.position = startPosition + MoveVector * (MoveRange * Mathf.Sin(Time.timeSinceLevelLoad * MoveSpeed));

	}
}