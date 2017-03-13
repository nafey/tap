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

        segments.Add(this.GetParallel(s1, thickness));
        segments.Add(this.GetParallel(s2, thickness));
        segments.Add(this.GetParallel(s3, thickness));
        segments.Add(this.GetParallel(s4, thickness));

        arcs.Add(new Arc(thickness, segments[0].End, segments[1].Start));
        arcs.Add(new Arc(thickness, segments[1].End, segments[2].Start));
        arcs.Add(new Arc(thickness, segments[2].End, segments[3].Start));
        arcs.Add(new Arc(thickness, segments[3].End, segments[0].Start));
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
            
        }
    }

    public Segment GetParallel(Segment seg, float margin) {
        Line l1 = new Line(-1f / seg.Slope(), seg.Start);
        Line l2 = new Line(-1f / seg.Slope(), seg.End);

        float cos = Mathf.Pow(1 + l1.Slope * l1.Slope, -0.5f);
        Vector3 s1 = new Vector3(seg.Start.x + margin * cos, l1.Slope * (seg.Start.x + margin * cos) + l1.Intercept);
        Vector3 s2 = new Vector3(seg.Start.x - margin * cos, l1.Slope * (seg.Start.x - margin * cos) + l1.Intercept);

        float sign1 = Vector3.Dot(Vector3.Cross(s1 - seg.Start, seg.End - seg.Start), new Vector3(0, 0, 1));
        float sign2 = Vector3.Dot(Vector3.Cross(s2 - seg.Start, seg.End - seg.Start), new Vector3(0, 0, 1));

        if (sign1 < sign2) {
            Vector3 end = new Vector3(seg.End.x + margin * cos, l1.Slope * (seg.End.x + margin * cos) + l2.Intercept);
            return new Segment(s1, end);
        }
        else {
            Vector3 end = new Vector3(seg.End.x - margin * cos, l1.Slope * (seg.End.x - margin * cos) + l2.Intercept);
            return new Segment(s2, end);
        }
    }
}
