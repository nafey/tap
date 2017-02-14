using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickCollider : MonoBehaviour {
    public float smallImpulse = 1f;

    void OnCollisionEnter2D(Collision2D c) {
        if (c.gameObject.tag == "Player") {
            Vector2 unitNormal = (-1f * c.contacts[0].normal) / c.contacts[0].normal.magnitude;

            Vector2 velocityNow = c.collider.gameObject.GetComponent<Rigidbody2D>().velocity;
            c.collider.gameObject.GetComponent<Rigidbody2D>().velocity = velocityNow + unitNormal * smallImpulse;
        }
    }
}
