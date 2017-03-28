using System.Collections.Generic;
using UnityEngine;

public class Line{
    private float slope;
    private float intercept;
    private Vector3 examplePoint;
    private float tolerance = 0.001f;

    public float Slope {
        get {
            return slope;
        }
    }

    public float Intercept {
        get {
            return intercept;
        }
    }

    public Vector3 ExamplePoint {
        get {
            return examplePoint;
        }
    }

    public Line(float slope, float intercept) {
        this.slope = slope;
        this.intercept = intercept;

        this.examplePoint = new Vector3(0, this.intercept);
    }

    public Line(float slope, Vector3 point) {
        this.slope = slope;
        this.intercept = point.y - slope * point.x;

        this.examplePoint = point;
    }

    public Line (Segment segment) {
        if (segment.End.x == segment.Start.x) {
            this.slope = float.PositiveInfinity;
            this.intercept = float.PositiveInfinity;
        } else {
            this.slope = (segment.End.y - segment.Start.y) / (segment.End.x - segment.Start.x);
            this.intercept = segment.Start.y - slope * segment.Start.x;
        }

        this.examplePoint = segment.Start;
    }

    public bool HasPoint(Vector3 point) {
        if (this.slope != float.PositiveInfinity) {
            return (Mathf.Abs(slope * point.x + intercept - point.y) < this.tolerance);
        } else {
            return (point.x - this.examplePoint.x < this.tolerance);
        }
    }

    public List<Vector3> Intersection(Line l) {
        List<Vector3> ret = new List<Vector3>();

        if (this.Slope != l.Slope) {
            if (Mathf.Abs(this.slope) == float.PositiveInfinity) {
                float x = this.examplePoint.x;
                float y = l.slope * x + l.intercept;

                ret.Add(new Vector3(x, y));
            } else if (Mathf.Abs(l.slope) == float.PositiveInfinity) {
                float x = l.examplePoint.x;
                float y = this.slope * x + this.intercept;

                ret.Add(new Vector3(x, y));
            } else {
                float x = (l.Intercept - this.Intercept) / (this.Slope - l.Slope);
                float y = (l.Slope * this.Intercept - this.Slope * l.Intercept) / (l.Slope - this.Slope);

                ret.Add(new Vector3(x, y));
            }
        }

        return ret;
    }

    public float Distance(Vector3 point) {
        float perpSlope = -1f / this.Slope;

        Line perp = new Line(perpSlope, point);
        List<Vector3> intersection = this.Intersection(perp);
            
        return Vector3.Distance(point, intersection[0]);
    }
}
