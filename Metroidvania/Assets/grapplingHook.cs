using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grapplingHook : MonoBehaviour {

    DistanceJoint2D joint;
    Vector3 targetPos;
    RaycastHit2D hit;
    public float distance = 10f;
    public LayerMask mask;

	void Start () {
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        if (Input.GetKeyDown(KeyCode.E))
        {
            targetPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            //targetPos.z = 0;
            hit = Physics2D.Raycast(transform.position, targetPos - transform.position, distance, mask);
            if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                joint.enabled = true;
                joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                joint.distance = Vector2.Distance(transform.position, hit.point);
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            joint.enabled = false;
        }
	}
}
