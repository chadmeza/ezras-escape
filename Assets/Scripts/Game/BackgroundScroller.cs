using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {
	private GameController _gameController;

	[SerializeField]
	private float yOffset = 25.53f;

	private float _speed = 2.0f;

	// Use this for initialization
	void Start () {
		_gameController = GameObject.Find ("GameController").GetComponent<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_gameController.isAlive) {
			// Scroll downward
			transform.Translate (Vector3.down * _speed * Time.deltaTime);

			// Once it is off the screen, move back to the top
			if (transform.position.y <= -yOffset) {
				transform.position = new Vector3 (0, yOffset, 0);
			}
		}
	}
}
