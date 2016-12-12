using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public GameObject tick;

	
	void Update () {
        this.transform.position = new Vector3(this.transform.position.x, this.tick.transform.position.y, this.transform.position.z);
	}
}
