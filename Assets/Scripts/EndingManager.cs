using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public GameObject panel; // N : ȭ�� ��Ӱ�
    public GameObject badHungry; // N : ��� ���� (�����) ī��

    // N : ����� ������ 0�� ���
    public void failHungry()
    {
        Debug.Log("Hungry,,,");
        panel.SetActive(true);
        badHungry.SetActive(true);
    }

    // N : �ູ ������ 0�� ���
    public void failLonely()
    {
        Debug.Log("Lonely,,,");
    }

    // N : ü�� ������ 0�� ���
    public void failCold()
    {
        Debug.Log("Cold,,,");
    }

    public void successPeople()
    {
        Debug.Log("��ű⸦ ���� �ٸ� �����ڵ��� ����");
    }

    public void successAI()
    {
        Debug.Log("�ٸ� ai�� ������ ai��� �Բ� ��� ��");
    }

    public void successTwo()
    {
        Debug.Log("human�� ai�� �ܵ��� �ູ�ϰ� ��Ҵ�ϴ�");
    }

    // N : �̺�Ʈ ����
    public void suddenEnding(int endingCode)
    {
        switch (endingCode)
        {
            case 1: // N : Bad Ending (������)
                Debug.Log("Poison Berry,,,");
                break;
            case 2: // N : Bad Ending (AI�� �������� ����)
                Debug.Log("�ռҸ���,,,");
                break;
            case 3: // N : Bad Ending (AI ����)
                Debug.Log("Broken,,,");
                break;
            case 4: // J : Bad Ending (������)
                Debug.Log("������");
                break;
            case 5: // J : Bad Ending (��浹)
                Debug.Log("� �浹");
                break;
        }
    }

    public void timeOutEnding()
    {
        Debug.Log("�׳� ���� ��ҽ��ϴ� ~~");
    }
}
