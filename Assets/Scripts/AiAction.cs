using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Scene : AI�� Rigidbody 2D > body type > kinetic
// Scene : AI�� AiAcition �߰�
public class AiAction : MonoBehaviour
{
    Rigidbody2D rigid;
    SpecialEventManager specialManager; // K : SpecialEventManager�� �Լ��� ȣ���� �� �ֵ��� specialManager ���� ����
    public int nextAIMoveX;             // K : ai�� ���� X�� ���� ����
    public int nextAIMoveY;             // K : ai�� ���� Y�� ���� ����
    // public bool isAICollision = false;

    void nextAiMoveDirection()              // K : ai�� �����ϰ� �����̵��� ������ ������ �������ִ� �Լ�
    {
        int random = Random.Range(1, 6);    // K : ai�� ������ ���� ���� ����
        int vel = 1;                        // K : ai �̵� �ӵ� ���� 
        switch (random)
        {
            case 1:                         // K : ����
                nextAIMoveX = 0;
                nextAIMoveY = 0;
                break;
            case 2:                         // K : ����
                nextAIMoveX = -1 * vel;
                nextAIMoveY = 0;
                break;
            case 3:                         // K : ��
                nextAIMoveX = 0;
                nextAIMoveY = vel;
                break;
            case 4:                         // K : ������
                nextAIMoveX = vel; ;
                nextAIMoveY = 0;
                break;
            case 5:                         // K : �Ʒ�
                nextAIMoveX = 0;
                nextAIMoveY = -1 * vel;
                break;
            default:
                nextAIMoveX = 0;
                nextAIMoveY = 0;
                break;
        }

        Invoke("nextAiMoveDirection", 5);   // K : ����Լ�, 5�� �� �ڱ� �ڽ��� ����� 
    }

    void Awake()
    {
        // K : SpecialEventManager�� �Լ��� ȣ���� �� �ֵ��� specialManager ������ �ҷ����� ���� ȣ��
        specialManager = GameObject.Find("SpecialEventManager").GetComponent<SpecialEventManager>();
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        Invoke("nextAiMoveDirection", 5);   // K : 5�� �� ai�� ������ ���� ���� �Լ� ����
    }

    void FixedUpdate()
    {
        if (specialManager.AItalk)    // ����� �̺�Ʈ, �÷��̾ AI�� ��ȭ�ϴ� �� AI ����
        {
            rigid.velocity = new Vector2(0, 0); // K : ai ����
        } else {
            rigid.velocity = new Vector2(nextAIMoveX, nextAIMoveY); // K : ai �̵�
        }
    }

    void OnCollisionEnter2D(Collision2D coll)   // Ai �浹 ���� �Լ�
    {
        Debug.Log("Ai �浹 �߻�");             
        nextAIMoveX = 0;                        // Ai �浹 �߻��� ������ ����
        nextAIMoveY = 0;
    }
}
