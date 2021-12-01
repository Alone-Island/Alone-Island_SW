using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingManager : MonoBehaviour
{
    public Slider slider;       // J : 음량 조절 슬라이더
    public AudioSource bgm;     // J : 배경음악 component

    public GameObject On;       // J : 소리 on Image
    public GameObject Off;      // J : 소리 off Image

    private GameObject setting; // J : 설정창
    public bool nowSetting = false;

    void Start()
    {
        float volume = DataController.Instance.settingData.BGMSound;    // J : 설정 데이터의 음량 가져오기
        bgm.volume = volume;  // J : 설정 데이터의 음량으로 게임 시작 시 음량 초기값 세팅
        slider.value = volume;  // J : 설정 데이터의 음량으로 음량 조절 슬라이더의 초기값 세팅
        SetSoundImage(volume);  // J : 초기값으로 소리 이미지 세팅
    }

    void Update()
    {
        SoundSlider();
    }

    public void SelectSetting()
    {
        setting = GameObject.Find("Screen").transform.Find("Setting").gameObject;
        setting.SetActive(true);// J : 설정창 활성화
        nowSetting = true;      // J : 시간 멈추기
    }

    public void SelectSettingQuit()
    {
        setting.SetActive(false);   // J : 설정창 비활성화
        nowSetting = false;         // J : 시간 재생

    }

    // J : 슬라이더의 값으로 음량 조절+설정 데이터에 저장
    private void SoundSlider()
    {
        float volume = slider.value;    // J : 슬라이더의 값 가져오기
        bgm.volume = volume;  // J : 음량을 슬라이더의 값으로 세팅
        DataController.Instance.settingData.BGMSound = volume;  // J : 설정 데이터에 저장
        SetSoundImage(volume);  // J : 음량에 맞게 소리 이미지 세팅
    }

    // J : 음량에 맞게 소리 이미지 세팅
    private void SetSoundImage(float volume)
    {
        // J : 소리가 0보다 크면 On 이미지가 보임
        if (volume > 0)
        {
            On.SetActive(true);
            Off.SetActive(false);
        }

        // J : 소리가 0이면 Off 이미지가 보임
        else
        {
            On.SetActive(false);
            Off.SetActive(true);
        }
    }

    // J : 저장하기 버튼 onclick
    public void SelectStore()
    {
        Debug.Log("저장하기");
    }

    // J : 메뉴로 돌아가기 버튼 onclick
    public void SelectMenu()
    {
        Debug.Log("메뉴로 돌아가기");
        SceneManager.LoadScene("GameMenu");
    }
}
