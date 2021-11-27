using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject gameRule;     // J : ���ӹ�� â
    public GameObject scrollView;   // J : ����ī��â

    public GameObject BGM;          // J : ������� on/off�� ���� BGM object ������
    public GameObject onImage;      // J : BGM on �̹���
    public GameObject offImage;     // J : BGM off �̹���
    AudioSource audioSource;        // J : BGM

    public GameObject endingCards;  // J : Scroll View->Viewport->Content
    public Sprite badCard;

    private void Start()
    {
        audioSource = BGM.GetComponent<AudioSource>();
        // J : ���� �����Ϳ��� ������ �ʱ� ����
        if (DataController.Instance.settingData.BGMSound == 1)
            audioPlay();

        else if (DataController.Instance.settingData.BGMSound == 0)
            audioStop();
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
        scrollView.SetActive(true);

        if (DataController.Instance.endingData.hungry == 1)
        {

            Image card = endingCards.transform.Find("BadLine0").transform.Find("hungry").GetComponent<Image>();
            card.sprite = badCard;
            endingCards.transform.Find("BadLine0").transform.Find("hungry").transform.Find("Image").gameObject.SetActive(false);
            endingCards.transform.Find("BadLine0").transform.Find("hungry").transform.Find("hungry").gameObject.SetActive(true);
        }
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
            audioStop();
            DataController.Instance.settingData.BGMSound = 0;
        }
        else        // J : BGM ������̸� on
        {
            Debug.Log("BGM on");
            audioPlay();
            DataController.Instance.settingData.BGMSound = 1;
        }
    }

    private void audioPlay()
    {
        audioSource.Play();     // J : BGM on
        // J : on Image ���̰�
        onImage.SetActive(true);
        offImage.SetActive(false);
    }

    private void audioStop()
    {
        audioSource.Stop();     // J : BGM off
        // J : off Image ���̰�
        onImage.SetActive(false);
        offImage.SetActive(true);
    }
}
