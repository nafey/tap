using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle {
    private Vector3 origin;
    private float radius;
    private float tolerance = 0.0001f;

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

    public Circle(Vector3 origin, float radius) {
        this.origin = origin;
        this.radius = radius;
    }

    public bool Intersects(Line l) {
        float distance = l.Distance(this.Origin);
        return (distance <= Radius);
    }

    public List<Vector3> Intersection(Line l) {
        List<Vector3> intersections = new List<Vector3>();
        if (this.Intersects(l)) {
            float m = l.Slope;
            float d = l.Intercept;
            float x1 = this.origin.x;
            float y1 = this.origin.y;
            float r = this.radius;

            float a = (1 + m * m);
            float b = 2 * m * (d - y1) - 2 * x1;
            float c = x1 * x1 - r * r + (d - y1) * (d - y1);

            float disc = b * b - 4 * a * c;

            if (disc == 0) {
                float x2 = -b / (2 * a);
                float y2 = m * x2 + d;

                intersections.Add(new Vector3(x2, y2, 0));
            } else {
                disc = Mathf.Sqrt(disc);
                float x2 = (-b + disc) / (2 * a);
                float y2 = m * x2 + d;

                intersections.Add(new Vector3(x2, y2, 0));

                float x3 = (-b - disc) / (2 * a);
                float y3 = m * x3 + d;

                intersections.Add(new Vector3(x3, y3, 0));
            }
        }

        return intersections;
    }

    public List<Vector3> Intersection(Segment s) {
        List<Vector3> intersections = new List<Vector3>();
        Line l = s.GetLine();
        if (this.Intersects(l)) {
            List<Vector3> lineIntersection = this.Intersection(l);

            foreach (Vector3 point in lineIntersection) {
                if (s.HasPoint(point)) {
                    intersections.Add(point);
                }
            }
        }

        return intersections;
    }

    public bool HasPoint(Vector3 point) {
        return ((Vector3.Distance(point, this.Origin) / Radius) < tolerance);
    }
}
