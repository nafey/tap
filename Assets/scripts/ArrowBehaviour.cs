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

        Vector3 origin = new Vector3(0, 0, 0);
        float x = 0.02f;
        float y = 0.02f;
        float cx = 30;
        float cy = 30;

        Vector3 current = new Vector3(0, 0, 0);

        for (int j = 0; j < cy; j++) {
            for (int i = 0; i < cx; i++) {
                current = origin + new Vector3(i * x, j * y, 0f);

                GameObject.Instantiate(block, current, Quaternion.identity, holder.transform);
            }
        }


        this.redrawLine();
    }

    public void Update() {
        //for (int i = 0; i < holder.transform.childCount; i++) {
        //    Transform child = holder.transform.GetChild(i);
        //    child.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        //}

        //Vector3 p1 = new Vector3(6.538494f, 0.09367175f, 0f);
        //Vector3 p2 = new Vector3(7.927383f, 0.9115727f, 0f);
        //Vector3 p3 = new Vector3(8.739284f, -0.4671282f, 0f);
        //Vector3 p4 = new Vector3(7.350395f, -1.285029f, 0f);

        //Quad q = new Quad(p1, p2, p3, p4);

        //float time = 1f;

        //Debug.DrawLine(q.Verts[0], q.Verts[1], Color.cyan, time);
        //Debug.DrawLine(q.Verts[1], q.Verts[2], Color.cyan, time);
        //Debug.DrawLine(q.Verts[2], q.Verts[3], Color.cyan, time);
        //Debug.DrawLine(q.Verts[3], q.Verts[0], Color.cyan, time);

        //float qx = (q.Verts[1] - q.Verts[0]).magnitude;
        //float qy = (q.Verts[2] - q.Verts[1]).magnitude;

        //Vector2 side = (q.Verts[1] - q.Verts[0]);

        //float tan = side.y / side.x;
        //float radian = Mathf.Atan(tan);
        //float angle = 57.3f * radian;
        //angle = 0;

        ////if (i == 0  ) {
        ////    Debug.Log(angle);
        ////    Debug.Log(q.ToString());
        ////}

        //// angle
        //Vector2 size = new Vector2(qx, qy);

        ////Vector3 size = new Vector3(0.5f, 0.25f, 0f);
        ////float angle = 45f;


        //Collider2D[] colls = Physics2D.OverlapBoxAll(q.GetCenter(), size, angle);
        //foreach (Collider2D coll in colls) {
        //    coll.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        //}
        //redrawLine();
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

            if (i == 5) {
                Debug.Log(q.ToString());
            }

            // angle
            Vector2 size = new Vector2(qx, qy);

            Collider2D[] colls = Physics2D.OverlapBoxAll(q.GetCenter(), size, angle);
            
            if (colls.Length > 0) {
                foreach (Collider2D coll in colls) {
                    float collider_angle = coll.gameObject.transform.eulerAngles.z;
                    Debug.Log(collider_angle);

                    
                    if (coll.GetType() == typeof(BoxCollider2D) && coll.tag != "Player") {
                        BoxCollider2D box = (BoxCollider2D) coll;
                        
                        
                    }
                }
            }
        }
    }

}
