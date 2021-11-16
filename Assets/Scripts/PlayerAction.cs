using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// C : Dr.Kim 오브젝트의 모든 Action과 관련된 기능들이 들어있는 스크립트
public class PlayerAction : MonoBehaviour
{
    float h;    // horizontal (수평 이동)
    float v;    // vertical (수직 이동)

    Rigidbody2D rigid;  // 물리 제어

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();    // component instance 생성
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");     // 입력된 수평 이동을 대입 (-1, 0, 1)
        v = Input.GetAxisRaw("Vertical");       // 입력된 수직 이동을 대입 (-1, 0, 1)
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(h, v);     // rigid의 속도 설정
    }
}
