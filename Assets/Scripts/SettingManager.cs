using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingManager : MonoBehaviour
{
    public Slider slider;       // J : ���� ���� �����̴�
    public AudioSource bgm;     // J : ������� component

    public GameObject On;       // J : �Ҹ� on Image
    public GameObject Off;      // J : �Ҹ� off Image

    public GameObject setting;  // J : ����â
    public bool nowSetting = false;

    void Start()
    {
        float volume = DataController.Instance.settingData.BGMSound;    // J : ���� �������� ���� ��������
        bgm.volume = volume;    // J : ���� �������� �������� ���� ���� �� ���� �ʱⰪ ����
        slider.value = volume;  // J : ���� �������� �������� ���� ���� �����̴��� �ʱⰪ ����
        SetSoundImage(volume);  // J : �ʱⰪ���� �Ҹ� �̹��� ����
    }

    void Update()
    {
        SoundSlider();  
    }

    // J : ���� ��ư onclick
    public void SelectSetting()
    {
        setting.SetActive(true);// J : ����â Ȱ��ȭ
        nowSetting = true;      // J : �ð� ���߱�
    }

    // J : ����â ������ ��ư onclick
    public void SelectSettingQuit()
    {
        setting.SetActive(false);   // J : ����â ��Ȱ��ȭ
        nowSetting = false;         // J : �ð� ���

    }

    // J : �����ϱ� ��ư onclick
    public void SelectStore()
    {
        Debug.Log("�����ϱ�");
    }

    // J : �޴��� ���ư��� ��ư onclick
    public void SelectMenu()
    {
        Debug.Log("�޴��� ���ư���");
        setting.transform.Find("MenuAlert").gameObject.SetActive(true);    // J : �޴��� ���ư��� ���â Ȱ��ȭ
    }

    public void SelectMenuYes()
    {
        Debug.Log("�޴��� ���ư��� O");
        SceneManager.LoadScene("GameMenu"); // J : ���Ӹ޴��� �̵�
    }
    public void SelectMenuNo()
    {
        Debug.Log("�޴��� ���ư��� X");
        setting.transform.Find("MenuAlert").gameObject.SetActive(false);    // J : �޴��� ���ư��� ���â ��Ȱ��ȭ
    }

    // J : ���� �ʱ�ȭ ��ư onclick
    public void SelectReset()
    {
        Debug.Log("���� �ʱ�ȭ");
        GameObject.Find("Windows").transform.Find("ResetAlert").gameObject.SetActive(true); // J : ���� �ʱ�ȭ ���â Ȱ��ȭ
    }

    // J : ���� �ʱ�ȭ yes ��ư onclick
    public void SelectResetYes()
    {
        Debug.Log("���� �ʱ�ȭ O");        
        GameObject.Find("Windows").transform.Find("ResetAlert").gameObject.SetActive(false);    // J : ���� �ʱ�ȭ ���â ��Ȱ��ȭ
        SelectSettingQuit();    // J : ����â ��Ȱ��ȭ

        DataController.Instance.DeleteAllData();    // J : ��� ������ ����(.json) ����
        ResetSetting(); // J : ���� �����ص� ���� ���� �������� �ʱ�ȭ
    }

    // J : ���� �ʱ�ȭ no ��ư onclick
    public void SelectResetNo()
    {
        Debug.Log("���� �ʱ�ȭ X");
        GameObject.Find("Windows").transform.Find("ResetAlert").gameObject.SetActive(false);    // J : ���� �ʱ�ȭ ���â ��Ȱ��ȭ
    }

    // J : ���� ������ �ʱ�ȭ
    // J : SettingData�� �����ص� ����â�� ���� ���� �����̴������� ���� �Ҹ� �����ʹ� ����
    // J : =>�����̴����� 1�� ����
    private void ResetSetting()
    {
        slider.value = 1;
    }

    // J : �����̴��� ������ ���� ����+���� �����Ϳ� ����
    private void SoundSlider()
    {
        float volume = slider.value;    // J : �����̴��� �� ��������
        bgm.volume = volume;  // J : ������ �����̴��� ������ ����
        DataController.Instance.settingData.BGMSound = volume;  // J : ���� �����Ϳ� ����
        SetSoundImage(volume);  // J : ������ �°� �Ҹ� �̹��� ����
    }

    // J : ������ �°� �Ҹ� �̹��� ����
    private void SetSoundImage(float volume)
    {
        // J : �Ҹ��� 0���� ũ�� On �̹����� ����
        if (volume > 0)
        {
            On.SetActive(true);
            Off.SetActive(false);
        }

        // J : �Ҹ��� 0�̸� Off �̹����� ����
        else
        {
            On.SetActive(false);
            Off.SetActive(true);
        }
    }
}
