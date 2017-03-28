using System.Collections.Generic;
using UnityEngine;

public class Border {
    //private Quad q;
    //private float thickness;

    private List<Segment> segments;
    private List<Arc> arcs;

    public Border(Quad q, float thickness) {
        //this.q = q;
        //this.thickness = thickness;

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
            List<Segment> segments = a.GetTrajectory();

            for (int i = 0; i < segments.Count; i++) {
                Vector3 start = segments[i].Start;
                Vector3 end = segments[i].End;

                Debug.DrawLine(start, end, Color.cyan, 1000);
            }
        }
    }

    private Segment GetParallel(Segment seg, float margin) {
        // Handle flat lines
        if (seg.Start.y == seg.End.y) {
            int sign = 1;
            if (seg.Start.x > seg.End.x) {
                sign = -1;
            }

            Vector3 start = new Vector3(seg.Start.x, seg.Start.y + sign * margin);
            Vector3 end = new Vector3(seg.End.x, seg.End.y + sign * margin);

            return new Segment(start, end);
        }


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

    public Line ClosestIntersection(Segment seg) {
        float slope = float.NaN;
        float dist = float.MaxValue;
        Vector3 pt = new Vector3(0, 0);

        foreach (Arc a in this.arcs) {
            Line l = a.ClosestIntersection(seg);
            if (!float.IsNaN(l.Slope)) {
                if (Vector3.Distance(l.ExamplePoint, seg.Start) < dist) {
                    dist = Vector3.Distance(l.ExamplePoint, seg.Start);
                    slope = l.Slope;
                    pt = l.ExamplePoint;
                }
            }
        }

        foreach (Segment s in this.segments) {
            List<Vector3> intersections = s.Intersection(seg);

            foreach (Vector3 intersection in intersections) {
                if (Vector3.Distance(intersection, seg.Start) < dist) {
                    dist = Vector3.Distance(intersection, seg.Start);
                    slope = s.Slope();
                    pt = intersection;
                }
            }
        }

        return new Line(slope, pt);
    }

    public Line TrajectoryIntersection(List<Segment> lst) {
        float slope = float.NaN;
        Vector3 pt = new Vector3(0, 0);
        for (int i = 0; i < lst.Count; i++) {
            Line l = this.ClosestIntersection(lst[i]);

            if (!float.IsNaN(l.Slope)) {
                return l;
            }
        }

        return new Line(slope, pt);
    }

    public Line TrajectoryIntersection(Vector3[] lst) {
        float slope = float.NaN;
        Vector3 pt = new Vector3(0, 0);
        for (int i = 0; i < lst.Length - 1; i++) {
            Segment seg = new Segment(lst[i], lst[i + 1]);
            Line l = this.ClosestIntersection(seg);

            if (!float.IsNaN(l.Slope)) {
                return l;
            }
        }

        return new Line(slope, pt);
    }
}
