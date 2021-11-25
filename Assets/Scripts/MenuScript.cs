using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject gameRule; // J : ���ӹ�� â
    public GameObject BGM;      // J : ������� on/off�� ���� BGM object ������
    public GameObject onImage;  // J : BGM on �̹���
    public GameObject offImage; // J : BGM off �̹���

    AudioSource audioSource;    // J : BGM

    private void Start()
    {
        audioSource = BGM.GetComponent<AudioSource>();
        // J : ���� �����Ϳ��� ������ �ʱ� ����
        if (DataController.Instance.settingData.BGMSound == 1)
            audioSource.Play();     // J : BGM on
        else if (DataController.Instance.settingData.BGMSound == 0)
            audioSource.Stop();     // J : BGM off
    }

    // J : �����ϱ� ��ư onclick
    public void SelectStart()
    {
        Debug.Log("�����ϱ�");
        SceneManager.LoadScene("Synopsis"); // J : Synopsis scene���� �̵�
    }

    // J : �ҷ����� ��ư onclick
    public void SelectBring()
    {
        Debug.Log("�ҷ�����");
    }

    // J : ����ī�� ��ư onclick
    public void SelectCard()
    {
        Debug.Log("����ī��");
    }

    // J : ���ӹ�� ��ư onclick
    public void SelectRule()
    {
        Debug.Log("���ӹ��");
        gameRule.SetActive(true);   // J : ���ӹ��â Ȱ��ȭ
    }

    // J : ���ӹ�� ������ ��ư onclick
    public void SelectRuleQuit()
    {
        Debug.Log("���ӹ�� ������");
        gameRule.SetActive(false);  // J : ���ӹ��â ��Ȱ��ȭ
    }

    // J : ����� ��ư onclick
    public void SelectAudio()
    {
        if (DataController.Instance.settingData.BGMSound == 1)   // J : BGM ������̸� off
        {
            Debug.Log("BGM off");
            audioSource.Stop();     // J : BGM off
            // J : off Image ���̰�
            onImage.SetActive(false);
            offImage.SetActive(true);
            DataController.Instance.settingData.BGMSound = 0;
        }
        else        // J : BGM ������̸� on
        {
            Debug.Log("BGM on");
            audioSource.Play();     // J : BGM on
            // J : on Image ���̰�
            onImage.SetActive(true);
            offImage.SetActive(false);
            DataController.Instance.settingData.BGMSound = 1;
        }
    }
}
