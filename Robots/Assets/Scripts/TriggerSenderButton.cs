using UnityEngine;
using System.Collections;

public class TriggerSenderButton : MonoBehaviour {

	public GameObject[] triggerReceivers;
	public bool isReversible = false;
	public bool isAnimated = false;

	public AudioClip sfxButtonPress;

	bool triggered = false;
	Animator anim;
	bool thingOnButton = false;
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Animator> ().SetBool ("buttonstate", false);
		if (isAnimated)
			anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (triggered) {
			if (!thingOnButton)
				SendTrigger ();
		}
	}



	void OnCollisionStay2D (Collision2D collision) {
		thingOnButton = true;
		if (!triggered)
			SendTrigger ();
		GetComponent <AudioSource> ().PlayOneShot (sfxButtonPress);
	}

	void OnCollisionExit2D (Collision2D collision) {
		thingOnButton = false;
	}

	public void SendTrigger() {
		// If we've already triggered this sender and we shouldn't reverse it, bail out.
		if (triggered && !isReversible)
			return;

		// Invert our state
		triggered = !triggered;
		gameObject.GetComponent<Animator> ().SetBool ("buttonstate", !gameObject.GetComponent<Animator> ().GetBool ("buttonstate"));

		if (isAnimated)
			anim.SetBool ("on", triggered);

		foreach (GameObject o in triggerReceivers) {
			o.GetComponent<TriggerReceiver>().executeTrigger(triggered);
		}
	}
}
