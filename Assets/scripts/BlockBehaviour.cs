using UnityEngine;

public class BlockBehaviour : MonoBehaviour {

    public GameObject tick;
    public float distanceLimit;
    
	void Update () {
        bool tick_dead = this.tick.GetComponent<TickBehaviour>().isDead;
        if (!tick_dead) {
            if (this.transform.position.y - tick.transform.position.y > distanceLimit) {
                this.transform.Translate(0f, -distanceLimit, 0f);
            } else if (this.tick.transform.position.y - this.transform.position.y > distanceLimit) {
                this.transform.Translate(0f, distanceLimit, 0f);
            } 
        }
	}
}
