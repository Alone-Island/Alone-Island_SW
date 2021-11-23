using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// K : Scene > AI > Rigidbody 2D > body type > kinetic
// K : Scene > AI > AiAcition script 추가
public class AIAction : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;                          // C : 애니메이션 제어
    SpecialEventManager specialManager;     // K : SpecialEventManager의 함수를 호출할 수 있도록 specialManager 변수 생성
    bool isAiStuding = true;                // K : ???의 AI가 학습중임을 확인할 수 있는 변수 호출을 위한 변수 생성
    public int nextAIMoveX = 0;             // K : ai의 다음 X축 방향 변후
    public int nextAIMoveY = 0;             // K : ai의 다음 Y축 방향 변후
    //bool isAICollision = false;             // K : ai 충돌 확인 변수 > 추후 사용 예정

    void NextAiMoveDirection()              // K : ai가 랜덤하게 움직이도록 랜덤한 방향을 결정해주는 함수
    {
        int random = Random.Range(1, 6);    // K : ai가 움직이 방향 랜덤 설정
        int vel = 1;                        // K : ai 이동 속도 조절 
        switch (random)
        {
            case 1:                         // K : 정지
                nextAIMoveX = 0;
                nextAIMoveY = 0;
                break;
            case 2:                         // K : 왼쪽
                nextAIMoveX = -1 * vel;
                nextAIMoveY = 0;
                break;
            case 3:                         // K : 위
                nextAIMoveX = 0;
                nextAIMoveY = vel;
                break;
            case 4:                         // K : 오른쪽
                nextAIMoveX = vel; ;
                nextAIMoveY = 0;
                break;
            case 5:                         // K : 아래
                nextAIMoveX = 0;
                nextAIMoveY = -1 * vel;
                break;
            default:
                nextAIMoveX = 0;
                nextAIMoveY = 0;
                break;
        }

        Invoke("NextAiMoveDirection", 5);   // K : 재귀함수, 5초 후 자기 자신을 재실행 
    }

    public void GoToStudyPlace(int x, int y)
    {
        transform.position = new Vector3(x, y, 0);
    }

    void Awake()
    {
        // C : Animator component instance 생성
        anim = GetComponent<Animator>();
        // K : SpecialEventManager의 함수를 호출할 수 있도록 specialManager 변수를 불러오기 위해 호출
        specialManager = GameObject.Find("SpecialEventManager").GetComponent<SpecialEventManager>();
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        
        //Invoke("NextAiMoveDirection", 5);   // K : 5초 후 ai가 움직일 방향 결정 함수 실행
    }

    void Update()
    {   
        // C : ai 이동(NextAiMoveDirection의 결과)값에 따라 애니메이션 적용
        if (anim.GetInteger("hAxisRaw") != nextAIMoveX)         // C : ai 좌우 이동 시 적절한 애니메이션 실행
        {
            anim.SetInteger("hAxisRaw", (int)nextAIMoveX);
        }
        else if (anim.GetInteger("vAxisRaw") != nextAIMoveY)    // C : ai 상하 이동 시 적절한 애니메이션 실행
        {
            anim.SetInteger("vAxisRaw", (int)nextAIMoveY);
        }
    }

    void FixedUpdate()
    {
        if (specialManager.AItalk || isAiStuding)    // 스페셜 이벤트, 플레이어가 AI와 대화하는 중 또는 AI가 학습중일때 정지
        {
            rigid.velocity = new Vector2(0, 0); // K : ai 정지
        }
        else
        {
            rigid.velocity = new Vector2(nextAIMoveX, nextAIMoveY); // K : ai 이동
        }
    }

    void OnCollisionEnter2D(Collision2D coll)   // Ai 충돌 감지 함수
    {
        //isAICollision = true;                   // K : AI 충돌
        nextAIMoveX = 0;                        // Ai 충돌 발생시 무조건 멈춤
        nextAIMoveY = 0;
    }

    void OnCollisionExit2D(Collision2D coll)   // Ai 충돌 제거 감지 함수
    {
        //isAICollision = false;                 // K : AI 충돌 멈춤
    }
}
