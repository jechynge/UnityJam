﻿using UnityEngine;
using System.Collections;

public class HingeController : MonoBehaviour {
	
	Animator animator;

	public AudioClip sfxHingeController;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		animator.SetBool ("drop", false);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void onTrigger(bool triggered) {
		if (triggered) {
			animator.SetBool ("drop", true);
			GetComponent <AudioSource> ().PlayOneShot (sfxHingeController);
		} else {
			animator.SetBool ("drop", false);
		}
	}
}