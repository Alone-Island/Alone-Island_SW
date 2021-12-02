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
    public LearningManager learningManager;
    public GameManager gameManager;
    public int vel = 1;                     // K : ai �̵� �ӵ� ����
    public int nextAIMoveX = 0;             // K : ai�� ���� X�� ���� ����
    public int nextAIMoveY = 0;             // K : ai�� ���� Y�� ���� ����
    public bool isAICollisionToPlayer = false;     // K : ai�� player�� �浹

    void NextAiMoveDirection()              // K : ai�� �����ϰ� �����̵��� ������ ������ �������ִ� �Լ�
    {
        int random = Random.Range(2, 6);    // K : ai�� ������ ���� ���� ����

        // K : AI�� ���� �ȵǴ� ���� ����

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
                nextAIMoveX = vel;
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

    public void GoToLearningPlace(int x, int y) // K : AI�� �н� ��ҷ� �����̵� �ϰ� �ϴ� �Լ�
                                                // (x,y)��ǥ�� �Ķ���ͷ� �޴´�.
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

        Invoke("NextAiMoveDirection", 5);   // K : 5�� �� ai�� ������ ���� ���� �Լ� ����
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
        if (specialManager.special || learningManager.isAILearning || gameManager.isEndingShow || gameManager.playerTalk)    // ����� �̺�Ʈ, �÷��̾ AI�� ��ȭ�ϴ� �� �Ǵ� AI�� �н����϶� ����
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
        //isAICollision = true;
        if (coll.gameObject.name == "Dr.Kim")
        {
            isAICollisionToPlayer = true;
            nextAIMoveX = 0;
            nextAIMoveY = 0;
            vel = 0;
        }

        
    }

    void OnCollisionStay2D(Collision2D coll)  // Ai �浹 ���� ���� �Լ�
    {
        
    }

    void OnCollisionExit2D(Collision2D coll)   // K : Ai �浹 ���� ���� �Լ�
    {
        //isAICollision = false;                 // K : AI �浹 ����
        if (coll.gameObject.name == "Dr.Kim")  // K : Ai�� �÷��̾�� �浹�� ������ ���
        {
            isAICollisionToPlayer = false;
            vel = 1;
        }
    }
}