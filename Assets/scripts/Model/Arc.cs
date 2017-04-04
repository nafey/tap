using System.Collections.Generic;
using UnityEngine;

public class Arc {
    private Vector3 start;
    private Vector3 end;
    private Vector3 origin;
    private float radius;
    private List<Segment> trajectory;

    public Vector3 Origin {
        get {
            return origin;
        }
    }

    public float Radius {
        get {
            return radius;
        }
    }

    public Vector3 Start {
        get {
            return start;
        }
    }

    public Vector3 End {
        get {
            return end;
        }
    }

    public Arc(float radius, Vector3 start, Vector3 end, int segments = 3) {
        //this.origin = origin;
        this.radius = radius;
        this.start = start;
        this.end = end;

        this.trajectory = new List<Segment>();

        Vector3 mid = (start + end) / 2;
        float mid_length = (mid - this.Start).magnitude; 

        if (mid_length > radius) {
            return;
        }

        Line l = new Line(-1f / new Segment(this.start, this.end).Slope(), mid);
        Circle c = new Circle(this.start, this.radius);
        List<Vector3> list = c.Intersection(l);
        Vector3 s1 = list[0];
        Vector3 s2 = list[1];
        
        float sign = Vector3.Dot(Vector3.Cross(this.End - mid, s1 - mid), new Vector3(0, 0, 1f));

        if (sign < 0) {
            this.origin = s1;
        } else {
            this.origin = s2;
        }

        // make trajectory with 2 points
        float angle = Vector3.Angle(this.start - this.origin, this.end - this.origin);
        float quanta = angle / segments;

        Vector3 last = this.Start - this.Origin;
        Vector3 current = this.Start - this.Origin;

        for (float i = 0; i < segments; i++) {
            current = Quaternion.Euler(0, 0, -quanta) * current;
            this.trajectory.Add(new Segment(last + this.origin, current + this.origin));
            last = Quaternion.Euler(0, 0, -quanta) * last;
        }
    }

    public Circle GetCircle() {
        return new Circle(origin, radius);
    }

    public List<Vector3> Intersection(Line l) {
        List<Vector3> ret = new List<Vector3>();
        foreach (Segment seg in this.trajectory) {
            List<Vector3> intersections = seg.Intersection(l);

            foreach (Vector3 point in intersections) {
                ret.Add(point);
            }
        }

        return ret;
    }

    public List<Vector3> Intersection(Segment s) {
        List<Vector3> ret = new List<Vector3>();

        foreach (Segment seg in this.trajectory) {
            List<Vector3> intersections = seg.Intersection(s);
            
            foreach (Vector3 point in intersections) {
                ret.Add(point);
            }
        }

        return ret;
    }

    public Line ClosestIntersection(Segment s) {
        float slope = float.NaN;
        float dist = float.MaxValue;
        Vector3 v = new Vector3(0, 0);

        foreach (Segment seg in this.trajectory) {
            List<Vector3> intersections = seg.Intersection(s);

            foreach (Vector3 point in intersections) {
                if (Vector3.Distance(s.Start, point) < dist) {
                    v = point;
                    slope = seg.Slope();
                    dist = Vector3.Distance(s.Start, point);
                }
            }
        }

        return new Line(slope, v);
    }

    public List<Segment> GetTrajectory() {
        return this.trajectory;
    }
}
