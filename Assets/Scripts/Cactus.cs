using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour {
    public float destoryX = -20f;

    float speed = 0f;

    PlayerController player;

    private void Start() {
        speed = GameObject.Find("Grounds").GetComponent<Scrolling>().speed;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update() {
        if (player.isDead) {
            return;
        }

        if (transform.position.x <= destoryX) {
            Destroy(gameObject);
        }

        transform.Translate(new Vector3(-speed, 0, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag.Equals("Player")) {
            player.isDead = true;
        }
    }
}
