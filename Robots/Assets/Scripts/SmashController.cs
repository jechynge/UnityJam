using UnityEngine;
using System.Collections;

public class SmashController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	public void onSmash( ) {
		Debug.Log ("on smash");
		Destroy (gameObject);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
