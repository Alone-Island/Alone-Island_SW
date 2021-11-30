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

    // J : ��� ������ ���� �ڵ�
    private void ending()
    {
        panel.SetActive(true);
        manager.isEndingShow = true;
        DataController.Instance.settingData.firstGame = 0;
    }

    // N : ����� ������ 0�� ���
    public void failHungry()
    {
        Debug.Log("Hungry,,,");
        DataController.Instance.endingData.hungry = 1;

        ending();
        badHungry.SetActive(true);
        Invoke("TheEnd", 2.0f);
    }

    // N : �ູ ������ 0�� ���
    public void failLonely()
    {
        Debug.Log("Lonely,,,");
        DataController.Instance.endingData.lonely = 1;

        ending();
        panel.transform.Find("Bad-Lonely").gameObject.SetActive(true);
        Invoke("TheEnd", 2.0f);
    }

    // N : ü�� ������ 0�� ���
    public void failCold()
    {
        Debug.Log("Cold,,,");
        DataController.Instance.endingData.cold = 1;

        ending();
        panel.transform.Find("Bad-Frozen").gameObject.SetActive(true);
        Invoke("TheEnd", 2.0f);
    }

    public void successPeople()
    {
        Debug.Log("��ű⸦ ���� �ٸ� �����ڵ��� ����");
        DataController.Instance.endingData.people = 1;

        ending();
        happyPeople.SetActive(true);
        Invoke("TheEnd", 2.0f);
    }

    public void successAI()
    {
        Debug.Log("�ٸ� ai�� ������ ai��� �Բ� ��� ��");
        DataController.Instance.endingData.AITown = 1;

        ending();
        happyAITown.SetActive(true);
        Invoke("TheEnd", 2.0f);
    }

    public void successTwo()
    {
        Debug.Log("human�� ai�� �ܵ��� �ູ�ϰ� ��Ҵ�ϴ�");
        DataController.Instance.endingData.two = 1;

        ending();
        happyTwo.SetActive(true);
        Invoke("TheEnd", 2.0f);
    }

    // N : �̺�Ʈ ����
    public void suddenEnding(int endingCode)
    {
        ending();
        switch (endingCode)
        {
            case 1: // N : Bad Ending (������)
                Debug.Log("Poison Berry,,,");
                DataController.Instance.endingData.poisonBerry = 1;
                badBerry.SetActive(true);
                break;
            case 2: // N : Bad Ending (AI�� �������� ����)
                Debug.Log("�ռҸ���,,,");
                DataController.Instance.endingData.error = 1;
                panel.transform.Find("Bad-Error").gameObject.SetActive(true);
                break;
            case 3: // J : Bad Ending (������)
                Debug.Log("������,,,");
                DataController.Instance.endingData.electric = 1;
                badElectric.SetActive(true);
                break;
            case 4: // J : Bad Ending (�����)
                Debug.Log("�����");
                DataController.Instance.endingData.pig = 1;
                badPig.SetActive(true);
                break;
            case 5: // J : Bad Ending (������)
                Debug.Log("������");
                DataController.Instance.endingData.storm = 1;
                panel.transform.Find("Bad-Storm").gameObject.SetActive(true);
                break;
            case 6: // J : Bad Ending (��浹)
                Debug.Log("� �浹");
                DataController.Instance.endingData.space = 1;
                panel.transform.Find("Bad-Space").gameObject.SetActive(true);
                break;
        }
        Invoke("TheEnd", 2.0f);
    }

    public void timeOutEnding()
    {
        Debug.Log("�׳� ���� ��ҽ��ϴ� ~~");
        DataController.Instance.endingData.timeOut = 1;

        ending();
        panel.transform.Find("Happy-SosoLife").gameObject.SetActive(true);
        Invoke("TheEnd", 2.0f);
    }
}
