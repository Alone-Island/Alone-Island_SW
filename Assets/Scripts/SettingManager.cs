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
        setting.SetActive(true); // J : 설정창 활성화
        // J : 시간 멈추기
    }

    public void SelectSettingQuit()
    {
        setting.SetActive(false); // J : 설정창 비활성화
        // J : 시간 재생
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
