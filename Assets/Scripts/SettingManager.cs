using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public Slider slider;
    public AudioSource audio;

    private GameObject setting;
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
        audio.volume = slider.value;
        DataController.Instance.settingData.BGMSound = slider.value;
    }

    private void Update()
    {
        SoundSlider();
    }
}
