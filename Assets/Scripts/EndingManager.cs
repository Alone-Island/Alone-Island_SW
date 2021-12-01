using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public GameManager manager;
    public GameObject panel;            // N : ȭ�� ��Ӱ�

    // N : �����̽��� �Է� �޴� �ð��� �ֱ� ����
    public void TheEnd()
    {
        manager.isTheEnd = true;
        DataController.Instance.endingData.currentEndingCode = 0;
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
        panel.transform.Find("Bad-Hungry").gameObject.SetActive(true);
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

    // N : �̺�Ʈ ���� endingCode 1-6
    public void suddenEnding(int endingCode)
    {
        ending();
        switch (endingCode)
        {
            case 1: // N : Bad Ending (������)
                Debug.Log("Poison Berry,,,");
                DataController.Instance.endingData.poisonBerry = 1;
                panel.transform.Find("Bad-Berry").gameObject.SetActive(true);
                break;
            case 2: // N : Bad Ending (AI�� �������� ����)
                Debug.Log("�ռҸ���,,,");
                DataController.Instance.endingData.error = 1;
                panel.transform.Find("Bad-Error").gameObject.SetActive(true);
                break;
            case 3: // J : Bad Ending (������)
                Debug.Log("������,,,");
                DataController.Instance.endingData.electric = 1;
                panel.transform.Find("Bad-Electric").gameObject.SetActive(true);
                break;
            case 4: // J : Bad Ending (�����)
                Debug.Log("�����");
                DataController.Instance.endingData.pig = 1;
                panel.transform.Find("Bad-Pi").gameObject.SetActive(true);
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

    // K : ���� ���� endingCode 101-104
    public void ShowHappyEndingCard(int endingCode)
    {
        ending();
        switch (endingCode)
        {
            case 101:
                panel.transform.Find("Happy-SosoLife").gameObject.SetActive(true);
                break;
            case 102:
                panel.transform.Find("Happy-Two").gameObject.SetActive(true);
                break;
            case 103:
                panel.transform.Find("Happy-AI").gameObject.SetActive(true);
                break;
            case 104:
                panel.transform.Find("Happy-People").gameObject.SetActive(true);
                break;
            default:
                Debug.Log("���ǿ��� ����");
                break;
        }
        
        Invoke("TheEnd", 2.0f);
    }

    // K : ���� ����
    public void timeOutEnding()
    {        
        DataController.Instance.endingData.timeOut = 1;
        DataController.Instance.endingData.currentEndingCode = 101;
        Debug.Log("�׳� ���� ��ҽ��ϴ� ~~");
        SceneManager.LoadScene("Synopsis");
    }

    public void successTwo()
    {
        Debug.Log("human�� ai�� �ܵ��� �ູ�ϰ� ��Ҵ�ϴ�");
        DataController.Instance.endingData.two = 1;
        DataController.Instance.endingData.currentEndingCode = 102;
        SceneManager.LoadScene("Synopsis");
    }

    public void successAI()
    {
        Debug.Log("�ٸ� ai�� ������ ai��� �Բ� ��� ��");
        DataController.Instance.endingData.AITown = 1;
        DataController.Instance.endingData.currentEndingCode = 103;
        SceneManager.LoadScene("Synopsis");
    }

    public void successPeople()
    {
        Debug.Log("��ű⸦ ���� �ٸ� �����ڵ��� ����");
        DataController.Instance.endingData.people = 1;
        DataController.Instance.endingData.currentEndingCode = 104;
        SceneManager.LoadScene("Synopsis");
    }
}

