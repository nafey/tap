using UnityEngine;

public class TickBehaviour : MonoBehaviour {
    public GameObject deathLine;

    // when tick dies set is dead flag
    public bool isDead = false;

    void Start() {
    }

    void Update() {
        if (this.transform.position.y < this.deathLine.transform.position.y) {
            isDead = true;
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
