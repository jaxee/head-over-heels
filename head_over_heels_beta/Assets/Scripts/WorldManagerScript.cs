using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class WorldManagerScript : MonoBehaviour {

	public GameObject solCharacter;
	public GameObject lunaCharacter;
	public Text loooveTokens;
	public Text storyTokensText;
	public Image lives;
	public Image mysteryBox;
	public Sprite mysteryBoxUnlocked;
	public static GameObject[] solEnemies;
	public static GameObject[] lunaEnemies;
	public static GameObject[] solObstacles;
	public static GameObject[] lunaObstacles;
	public int playerLives;
	public int loveTokens;
	public int storyTokens;
	public bool unlockMysteryBox;
	public bool isMusicOn;
	public bool isSoundOn;
	public bool isManualRotationOn;

	private Sprite livesTwo;
	private Sprite livesOne;
	private Sprite livesZero;

	// Use this for initialization
	void Start () {
		Screen.SetResolution (600, 800, true);

		solCharacter = GameObject.Find ("SolCharacter");
		lunaCharacter = GameObject.Find ("LunaCharacter");
		solEnemies = GameObject.FindGameObjectsWithTag ("SolEnemy");
		lunaEnemies = GameObject.FindGameObjectsWithTag ("LunaEnemy");
		solObstacles = GameObject.FindGameObjectsWithTag ("SolObstacle");
		lunaObstacles = GameObject.FindGameObjectsWithTag ("LunaObstacle");
		livesTwo = Resources.Load<Sprite> ("Life-full");
		livesOne = Resources.Load<Sprite> ("Life-half");
		livesZero = Resources.Load<Sprite> ("Life-dead");
		lives = GameObject.Find("Lives").GetComponent<Image>();
		unlockMysteryBox = false;
		playerLives = 2;
		loveTokens = 0;
		storyTokens = 0;
		setTokenText ();
	}
	
	// Update is called once per frame
	void Update () {

		if (storyTokens == 1) {
			unlockMysteryBox = true;
			//mysteryBox.sprite = mysteryBoxUnlocked;
		}
		if (playerLives == 2) {
			lives.sprite = livesTwo;
		} else if (playerLives == 1) {
			lives.sprite = livesOne;
		} else if (playerLives == 0) {
			lives.sprite = livesZero;
			loveTokens = 0;
			solCharacter.GetComponent<PlayerController> ().playerAnimator.SetBool ("IsDead", true);
			solCharacter.GetComponent<PlayerController> ().isActive = false;
			lunaCharacter.GetComponent<PlayerController> ().playerAnimator.SetBool ("IsDead", true);
			lunaCharacter.GetComponent<PlayerController> ().isActive = false;
			Invoke ("Restart", 1);
		}

		if (solCharacter.GetComponent<PlayerController> ().hasReachedGoal && lunaCharacter.GetComponent<PlayerController> ().hasReachedGoal) {
			SceneManager.LoadScene("cinematic_level1");
		}
	}

	public void restartLevel () {
		SceneManager.LoadScene("LevelOne_Beta_2");
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
		lunaCharacter.GetComponent<PlayerController>().isActive = isSolActive ? true : false;
		solCharacter.GetComponent<PlayerController>().isActive = isSolActive ? false : true;
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

	public void setStoryTokenText() {
		storyTokensText.text = storyTokens.ToString ();

	}

	public void switchMusic () {
		if (isMusicOn) {
			isMusicOn = false;
		} else {
			isMusicOn = true;
		}
	}

	public void switchSound () {
		if (isSoundOn) {
			isSoundOn = false;
		} else {
			isSoundOn = true;
		}
	}

	public void switchManualRotation () {
		if (isManualRotationOn) {
			isManualRotationOn = false;
		} else {
			isManualRotationOn = true;
		}
	}

	public void Restart() {
		restartLevel ();
	}
}
