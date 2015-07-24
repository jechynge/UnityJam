using UnityEngine;
using System.Collections;

public class StrumController : MonoBehaviour {
	
	public float maxSpeed = 7.5f;
	public Transform groundCheck;
	public LayerMask whatIsGround;
    public GameObject fret;
    public GameObject fretStrumPrefab;
	
	Animator anim;
	Rigidbody2D body;
	
	bool facingRight = true;
	bool grounded = false;
	float groundRadius = 0.2f;

	public AudioClip sfxHurt;
	public AudioClip sfxPushSmall;

	GameObject combineChecker;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		body = GetComponent<Rigidbody2D> ();
		fret = GameObject.FindGameObjectWithTag ("Player");
	
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

    void Update()
    {
		if (GameObject.FindGameObjectWithTag ("CombineChecker") != null) {
			// Below is the combine code. Maybe we should set a flag to make sure that they don't do this before the last level?
			// If Strum is holding LT, RT, and the A button...
			if (Input.GetAxis ("StrumCombine1") > 0 && Input.GetAxis ("StrumCombine2") > 0 && Input.GetAxis ("StrumCombine") > 0) {
				// ...and is close enough to fret...
				if (Vector3.Distance (transform.position, fret.transform.position) < 2.5f) {
					// ...and Fret is also holding down LT, RT, and the A button...
					if (fret.GetComponent<FretController> ().Combine ()) {
						// DO IT UP, SOOOOOOOOOOOOOOON
						//Instantiate(fretStrumPrefab, transform.position, transform.rotation);
						GameObject fretstrum = (GameObject)Instantiate (Resources.Load ("FretStrum"));
						fretstrum.transform.position = new Vector2 (transform.position.x, transform.position.y);
						Destroy (fret);
						Destroy (gameObject);
					}
				}
			}
		}
    }
	
	void Flip() {
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
