using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public GameManager manager;

    public GameObject panel;            // N : ȭ�� ��Ӱ�
    public GameObject badHungry;        // N : ��� ���� (�����) ī��
    public GameObject badBerry;         // J : ��� ���� (������) ī��
    public GameObject badElectric;      // J : ��� ���� (������) ī��
    public GameObject badPig;           // J : ��� ���� (�����) ī��
    public GameObject happyAITown;      // J : ���� ���� (AITown) ī��
    public GameObject happyPeople;      // J : ���� ���� (��ű�) ī��
    public GameObject happyTwo;         // J : ���� ���� (�ܵ���) ī��

    // N : �����̽��� �Է� �޴� �ð��� �ֱ� ����
    public void TheEnd()
    {
        manager.isTheEnd = true;
    }

    // N : ����� ������ 0�� ���
    public void failHungry()
    {
        Debug.Log("Hungry,,,");
        panel.SetActive(true);
        badHungry.SetActive(true);
        manager.isEndingShow = true;
        Invoke("TheEnd", 2.0f);
    }

    // N : �ູ ������ 0�� ���
    public void failLonely()
    {
        Debug.Log("Lonely,,,");
        panel.SetActive(true);
        manager.isEndingShow = true;
        Invoke("TheEnd", 2.0f);
    }

    // N : ü�� ������ 0�� ���
    public void failCold()
    {
        Debug.Log("Cold,,,");
        panel.SetActive(true);
        manager.isEndingShow = true;
        Invoke("TheEnd", 2.0f);
    }

    public void successPeople()
    {
        Debug.Log("��ű⸦ ���� �ٸ� �����ڵ��� ����");
        panel.SetActive(true);
        happyPeople.SetActive(true);
        manager.isEndingShow = true;
        Invoke("TheEnd", 2.0f);
    }

    public void successAI()
    {
        Debug.Log("�ٸ� ai�� ������ ai��� �Բ� ��� ��");
        panel.SetActive(true);
        happyAITown.SetActive(true);
        manager.isEndingShow = true;
        Invoke("TheEnd", 2.0f);
    }

    public void successTwo()
    {
        Debug.Log("human�� ai�� �ܵ��� �ູ�ϰ� ��Ҵ�ϴ�");
        panel.SetActive(true);
        happyTwo.SetActive(true);
        manager.isEndingShow = true;
        Invoke("TheEnd", 2.0f);
    }

    // N : �̺�Ʈ ����
    public void suddenEnding(int endingCode)
    {
        panel.SetActive(true);  // J : ȭ�� ��ο���
        manager.isEndingShow = true;
        switch (endingCode)
        {
            case 1: // N : Bad Ending (������)
                Debug.Log("Poison Berry,,,");
                badBerry.SetActive(true);
                break;
            case 2: // N : Bad Ending (AI�� �������� ����)
                Debug.Log("�ռҸ���,,,");
                break;
            case 3: // J : Bad Ending (������)
                Debug.Log("������,,,");
                badElectric.SetActive(true);
                break;
            case 4: // J : Bad Ending (�����)
                Debug.Log("�����");
                badPig.SetActive(true);
                break;
            case 5: // J : Bad Ending (������)
                Debug.Log("������");
                break;
            case 6: // J : Bad Ending (��浹)
                Debug.Log("� �浹");
                break;
        }
        Invoke("TheEnd", 2.0f);
    }

    public void timeOutEnding()
    {
        Debug.Log("�׳� ���� ��ҽ��ϴ� ~~");
        panel.SetActive(true);
        manager.isEndingShow = true;
        Invoke("TheEnd", 2.0f);
    }
}
