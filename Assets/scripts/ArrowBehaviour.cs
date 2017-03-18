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

        Quad q1 = new Quad(new Vector2(2, 2), new Vector3(1, 0), 0);
        Support.DrawQuad(q1);

        Quad q2 = new Quad(new Vector2(2, 2), new Vector3(0, 0), 45);
        Support.DrawQuad(q2);

        List<Vector3> list = q1.Intersections(q2);

        Debug.Log(list.Count);

        //Segment s1 = new Segment(new Vector3(-1, -1), new Vector3(1, -1));
        //Segment s2 = new Segment(new Vector3(1.4f, 0), new Vector3(0, -1.4f));

        //List<Vector3> list = s1.Intersection(s2);
        //Debug.Log(list.Count);

        //this.RedrawLine();
    }

    public void RedrawLine() {
        float time = 3600f;
        Vector3[] trajectory = Support.ComputeTrajectory(start, accel,
            tick.position, forecastPeriod);

        Quad[] path = Support.ComputePath(trajectory, tick.GetComponent<CircleCollider2D>().radius * 2f);

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

            
            //if (i == 2) {
            //    Border b = new Border(q, 0.1f);
            //    b.Draw();
            //    //Debug.Log(q.ToString());.
            //}

            // angle

            Collider2D[] colls = Physics2D.OverlapBoxAll(q.GetCenter(), new Vector2(qx, qy), angle);
            
            if (colls.Length > 0) {
                foreach (Collider2D coll in colls) {
                    float collider_angle = coll.gameObject.transform.eulerAngles.z;
                    Vector3 size = coll.bounds.size;
                    Vector3 center = coll.bounds.center;

                    Quad other = new Quad(size, center, collider_angle);
                    
                    if (other.Intersections(q).Count > 0) {
                        Debug.Log("Intersections yo");
                    }

                    if (coll.GetType() == typeof(BoxCollider2D) && coll.tag != "Player") {
                        //  BoxCollider2D box = (BoxCollider2D) coll;
                    }
                }
            }
        }
    }

}
