using UnityEngine;
using System.Collections;

public class EndController : MonoBehaviour {

	bool started;
	AudioSource audio;
	// Use this for initialization
	void Start () {
		started = false;
	}

	public delegate void AudioCallback();
	public void PlaySoundWithCallback(AudioSource clip, AudioCallback callback)
	{
		clip.Play();
		StartCoroutine(DelayedCallback(40, callback));
	}
	private IEnumerator DelayedCallback(float time, AudioCallback callback)
	{
		yield return new WaitForSeconds(time);
		callback();
	}

	void OnTriggerEnter2D () {
		if (!started) {
			started = true;
			audio = GetComponent<AudioSource> ();
			PlaySoundWithCallback (audio,AudioFinished);
		}
	}
	void AudioFinished() {
		Application.LoadLevel("Credits");
	}

	// Update is called once per frame
	void Update () {
	
	}
}
