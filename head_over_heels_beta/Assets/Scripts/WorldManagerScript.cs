using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class WorldManagerScript : MonoBehaviour {

	public GameObject solCharacter;
	public GameObject lunaCharacter;
	public Text loooveTokens;
	public static GameObject[] solEnemies;
	public static GameObject[] lunaEnemies;
	public static GameObject[] solObstacles;
	public static GameObject[] lunaObstacles;
	public int playerLives;
	public int loveTokens;
	public int storyTokens;

	// Use this for initialization
	void Start () {
		solCharacter = GameObject.Find ("SolCharacter");
		lunaCharacter = GameObject.Find ("LunaCharacter");	
		solEnemies = GameObject.FindGameObjectsWithTag ("SolEnemy");
		lunaEnemies = GameObject.FindGameObjectsWithTag ("LunaEnemy");
		solObstacles = GameObject.FindGameObjectsWithTag ("SolObstacle");
		lunaObstacles = GameObject.FindGameObjectsWithTag ("LunaObstacle");
		playerLives = 3;
		loveTokens = 0;
		storyTokens = 0;
		setTokenText ();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerLives == 0) {
			loveTokens = 0;
			restartLevel ();
		}
		if (solCharacter.GetComponent<CharacterControl> ().hasReachedGoal && lunaCharacter.GetComponent<CharacterControl> ().hasReachedGoal) {
			SceneManager.LoadScene("cinematic_level1");
		}
	}

	public void restartLevel () {
		SceneManager.LoadScene("LevelOne_Beta");
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

	public void PauseGame() {
		Time.timeScale = 0.0f;
	}

	public void UnpauseGame() {
		Time.timeScale = 1.0f;
	}

	public void setTokenText() {
		loooveTokens.text = loveTokens.ToString ();
	}
}
