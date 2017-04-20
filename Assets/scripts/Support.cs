using UnityEngine;

public class Support {
    private static Support instance;
    private Support() {}

    public static Support GetInstance() {
        if (instance == null) {
            instance = new Support();
        }

        return instance;
    }

    public static Vector3 ComputeVelocity(Vector3 normal, Vector3 velocity) {
        Vector3 tangent = new Vector3(-normal.y, normal.x);
        float a = Vector3.Dot(velocity, normal);
        float b = Vector3.Dot(velocity, tangent);

        return a * normal + b * tangent;
    }

    private static float ComputeHeight(float v0, float a, float time) {
        return v0 * time - a * time * time / 2f;
    }

    public static Vector3[] ComputeTrajectory(Vector3 startVelocity, float acceleration, Vector3 origin) {
        float projectionTime = 0.6f;

        int size = 10;
        Vector3[] ret = new Vector3[size];

        float vy = startVelocity.y;
        float vx = startVelocity.x;
        
        float timeMax = projectionTime;

        for (int i = 0; i < size; i++) {
            float t = timeMax * (((float) i) / ((float) (size - 1f)));
            float x = vx * t;
            float y = ComputeHeight(vy, acceleration, t);

            ret[i] = new Vector3(x, y) + origin;
        }
        
        return ret;
    }

    public static Vector3 ComputeForce(Vector3 tickPosition, Vector3 mousePosition, float forceMultiplier) {
        Vector2 raw_force = tickPosition - mousePosition;
        Vector2 raw_unit_force = raw_force / raw_force.magnitude;
        Vector2 inverse_force = raw_unit_force / raw_force.magnitude;

        Vector2 adjusted_force = inverse_force * forceMultiplier;

        float forceMax = 1000;

        if (adjusted_force.magnitude > forceMax) {
            adjusted_force = adjusted_force * (forceMax / adjusted_force.magnitude);
        }

        return adjusted_force;
    }

    public static void DrawTrajectory(Vector3[] trajectory, float time = 0.02f) {
        for (int i = 0; i < trajectory.Length - 1; i++) {
            Debug.DrawLine(trajectory[i], trajectory[i + 1], Color.magenta, time);
        }
    }


}

