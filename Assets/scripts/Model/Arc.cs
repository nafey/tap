﻿using System.Collections.Generic;
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

    public Arc(Vector3 origin, float radius) {
        this.origin = origin;
        this.radius = radius;
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
                if (!this.HasPoint(point)) {
                    return false;
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
}
