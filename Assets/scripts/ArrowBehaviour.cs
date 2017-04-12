using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour {
    public Transform tick;

    private bool active = false;

    private void MouseHold(Vector3 mousePosition) {
        Vector3 adjustedForce = Support.ComputeForce(tick.transform.position, Camera.main.ScreenToWorldPoint(mousePosition), tick.GetComponent<TickBehaviour>().ForceMultiplier);
        Vector3[] traj = Support.ComputeTrajectory(adjustedForce, tick.GetComponent<Rigidbody2D>().gravityScale, tick.transform.position, 1.2f);
        Support.DrawTrajectory(traj);
    }

    public void Start() {
        GameBehaviour.instance.RegisterMouseHold(MouseHold);

        Vector3 start = tick.GetComponent<TickBehaviour>().startVelocity;
        float accel = tick.GetComponent<Rigidbody2D>().gravityScale;

        Support.DrawTrajectory(Support.ComputeTrajectory(start, accel, tick.position, 1.2f));
    }


}
