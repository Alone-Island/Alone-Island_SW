using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// K : Scene > AI > Rigidbody 2D > body type > kinetic
// K : Scene > AI > AiAcition script �߰�
public class AIAction : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;                          // C : �ִϸ��̼� ����
    SpecialEventManager specialManager;     // K : SpecialEventManager�� �Լ��� ȣ���� �� �ֵ��� specialManager ���� ����
    bool isAiStuding = true;                // K : ???�� AI�� �н������� Ȯ���� �� �ִ� ���� ȣ���� ���� ���� ����
    public int nextAIMoveX = 0;             // K : ai�� ���� X�� ���� ����
    public int nextAIMoveY = 0;             // K : ai�� ���� Y�� ���� ����
    //bool isAICollision = false;             // K : ai �浹 Ȯ�� ���� > ���� ��� ����

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

    public void GoToStudyPlace(int x, int y)
    {
        transform.position = new Vector3(x, y, 0);
    }

    void Awake()
    {
        // C : Animator component instance ����
        anim = GetComponent<Animator>();
        // K : SpecialEventManager�� �Լ��� ȣ���� �� �ֵ��� specialManager ������ �ҷ����� ���� ȣ��
        specialManager = GameObject.Find("SpecialEventManager").GetComponent<SpecialEventManager>();
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        
        //Invoke("NextAiMoveDirection", 5);   // K : 5�� �� ai�� ������ ���� ���� �Լ� ����
    }

    void Update()
    {   
        // C : ai �̵�(NextAiMoveDirection�� ���)���� ���� �ִϸ��̼� ����
        if (anim.GetInteger("hAxisRaw") != nextAIMoveX)         // C : ai �¿� �̵� �� ������ �ִϸ��̼� ����
        {
            anim.SetInteger("hAxisRaw", (int)nextAIMoveX);
        }
        else if (anim.GetInteger("vAxisRaw") != nextAIMoveY)    // C : ai ���� �̵� �� ������ �ִϸ��̼� ����
        {
            anim.SetInteger("vAxisRaw", (int)nextAIMoveY);
        }
    }

    void FixedUpdate()
    {
        if (specialManager.AItalk || isAiStuding)    // ����� �̺�Ʈ, �÷��̾ AI�� ��ȭ�ϴ� �� �Ǵ� AI�� �н����϶� ����
        {
            rigid.velocity = new Vector2(0, 0); // K : ai ����
        }
        else
        {
            rigid.velocity = new Vector2(nextAIMoveX, nextAIMoveY); // K : ai �̵�
        }
    }

    void OnCollisionEnter2D(Collision2D coll)   // Ai �浹 ���� �Լ�
    {
        //isAICollision = true;                   // K : AI �浹
        nextAIMoveX = 0;                        // Ai �浹 �߻��� ������ ����
        nextAIMoveY = 0;
    }

    void OnCollisionExit2D(Collision2D coll)   // Ai �浹 ���� ���� �Լ�
    {
        //isAICollision = false;                 // K : AI �浹 ����
    }
}
