using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {
    public GameObject tick;
    private float maxHeight;
	
	void Update () {
        if (this.tick.transform.position.y > maxHeight) {
            this.transform.position = new Vector3(this.transform.position.x, this.tick.transform.position.y, this.transform.position.z);
            this.maxHeight = this.tick.transform.position.y;
        }
	}
}
