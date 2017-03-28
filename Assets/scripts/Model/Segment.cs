using System.Collections.Generic;
using UnityEngine;

public class Segment  {
    private Vector3 start;
    private Vector3 end;
    private Line line;

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

    public float Slope() {
        if (this.End.x == this.Start.x) {
            return float.PositiveInfinity;
        }
        return (end.y - start.y) / (end.x - start.x);
    }

    public Segment(Vector3 start, Vector3 end) {
        this.end = end;
        this.start = start;
        this.line = new Line(this);
    }

    public Line GetLine() {
        return this.line;
    }

    public bool HasPoint(Vector3 point) {
        Line l = this.GetLine();
        if (l.HasPoint(point)) {
            if (l.Slope == float.PositiveInfinity) {
                float dist1 = Mathf.Abs(point.y - this.start.y);
                float dist2 = Mathf.Abs(point.y - this.end.y);
                float dist = Mathf.Abs(this.start.y - this.end.y);

                return (dist1 <= dist) && (dist2 <= dist);
            } else {
                float dist1 = Mathf.Abs(point.x - this.Start.x);
                float dist2 = Mathf.Abs(point.x - this.End.x);
                float dist = Mathf.Abs(this.Start.x - this.End.x);

                return (dist1 <= dist) && (dist2 <= dist);
            }
        } else {
            return false;
        }
    }

    public List<Vector3> Intersection(Line l) {
        List<Vector3> ret = new List<Vector3>();

        List<Vector3> lineIntersection = this.GetLine().Intersection(l);

        foreach (Vector3 point in lineIntersection) {
            if (this.HasPoint(point)) {
                ret.Add(point);
            }
        }

        return ret;
    }

    public List<Vector3> Intersection(Segment s) {
        List<Vector3> ret = new List<Vector3>();

        Line other = s.GetLine();

        List<Vector3> lineIntersection = this.Intersection(other);

        foreach(Vector3 point in lineIntersection) {
            if (s.HasPoint(point)) {
                ret.Add(point);
            }
        }

        return ret;
    }

    
}
