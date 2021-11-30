using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public Slider slider;
    public AudioSource audio;

    public GameObject On;
    public GameObject Off;

    private GameObject setting;

    void Start()
    {
        float volume = DataController.Instance.settingData.BGMSound;
        audio.volume = volume;
        slider.value = volume;
        SetSoundImage(volume);
    }

    void Update()
    {
        SoundSlider();
    }

    public void SelectSetting()
    {
        setting = GameObject.Find("Screen").transform.Find("Setting").gameObject;
        setting.SetActive(true); // J : ����â Ȱ��ȭ
        // J : �ð� ���߱�
    }

    public void SelectSettingQuit()
    {
        setting.SetActive(false); // J : ����â ��Ȱ��ȭ
        // J : �ð� ���
    }

    private void SoundSlider()
    {
        float volume = slider.value;
        audio.volume = volume;
        DataController.Instance.settingData.BGMSound = volume;
        SetSoundImage(volume);
    }

    private void SetSoundImage(float volume)
    {
        if (volume > 0)
        {
            On.SetActive(true);
            Off.SetActive(false);
        }

        else
        {
            On.SetActive(false);
            Off.SetActive(true);
        }
            
    }

}
