using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour {
    private Vector3[] line;
    private LineRenderer lineRenderer;
    public Transform tick;
    public Vector3 start;
    public float accel;

    public void Start() {
        this.lineRenderer = this.GetComponent<LineRenderer>();
        this.redrawLine();
    }

    public void redrawLine() {
        Vector3 startVelocity = start;

        Vector3[] trajectory = Support.computeTrajectory(startVelocity, accel, 
            tick.position, 1    );

        this.renderLine(trajectory);
    }

    public void renderLine(Vector3[] newLine) {
        line = newLine;
        this.lineRenderer.numPositions = newLine.Length;
        this.lineRenderer.SetPositions(line);
    }   
}
