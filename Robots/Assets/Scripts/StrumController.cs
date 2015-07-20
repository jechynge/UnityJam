using UnityEngine;
using System.Collections;

public class StrumController : MonoBehaviour {
	
	public float maxSpeed = 7.5f;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	
	Animator anim;
	Rigidbody2D body;
	
	bool facingRight = true;
	bool canTrigger = true;
	bool grounded = false;
	float groundRadius = 0.2f;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		body = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("grounded", grounded);
		anim.SetFloat ("vSpeed", body.velocity.y);
		
		
		float move = Input.GetAxis ("StrumHorizontal");
		
		anim.SetFloat ("speed", Mathf.Abs (move));
		
		body.velocity = new Vector2 (move * maxSpeed, body.velocity.y);
		
		if (move > 0 && !facingRight) {
			Flip ();
		} else if (move < 0 && facingRight) {
			Flip ();
		}
	}
	
	void Update () {
		if (Input.GetAxis ("FretInteract") == 0)
			canTrigger = true;
	}
	
	void OnTriggerStay2D(Collider2D coll) {
		if (Input.GetAxis ("FretInteract") > 0 && coll.gameObject.tag == "Interactive" && canTrigger) {
			canTrigger = false;
			coll.gameObject.GetComponent<TriggerSender> ().SendTrigger ();
		}
	}

	public float pushPower = 2.0f;
	void OnControllerColliderHit(ControllerColliderHit hit) { 
		Rigidbody body = hit.collider.attachedRigidbody;
		if (body == null || body.isKinematic)
			return;
		
		if (hit.moveDirection.y < -0.3f)
			return;
		
		Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
		body.velocity = pushDir * pushPower;
	}
	
	void Flip() {
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
