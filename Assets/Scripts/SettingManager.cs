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
