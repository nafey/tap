using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour {
    public Transform tick;
    public float forecastPeriod;

    public GameObject holder;
    public GameObject block;

    private Vector3 start;
    private float accel;

    public void Start() {
        start = tick.GetComponent<TickBehaviour>().startVelocity;
        accel = tick.GetComponent<Rigidbody2D>().gravityScale;

        this.redrawLine();
    }

    public void Update() {

    }

    public void redrawLine() {
        float time = 3600f;
        Vector3[] trajectory = Support.computeTrajectory(start, accel,
            tick.position, forecastPeriod);

        Quad[] path = Support.computePath(trajectory, tick.GetComponent<CircleCollider2D>().radius * 2f);

        for (int i = 0; i < trajectory.Length - 1; i++) {
            Debug.DrawLine(trajectory[i], trajectory[i + 1], Color.magenta, time);
        }

        for (int i = 0; i < path.Length; i++) {
            Quad q = path[i];

            Debug.DrawLine(q.Verts[0], q.Verts[1], Color.cyan, time);
            Debug.DrawLine(q.Verts[1], q.Verts[2], Color.cyan, time);
            Debug.DrawLine(q.Verts[2], q.Verts[3], Color.cyan, time);
            Debug.DrawLine(q.Verts[3], q.Verts[0], Color.cyan, time);
        }

        // Find the collision for path section
        for (int i = 0; i < path.Length; i++) {
            Quad q = path[i];

            float qx = (q.Verts[1] - q.Verts[0]).magnitude;
            float qy = (q.Verts[2] - q.Verts[1]).magnitude;

            Vector2 side = (q.Verts[1] - q.Verts[0]);

            float tan = side.y / side.x;
            float radian = Mathf.Atan(tan);
            float angle = 57.3f * radian;

            
            if (i == 2) {
                Border b = new Border(q, 0.1f);
                b.Draw();
                //Debug.Log(q.ToString());
            }

            // angle
            Vector2 size = new Vector2(qx, qy);

            Collider2D[] colls = Physics2D.OverlapBoxAll(q.GetCenter(), size, angle);
            
            if (colls.Length > 0) {
                foreach (Collider2D coll in colls) {
                    float collider_angle = coll.gameObject.transform.eulerAngles.z;
                    //Debug.Log(collider_angle);
                    if (coll.GetType() == typeof(BoxCollider2D) && coll.tag != "Player") {
                        //  BoxCollider2D box = (BoxCollider2D) coll;
                    }
                }
            }
        }
    }

}
