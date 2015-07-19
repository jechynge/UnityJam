using UnityEngine;
using System.Collections;

public class TriggerSender : MonoBehaviour {

	public GameObject[] triggerReceivers;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SendTrigger() {
		foreach (GameObject o in triggerReceivers) {
			o.GetComponent<TriggerReceiver>().executeTrigger();
		}
	}
}
