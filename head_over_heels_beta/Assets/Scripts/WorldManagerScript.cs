﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManagerScript : MonoBehaviour {

	public GameObject solCharacter;
	public GameObject lunaCharacter;
	public static GameObject[] solEnemies;
	public static GameObject[] lunaEnemies;
	public static GameObject[] solObstacles;
	public static GameObject[] lunaObstacles;

	// Use this for initialization
	void Start () {
		solCharacter = GameObject.Find ("SolCharacter");
		lunaCharacter = GameObject.Find ("LunaCharacter");	
		solEnemies = GameObject.FindGameObjectsWithTag ("SolEnemy");
		lunaEnemies = GameObject.FindGameObjectsWithTag ("LunaEnemy");
		solObstacles = GameObject.FindGameObjectsWithTag ("SolObstacle");
		lunaObstacles = GameObject.FindGameObjectsWithTag ("LunaObstacle");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SwitchActiveWorld(bool isSolActive)
	{
		// If Sol is active, we need to pause Sol's world and activate Luna's.
		// Deactivate character movement but keep them idle
		SwitchActiveCharacter(isSolActive);
		// Pause all enemies and moving hazards in that world
		PauseEnemies(isSolActive);
		DeactivateObstacles(isSolActive);
	}

	public void SwitchActiveCharacter(bool isSolActive)
	{
		lunaCharacter.GetComponent<CharacterControl>().active = isSolActive ? true : false;
		solCharacter.GetComponent<CharacterControl>().active = isSolActive ? false : true;
	}

	public void PauseEnemies(bool isSolActive)
	{
		if (isSolActive && solEnemies != null && lunaEnemies != null) {
			foreach(GameObject Enemy in solEnemies) {
				Enemy.SetActive (false);
			}
			foreach(GameObject Enemy in lunaEnemies) {
				Enemy.SetActive (true);
			}
		} else if (solEnemies != null && lunaEnemies != null) {
			foreach(GameObject Enemy in solEnemies) {
				Enemy.SetActive (true);
			}
			foreach(GameObject Enemy in lunaEnemies) {
				Enemy.SetActive (false);
			}
		}
	}

	public void DeactivateObstacles(bool isSolActive)
	{
		if (isSolActive && solObstacles != null && lunaObstacles != null) {
			foreach(GameObject Obstacle in solObstacles) {
				Obstacle.SetActive (false);
			}
			foreach(GameObject Obstacle in lunaObstacles) {
				Obstacle.SetActive (true);
			}
		} else if (solObstacles != null && lunaObstacles != null) {
			foreach(GameObject Obstacle in solObstacles) {
				Obstacle.SetActive (true);
			}
			foreach(GameObject Obstacle in lunaObstacles) {
				Obstacle.SetActive (false);
			}
		}
	}
}