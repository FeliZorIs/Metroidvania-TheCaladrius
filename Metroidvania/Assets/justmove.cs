using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class justmove : MonoBehaviour {

    public GameObject   targetObject;
    public GameObject   SpriteObject;

    public int          speed;
    float               angle;

    Vector3             direction;
    Vector3             target;

	void Start () 
    {
        SpriteObject = this.transform.GetChild(0).gameObject;
        target = targetObject.transform.position;
        direction = (target - transform.position);

        angle = -1 * (Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg);
	}
	
	// Update is called once per frame
	void Update () 
    {
        Debug.DrawLine(transform.position, direction);
        transform.Translate(direction * speed * Time.deltaTime);
        SpriteObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
