﻿using UnityEngine;
using System.Collections;

public class GarageDoorController : MonoBehaviour {

	Animator animator;

	public AudioClip sfxGarageDoor;
	
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		animator.SetBool ("doorup", false);
		gameObject.GetComponent<BoxCollider2D>().enabled = false;

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void onTrigger(bool triggered) {
		if (triggered) {
			animator.SetBool ("doorup", true);
			gameObject.GetComponent<BoxCollider2D>().enabled = true;
			GetComponent <AudioSource> ().PlayOneShot (sfxGarageDoor);

		} else {
			animator.SetBool ("doorup", false);
			gameObject.GetComponent<BoxCollider2D>().enabled = false;
		}
	}
}