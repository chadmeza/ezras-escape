using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
	private AudioController _audioController;

	[SerializeField]
	private Image _audioButtonImage;
	[SerializeField]
	private Sprite _audioOnSprite;
	[SerializeField]
	private Sprite _audioOffSprite;

	void Start () {
		// Get a handle for the audio controller
		_audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
	}
	
	void Update () {
		
	}

	public void LoadNewScene(string newScene) {
		// Play click sound
		_audioController.PlayUIAudio ();

		// Change the scene
		SceneManager.LoadScene (newScene);
	}

	public void ToggleIsAudioOn() {
		// Play click sound
		_audioController.PlayUIAudio();

		_audioController.ToggleIsAudioOn ();

		// Display the correct audio sprite
		if (_audioController.GetIsAudioOn()) {
			_audioButtonImage.sprite = _audioOnSprite;
		} else {
			_audioButtonImage.sprite = _audioOffSprite;
		}
	}
}
