using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// C : Dr.Kim ������Ʈ�� ��� Action�� ���õ� ��ɵ��� ����ִ� ��ũ��Ʈ
public class PlayerAction : MonoBehaviour
{
    public float speed;     // Dr.Kim �̵� �ӷ�

    float h;    // horizontal (���� �̵�)
    float v;    // vertical (���� �̵�)
    bool isHorizonMove;     // ���� �̵��̸� true, ���� �̵��̸� false
    Vector3 dirVec;     // ���� �ٶ󺸰� �ִ� ���� ��
    GameObject scanObject;  // ��ĵ�� game object

    Rigidbody2D rigid;  // ���� ����
    Animator anim;      // �ִϸ��̼� ����

    void Awake()
    {
        // component instance ����
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");     // �Էµ� ���� �̵��� ���� (-1, 0, 1)
        v = Input.GetAxisRaw("Vertical");       // �Էµ� ���� �̵��� ���� (-1, 0, 1)

        // Ű���� �Է�(down, up)�� horizontal���� vertical���� Ȯ��
        bool hDown = Input.GetButtonDown("Horizontal");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool vUp = Input.GetButtonUp("Vertical");

        // isHorizonMove �� ����
        if (hDown)           // ���� Ű�� ������ isHorizonMove�� true
            isHorizonMove = true;
        else if (vDown)      // ���� Ű�� ������ isHorizonMove�� false
            isHorizonMove = false;
        else if (hUp || vUp)        // ���� Ű�� ���� Ű�� �� ��(e.g.(<- && ->))�� �� �� ������ ���� ���� ���
            isHorizonMove = h != 0;

        // Animation - moving
        if (anim.GetInteger("hAxisRaw") != h)       // "hAxisRaw" ���� ���� h ���� �ٸ� ��
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);    // animation "hAxisRaw" parameter �� ����
        }
        else if (anim.GetInteger("vAxisRaw") != v)  // "vAxisRaw" ���� ���� v ���� �ٸ� ��
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);    // animation "vAxisRaw" parameter �� ����
        }
        else
            anim.SetBool("isChange", false);        // ���� ��ȭ�� ���� animation parameter ���� false�� ����

        // dirVec(���� �ٶ󺸰� �ִ� ����) �� ����
        if (vDown && v == 1)                // ���� Ű�� ������, �Էµ� ���� ���� 1�̸�
            dirVec = Vector3.up;            // dirVec ���� up
        else if (vDown && v == -1)          // ���� Ű�� ������, �Էµ� ���� ���� -1�̸�
            dirVec = Vector3.down;          // dirVec ���� down
        if (hDown && h == -1)               // ���� Ű�� ������, �Էµ� ���� ���� -1�̸�
            dirVec = Vector3.left;          // dirVec ���� left
        if (hDown && h == 1)                // ���� Ű�� ������, �Էµ� ���� ���� 1�̸�
            dirVec = Vector3.right;         // dirVec ���� right

        // scanObject ���
        if (Input.GetButtonDown("Jump") && scanObject != null)      // �����̽��ٸ� ������, scanObject�� ������
            Debug.Log("this is :" + scanObject.name);
    }

    void FixedUpdate()
    {
        // player moving
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);    // ���� Ȥ�� ���� �̵��� �����ϵ��� moveVec ����
        rigid.velocity = moveVec * speed;     // rigid�� �ӵ�(�ӷ� + ����) ����

        // Ray (
        // ���� ��ġ�� rigid�� ��ġ, ������ dirVec, ���̴� 0.7f, ������ green�� ����׶����� ����(ray�� �ð�ȭ)
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        // Object ���̾ ��ĵ�ϴ� ���� RayCast ����
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)    // ray�� Object�� �������� ��
        {
            scanObject = rayHit.collider.gameObject;    // RayCast�� ������Ʈ�� scanObject�� ����
        }
        else
            scanObject = null;
    }
}
