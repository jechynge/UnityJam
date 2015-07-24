using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onTrigger(bool triggered) {
		if (triggered)
			transform.Translate (new Vector3 (0, 1));
		else 
			transform.Translate (new Vector3 (0, -1));
	}
}
