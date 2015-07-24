using UnityEngine;
using System.Collections;

public class StrumController : MonoBehaviour {
	
	public float maxSpeed = 7.5f;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	
	Animator anim;
	Rigidbody2D body;
    DistanceJoint2D dragJoint;
	
	bool facingRight = true;
	bool canSwing = true;
    bool canSmash = true;
	bool grounded = false;
	float groundRadius = 0.2f;
    float maxDragDistance = 2.0f;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		body = GetComponent<Rigidbody2D> ();
        dragJoint = GetComponent<DistanceJoint2D>();
        dragJoint.enabled = false;
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
		if (Input.GetAxis ("StrumSwing") == 0)
			canSwing = true;

        if (Input.GetAxis("StrumSmash") == 0)
            canSmash = true;

        // If we have the grab key down and we're not currently dragging something...
        if (Input.GetAxis("StrumGrab") > 0 && dragJoint.connectedBody == null)
        {
            // ... find the closest object ...
            GameObject closest = GetClosestDraggable();

            // ... and if it exists, drag it
            if (closest != null)
            {
                DragObject(closest);
            }

        }
        // otherwise, if we don't have the grab key pressed and we're dragging something...
        else if(Input.GetAxis("StrumGrab") == 0 && dragJoint.connectedBody != null)
        {
            // ...release the dragged object
            ReleaseDraggedObject();
        }
	}

    void DragObject(GameObject draggable)
    {
        dragJoint.connectedBody = draggable.GetComponent<Rigidbody2D>();
        dragJoint.enabled = true;
    }

    void ReleaseDraggedObject()
    {
        dragJoint.connectedBody = null;
        dragJoint.enabled = false;
    }
	
	void Flip() {
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
 
     GameObject GetClosestDraggable () 
     {
         GameObject closest = null;
         float closestDistance = Mathf.Infinity;
         GameObject[] draggables = GameObject.FindGameObjectsWithTag("Draggable");

         foreach(GameObject o in draggables) {
             float distance = Mathf.Abs(Vector3.Distance(transform.position, o.transform.position));

             // Item is "behind" the player and shouldn't be dragged
             if(facingRight && transform.position.x - o.transform.position.x > 0) 
                 continue;

             if (!facingRight && transform.position.x - o.transform.position.x < 0)
                 continue;

             if (distance < closestDistance)
             {
                 closest = o;
                 closestDistance = distance;
             }
         }
     
         return closestDistance <= maxDragDistance ? closest : null;
     }
}
