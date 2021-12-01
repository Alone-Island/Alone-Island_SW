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

    private GameObject setting; // J : ����â
    public bool nowSetting = false;

    void Start()
    {
        float volume = DataController.Instance.settingData.BGMSound;    // J : ���� �������� ���� ��������
        bgm.volume = volume;  // J : ���� �������� �������� ���� ���� �� ���� �ʱⰪ ����
        slider.value = volume;  // J : ���� �������� �������� ���� ���� �����̴��� �ʱⰪ ����
        SetSoundImage(volume);  // J : �ʱⰪ���� �Ҹ� �̹��� ����
    }

    void Update()
    {
        SoundSlider();
    }

    public void SelectSetting()
    {
        setting = GameObject.Find("Screen").transform.Find("Setting").gameObject;
        setting.SetActive(true);// J : ����â Ȱ��ȭ
        nowSetting = true;      // J : �ð� ���߱�
    }

    public void SelectSettingQuit()
    {
        setting.SetActive(false);   // J : ����â ��Ȱ��ȭ
        nowSetting = false;         // J : �ð� ���

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

    // J : �����ϱ� ��ư onclick
    public void SelectStore()
    {
        Debug.Log("�����ϱ�");
    }

    // J : �޴��� ���ư��� ��ư onclick
    public void SelectMenu()
    {
        Debug.Log("�޴��� ���ư���");
        SceneManager.LoadScene("GameMenu");
    }
}
