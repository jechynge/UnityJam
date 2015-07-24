using UnityEngine;
using System.Collections;

public class CrushingController : MonoBehaviour {

    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}

	void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.tag == "Crusher") {
			anim.SetBool ("crushed", true);
			collision.collider.isTrigger = true;
			collision.gameObject.GetComponent <Animator>().SetBool("timetofall", true);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
