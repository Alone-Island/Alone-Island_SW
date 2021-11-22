using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// K : Scene > AI > Rigidbody 2D > body type > kinetic
// K : Scene > AI > AiAcition script �߰�
public class AIAction : MonoBehaviour
{
    Rigidbody2D rigid;
    SpecialEventManager specialManager; // K : SpecialEventManager�� �Լ��� ȣ���� �� �ֵ��� specialManager ���� ����
    public int nextAIMoveX = 0;             // K : ai�� ���� X�� ���� ����
    public int nextAIMoveY = 0;             // K : ai�� ���� Y�� ���� ����
    // public bool isAICollision = false;

    void NextAiMoveDirection()              // K : ai�� �����ϰ� �����̵��� ������ ������ �������ִ� �Լ�
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

        Invoke("NextAiMoveDirection", 5);   // K : ����Լ�, 5�� �� �ڱ� �ڽ��� ����� 
    }

    void Awake()
    {
        // K : SpecialEventManager�� �Լ��� ȣ���� �� �ֵ��� specialManager ������ �ҷ����� ���� ȣ��
        specialManager = GameObject.Find("SpecialEventManager").GetComponent<SpecialEventManager>();
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        Invoke("NextAiMoveDirection", 5);   // K : 5�� �� ai�� ������ ���� ���� �Լ� ����
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
