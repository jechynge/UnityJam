using UnityEngine;
using System.Collections;

public class TriggerSender : MonoBehaviour {

	public GameObject[] triggerReceivers;
	public bool isReversible = false;
	bool triggered = false;

	// Use this for initialization
	void Start () {
	
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

		foreach (GameObject o in triggerReceivers) {
			o.GetComponent<TriggerReceiver>().executeTrigger(triggered);
		}
	}
}
