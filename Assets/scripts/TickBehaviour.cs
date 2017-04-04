using UnityEngine;

public class TickBehaviour : MonoBehaviour {

    // when tick dies set is dead flag
    public bool isDead = false;
    public float maxSpeed = 10f;

    public float tickRadiusOffset = 1f;
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

    public void Launch() {
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        Rigidbody2D rigidbody = this.GetComponent<Rigidbody2D>();
        rigidbody.velocity = startVelocity;
    }
    
}
