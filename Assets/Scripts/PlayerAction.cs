using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

// C : Dr.Kim ������Ʈ�� ��� Action�� ���õ� ��ɵ��� ����ִ� ��ũ��Ʈ
public class PlayerAction : MonoBehaviour
{
    public float speed;     // C : Dr.Kim �̵� �ӷ�
    public GameManager manager;         // C : player���� GameManager�� �Լ��� ȣ���� �� �ֵ��� manager ���� ����
    public ScreenManager screenManager;         // J : å�� �ֿ��� �� å ���� ������ ���� ScreenManager ���� ����
    public SpecialEventManager specialManager;  // J : player���� SpecialEventManager�� �Լ��� ȣ���� �� �ֵ��� specialManager ���� ����

    float h;    // C : horizontal (���� �̵�)
    float v;    // C : vertical (���� �̵�)
    bool isHorizonMove;     // C : ���� �̵��̸� true, ���� �̵��̸� false
    Vector3 dirVec;     // C : ���� �ٶ󺸰� �ִ� ���� ��
    GameObject scanObject;  // C : ��ĵ�� game object

    //N : �н��ϱ� �ȳ� �����ܵ�
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

    Rigidbody2D rigid;  // C : ���� ����
    Animator anim;      // C : �ִϸ��̼� ����

