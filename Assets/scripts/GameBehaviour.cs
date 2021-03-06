﻿using System;
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
    private Action<Vector3> mouseHoldCallback;

    public void RegisterMouseDown(Action<Vector3> callback) {
        mouseDownCallback += callback;
    }

    public void RegisterMouseUp(Action<Vector3> callback) {
        mouseUpCallback += callback;
    }

    public void RegisterMouseHold(Action<Vector3> callback) {
        mouseHoldCallback += callback;
    }

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }


    public void Update() {
        if (Input.GetMouseButtonDown(0) && this.mouseDownCallback != null) {
            this.mouseDownCallback(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0) && this.mouseUpCallback != null) {
            this.mouseUpCallback(Input.mousePosition);
        }

        if (Input.GetMouseButton(0) && this.mouseHoldCallback != null) {
            this.mouseHoldCallback(Input.mousePosition);
        }
    }


    public void Restart() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
