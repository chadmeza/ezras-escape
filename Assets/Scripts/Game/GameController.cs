using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public bool isAlive;

	private EnemySpawnController _enemySpawnController;
	private UIController _uiController;

	private int _playerScore;
	private int _highScore;

	private bool _isPaused = false;

	void Start () {
		// The player is alive until they collide with an enemy
		isAlive = true;

		// Load the player's current high score
		_highScore = LoadHighScore();

		// Initialize the score
		_playerScore = 0;

		// Get a handle for the enemy spawn controller
		_enemySpawnController = GameObject.Find("EnemySpawnController").GetComponent<EnemySpawnController>();
		_enemySpawnController.StartSpawningEnemies ();

		// Get a handle for the UI controller
		_uiController = FindObjectOfType<UIController>();

		// Show the current score
		_uiController.UpdatePlayerScoreText (_playerScore);

		// Start the game on pause to show instructions
		ToggleIsPaused ();
	}
	
	void Update () {
		
	}

	public void ToggleIsPaused() {
		_isPaused = !_isPaused;

		_uiController.DisplayInstructionsPanel (_isPaused);

		// If paused, freeze the time
		if (_isPaused) {
			Time.timeScale = 0.0f;
		} else {
			Time.timeScale = 1.0f;
		}
	}

	public void IncreasePlayerScore() {
		_playerScore++;

		// Update the UI score text
		_uiController.UpdatePlayerScoreText(_playerScore);
	}

	public void ToggleIsAlive() {
		isAlive = !isAlive;

		// If player dies, display game over panel
		if (!isAlive) {
			_uiController.DisplayGameOverPanel (_playerScore);

			// If this is a new high score, save it
			if (_playerScore > _highScore) {
				_highScore = _playerScore;
				SaveNewHighScore ();

				_uiController.DisplayNewHighScoreLabel (true);
			}
		}
	}

	private int LoadHighScore() {
		if (PlayerPrefs.HasKey("highScore")) {
			return PlayerPrefs.GetInt ("highScore");
		}

		return 0;
	}

	private void SaveNewHighScore() {
		PlayerPrefs.SetInt ("highScore", _playerScore);
		PlayerPrefs.Save ();
	}

	public bool GetIsPaused() {
		return _isPaused;
	}
}
