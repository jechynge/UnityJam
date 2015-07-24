using UnityEngine;
using System.Collections;

public class StrumController : MonoBehaviour {
	
	public float maxSpeed = 7.5f;
	public Transform groundCheck;
	public LayerMask whatIsGround;
    public GameObject fret;
	
	Animator anim;
	Rigidbody2D body;
	
	bool facingRight = true;
	bool grounded = false;
	float groundRadius = 0.2f;

	public AudioClip sfxHurt;
	public AudioClip sfxPushSmall;
	
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
	
	void Flip() {
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
