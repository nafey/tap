using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour {
    public float forceMultiplier;
    public GameObject tick;
    

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {

            
            Vector2 raw_force = tick.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 raw_unit_force = raw_force / raw_force.magnitude;

            //Debug.Log("Raw Unit Force Magnitude " + raw_force.magnitude);

            Vector2 inverse_force = raw_unit_force / raw_force.magnitude;

            //Debug.Log("Inverse Force Magnitude " + inverse_force.magnitude);
            Vector2 adjusted_force = inverse_force * forceMultiplier;

            //Debug.Log("Raw Force " + raw_force);
            //Debug.Log("Inverse Force " + inverse_force);
            //Debug.Log("Adjusted Force " + adjusted_force);

            Debug.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition), tick.transform.position, Color.red, 2f);
           
            tick.GetComponent<Rigidbody2D>().AddForce(adjusted_force);
        }
	}
}
