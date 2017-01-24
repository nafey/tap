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

    public Vector3 computeVelocity(Vector3 normal, Vector3 velocity) {
        Vector3 tangent = new Vector3(-normal.y, normal.x, 0);
        float a = Vector3.Dot(velocity, normal);
        float b = Vector3.Dot(velocity, tangent);

        return a * normal + b * tangent;
    }
}
