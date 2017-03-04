using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quad {
    private Vector3[] verts;
    private Vector3 center;


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
        center = (p1 + p2 + p3 + p4) / 4;
    }

    public Vector3 GetCenter() {
        return center;
    }

    public override string ToString() {
        string ret = "";

        ret += "p1 is (" + verts[0].x + ", " + verts[0].y + ") ";
        ret += "p2 is (" + verts[1].x + ", " + verts[1].y + ") ";
        ret += "p3 is (" + verts[2].x + ", " + verts[2].y + ") ";
        ret += "p4 is (" + verts[3].x + ", " + verts[3].y + ") ";

        return ret;
    }
    
}
