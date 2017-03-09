using UnityEngine;

public class Segment  {
    private Vector3 start;
    private Vector3 end;

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
        return (end.y - start.y) / (end.x - start.y);
    }

    public Segment(Vector3 start, Vector3 end) {
        this.end = end;
        this.start = start;
    }

    public Line GetLine() {
        return new Line(this);
    }

    public bool HasPoint(Vector3 point) {
        Line l = this.GetLine();
        if (l.HasPoint(point)) {
            float dist1 = Mathf.Abs(point.x - this.Start.x);
            float dist2 = Mathf.Abs(point.x - this.End.x);
            float dist = Mathf.Abs(this.Start.x - this.End.x);

            return (dist1 < dist) && (dist2 < dist);
        } else {
            return false;
        }
    }

    public bool Intersects(Line l) {
        Line me = this.GetLine();

        if (me.Intersects(l)) {
            Vector3 p = me.Intersection(l);
            if (this.HasPoint(p)) {
                return true;
            }
        }

        return false;
    }

    public bool Intersects(Segment s) {
        Line l = s.GetLine();

        if (this.Intersects(l)) {
            Vector3 point = this.Intersection(l);
            return s.HasPoint(point);
        }

        return false;
    }

    public Vector3 Intersection(Line l) {
        return this.GetLine().Intersection(l);
    }

    public Vector3 Intersection(Segment s) {
        return this.Intersection(s.GetLine());
    }
}
