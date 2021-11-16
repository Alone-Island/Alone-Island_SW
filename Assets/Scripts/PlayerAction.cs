using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// C : Dr.Kim ������Ʈ�� ��� Action�� ���õ� ��ɵ��� ����ִ� ��ũ��Ʈ
public class PlayerAction : MonoBehaviour
{
    public float speed;     // C : Dr.Kim �̵� �ӷ�
    public GameManager manager;         // C : player���� GameManager�� �Լ��� ȣ���� �� �ֵ��� manager ���� ����

    float h;    // C : horizontal (���� �̵�)
    float v;    // C : vertical (���� �̵�)
    bool isHorizonMove;     // C : ���� �̵��̸� true, ���� �̵��̸� false
    Vector3 dirVec;     // C : ���� �ٶ󺸰� �ִ� ���� ��
    GameObject scanObject;  // C : ��ĵ�� game object

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
        h = Input.GetAxisRaw("Horizontal");     // C : �Էµ� ���� �̵��� ���� (-1, 0, 1)
        v = Input.GetAxisRaw("Vertical");       // C : �Էµ� ���� �̵��� ���� (-1, 0, 1)

        // C : Ű���� �Է�(down, up)�� horizontal���� vertical���� Ȯ��
        bool hDown = Input.GetButtonDown("Horizontal");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool vUp = Input.GetButtonUp("Vertical");

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

        // C : scanObject ���
        if (Input.GetButtonDown("Jump") && scanObject != null)      // C : �����̽��ٸ� ������, scanObject�� ������
            manager.Action(scanObject);     // C : ���� ��ȭâ�� ������ �޼����� �� �� �ֵ��� Action()�Լ� ����
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
}
