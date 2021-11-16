using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// C : Dr.Kim 오브젝트의 모든 Action과 관련된 기능들이 들어있는 스크립트
public class PlayerAction : MonoBehaviour
{
    public float speed;     // Dr.Kim 이동 속력

    float h;    // horizontal (수평 이동)
    float v;    // vertical (수직 이동)
    bool isHorizonMove;     // 수평 이동이면 true, 수직 이동이면 false

    Rigidbody2D rigid;  // 물리 제어

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();    // component instance 생성
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");     // 입력된 수평 이동을 대입 (-1, 0, 1)
        v = Input.GetAxisRaw("Vertical");       // 입력된 수직 이동을 대입 (-1, 0, 1)

        // 키보드 입력(down, up)이 horizontal인지 vertical인지 확인
        bool hDown = Input.GetButtonDown("Horizontal");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool vUp = Input.GetButtonUp("Vertical");

        // isHorizonMove 값 설정
        if (hDown || vUp)           // 수평 키를 누르거나 수직 키를 떼면 isHorizonMove는 true
            isHorizonMove = true;
        else if (vDown || hUp)      // 수직 키를 누르거나 수평 키를 떼면 isHorizonMove는 false
            isHorizonMove = false;
    }

    void FixedUpdate()
    {
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);    // 수평 혹은 수직 이동만 가능하도록 moveVec 설정
        rigid.velocity = moveVec * speed;     // rigid의 속도(속력 + 방향) 설정
    }
}
