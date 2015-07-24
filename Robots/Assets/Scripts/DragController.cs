using UnityEngine;
using System.Collections;

public class DragController : MonoBehaviour {

    public float maxDragDistance = 2.0f;

	public AudioClip sfxDragging;
    
    DistanceJoint2D dragJoint;

    bool facingRight = true;

	// Use this for initialization
	void Start () {
        dragJoint = GetComponent<DistanceJoint2D>();
        dragJoint.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        facingRight = transform.localScale.x >= 0;

        // If we have the grab key down and we're not currently dragging something...
        if (Input.GetAxis("StrumGrab") > 0 && dragJoint.connectedBody == null)
        {
            // ... find the closest object ...
            GameObject closest = GetClosestDraggable();

            // ... and if it exists, drag it
            if (closest != null)
            {
                DragObject(closest);
				GetComponent <AudioSource> ().PlayOneShot (sfxDragging);
            }

        }
        // otherwise, if we don't have the grab key pressed and we're dragging something...
        else if (Input.GetAxis("StrumGrab") == 0 && dragJoint.connectedBody != null)
        {
            // ...release the dragged object
            ReleaseDraggedObject();
        }
	}

    void DragObject(GameObject draggable)
    {
        // Always maintain the same distance between the objects
        draggable.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        dragJoint.distance = Mathf.Abs(Vector3.Distance(transform.position, draggable.transform.position));
        dragJoint.connectedBody = draggable.GetComponent<Rigidbody2D>();
        dragJoint.enabled = true;
    }

    void ReleaseDraggedObject()
    {
        dragJoint.connectedBody.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        dragJoint.connectedBody = null;
        dragJoint.enabled = false;
    }

    GameObject GetClosestDraggable()
    {
        GameObject closest = null;
        float closestDistance = Mathf.Infinity;
        GameObject[] draggables = GameObject.FindGameObjectsWithTag("Draggable");

        foreach (GameObject o in draggables)
        {
            float distance = Mathf.Abs(Vector3.Distance(transform.position, o.transform.position));

            // Item is "behind" the player and shouldn't be dragged
            if (facingRight && transform.position.x - o.transform.position.x > 0)
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
