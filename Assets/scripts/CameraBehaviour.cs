using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {
    public GameObject tick;

    public float pushBarHeight;

    private float maxHeight;

    void Start() {
        maxHeight = pushBarHeight;
    }
	
	void Update () {
        if (this.tick.transform.position.y > maxHeight) {
            this.transform.position = new Vector3(this.transform.position.x, this.tick.transform.position.y - pushBarHeight, this.transform.position.z);
            this.maxHeight = this.tick.transform.position.y;
        }
	}
}
