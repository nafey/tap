using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour {
    public GameObject startUi;
    public GameObject deathUi;
    public GameObject tick;

    // Use this for initialization
    void Start () {
        this.deathUi.GetComponent<CanvasGroup>().alpha = 0;
        this.deathUi.transform.GetChild(0).GetComponent<Button>().interactable = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (tick.GetComponent<TickBehaviour>().isDead) {
            this.deathUi.SetActive(true);
            this.deathUi.GetComponent<CanvasGroup>().alpha = 1;
            this.deathUi.transform.GetChild(0).GetComponent<Button>().interactable = true;
        }
    }

    public void HideStartUI() {
        this.startUi.SetActive(false);
        this.deathUi.GetComponent<CanvasGroup>().alpha = 0;
        this.deathUi.transform.GetChild(0).GetComponent<Button>().interactable = false;
    }
}
