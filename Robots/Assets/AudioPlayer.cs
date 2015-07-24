using UnityEngine;
using System.Collections;

public class AudioPlayer : MonoBehaviour {
	bool started;
	// Use this for initialization
	void Start () {
		started = false;
	}

	void OnTriggerEnter2D () {
		if (!started) {
			started = true;
			AudioSource audio = GetComponent<AudioSource> ();
			audio.Play ();
			audio.Play (44100);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
