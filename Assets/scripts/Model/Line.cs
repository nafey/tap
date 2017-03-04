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

    public Line (Segment segment) {
        float slope = (segment.End.y - segment.Start.y) / (segment.End.x - segment.Start.x);
    }
}
