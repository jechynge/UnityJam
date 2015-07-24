using UnityEngine;
using System.Collections.Generic;

public class AttackController : MonoBehaviour {

    public bool isAttacking = false;
    public float hitForce = 1000f;
    public List<string> smashableTags;
    public CircleCollider2D attackCollider;

    Animator anim;

    bool canAttackUnderhand = true;
    bool canAttackOverhand = true;
    string currentAttack = "";
    uint currentAttackId = 0;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}

    void FixedUpdate() {
        // Not sure how expensive this is, but we only need the info once per frame, so we'll put it here for now
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);

        isAttacking = state.IsTag("attack");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("StrumUnderhand") == 0)
            canAttackUnderhand = true;

        if (Input.GetAxis("StrumOverhand") == 0)
            canAttackOverhand = true;

        if (isAttacking)
        {
            return; // If we're already attacking, we don't nee to check for new attacks
        }

        if (Input.GetAxis("StrumUnderhand") > 0 && canAttackUnderhand && !isAttacking)
        {
            anim.SetTrigger("attackUnderhand");
            canAttackUnderhand = false;
            isAttacking = true;
            currentAttack = "under";
            currentAttackId++;
        }

        if (Input.GetAxis("StrumOverhand") > 0 && canAttackOverhand && !isAttacking)
        {
            anim.SetTrigger("attackOverhand");
            canAttackOverhand = false;
            isAttacking = true;
            currentAttack = "over";
            currentAttackId++;
        }
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!isAttacking) return;

        switch (currentAttack)
        {
            case "under":
                // We're hitting Fret
                if(coll.tag == "Player")
                    coll.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, hitForce));
                break;
            case "over":
                // We're hitting something that can be smashed
                if(smashableTags.Contains(coll.tag))
                    coll.GetComponent<HitController>().Smash(currentAttackId);
                break;
        }

        
    }
}
