using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Border {
    private Quad q;

    private List<Segment> segments;
    private List<Arc> arcs;
    private float thickness;

    public Border(Quad q, float thickness) {
        this.q = q;
        this.thickness = thickness;

        segments = new List<Segment>();
        arcs = new List<Arc>();

        Segment s1 = new Segment(q.Verts[0], q.Verts[1]);
        Segment s2 = new Segment(q.Verts[1], q.Verts[2]);
        Segment s3 = new Segment(q.Verts[2], q.Verts[3]);
        Segment s4 = new Segment(q.Verts[3], q.Verts[0]);

        segments.Add(s1.GetParallel(thickness));
        segments.Add(s2.GetParallel(thickness));
        segments.Add(s3.GetParallel(thickness));
        segments.Add(s4.GetParallel(thickness));

        arcs.Add(new Arc(q.Verts[0], thickness, segments[0].End, segments[1].Start));
        arcs.Add(new Arc(q.Verts[1], thickness, segments[1].End, segments[2].Start));
        arcs.Add(new Arc(q.Verts[2], thickness, segments[2].End, segments[3].Start));
        arcs.Add(new Arc(q.Verts[3], thickness, segments[3].End, segments[0].Start));
    }

    public void Draw() {
        foreach (Segment s in segments) {
            Debug.DrawLine(s.Start, s.End, Color.magenta, 100);
        }

        foreach (Arc a in arcs) {
            List<Vector3> traj = a.GetTrajectory();

            for (int i = 1; i < traj.Count; i++) {
                Vector3 start = traj[i - 1];
                Vector3 end = traj[i];

                Debug.DrawLine(start, end, Color.cyan, 1000);
            }

            return;
        }
    }
}
