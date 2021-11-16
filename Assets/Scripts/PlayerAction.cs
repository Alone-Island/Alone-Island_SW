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

    Rigidbody2D rigid;  // ���� ����

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();    // component instance ����
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
        if (hDown || vUp)           // ���� Ű�� �����ų� ���� Ű�� ���� isHorizonMove�� true
            isHorizonMove = true;
        else if (vDown || hUp)      // ���� Ű�� �����ų� ���� Ű�� ���� isHorizonMove�� false
            isHorizonMove = false;
    }

    void FixedUpdate()
    {
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);    // ���� Ȥ�� ���� �̵��� �����ϵ��� moveVec ����
        rigid.velocity = moveVec * speed;     // rigid�� �ӵ�(�ӷ� + ����) ����
    }
}
