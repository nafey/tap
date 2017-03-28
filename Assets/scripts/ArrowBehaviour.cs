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

        //Quad q1 = new Quad(new Vector2(2, 2), new Vector3(1, 0), 0);
        //Support.DrawQuad(q1);

        //Quad q2 = new Quad(new Vector2(2, 2), new Vector3(0, 0), 45);
        //Support.DrawQuad(q2);

        //List<Vector3> list = q1.Intersections(q2);

        //Debug.Log(list.Count);

        //Segment s1 = new Segment(new Vector3(-1, -1), new Vector3(1, -1));
        //Segment s2 = new Segment(new Vector3(1.4f, 0), new Vector3(0, -1.4f));

        //List<Vector3> list = s1.Intersection(s2);
        //Debug.Log(list.Count);

        //Quad q = this.GetQuad(box.GetComponent<Collider2D>());
        //Border b = new Border(q, 0.08f);
        //b.Draw();

        //Debug.DrawLine(new Vector2(0, 0), new Vector2(0.17f, 0), Color.red, 1000);
        this.RedrawLine();

        //Vector3 v = new Vector3(1, 0, 0);
        //v = Quaternion.Euler(0, 0, 45) * (v);
        //Debug.Log(v);
    }

    public void RedrawLine() {
        float time = 3600f;
        Vector3[] trajectory = Support.ComputeTrajectory(start, accel,
            tick.position, forecastPeriod);

        Quad[] path = Support.ComputePath(trajectory, 0.17f );

        for (int i = 0; i < trajectory.Length - 1; i++) {
            Debug.DrawLine(trajectory[i], trajectory[i + 1], Color.magenta, time);
        }

        //for (int i = 0; i < path.Length; i++) {
        //    Quad q = path[i];

        //    q.Draw();
        //}

        // Find the collision for path section
        for (int i = 0; i < path.Length; i++) {
            Quad q = path[i];

            float qx = (q.Verts[1] - q.Verts[0]).magnitude;
            float qy = (q.Verts[2] - q.Verts[1]).magnitude;

            Vector2 side = (q.Verts[1] - q.Verts[0]);

            float tan = side.y / side.x;
            float radian = Mathf.Atan(tan);
            float angle = 57.3f * radian;

            // angle
            Collider2D[] colls = Physics2D.OverlapBoxAll(q.GetCenter(), new Vector2(qx, qy), angle, LayerMask.GetMask("Default"));
            
            foreach (Collider2D coll in colls) {
                Quad coll_quad = this.GetQuad(coll);

                Border b = new Border(coll_quad, 0.08f);
                b.Draw();

                Line l = b.TrajectoryIntersection(trajectory);

                Debug.DrawRay(l.ExamplePoint, this.DirFromSlope(l.Slope), Color.cyan, 1000);
                return;

                //List<Vector3> intersections //(trajectory[i]);
                
                //if (intersections.Count > 0) {
                //    //Debug.DrawLine(intersections[0], intersections[1], Color.red, 1000);
                //    return;
                //}
            }
        }
    }

    private Vector3 DirFromSlope(float slope) {
        return new Vector3(Mathf.Pow(1 + slope * slope, -0.5f), Mathf.Pow(1 + slope * slope, -0.5f) * slope);
    }
}
