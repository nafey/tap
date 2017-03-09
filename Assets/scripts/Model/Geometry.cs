using UnityEngine;

public class Geometry {
    public static float Angle(Vector3 from, Vector3 to) {
        float ret = 0f;

        ret = Vector3.Angle(from, to);

        if (Vector3.Dot(Vector3.Cross(from, to), new Vector3(0, 0, 1f)) < 0) {
            ret = ret * -1;
        }

        return ret;
    }
}
