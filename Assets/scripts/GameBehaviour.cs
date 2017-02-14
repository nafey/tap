using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameBehaviour : MonoBehaviour {
    public float forceMultiplier;
    public float forceMax;

    public GameObject tick;

    public bool isDown;
    public Vector3 downPosition;

       void Update() {
        if (Input.GetMouseButton(0)) {
            this.isDown = true;
            Debug.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition), tick.transform.position, Color.red, 0.05f);

            this.downPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0)) {
            this.isDown = false;
            
            Vector2 raw_force = tick.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 raw_unit_force = raw_force / raw_force.magnitude;
            
            Vector2 inverse_force = raw_unit_force / raw_force.magnitude;

            Vector2 adjusted_force = inverse_force * forceMultiplier;

            Debug.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition), tick.transform.position, Color.red, 2f);
           
            if (adjusted_force.magnitude > forceMax) {
                adjusted_force = adjusted_force * (forceMax / adjusted_force.magnitude);
            }

            tick.GetComponent<Rigidbody2D>().AddForce(adjusted_force);
        }
    }

    public void Restart() {

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
