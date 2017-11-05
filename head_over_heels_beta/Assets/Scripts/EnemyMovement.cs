using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour 
{
	//Public to editor
	public Waypoint[] waypoints;
	public float speed = 3f;
	//Circular collider? Or circular movement?
	public bool isCircular;
	// Begin finding the first waypoint since none are defined
	public bool inReverse = true;
	//How close the enemy has to be to the waypoint
	public float distanceThreshold = 0.1f;

	//Private to class
	private Waypoint currentWaypoint;
	private int currentIndex = 0;
	//Retrieved from waypoint wait time
	private bool isWaiting = false;
	//Unsure
	private float speedStorage = 0;

	// Initialization
	void Start() 
	{
		if (wayPoints.Length > 0) 
		{
			currentWaypoint = wayPoints[0];
		}
	}
	
	void Update() 
	{
		//Check if there is a waypoint set and if the wait time is finished
		if (currentWaypoint != null && !isWaiting) 
		{
			MoveTowardsWaypoint();
		}		
	}

	//Pause movement
	void Pause()
	{
		isWaiting != isWaiting;
	}

	private void MoveTowardsWaypoint() 
	{
		//Get current position
		Vector3 currentPosition = this.transform.position;

		//Get waypoint position
		Vector3 waypointPosition = currentWaypoint.transform.position;

		if (Vector3.Distance (currentPosition, waypointPosition) > distanceThreshold) {

			//Determine direction
			Vector3 direction = waypointPosition - currentPosition;
			direction.Normalize();

			//Scale movement depending on direction
			this.transform.Translate(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, direction.z * speed * Time.deltaTime, Space.World);
		}
		else 


	}
}
