using UnityEngine;
using System.Collections;

public class SceneExit : MonoBehaviour {

	public string nextScene;
	private bool fretIn;
	private bool strumIn;

	public AudioClip sfxBothInDoor;

	// Use this for initialization
	void Start () {
		fretIn = false;
		strumIn = false;
	}


	void OnTriggerStay2D(Collider2D collider) {
		if (collider.gameObject.tag == "Player")
			fretIn = true;
		if (collider.gameObject.tag == "Player 2") 
			strumIn = true;
	}
	// Update is called once per frame
	void Update () {

		if (fretIn && strumIn) {
			Debug.Log ("Changing level");
			Application.LoadLevel (nextScene);
			GetComponent<AudioSource> ().PlayOneShot (sfxBothInDoor);
		}
		fretIn = false;
		strumIn = false;

	}
}
