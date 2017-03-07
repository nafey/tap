using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arc {
    private float start;
    private float end;

    private Vector3 origin;
    private float radius;

    public Arc(float start, float end, Vector3 origin, float radius) {
        this.start = start;
        this.end = end;

        this.origin = origin;
        this.radius = radius;
    }

    public Circle getCircle() {
        return new Circle(this.origin, this.radius);
    }
}
