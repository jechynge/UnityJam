using UnityEngine;
using System.Collections;

public class ForkliftController : MonoBehaviour {
	
	Animator animator;

	public AudioClip sfxMoveUp;
	public AudioClip sfxMoveDown;
	
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		animator.SetBool ("movedown", true);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void onTrigger(bool triggered) {
		if (triggered) {
			animator.SetBool ("movedown", false);
			animator.SetBool ("moveup", true);
			GetComponent <AudioSource> ().PlayOneShot (sfxMoveUp);
		} else {
			animator.SetBool ("moveup", false);
			animator.SetBool ("movedown", true);
			GetComponent <AudioSource> ().PlayOneShot (sfxMoveDown);
		}
	}
}