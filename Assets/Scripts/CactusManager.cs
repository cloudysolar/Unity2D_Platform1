using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusManager : MonoBehaviour {
    public GameObject[] cactuses = new GameObject[3];
    public float interval = 1.5f;

    private float nowInterval = 0f;

    /*
        [ 선인장 스폰 조건 ]
        - 무작위 숫자를 생성 (0, 1, 2, 3 중 하나로 생성)
        - 이 중 3은 선인장을 스폰하지 않고, 나머지는 배열에 맞는 선인장을 스폰.
     */

    private void Update() {
        if (nowInterval >= interval) {
            nowInterval = 0f;

            int random = Random.Range(0, 4);

            if (random != 3) {
                GameObject cactus = Instantiate(cactuses[random], new Vector3(10f, -0.6f, 0), Quaternion.identity);
                cactus.transform.SetParent(transform);
            }
        }

        nowInterval += Time.deltaTime;
    }

    // 실제 실습 당시에는 initGame() 메소드였음.
    public void OnGameRestart() {
        GameObject[] spawned = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach(GameObject go in spawned) {
            Destroy(go);
        }
    }
}
