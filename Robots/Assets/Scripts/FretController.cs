using UnityEngine;
using System.Collections;

public class FretController : MonoBehaviour {

	public float maxSpeed = 7.5f;
	public float jumpForce = 400f;
    public float hitMod = 2.0f;
	public Transform groundCheck;
	public LayerMask whatIsGround;
    public GameObject strum;
	
	Animator anim;
	Rigidbody2D body;
    Collider[] colliders;

	bool facingRight = true;
	bool canJump = true;
	bool canTrigger = true;
	bool grounded = false;
    bool smashed = false;
	float groundRadius = 0.2f;
    uint lastAttackId = 0;

	public AudioClip sfxJump;
	public AudioClip sfxLand;
	public AudioClip sfxHurt;
	public AudioClip sfxPushSmall;
	public AudioClip sfxInteract;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		body = GetComponent<Rigidbody2D> ();
        colliders = GetComponents<Collider>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("grounded", grounded);
		anim.SetFloat ("vSpeed", body.velocity.y);

		float move = Input.GetAxis ("FretHorizontal");

		anim.SetFloat ("speed", Mathf.Abs (move));

		body.velocity = new Vector2 (move * maxSpeed, body.velocity.y);

		if (move > 0 && !facingRight) {
			Flip ();
		} else if (move < 0 && facingRight) {
			Flip ();
		}
	}

	void Update () {
		if(Input.GetAxis ("FretJump") == 0)
			canJump = true;
		GetComponent<AudioSource> ().PlayOneShot (sfxJump);
	


		if (Input.GetAxis ("FretInteract") == 0)
			canTrigger = true;
		GetComponent<AudioSource> ().PlayOneShot (sfxInteract);


		if (Input.GetAxis ("FretJump") > 0 && grounded && canJump && !smashed) {
			canJump = false;
			grounded = false;
			anim.SetBool("grounded", grounded);
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
			GetComponent<AudioSource> ().PlayOneShot (sfxJump);

		}

        if (smashed && body.velocity.y <= 0)
        {
            smashed = false;
            foreach (Collider c in colliders)
            {
                c.isTrigger = false;
            }
        }
	}

	void OnTriggerStay2D(Collider2D coll) {
		if (Input.GetAxis ("FretInteract") > 0 && coll.gameObject.tag == "Interactive" && canTrigger) {
			canTrigger = false;
			coll.gameObject.GetComponent<TriggerSender> ().SendTrigger ();
		}
	}

    public void OnStrumSmash(uint attackId)
    {
        if (lastAttackId == attackId) return;

        lastAttackId = attackId;
        smashed = true;

        foreach (Collider c in colliders)
        {
            c.isTrigger = true;
        }

        body.AddForce(new Vector2(0, jumpForce * hitMod));
    }

	void Flip() {
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
