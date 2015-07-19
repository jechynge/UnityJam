using UnityEngine;
using System.Collections;

public class TriggerReceiver : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void executeTrigger(bool triggered) {
		transform.gameObject.SendMessage ("onTrigger", triggered);
	}
}
