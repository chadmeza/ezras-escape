using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
	public static AudioController instance = null;

	[SerializeField]
	private AudioSource _backgroundAudio;
	[SerializeField]
	private AudioSource _uiAudio;
	[SerializeField]
	private AudioSource _movementAudio;
	[SerializeField]
	private AudioClip _explosionSound;

	[SerializeField]
	private bool _isAudioOn = true;

	void Awake () {
		// Make sure there is only one instance of this object
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}
	
	void Update () {
		
	}

	public void ToggleIsAudioOn() {
		_isAudioOn = !_isAudioOn;

		// Turn on/off the background music
		if (_isAudioOn) {
			_backgroundAudio.Play ();
		} else {
			_backgroundAudio.Stop ();
		}
	}

	public void PlayUIAudio() {
		if (_isAudioOn) {
			_uiAudio.Play ();
		}
	}

	public void PlayMovementAudio() {
		if (_isAudioOn) {
			_movementAudio.Play ();
		}
	}

	public void PlayExplosionSound() {
		if (_isAudioOn) {
			AudioSource.PlayClipAtPoint (_explosionSound, Camera.main.transform.position);
		}
	}

	public bool GetIsAudioOn() {
		return _isAudioOn;
	}
}
