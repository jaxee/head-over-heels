using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudEnemyScript : MonoBehaviour {

	GameObject cloud; 
	// Use this for initialization
	void Start () {
		cloud = GameObject.Find ("CloudEnemy");

	}
	
	// Update is called once per frame
	void Update () {
		
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
