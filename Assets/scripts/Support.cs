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
        Vector3 origin, float bound) {
        int size = 10;
        Vector3[] ret = new Vector3[size];

        float vy = startVelocity.y;
        float vx = startVelocity.x;

        float timeToFall = vy / acceleration;
        float timeToBoundary = (bound - origin.x) / vx;

        
        float timeMax = float.MaxValue;

        if (timeToFall > timeToBoundary) {
            timeMax = timeToBoundary;
        } else {
            timeMax = timeToFall;
        }
        
        for (int i = 0; i < size; i++) {
            float t = timeMax * (((float) i) / ((float) (size - 1f)));
            float x = vx * t;
            float y = computeHeight(vy, acceleration, t);

            ret[i] = new Vector3(x, y) + origin;
        }
        
        return ret;
    }
}

