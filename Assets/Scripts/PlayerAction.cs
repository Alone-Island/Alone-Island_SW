using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// C : Dr.Kim 오브젝트의 모든 Action과 관련된 기능들이 들어있는 스크립트
public class PlayerAction : MonoBehaviour
{
    public float speed;     // C : Dr.Kim 이동 속력
    public GameManager manager;         // C : player에서 GameManager의 함수를 호출할 수 있도록 manager 변수 생성

    float h;    // C : horizontal (수평 이동)
    float v;    // C : vertical (수직 이동)
    bool isHorizonMove;     // C : 수평 이동이면 true, 수직 이동이면 false
    Vector3 dirVec;     // C : 현재 바라보고 있는 방향 값
    GameObject scanObject;  // C : 스캔된 game object

    Rigidbody2D rigid;  // C : 물리 제어
    Animator anim;      // C : 애니메이션 제어

    void Awake()
    {
        // C : component instance 생성
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");     // C : 입력된 수평 이동을 대입 (-1, 0, 1)
        v = Input.GetAxisRaw("Vertical");       // C : 입력된 수직 이동을 대입 (-1, 0, 1)

        // C : 키보드 입력(down, up)이 horizontal인지 vertical인지 확인
        bool hDown = Input.GetButtonDown("Horizontal");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool vUp = Input.GetButtonUp("Vertical");

        // C : isHorizonMove 값 설정
        if (hDown)           // C : 수평 키를 누르면 isHorizonMove는 true
            isHorizonMove = true;
        else if (vDown)      // C : 수직 키를 누르면 isHorizonMove는 false
            isHorizonMove = false;
        else if (hUp || vUp)        // C : 수평 키나 수직 키의 양 쪽(e.g.(<- && ->))을 둘 다 눌렀다 뗐을 때도 고려
            isHorizonMove = h != 0;

        // C : Animation - moving
        if (anim.GetInteger("hAxisRaw") != h)       // C : "hAxisRaw" 값이 현재 h 값과 다를 때
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);    // C : animation "hAxisRaw" parameter 값 설정
        }
        else if (anim.GetInteger("vAxisRaw") != v)  // C : "vAxisRaw" 값이 현재 v 값과 다를 때
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);    // C : animation "vAxisRaw" parameter 값 설정
        }
        else
            anim.SetBool("isChange", false);        // C : 방향 변화를 위한 animation parameter 값을 false로 설정

        // C : dirVec(현재 바라보고 있는 방향) 값 설정
        if (vDown && v == 1)                // C : 수직 키를 눌렀고, 입력된 수직 값이 1이면
            dirVec = Vector3.up;            // C : dirVec 값은 up
        else if (vDown && v == -1)          // C : 수직 키를 눌렀고, 입력된 수직 값이 -1이면
            dirVec = Vector3.down;          // C : dirVec 값은 down
        if (hDown && h == -1)               // C : 수평 키를 눌렀고, 입력된 수평 값이 -1이면
            dirVec = Vector3.left;          // C : dirVec 값은 left
        if (hDown && h == 1)                // C : 수평 키를 눌렀고, 입력된 수평 값이 1이면
            dirVec = Vector3.right;         // C : dirVec 값은 right

        // C : scanObject 출력
        if (Input.GetButtonDown("Jump") && scanObject != null)      // C : 스페이스바를 눌렀고, scanObject가 있으면
            manager.Action(scanObject);     // C : 맵의 대화창에 적절한 메세지가 뜰 수 있도록 Action()함수 실행
    }

    void FixedUpdate()
    {
        // C : player moving
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);    // C : 수평 혹은 수직 이동만 가능하도록 moveVec 설정
        rigid.velocity = moveVec * speed;     // C : rigid의 속도(속력 + 방향) 설정

        // C : Ray
        // C : 시작 위치는 rigid의 위치, 방향은 dirVec, 길이는 0.7f, 색깔은 green인 디버그라인을 설정(ray를 시각화)
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        // C : Object 레이어를 스캔하는 실제 RayCast 구현
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)    // C : ray가 Object를 감지했을 때
        {
            scanObject = rayHit.collider.gameObject;    // C : RayCast된 오브젝트를 scanObject로 설정
        }
        else
            scanObject = null;
    }
}
