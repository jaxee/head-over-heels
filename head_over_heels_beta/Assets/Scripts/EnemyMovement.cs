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
	//Check if we want to move in both x and y
	public bool moveHorizontally = true;

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
		if (waypoints.Length > 0) 
		{
			currentWaypoint = waypoints[0];

		}
	}
	
	void Update() 
	{
		Debug.Log ("Current target waypoint: " + currentWaypoint);

		//Check if there is a waypoint set and if the wait time is finished
		if (currentWaypoint != null && !isWaiting) 
		{
			MoveTowardsWaypoint();
		}		
	}

	//Pause movement
	void Pause()
	{
		isWaiting = !isWaiting;
	}

	private void MoveTowardsWaypoint() 
	{
		//Get current position
		Vector3 currentPosition = this.transform.position;

		//Get waypoint position
		Vector3 waypointPosition = currentWaypoint.transform.position;

		if (Vector3.Distance (currentPosition, waypointPosition) > distanceThreshold) 
		{
			//Determine direction
			Vector3 direction = waypointPosition - currentPosition;
			direction.Normalize ();

			if (moveHorizontally) {
				direction.y = 0;
			}

			//Scale movement depending on direction, don't translate in z
			this.transform.Translate (direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, 0, Space.World);
		} 
		else 
		{
			//If the waypoint requires waiting, pause movement
			if (currentWaypoint.waitSeconds > 0) 
			{
				Pause();
				//Unpause after wait time is finished
				Invoke ("Pause", currentWaypoint.waitSeconds);
			}

			//If the waypoint requires faster movement, change speed 
			if (currentWaypoint.speedOut > 0) 
			{
				speedStorage = speed;
				speed = currentWaypoint.speedOut;
			}
			else if (speedStorage != 0) 
			{
				speed = speedStorage;
				speedStorage = 0;
			}

			NextWaypoint();
		}
	}

	//Determine next waypoint
	private void NextWaypoint()
	{
		//If the movement path should go from the last waypoint back to the first waypoint
		if (isCircular) 
		{
			if (!inReverse) 
			{
				currentIndex = (currentIndex + 1 >= waypoints.Length) ? 0 : currentIndex + 1;
			} 
			else 
			{
				currentIndex = (currentIndex == 0) ? waypoints.Length - 1 : currentIndex - 1;
			}
		} 
		//If the movement path should boomerang back from first waypoint to last waypoint
		else 
		{
			//If at the start or the end, we need to reverse movement
			if ((!inReverse && currentIndex + 1 >= waypoints.Length) || (inReverse && currentIndex == 0)) 
			{
				inReverse = !inReverse;
			}

			currentIndex = (!inReverse) ? currentIndex + 1 : currentIndex - 1;
		}

		currentWaypoint = waypoints[currentIndex];
	}


}
