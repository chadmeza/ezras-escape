using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private GameController _gameController;
	private AudioController _audioController;

	private float _moveDistance;

	private Vector2 _startTouchPosition;
	private Vector2 _endTouchPosition;

	void Start () {
		// Keeps the player in an appropriate lane
		_moveDistance = 1.75f;

		// Initialize the touch positions
		_startTouchPosition = Vector2.zero;
		_endTouchPosition = Vector2.zero;

		_gameController = FindObjectOfType<GameController> ();

		// Get a handle for the audio controller
		_audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
	}
	
	void Update () {
		// If player is alive, allow the player to move
		if (_gameController.isAlive) {
			#if UNITY_ANDROID
			if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
				// When the player first touches the screen, store the position
				_startTouchPosition = Input.touches[0].position;
			} else if (Input.touches[0].phase == TouchPhase.Ended) {
				// When the player stops touching the screen, store the position
				_endTouchPosition = Input.touches[0].position;
			}

			if (_startTouchPosition != Vector2.zero && _endTouchPosition != Vector2.zero) {
				// Check if there was a swipe, and if so, which direction
				// Get the distance of the starting touch and ending touch
				float touchDistance = Mathf.Abs(_startTouchPosition.x - _endTouchPosition.x);

				// If the touch distance is greater than 1, it is a swipe
				if (touchDistance > 1.0f) {
					// If the instructions are on, trigger them to turn off and start the game
					if (_gameController.GetIsPaused()) {
						_gameController.ToggleIsPaused();
					}

					if (_startTouchPosition.x > _endTouchPosition.x) {
						// Swipe left
						MoveLeft();
					} else {
						// Swipe right
						MoveRight();
					}
				}

				ResetTouchPositions();
			}
			#else
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				MoveLeft ();
			}

			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				MoveRight ();
			}
			#endif
		}
	}

	void MoveLeft() {
		// Play swipe sound
		_audioController.PlayMovementAudio();

		if (transform.position.x > -_moveDistance) {
			transform.Translate (new Vector3 (-_moveDistance, 0, 0));
		}
	}

	void MoveRight() {
		// Play swipe sound
		_audioController.PlayMovementAudio();

		if (transform.position.x < _moveDistance) {
			transform.Translate (new Vector3 (_moveDistance, 0, 0));
		}
	}

	void ResetTouchPositions() {
		_startTouchPosition = Vector2.zero;
		_endTouchPosition = Vector2.zero;
	}
}
