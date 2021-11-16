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
    Vector3 dirVec;     // 현재 바라보고 있는 방향 값
    GameObject scanObject;  // 스캔된 game object

    Rigidbody2D rigid;  // 물리 제어
    Animator anim;      // 애니메이션 제어

    void Awake()
    {
        // component instance 생성
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
        if (hDown)           // 수평 키를 누르면 isHorizonMove는 true
            isHorizonMove = true;
        else if (vDown)      // 수직 키를 누르면 isHorizonMove는 false
            isHorizonMove = false;
        else if (hUp || vUp)        // 수평 키나 수직 키의 양 쪽(e.g.(<- && ->))을 둘 다 눌렀다 뗐을 때도 고려
            isHorizonMove = h != 0;

        // Animation - moving
        if (anim.GetInteger("hAxisRaw") != h)       // "hAxisRaw" 값이 현재 h 값과 다를 때
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);    // animation "hAxisRaw" parameter 값 설정
        }
        else if (anim.GetInteger("vAxisRaw") != v)  // "vAxisRaw" 값이 현재 v 값과 다를 때
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);    // animation "vAxisRaw" parameter 값 설정
        }
        else
            anim.SetBool("isChange", false);        // 방향 변화를 위한 animation parameter 값을 false로 설정

        // dirVec(현재 바라보고 있는 방향) 값 설정
        if (vDown && v == 1)                // 수직 키를 눌렀고, 입력된 수직 값이 1이면
            dirVec = Vector3.up;            // dirVec 값은 up
        else if (vDown && v == -1)          // 수직 키를 눌렀고, 입력된 수직 값이 -1이면
            dirVec = Vector3.down;          // dirVec 값은 down
        if (hDown && h == -1)               // 수평 키를 눌렀고, 입력된 수평 값이 -1이면
            dirVec = Vector3.left;          // dirVec 값은 left
        if (hDown && h == 1)                // 수평 키를 눌렀고, 입력된 수평 값이 1이면
            dirVec = Vector3.right;         // dirVec 값은 right

        // scanObject 출력
        if (Input.GetButtonDown("Jump") && scanObject != null)      // 스페이스바를 눌렀고, scanObject가 있으면
            Debug.Log("this is :" + scanObject.name);
    }

    void FixedUpdate()
    {
        // player moving
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);    // 수평 혹은 수직 이동만 가능하도록 moveVec 설정
        rigid.velocity = moveVec * speed;     // rigid의 속도(속력 + 방향) 설정

        // Ray (
        // 시작 위치는 rigid의 위치, 방향은 dirVec, 길이는 0.7f, 색깔은 green인 디버그라인을 설정(ray를 시각화)
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        // Object 레이어를 스캔하는 실제 RayCast 구현
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)    // ray가 Object를 감지했을 때
        {
            scanObject = rayHit.collider.gameObject;    // RayCast된 오브젝트를 scanObject로 설정
        }
        else
            scanObject = null;
    }
}
