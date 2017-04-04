using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour {
    public Transform tick;
    private Vector3 start;
    private float accel;

    public void Start() {
        start = tick.GetComponent<TickBehaviour>().startVelocity;
        accel = tick.GetComponent<Rigidbody2D>().gravityScale;

        this.redrawLine();
    }

    public void redrawLine() {
        Vector3[] trajectory = Support.computeTrajectory(start, accel, 
            tick.position, 1.2f );

        Quad[] path = Support.computePath(trajectory, tick.GetComponent<CircleCollider2D>().radius * 2f);

        for (int i = 0; i < trajectory.Length - 1; i++) {
            Debug.DrawLine(trajectory[i], trajectory[i + 1], Color.magenta, 10f);
        }

        for (int i = 0; i < path.Length - 1; i++) {
            Quad q = path[i];

            Debug.DrawLine(q.Verts[0], q.Verts[1], Color.cyan, 10f);
            Debug.DrawLine(q.Verts[1], q.Verts[2], Color.cyan, 10f);
            Debug.DrawLine(q.Verts[2], q.Verts[3], Color.cyan, 10f);
            Debug.DrawLine(q.Verts[3], q.Verts[0], Color.cyan, 10f);
        }
    }

}
