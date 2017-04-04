using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quad {
    private Vector3[] verts;

    public Vector3[] Verts {
        get {
            return verts;
        }
    }

    public Quad(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4) {
        verts = new Vector3[4];
        Verts[0] = p1;
        Verts[1] = p2;
        Verts[2] = p3;
        Verts[3] = p4;
    }
}
