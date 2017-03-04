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

    public float slope() {
        return (end.y - start.y) / (end.x - start.y);
    }

    public Segment(Vector3 start, Vector3 end) {
        this.end = end;
        this.start = start;
    }

    
}
