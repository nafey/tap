using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehaviour : MonoBehaviour {
    public List<GameObject> bg;
    public GameObject tick;

    public float bgJumpSize;

    public float upper_limit;
    public float lower_limit;

    private int selected_bg;

    private int getOther(int i) {
        if (i == 0) {
            return 1;
        } else {
            return 0;
        }
    }

    void Start()    {
        this.selected_bg = 0;
    }

	void Update () {
        bool tick_dead = this.tick.GetComponent<TickBehaviour>().isDead;

        if (!tick_dead) {
            // Move the Background
            GameObject current_bg = this.bg[selected_bg];
            GameObject other_bg = this.bg[getOther(selected_bg)];

            if (this.tick.transform.position.y - current_bg.transform.position.y > bgJumpSize / 4) {
                selected_bg = getOther(selected_bg);
            } else if (this.tick.transform.position.y - current_bg.transform.position.y > upper_limit) {
                if (other_bg.transform.position.y < current_bg.transform.position.y) {
                    other_bg.transform.Translate(0f, this.bgJumpSize, 0f);
                }
            } else if (this.tick.transform.position.y - current_bg.transform.position.y < -1 * bgJumpSize / 4) {
                selected_bg = getOther(selected_bg);
            } else if (this.tick.transform.position.y - current_bg.transform.position.y < lower_limit) {
                if (other_bg.transform.position.y > current_bg.transform.position.y) {
                    other_bg.transform.Translate(0f, -this.bgJumpSize, 0f);
                }
            }
        }



    }
}
