using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour {
    public List<GameObject> scroll = new List<GameObject>();
    public GameObject prefab;

    public float initY = 0f;
    public float destoryX = -12f;
    public float speed = 0.15f;
    public float distance = 4.5f;

    private void Update() {
        if (scroll.Count == 0) {
            GameObject spawn = Instantiate(prefab, new Vector3(distance, initY, 0), Quaternion.identity);

            spawn.transform.SetParent(gameObject.transform);
            scroll.Add(spawn);
        }
        else {
            if (scroll[0].transform.position.x <= destoryX) {
                GameObject target = scroll[0];

                scroll.Remove(target);
                Destroy(target);

                float newX = distance;

                if (scroll.Count > 0) {
                    newX = scroll[scroll.Count - 1].transform.position.x + distance;
                }

                GameObject spawn = Instantiate(prefab, new Vector3(newX, initY, 0), Quaternion.identity);

                spawn.transform.SetParent(gameObject.transform);
                scroll.Add(spawn);
            }
            else {
                PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

                if (!player.isDead) {
                    for (int i = 0; i < scroll.Count; i++) {
                        scroll[i].transform.Translate(-speed, 0, 0);
                    }
                }
            }
        }
    }
}
