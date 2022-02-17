using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour {
    public AudioClip jump;
    public AudioClip land;
    public AudioClip die;

    bool isPlayed_die = false;
    bool isPlayed_land = false;

    bool _isDead = false;

    public bool isDead {
        set {
            _isDead = value;
        }
        get {
            return _isDead;
        }
    }

    Animator ani;
    AudioSource au;
    Rigidbody2D rig;

    private void Start() {
        ani = GetComponent<Animator>();
        au = GetComponent<AudioSource>();
        rig = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if (isDead) {
            ani.SetBool("Dead", true);

            if (!isPlayed_die) {
                au.PlayOneShot(die);
                isPlayed_die = true;
            }

            rig.velocity = Vector3.zero;
            rig.angularVelocity = 0f;
            rig.bodyType = RigidbodyType2D.Kinematic;
            rig.simulated = false;

            return;
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("[디버그] 플레이어가 스페이스 바 버튼을 눌렀습니다!");

            if (transform.position.y <= -0.58f) {
                Debug.Log("[디버그] 플레이어가 " + transform.position.y + "좌표에서 점프하였습니다.");
                rig.AddForce(new Vector3(0, 500f, 0));

                ani.SetBool("Jump", true);

                au.PlayOneShot(jump);
                isPlayed_land = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name.Contains("Ground")) {
            ani.SetBool("Jump", false);

            if (!isPlayed_land) {
                au.PlayOneShot(land);
                isPlayed_land = true;
            }
        }
    }

    // 실제 실습 당시에는 initGame() 메소드였음.
    public void OnGameRestart() {
        isDead = false;
        isPlayed_die = false;
        isPlayed_land = false;

        ani.SetBool("Dead", false);

        rig.bodyType = RigidbodyType2D.Dynamic;
        rig.simulated = true;
    }
}
