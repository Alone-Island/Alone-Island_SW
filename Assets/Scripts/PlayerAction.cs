using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

// C : Dr.Kim 오브젝트의 모든 Action과 관련된 기능들이 들어있는 스크립트
public class PlayerAction : MonoBehaviour
{
    public float speed;     // C : Dr.Kim 이동 속력
    public GameManager manager;         // C : player에서 GameManager의 함수를 호출할 수 있도록 manager 변수 생성
    public ScreenManager screenManager;         // J : 책을 주웠을 때 책 개수 증가를 위해 ScreenManager 변수 생성
    public SpecialEventManager specialManager;  // J : player에서 SpecialEventManager의 함수를 호출할 수 있도록 specialManager 변수 생성

    float h;    // C : horizontal (수평 이동)
    float v;    // C : vertical (수직 이동)
    bool isHorizonMove;     // C : 수평 이동이면 true, 수직 이동이면 false
    Vector3 dirVec;     // C : 현재 바라보고 있는 방향 값
    GameObject scanObject;  // C : 스캔된 game object

    //N : 학습하기 안내 아이콘들
    public GameObject farmIcon;
    public GameObject houseIcon;
    public GameObject craftIcon;
    public GameObject engineerIcon;

    // C :
    public GameObject addBook;      // C :
    //private float time = 0;         // C :
    public GameObject player;       // C :
    private List<GameObject> addBookListG = new List<GameObject>();      // C :
    private List<float> addBookListT = new List<float>();      // C :

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
        // C : 입력된 수평/수직 이동을 대입 (-1, 0, 1)
        // C : GameManager의 isTPShow를 사용하여 talkPanel이 보여지고 있을 때는 플레이어의 이동을 제한
        h = manager.isTPShow ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isTPShow ? 0 : Input.GetAxisRaw("Vertical");
        // J : SpecialEventManager의 special을 사용하여 스페셜 이벤트 진행 중인 경우 플레이어의 이동을 제한
        h = specialManager.special ? 0 : Input.GetAxisRaw("Horizontal");
        v = specialManager.special ? 0 : Input.GetAxisRaw("Vertical");
        // N :
        h = manager.isEndingShow ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isEndingShow ? 0 : Input.GetAxisRaw("Vertical");

        // C : 키보드 입력(down, up)이 horizontal인지 vertical인지 확인
        // C : GameManager의 isTPShow를 사용하여 talkPanel이 보여지고 있을 때는 플레이어의 이동을 제한
        bool hDown = manager.isTPShow ? false : Input.GetButtonDown("Horizontal");
        bool hUp = manager.isTPShow ? false : Input.GetButtonUp("Horizontal");
        bool vDown = manager.isTPShow ? false : Input.GetButtonDown("Vertical");
        bool vUp = manager.isTPShow ? false : Input.GetButtonUp("Vertical");
        // J : SpecialEventManager의 special을 사용하여 스페셜 이벤트 진행 중인 경우 플레이어의 이동을 제한
        hDown = specialManager.special ? false : Input.GetButtonDown("Horizontal");
        hUp = specialManager.special ? false : Input.GetButtonUp("Horizontal");
        vDown = specialManager.special ? false : Input.GetButtonDown("Vertical");
        vUp = specialManager.special ? false : Input.GetButtonUp("Vertical");
        //N :
        hDown = manager.isEndingShow ? false : Input.GetButtonDown("Horizontal");
        hUp = manager.isEndingShow ? false : Input.GetButtonUp("Horizontal");
        vDown = manager.isEndingShow ? false : Input.GetButtonDown("Vertical");
        vUp = manager.isEndingShow ? false : Input.GetButtonUp("Vertical");

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

        // J : 스페이스바 누름
        if (Input.GetButtonDown("Jump"))
        {
            if (specialManager.special)     // J : 스페셜 이벤트 진행 중
            {
                if (specialManager.AItalk)  // J : 선택지가 뜨기 전이라면
                    specialManager.Talk();  // J : specialManager의 Talk 함수 호출
                else                        // J : 선택지 클릭한 후 (스페셜 이벤트 진행중)
                    specialManager.ResultTalk();    // J : 결과 텍스트 보여주기
            }
            else if (scanObject != null)        // J : 스페셜 이벤트 진행 중이 아니고 scanObject가 있으면
                manager.Action(scanObject);     // C : 맵의 대화창에 적절한 메세지가 뜰 수 있도록 Action()함수 실행
            else    // J : 아무 상태도 아니거나 책 찾았다는 대화창이 뜬 상태..
                manager.talkPanel.SetActive(false); // J : 대화창 끄기

            // N : 엔딩 크레딧으로 연결
            // N : 나중에 버튼 만들어서 클릭으로 처리하면 좋을 것 같음.
            if (manager.isTheEnd)
            {
                SceneManager.LoadScene("EndingCredits");
            }
        }

        
        // C : 
        for (int i = 0; i < addBookListG.Count; i++)
        {
            if (addBookListG[i].activeSelf == true)
            {
                addBookListT[i] += Time.deltaTime;
                if (addBookListT[i] > 2f)
                {
                    addBookListT[i] = 0;
                    addBookListG[i].SetActive(false);
                    Destroy(addBookListG[i]);
                    addBookListG.RemoveAt(i);
                    addBookListT.RemoveAt(i);
                }
            }
        }
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

    // J : 책을 찾았을 때
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Book(Clone)") {        // J : 부딪힌 오브젝트가 책인 경우
            coll.gameObject.SetActive(false);               // J : Book Object 비활성화
            manager.talkPanel.SetActive(true);              // J : 대화창 활성화
            manager.talkText.text = "책을 찾았습니다!";     // J : 대화창 텍스트 적용
            screenManager.getBook();                        // J : 책 개수 증가

            // C :
            GameObject bookInstance = Instantiate(addBook, player.transform.localPosition, Quaternion.identity);
            bookInstance.transform.SetParent(player.transform);
            bookInstance.SetActive(true);                   // C : player 머리 위의 책 object 보이기
            addBookListG.Add(bookInstance);
            addBookListT.Add(0f);                       
        }
    }

    // N : 장소에 들어가면
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "FarmLearning")
        {
            farmIcon.SetActive(true);
        }
        if (coll.gameObject.name == "HouseLearning")
        {
            houseIcon.SetActive(true);
        }
        if (coll.gameObject.name == "CraftLearning")
        {
            craftIcon.SetActive(true);
        }
        if (coll.gameObject.name == "EngineerLearning")
        {
            engineerIcon.SetActive(true);
        }
    }
    // N : 장소에서 나오면
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.name == "FarmLearning")
        {
            farmIcon.SetActive(false);
        }
        if (coll.gameObject.name == "HouseLearning")
        {
            houseIcon.SetActive(false);
        }
        if (coll.gameObject.name == "CraftLearning")
        {
            craftIcon.SetActive(false);
        }
        if (coll.gameObject.name == "EngineerLearning")
        {
            engineerIcon.SetActive(false);
        }
    }
}
