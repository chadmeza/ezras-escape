using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	private GameController _gameController;
	private AudioController _audioController;

	[SerializeField]
	private GameObject _explosionFX;

	private float _moveSpeed = 0f;

	void Start () {
		_gameController = FindObjectOfType<GameController> ();

		// Get a handle for the audio controller
		_audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
	}
	
	void Update () {
		// If the player is alive, keep moving
		if (_gameController.isAlive) {
			// Keep moving down
			transform.Translate (new Vector3 (0, _moveSpeed, 0) * Time.deltaTime);	

			// When the enemy goes off-screen, it is destroyed and the score is increased
			if (transform.position.y < -6) {
				_gameController.IncreasePlayerScore ();
				Destroy (gameObject);
			}
		} else {
			Destroy (gameObject);
		}
	}

	public void SetMoveSpeed(float moveSpeed) {
		_moveSpeed = moveSpeed;
	}

	void OnTriggerEnter2D (Collider2D other) {
		// If an enemy collides with the player, the player dies
		if (other.gameObject.tag == "Player") {
			_gameController.ToggleIsAlive ();

			// Show the explosion fx
			Instantiate (_explosionFX, other.gameObject.transform.position, Quaternion.identity);

			// Play the explosion sound
			_audioController.PlayExplosionSound();

			// Destroy the player and the enemy
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}
}
