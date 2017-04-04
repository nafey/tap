using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ArrowBehaviour : MonoBehaviour {
    public Transform tick;
    private Vector3 start;
    private float accel;
    
    public void Foo() {
        Debug.Log("Foo");
    }

    public void Bar() {
        Debug.Log("Bar");
    }

    public void Start() {
        GameManager.instance.RegisterMouseDown(Foo);
        GameManager.instance.RegisterMouseDown(Bar);
        start = tick.GetComponent<TickBehaviour>().startVelocity;
        accel = tick.GetComponent<Rigidbody2D>().gravityScale;

        this.redrawLine();
    }

    public void redrawLine() {
        Vector3[] trajectory = Support.computeTrajectory(start, accel, 
            tick.position, 1.2f );

        for (int i = 0; i < trajectory.Length - 1; i++) {
            Debug.DrawLine(trajectory[i], trajectory[i + 1], Color.magenta, 10f);
        }
    }

}
