using System.Collections.Generic;
using UnityEngine;

public class Line{
    private float slope;
    private float intercept;

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

    public Line(float slope, float intercept) {
        this.slope = slope;
        this.intercept = intercept;
    }

    public Line(float slope, Vector3 point) {
        this.slope = slope;
        this.intercept = point.y - slope * point.x;
    }

    public Line (Segment segment) {
        if (segment.End.x == segment.Start.x) {
            this.slope = float.PositiveInfinity;
            this.intercept = float.PositiveInfinity;
        } else {
            this.slope = (segment.End.y - segment.Start.y) / (segment.End.x - segment.Start.x);
            this.intercept = segment.Start.y - slope * segment.Start.x;
        }
    }

    public bool HasPoint(Vector3 point) {
        return (slope * point.x + intercept - point.y == 0);
    }

    public List<Vector3> Intersection(Line l) {
        List<Vector3> ret = new List<Vector3>();

        if (this.Slope != l.Slope) {
            float x = (l.Intercept - this.Intercept) / (this.Slope - l.Slope);
            float y = (l.Slope * this.Intercept - this.Slope * l.Intercept) / (l.Slope - this.Slope);

            ret.Add(new Vector3(x, y));
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
