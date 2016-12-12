using UnityEngine;

public class BlockBehaviour : MonoBehaviour {

    public GameObject tick;
    public float distanceLimit;
    
	void Update () {
        if (this.transform.position.y - tick.transform.position.y > distanceLimit) {
            this.transform.Translate(0f, -distanceLimit, 0f);
        } else if (this.tick.transform.position.y - this.transform.position.y > distanceLimit) {
            this.transform.Translate(0f, distanceLimit, 0f);
        } 
	}
}
