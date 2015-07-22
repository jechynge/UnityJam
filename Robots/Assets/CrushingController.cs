using UnityEngine;
using System.Collections;

public class CrushingController : MonoBehaviour {
	Rigidbody2D rigidbody;
	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.tag == "Crusher") {
			gameObject.GetComponent<Animator> ().SetBool ("crushed", true);
			rigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
			//rigidbody.isKinematic = true;
			collision.collider.isTrigger = true;
			collision.gameObject.GetComponent <Animator>().SetBool("timetofall", true);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
