using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAction : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextAiMoveX;
    public int nextAiMoveY;

    void nextAiMoveDirection()
    {
        int random = Random.Range(1, 6);    // K : ai�� ������ ���� ���� ����
        int vel = 1;                        // K : ai �̵� �ӵ� ���� 
        switch (random)
        {
            case 1:                         // K : ����
                nextMoveX = 0;
                nextMoveY = 0;
                break;
            case 2:                         // K : ����
                nextMoveX = -1 * vel;
                nextMoveY = 0;
                break;
            case 3:                         // K : ��
                nextMoveX = 0;
                nextMoveY = vel;
                break;
            case 4:                         // K : ������
                nextMoveX = vel; ;
                nextMoveY = 0;
                break;
            case 5:                         // K : �Ʒ�
                nextMoveX = 0;
                nextMoveY = -1 * vel;
                break;
            default:
                nextMoveX = 0;
                nextMoveY = 0;
                break;
        }

        Invoke("nextAiMoveDirection", 5);   // K : ����Լ�, 5�� �� �ڱ� �ڽ��� ����� 
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        Invoke("nextAiMoveDirection", 5);   // K : 5�� �� ai�� ������ ���� ���� �Լ� ����
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextAiMoveX, nextAiMoveY); // K : ai �̵�
    }
}
