using UnityEngine;
using System.Collections;

public class TriggerSender : MonoBehaviour {

	public GameObject[] triggerReceivers;
	public bool isReversible = false;
	public bool isAnimated = false;

	bool triggered = false;
	Animator anim;

	// Use this for initialization
	void Start () {
		if (isAnimated)
			anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SendTrigger() {
		// If we've already triggered this sender and we shouldn't reverse it, bail out.
		if (triggered && !isReversible)
			return;

		// Invert our state
		triggered = !triggered;

		if (isAnimated)
			anim.SetBool ("on", triggered);

		foreach (GameObject o in triggerReceivers) {
			o.GetComponent<TriggerReceiver>().executeTrigger(triggered);
		}
	}
}
