using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// C : Dr.Kim ������Ʈ�� ��� Action�� ���õ� ��ɵ��� ����ִ� ��ũ��Ʈ
public class PlayerAction : MonoBehaviour
{
    float h;    // horizontal (���� �̵�)
    float v;    // vertical (���� �̵�)

    Rigidbody2D rigid;  // ���� ����

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();    // component instance ����
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");     // �Էµ� ���� �̵��� ���� (-1, 0, 1)
        v = Input.GetAxisRaw("Vertical");       // �Էµ� ���� �̵��� ���� (-1, 0, 1)
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(h, v);     // rigid�� �ӵ� ����
    }
}
