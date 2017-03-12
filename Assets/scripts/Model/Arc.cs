using System.Collections.Generic;
using UnityEngine;

public class Arc {
    private Vector3 start;
    private Vector3 end;
    private Vector3 origin;
    private float radius;

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

    public Arc(Vector3 origin, float radius, Vector3 start, Vector3 end) {
        this.origin = origin;
        this.radius = radius;
        this.start = start;
        this.end = end;
    }

    public Circle GetCircle() {
        return new Circle(origin, radius);
    } 

    public bool HasPoint(Vector3 point) {
        if (this.GetCircle().HasPoint(point)) {
            float theta12 = Geometry.Angle(start, end);
            float theta1 = Geometry.Angle(start, point - origin);

            if (theta12 > 0) {
                if (theta1 > 0) {
                    if (theta1 < theta12) {
                        return true;
                    }
                } 
            } else {
                if (theta1 > 0) {
                    return true;
                } else {
                    if (theta1 < theta12) {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public bool Intersects(Line l) {
        if (this.GetCircle().Intersects(l)) {
            List<Vector3> intersections = this.GetCircle().Intersection(l);

            foreach(Vector3 point in intersections) {
                if (this.HasPoint(point)) {
                    return true;
                }
            }
        } else {
            return false;
        }

        return true;
    }

    public List<Vector3> Intersection(Line l) {
        List<Vector3> ret = new List<Vector3>();
        if (this.Intersects(l)) {
            List<Vector3> intersections = this.GetCircle().Intersection(l);

            foreach(Vector3 point in intersections) {
                if (this.HasPoint(point)) {
                    ret.Add(point);
                }
            }
        }

        return ret;
    }

    public List<Vector3> Intersection(Segment s) {
        List<Vector3> ret = new List<Vector3>();

        List<Vector3> lineIntersections = this.Intersection(s.GetLine());

        foreach (Vector3 point in lineIntersections) {
            if (this.HasPoint(point)) {
                ret.Add(point);
            }
        }

        return ret;
    }

    public List<Vector3> GetTrajectory() {
        List<Vector3> list = new List<Vector3>();
        Vector3 current = this.Start - this.Origin;

        float angle = Vector3.Angle(this.Start - this.Origin, this.End - this.Origin);
        for (float w = 0; w < angle; w = w + 5f) {
            list.Add(current + this.Origin);
            current = Quaternion.Euler(0, 0, 5) * current;
        }

        list.Add(this.End);

        return list;
    }
}
