using UnityEngine;
using System.Collections;

public class WeightController : MonoBehaviour {
	Rigidbody2D rigidbody;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void onTrigger(bool triggered) {
		if (triggered) {
			gameObject.transform.parent.gameObject.AddComponent<Rigidbody2D>();
			rigidbody = gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>(); 
			rigidbody.mass = 100;
		}
	}
}