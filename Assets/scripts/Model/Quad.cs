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

    private Vector3 Rotate(Vector3 v, float angle) {
        angle = Mathf.Deg2Rad* angle;
        float x = v.x * Mathf.Cos(angle) - v.y * Mathf.Sin(angle);
        float y = v.y * Mathf.Cos(angle) + v.x * Mathf.Sin(angle);

        return new Vector3(x, y);
    }

    public Quad(Vector3 size, Vector3 center, float rotation) {
        //center += new Vector3(0.02f, 0.02f);
        this.verts = new Vector3[4];
        Verts[0] = center + Rotate(new Vector3(-size.x / 2, -size.y / 2), rotation);
        Verts[1] = center + Rotate(new Vector3(size.x / 2, -size.y / 2), rotation);
        Verts[2] = center + Rotate(new Vector3(size.x / 2, size.y / 2), rotation);
        Verts[3] = center + Rotate(new Vector3(-size.x / 2, size.y / 2), rotation);
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

    public List<Segment> GetSegments() {
        List<Segment> ret = new List<Segment>();

        ret.Add(new Segment(verts[0], verts[1]));
        ret.Add(new Segment(verts[1], verts[2]));
        ret.Add(new Segment(verts[2], verts[3]));
        ret.Add(new Segment(verts[3], verts[0]));


        return ret;
    }

    public List<Vector3> Intersections(Quad q) {
        List<Vector3> ret = new List<Vector3>();

        List<Segment> mySegments = this.GetSegments();
        List<Segment> otherSegments = q.GetSegments();

        for (int i = 0; i < mySegments.Count; i++) {
            for (int j = 0; j < otherSegments.Count; j++) {
                List<Vector3> point = (mySegments[i].Intersection(otherSegments[j]));

                if (point.Count > 0) {
                    ret.Add(point[0]);
                }
            }
        }

        return ret;
    }

    public void Draw() {
        Debug.DrawLine(this.Verts[0], this.Verts[1], Color.cyan, 1000);
        Debug.DrawLine(this.Verts[1], this.Verts[2], Color.cyan, 1000);
        Debug.DrawLine(this.Verts[2], this.Verts[3], Color.cyan, 1000);
        Debug.DrawLine(this.Verts[3], this.Verts[0], Color.cyan, 1000);
    }
    
}
