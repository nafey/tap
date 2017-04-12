using UnityEngine;

public class TickBehaviour : MonoBehaviour {

    // when tick dies set is dead flag
    public bool isDead = false;
    public float maxSpeed = 10f;

    public float tickRadiusOffset = 1f;

    public float ForceMultiplier = 2.5f;
    public GameObject deathLine;

    public Vector3 startVelocity;

    void Update() {
        Rigidbody2D rigidbody = this.GetComponent<Rigidbody2D>();
        // Takes care of death
        if (this.transform.position.y < this.deathLine.transform.position.y) {
            isDead = true;
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        // Max the speed
        if (rigidbody.velocity.magnitude > maxSpeed) {
            rigidbody.velocity = (rigidbody.velocity * this.maxSpeed) / rigidbody.velocity.magnitude;
        }
    }

    void Start() {
        GameBehaviour.instance.RegisterMouseUp(MouseUp);
    }

    private void MouseUp(Vector3 mousePosition) {
        Vector3 adjustedForce = Support.ComputeForce(this.transform.position, Camera.main.ScreenToWorldPoint(mousePosition), this.ForceMultiplier);
        Vector3[] traj = Support.ComputeTrajectory(adjustedForce, this.GetComponent<Rigidbody2D>().gravityScale, this.transform.position, 1.2f);
        Support.DrawTrajectory(traj, 1f);

        this.ApplyVelocity(adjustedForce);
    }



    public void ApplyVelocity(Vector3 velocity) {
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        Rigidbody2D rigidbody = this.GetComponent<Rigidbody2D>();
        rigidbody.velocity = velocity;
    }

    public void Launch() {
        this.ApplyVelocity(startVelocity);
    }

}
