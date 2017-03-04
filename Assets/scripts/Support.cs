using UnityEngine;

public class Support {

    private static Support instance;

    private Support() {}

    public static Support getInstance() {
        if (instance == null) {
            instance = new Support();
        }

        return instance;
    }

    public static Vector3 computeVelocity(Vector3 normal, Vector3 velocity) {
        Vector3 tangent = new Vector3(-normal.y, normal.x);
        float a = Vector3.Dot(velocity, normal);
        float b = Vector3.Dot(velocity, tangent);

        return a * normal + b * tangent;
    }

    private static float computeHeight(float v0, float a, float time) {
        return v0 * time - a * time * time / 2f;
    }

    public static Vector3[] computeTrajectory(Vector3 startVelocity, float acceleration, 
        Vector3 origin, float timeBound) {
        int size = 10;
        Vector3[] ret = new Vector3[size];

        float vy = startVelocity.y;
        float vx = startVelocity.x;

        float timeToTop = vy / acceleration;        
        float timeMax = float.MaxValue;

        if (timeToTop > 0 && timeToTop < timeBound) {
            timeMax = timeToTop;
        } else {
            timeMax = timeBound;
        }
        
        for (int i = 0; i < size; i++) {
            float t = timeMax * (((float) i) / ((float) (size - 1f)));
            float x = vx * t;
            float y = computeHeight(vy, acceleration, t);

            ret[i] = new Vector3(x, y) + origin;
        }
        
        return ret;
    }

    public static Quad[] computePath(Vector3[] traj, float size) {
        Quad[] quads = new Quad[traj.Length - 1];
        Vector3 zpos = new Vector3(0, 0, 1f);
        Vector3 zneg = new Vector3(0, 0, -1f);

        for (int i = 0; i < (traj.Length - 1); i++) {
            Vector3 start = traj[i];
            Vector3 end = traj[i + 1];

            Vector3 line = end - start;
            Vector3 perp1 = Vector3.Cross(line, zpos);
            perp1 = (perp1 * size) / (2 * perp1.magnitude);

            Vector3 perp2 = Vector3.Cross(line, zneg);
            perp2 = (perp2 * size) / (2 * perp2.magnitude);

            Vector3 p1 = start + perp2;
            Vector3 p2 = end + perp2;
            Vector3 p3 = end + perp1;
            Vector3 p4 = start + perp1;

            Quad q = new Quad(p1, p2, p3, p4);
            quads[i] = q;
        }

        return quads;
    }

}

