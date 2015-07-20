using UnityEngine;
using System.Collections;

public class ForkilftController : MonoBehaviour {

	Animator animator;

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
		} else {
			animator.SetBool ("moveup", false);
			animator.SetBool ("movedown", true);
		}
	}
}