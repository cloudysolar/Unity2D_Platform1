using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public GameObject gameOver;

    public Text nowScoreText;
    public Text maxScoreText;

    public Button restart;

    public Sprite btnUp;
    public Sprite btnDown;

    public float interval = 0.1f;
    public int addScore = 1;

    PlayerController player;

    float nowInterval = 0;

    int nowScore = 0;
    int maxScore = 0;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update() {
        if (player.isDead) {
            gameOver.SetActive(true);
            return;
        }

        if (nowInterval >= interval) {
            nowInterval = 0;
            nowScore += addScore;
        }
        else {
            nowInterval += Time.deltaTime;
        }

        nowScoreText.text = nowScore.ToString();

        if (nowScore >= maxScore) {
            maxScore = nowScore;
            maxScoreText.text = maxScore.ToString();
        }
    }

    // 실제 실습 당시에는 initGame()이었음.
    public void OnGameRestart() {
        gameOver.SetActive(false);

        nowScore = 0;
        nowInterval = 0;

        nowScoreText.text = nowScore.ToString();
        maxScoreText.text = maxScore.ToString();
    }

    public void OnRestartButtonClick() {
        restart.gameObject.GetComponent<Image>().sprite = btnDown;

        GameObject.Find("Cactuses").GetComponent<CactusManager>().OnGameRestart();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().OnGameRestart();
        OnGameRestart();
    }
}
