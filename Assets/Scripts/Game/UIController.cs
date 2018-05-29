using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {
	private AudioController _audioController;

	[SerializeField]
	private GameObject _instructionsPanel;
	[SerializeField]
	private Text _scoreText;
	[SerializeField]
	private GameObject _gameOverPanel;
	[SerializeField]
	private Text _finalScoreText;
	[SerializeField]
	private GameObject _newHighScoreLabel;

	// Use this for initialization
	void Start () {
		// Get a handle for the audio controller
		_audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdatePlayerScoreText(int score) {
		_scoreText.text = "" + score;
	}

	public void DisplayGameOverPanel(int score) {
		// Display the game over panel
		_gameOverPanel.SetActive (true);
		_finalScoreText.text = score + "";
	}

	public void DisplayNewHighScoreLabel(bool active) {
		_newHighScoreLabel.SetActive (active);
	}

	public void LoadNewScene (string newScene) {
		// Play click sound
		_audioController.PlayUIAudio();

		SceneManager.LoadScene (newScene);
	}

	public void DisplayInstructionsPanel(bool active) {
		_instructionsPanel.SetActive (active);
	}
}
