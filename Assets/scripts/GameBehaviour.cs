using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehaviour : MonoBehaviour {
    public static GameBehaviour instance = null;    
    public float forceMultiplier;
    public float forceMax;

    public GameObject tick;

    //public bool isDown;
    //public Vector3 downPosition;

    private Action<Vector3> mouseDownCallback;
    private Action<Vector3> mouseUpCallback;

    public void RegisterMouseDown(Action<Vector3> callback) {
        mouseDownCallback += callback;
    }

    public void RegisterMouseUp(Action<Vector3> callback) {
        mouseUpCallback += callback;
    }

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }


    public void Update() {
        if (Input.GetMouseButtonDown(0)) {
            this.mouseDownCallback(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0)) {
            this.mouseUpCallback(Input.mousePosition);
        }
    }


    public void Restart() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
