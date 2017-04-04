using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour {
    public Transform tick;
    public float forecastPeriod;

    public GameObject holder;
    public GameObject block;

    public GameObject box;

    private Vector3 start;
    private float accel;

    private Quad GetQuad(Collider2D coll) {
        float collider_angle = coll.gameObject.transform.eulerAngles.z;
        Vector3 size = coll.gameObject.GetComponent<BoxCollider2D>().size;
        Vector3 center = coll.gameObject.GetComponent<BoxCollider2D>().bounds.center;

        return new Quad(size, center, collider_angle);
    }

    public void Start() {
        start = tick.GetComponent<TickBehaviour>().startVelocity;
        accel = tick.GetComponent<Rigidbody2D>().gravityScale;
        
        this.RedrawLine();
    }

    public void RedrawLine() {
        Vector3[] trajectory = Support.ComputeTrajectory(start, accel,
            tick.position, forecastPeriod);
    }

}