    void Awake()
    {
        // C : component instance ����
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // C : �Էµ� ����/���� �̵��� ���� (-1, 0, 1)
        // C : GameManager�� isTPShow�� ����Ͽ� talkPanel�� �������� ���� ���� �÷��̾��� �̵��� ����
        h = manager.isTPShow ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isTPShow ? 0 : Input.GetAxisRaw("Vertical");
        // J : SpecialEventManager�� special�� ����Ͽ� ����� �̺�Ʈ ���� ���� ��� �÷��̾��� �̵��� ����
        h = specialManager.special ? 0 : Input.GetAxisRaw("Horizontal");
        v = specialManager.special ? 0 : Input.GetAxisRaw("Vertical");
        // N :
        h = manager.isEndingShow ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isEndingShow ? 0 : Input.GetAxisRaw("Vertical");

        // C : Ű���� �Է�(down, up)�� horizontal���� vertical���� Ȯ��
        // C : GameManager�� isTPShow�� ����Ͽ� talkPanel�� �������� ���� ���� �÷��̾��� �̵��� ����
        bool hDown = manager.isTPShow ? false : Input.GetButtonDown("Horizontal");
        bool hUp = manager.isTPShow ? false : Input.GetButtonUp("Horizontal");
        bool vDown = manager.isTPShow ? false : Input.GetButtonDown("Vertical");
        bool vUp = manager.isTPShow ? false : Input.GetButtonUp("Vertical");
        // J : SpecialEventManager�� special�� ����Ͽ� ����� �̺�Ʈ ���� ���� ��� �÷��̾��� �̵��� ����
        hDown = specialManager.special ? false : Input.GetButtonDown("Horizontal");
        hUp = specialManager.special ? false : Input.GetButtonUp("Horizontal");
        vDown = specialManager.special ? false : Input.GetButtonDown("Vertical");
        vUp = specialManager.special ? false : Input.GetButtonUp("Vertical");
        //N :
        hDown = manager.isEndingShow ? false : Input.GetButtonDown("Horizontal");
        hUp = manager.isEndingShow ? false : Input.GetButtonUp("Horizontal");
        vDown = manager.isEndingShow ? false : Input.GetButtonDown("Vertical");
        vUp = manager.isEndingShow ? false : Input.GetButtonUp("Vertical");

        // C : isHorizonMove �� ����
        if (hDown)           // C : ���� Ű�� ������ isHorizonMove�� true
            isHorizonMove = true;
        else if (vDown)      // C : ���� Ű�� ������ isHorizonMove�� false
            isHorizonMove = false;
        else if (hUp || vUp)        // C : ���� Ű�� ���� Ű�� �� ��(e.g.(<- && ->))�� �� �� ������ ���� ���� ���
            isHorizonMove = h != 0;

        // C : Animation - moving
        if (anim.GetInteger("hAxisRaw") != h)       // C : "hAxisRaw" ���� ���� h ���� �ٸ� ��
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);    // C : animation "hAxisRaw" parameter �� ����
        }
        else if (anim.GetInteger("vAxisRaw") != v)  // C : "vAxisRaw" ���� ���� v ���� �ٸ� ��
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);    // C : animation "vAxisRaw" parameter �� ����
        }
        else
            anim.SetBool("isChange", false);        // C : ���� ��ȭ�� ���� animation parameter ���� false�� ����

        // C : dirVec(���� �ٶ󺸰� �ִ� ����) �� ����
        if (vDown && v == 1)                // C : ���� Ű�� ������, �Էµ� ���� ���� 1�̸�
            dirVec = Vector3.up;            // C : dirVec ���� up
        else if (vDown && v == -1)          // C : ���� Ű�� ������, �Էµ� ���� ���� -1�̸�
            dirVec = Vector3.down;          // C : dirVec ���� down
        if (hDown && h == -1)               // C : ���� Ű�� ������, �Էµ� ���� ���� -1�̸�
            dirVec = Vector3.left;          // C : dirVec ���� left
        if (hDown && h == 1)                // C : ���� Ű�� ������, �Էµ� ���� ���� 1�̸�
            dirVec = Vector3.right;         // C : dirVec ���� right

        // J : �����̽��� ����
        if (Input.GetButtonDown("Jump"))
        {
            if (specialManager.special)     // J : ����� �̺�Ʈ ���� ��
            {
                if (specialManager.AItalk)  // J : �������� �߱� ���̶��
                    specialManager.Talk();  // J : specialManager�� Talk �Լ� ȣ��
                else                        // J : ������ Ŭ���� �� (����� �̺�Ʈ ������)
                    specialManager.ResultTalk();    // J : ��� �ؽ�Ʈ �����ֱ�
            }
            else if (scanObject != null)        // J : ����� �̺�Ʈ ���� ���� �ƴϰ� scanObject�� ������
                manager.Action(scanObject);     // C : ���� ��ȭâ�� ������ �޼����� �� �� �ֵ��� Action()�Լ� ����
            else    // J : �ƹ� ���µ� �ƴϰų� å ã�Ҵٴ� ��ȭâ�� �� ����..
                manager.talkPanel.SetActive(false); // J : ��ȭâ ����

            // N : ���� ũ�������� ����
            // N : ���߿� ��ư ���� Ŭ������ ó���ϸ� ���� �� ����.
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
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);    // C : ���� Ȥ�� ���� �̵��� �����ϵ��� moveVec ����
        rigid.velocity = moveVec * speed;     // C : rigid�� �ӵ�(�ӷ� + ����) ����

        // C : Ray
        // C : ���� ��ġ�� rigid�� ��ġ, ������ dirVec, ���̴� 0.7f, ������ green�� ����׶����� ����(ray�� �ð�ȭ)
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        // C : Object ���̾ ��ĵ�ϴ� ���� RayCast ����
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)    // C : ray�� Object�� �������� ��
        {
            scanObject = rayHit.collider.gameObject;    // C : RayCast�� ������Ʈ�� scanObject�� ����
        }
        else
            scanObject = null;
    }

    // J : å�� ã���� ��
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Book(Clone)") {        // J : �ε��� ������Ʈ�� å�� ���
            coll.gameObject.SetActive(false);               // J : Book Object ��Ȱ��ȭ
            manager.talkPanel.SetActive(true);              // J : ��ȭâ Ȱ��ȭ
            manager.talkText.text = "å�� ã�ҽ��ϴ�!";     // J : ��ȭâ �ؽ�Ʈ ����
            screenManager.getBook();                        // J : å ���� ����

            // C :
            GameObject bookInstance = Instantiate(addBook, player.transform.localPosition, Quaternion.identity);
            bookInstance.transform.SetParent(player.transform);
            bookInstance.SetActive(true);                   // C : player �Ӹ� ���� å object ���̱�
            addBookListG.Add(bookInstance);
            addBookListT.Add(0f);                       
        }
    }

    // N : ��ҿ� ����
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
    // N : ��ҿ��� ������
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
