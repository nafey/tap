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

    public Segment GetParallel(float margin, Vector3 opposite) {
        // For start
        Line l1 = new Line(-1f / this.Slope(), this.Start);
        float cos = Mathf.Pow(1 + l1.Slope * l1.Slope, -0.5f);
        Vector3 s1 = new Vector3(this.Start.x + margin * cos, l1.Slope * (this.Start.x + margin * cos) + l1.Intercept);
        Vector3 s2 = new Vector3(this.Start.x - margin * cos, l1.Slope * (this.Start.x - margin * cos) + l1.Intercept);

        Line l2 = new Line(-1f / this.Slope(), this.End);
        Vector3 e1 = new Vector3(this.End.x + margin * cos, l1.Slope * (this.End.x + margin * cos) + l2.Intercept);
        Vector3 e2 = new Vector3(this.End.x - margin * cos, l1.Slope * (this.End.x - margin * cos) + l2.Intercept);

        if (Vector3.Distance(opposite, s1) < Vector3.Distance(opposite, s2)) {
            return new Segment(s2, e2);
        } else {
            return new Segment(s1, e1);
        }
    }
}
