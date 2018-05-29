using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoreController : MonoBehaviour {
	private AudioController _audioController;

	[SerializeField]
	private Text _highScoreText;

	private int _highScore;

	void Start () {
		// Get a handle for the audio controller
		_audioController = GameObject.Find("AudioController").GetComponent<AudioController>();

		// Get the current high score
		_highScore = LoadHighScore();

		_highScoreText.text = _highScore + "";
	}
	
	void Update () {
		
	}

	private int LoadHighScore() {
		if (PlayerPrefs.HasKey("highScore")) {
			return PlayerPrefs.GetInt ("highScore");
		}

		return 0;
	}

	public void LoadNewScene(string newScene) {
		// Play click sound
		_audioController.PlayUIAudio();

		SceneManager.LoadScene (newScene);
	}
}
