using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour {
    public Transform tick;
    private Vector3 start;
    private float accel;

    private bool active = false;


    private Vector3 CalculateForce(Vector3 tickPosition, Vector3 mousePosition, float forceMultiplier) {
        Vector2 raw_force = tickPosition - mousePosition;
        Vector2 raw_unit_force = raw_force / raw_force.magnitude;
        Vector2 inverse_force = raw_unit_force / raw_force.magnitude;

        Vector2 adjusted_force = inverse_force * forceMultiplier;

        float forceMax = 1000;

        if (adjusted_force.magnitude > forceMax) {
            adjusted_force = adjusted_force * (forceMax / adjusted_force.magnitude);
        }

        return adjusted_force;
    }

    private void MouseHold(Vector3 mousePosition) {
        if (this.active) {
            Vector3 adjustedForce = this.CalculateForce(tick.transform.position, Camera.main.ScreenToWorldPoint(mousePosition), tick.GetComponent<TickBehaviour>().ForceMultiplier);
            Vector3[] traj = Support.ComputeTrajectory(adjustedForce, tick.GetComponent<Rigidbody2D>().gravityScale, tick.transform.position, 1.2f);
            this.DrawTrajectory(traj);
        }
    }


    private void MouseDown(Vector3 mousePosition) {
        this.active = true;
    }

    private void MouseUp(Vector3 mousePosition) {
        this.active = false;

        Vector3 adjustedForce = this.CalculateForce(tick.transform.position, Camera.main.ScreenToWorldPoint(mousePosition), tick.GetComponent<TickBehaviour>().ForceMultiplier);
        Vector3[] traj = Support.ComputeTrajectory(adjustedForce, tick.GetComponent<Rigidbody2D>().gravityScale, tick.transform.position, 1.2f);
        this.DrawTrajectory(traj, 1f);

        tick.GetComponent<TickBehaviour>().ApplyVelocity(adjustedForce);
    }

    private void DrawTrajectory(Vector3[] trajectory, float time = 0.02f) {
        for (int i = 0; i < trajectory.Length - 1; i++) {
            Debug.DrawLine(trajectory[i], trajectory[i + 1], Color.magenta, time);
        }
    }

    private void RedrawLine() {
        this.DrawTrajectory(Support.ComputeTrajectory(start, accel, tick.position, 1.2f));
    }


    public void Start() {
        GameBehaviour.instance.RegisterMouseDown(MouseDown);
        GameBehaviour.instance.RegisterMouseUp(MouseUp);
        GameBehaviour.instance.RegisterMouseHold(MouseHold);

        start = tick.GetComponent<TickBehaviour>().startVelocity;
        accel = tick.GetComponent<Rigidbody2D>().gravityScale;

        this.RedrawLine();
    }

    //void Update() {
    //    if (Input.GetMouseButton(0)) {
    //        this.isDown = true;
    //        //Debug.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition), tick.transform.position, Color.red, 0.05f);

    //        this.downPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    }

    //    if (Input.GetMouseButtonUp(0)) {
    //        this.isDown = false;

    //        Vector2 raw_force = tick.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        Vector2 raw_unit_force = raw_force / raw_force.magnitude;

    //        Vector2 inverse_force = raw_unit_force / raw_force.magnitude;

    //        Vector2 adjusted_force = inverse_force * forceMultiplier;

    //        //Debug.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition), tick.transform.position, Color.red, 2f);

    //        if (adjusted_force.magnitude > forceMax) {
    //            adjusted_force = adjusted_force * (forceMax / adjusted_force.magnitude);
    //        }

    //        tick.GetComponent<Rigidbody2D>().AddForce(adjusted_force);
    //    }
    //}-+

}
