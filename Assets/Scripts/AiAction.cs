using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAction : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextAiMoveX;
    public int nextAiMoveY;

    void nextAiMoveDirection()
    {
        int random = Random.Range(1, 6);    // K : ai가 움직이 방향 랜덤 설정
        int vel = 1;                        // K : ai 이동 속도 조절 
        switch (random)
        {
            case 1:                         // K : 정지
                nextMoveX = 0;
                nextMoveY = 0;
                break;
            case 2:                         // K : 왼쪽
                nextMoveX = -1 * vel;
                nextMoveY = 0;
                break;
            case 3:                         // K : 위
                nextMoveX = 0;
                nextMoveY = vel;
                break;
            case 4:                         // K : 오른쪽
                nextMoveX = vel; ;
                nextMoveY = 0;
                break;
            case 5:                         // K : 아래
                nextMoveX = 0;
                nextMoveY = -1 * vel;
                break;
            default:
                nextMoveX = 0;
                nextMoveY = 0;
                break;
        }

        Invoke("nextAiMoveDirection", 5);   // K : 재귀함수, 5초 후 자기 자신을 재실행 
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        Invoke("nextAiMoveDirection", 5);   // K : 5초 후 ai가 움직일 방향 결정 함수 실행
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextAiMoveX, nextAiMoveY); // K : ai 이동
    }
}
