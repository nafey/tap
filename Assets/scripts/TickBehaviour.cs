using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickBehaviour : MonoBehaviour {
    private Plane[] planes;
    

    public bool isDead = false;
    // when tick dies set is dead flag
    
    void Start() {
        planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
    }


    void Update() {
        if (!GeometryUtility.TestPlanesAABB(planes, this.GetComponent<Collider2D>().bounds)) {
            this.isDead = true;
        }
    }
}
