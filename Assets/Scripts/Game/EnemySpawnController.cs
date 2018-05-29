using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour {
	private GameController _gameController;

	[SerializeField]
	private GameObject[] _enemyPrefabs;

	private Stage[] _stages;
	private int _currentStage = 0;

	private Vector3[] _spawnPositions;

	// Use this for initialization
	void Awake () {
		// Get a handle for the game controller
		_gameController = FindObjectOfType<GameController>();

		// Setup the stages for gameplay
		SetupStages();

		// Setup the lanes for the enemies
		SetupSpawnPositions();

		// First stage is 0
		_currentStage = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartSpawningEnemies() {
		// Start spawning enemies
		StartCoroutine(SpawnEnemies());
	}

	private void SetupSpawnPositions() {
		// Setup the 3 lanes for the enemy spawn points
		_spawnPositions = new Vector3[3];
		_spawnPositions [0] = new Vector3 (-1.75f, 6, 0);
		_spawnPositions [1] = new Vector3 (0, 6, 0);
		_spawnPositions [2] = new Vector3 (1.75f, 6, 0);
	}

	private void SetupStages() {
		// The enemies gradually move faster, and the spawn wait time changes with each stage
		_stages = new Stage[10];
		_stages [0] = new Stage ();
		_stages [0].SpawnMultiplier = 1.0f;
		_stages [0].EnemyMoveSpeed = -5.0f;
		_stages [0].TotalEnemies = 10;

		_stages [1] = new Stage ();
		_stages [1].SpawnMultiplier = 0.7f;
		_stages [1].EnemyMoveSpeed = -6.0f;
		_stages [1].TotalEnemies = 10;

		_stages [2] = new Stage ();
		_stages [2].SpawnMultiplier = 0.7f;
		_stages [2].EnemyMoveSpeed = -7.0f;
		_stages [2].TotalEnemies = 25;

		_stages [3] = new Stage ();
		_stages [3].SpawnMultiplier = 0.7f;
		_stages [3].EnemyMoveSpeed = -8.0f;
		_stages [3].TotalEnemies = 10;

		_stages [4] = new Stage ();
		_stages [4].SpawnMultiplier = 0.7f;
		_stages [4].EnemyMoveSpeed = -9.0f;
		_stages [4].TotalEnemies = 10;

		_stages [5] = new Stage ();
		_stages [5].SpawnMultiplier = 0.75f;
		_stages [5].EnemyMoveSpeed = -10.0f;
		_stages [5].TotalEnemies = 10;

		_stages [6] = new Stage ();
		_stages [6].SpawnMultiplier = 0.4f;
		_stages [6].EnemyMoveSpeed = -10.0f;
		_stages [6].TotalEnemies = 25;

		_stages [7] = new Stage ();
		_stages [7].SpawnMultiplier = 0.4f;
		_stages [7].EnemyMoveSpeed = -11.0f;
		_stages [7].TotalEnemies = 10;

		_stages [8] = new Stage ();
		_stages [8].SpawnMultiplier = 0.4f;
		_stages [8].EnemyMoveSpeed = -12.0f;
		_stages [8].TotalEnemies = 10;

		_stages [9] = new Stage ();
		_stages [9].SpawnMultiplier = 0.3f;
		_stages [9].EnemyMoveSpeed = -12.0f;
		_stages [9].TotalEnemies = 100000;
	}

	private void CreateEnemies(int numberOfEnemies) {
		if (numberOfEnemies == 2) {
			// Get a random enemy prefab
			int enemyOnePrefabPosition = Random.Range (0, _enemyPrefabs.Length);
			// Get a random lane
			int enemyOneXPosition = Random.Range (0, 3);

			GameObject enemyObject = Instantiate (_enemyPrefabs[enemyOnePrefabPosition], _spawnPositions[enemyOneXPosition], Quaternion.identity) as GameObject;

			// Get a handle for the enemy's script
			Enemy enemyOne = enemyObject.GetComponent<Enemy>();

			if (enemyOne != null) {
				// Set the enemy's move speed
				enemyOne.SetMoveSpeed (_stages[_currentStage].EnemyMoveSpeed);
			}

			// Get a random enemy prefab
			int enemyTwoPrefabPosition = Random.Range(0, _enemyPrefabs.Length);
			// Get a random lane
			int enemyTwoXPosition = Random.Range (0, 3);

			// If 2 enemies will spawn together, make sure they don't spawn at the same position
			while (enemyTwoXPosition == enemyOneXPosition) {
				enemyTwoXPosition = Random.Range (0, 3);
			}

			GameObject enemyTwoObject = Instantiate (_enemyPrefabs[enemyTwoPrefabPosition], _spawnPositions[enemyTwoXPosition], Quaternion.identity) as GameObject;

			// Get a handle for the enemy's script
			Enemy enemyTwo = enemyTwoObject.GetComponent<Enemy>();

			if (enemyTwo != null) {
				// Set the enemy's move speed
				enemyTwo.SetMoveSpeed(_stages[_currentStage].EnemyMoveSpeed);
			}

		} else {
			// Get a random enemy prefab
			int enemyPrefabPosition = Random.Range(0, _enemyPrefabs.Length);
			// Get a random lane
			int randomXPosition = Random.Range (0, 3);

			GameObject enemyObject = Instantiate (_enemyPrefabs[enemyPrefabPosition], _spawnPositions[randomXPosition], Quaternion.identity) as GameObject;

			// Get a handle for the enemy's script
			Enemy enemy = enemyObject.GetComponent<Enemy> ();

			if (enemy != null) {
				// Set the enemy's move speed
				enemy.SetMoveSpeed (_stages [_currentStage].EnemyMoveSpeed);
			}
		}

	}

	IEnumerator SpawnEnemies() {
		int enemyCount = 0;

		// Continue spawning until the player dies
		while (_gameController.isAlive) {
			// Update the current stage if necessary
			if (_stages [_currentStage].TotalEnemies <= enemyCount) {
				if (_currentStage < _stages.Length) {
					_currentStage++;
					enemyCount = 0;
				}
			}

			// Chance element - Determine if 1 or 2 enemies will spawn at once
			int numberOfNewEnemies = Random.Range (1, 3);

			// Spawn new enemies
			CreateEnemies (numberOfNewEnemies);

			enemyCount += numberOfNewEnemies;

			yield return new WaitForSeconds (_stages[_currentStage].SpawnMultiplier);
		}
	}
}
