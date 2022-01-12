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

    public GameObject setting;  // J : 설정창
    public bool nowSetting = false;

    void Start()
    {
        float volume = DataController.Instance.settingData.BGMSound;    // J : 설정 데이터의 음량 가져오기
        bgm.volume = volume;    // J : 설정 데이터의 음량으로 게임 시작 시 음량 초기값 세팅
        slider.value = volume;  // J : 설정 데이터의 음량으로 음량 조절 슬라이더의 초기값 세팅
        SetSoundImage(volume);  // J : 초기값으로 소리 이미지 세팅
    }

    void Update()
    {
        SoundSlider();

        // K : 세팅창이 켜져있을 때, esc키를 누르면 세팅창 꺼짐
        if (nowSetting)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                setting.SetActive(false);
                nowSetting = false;
            }
        }        
    }

    // J : 설정 버튼 onclick
    public void SelectSetting()
    {
        setting.SetActive(true);// J : 설정창 활성화
        nowSetting = true;      // J : 시간 멈추기
    }

    // J : 설정창 나가기 버튼 onclick
    public void SelectSettingQuit()
    {
        setting.SetActive(false);   // J : 설정창 비활성화
        nowSetting = false;         // J : 시간 재생

    }

    // J : 메뉴로 돌아가기 버튼 onclick
    public void SelectMenu()
    {
        Debug.Log("메뉴로 돌아가기");
        setting.transform.Find("MenuAlert").gameObject.SetActive(true);    // J : 메뉴로 돌아가기 경고창 활성화
    }

    //J : 메뉴로 돌아가기 경고창 Yes 버튼 onclick
    public void SelectMenuYes()
    {
        Debug.Log("메뉴로 돌아가기 O");
        SceneManager.LoadScene("GameMenu"); // J : 게임메뉴로 이동
    }

    //J : 메뉴로 돌아가기 경고창 No 버튼 onclick
    public void SelectMenuNo()
    {
        Debug.Log("메뉴로 돌아가기 X");
        setting.transform.Find("MenuAlert").gameObject.SetActive(false);    // J : 메뉴로 돌아가기 경고창 비활성화
    }

    // J : 게임 종료 버튼 onclick
    public void SelectGameQuit()
    {
        Debug.Log("게임 종료");
        setting.transform.Find("GameQuitAlert").gameObject.SetActive(true);    // J : 게임 종료 경고창 활성화
    }

    //J : 게임 종료 경고창 Yes 버튼 onclick
    public void SelectGameQuitYes()
    {
        Debug.Log("게임 종료 O");
        Application.Quit(); // J : 프로그램 종료
    }

    //J : 게임 종료 경고창 No 버튼 onclick
    public void SelectGameQuitNo()
    {
        Debug.Log("게임 종료 X");
        setting.transform.Find("GameQuitAlert").gameObject.SetActive(false);    // J : 메뉴로 돌아가기 경고창 비활성화
    }

    // J : 게임 초기화 버튼 onclick
    public void SelectReset()
    {
        Debug.Log("게임 초기화");
        GameObject.Find("Windows").transform.Find("ResetAlert").gameObject.SetActive(true); // J : 게임 초기화 경고창 활성화
    }

    // J : 게임 초기화 yes 버튼 onclick
    public void SelectResetYes()
    {
        Debug.Log("게임 초기화 O");        
        GameObject.Find("Windows").transform.Find("ResetAlert").gameObject.SetActive(false);    // J : 게임 초기화 경고창 비활성화
        SelectSettingQuit();    // J : 설정창 비활성화

        DataController.Instance.DeleteAllData();    // J : 모든 데이터 파일(.json) 삭제
        ResetSetting(); // J : 파일 삭제해도 남는 설정 수동으로 초기화
    }

    // J : 게임 초기화 no 버튼 onclick
    public void SelectResetNo()
    {
        Debug.Log("게임 초기화 X");
        GameObject.Find("Windows").transform.Find("ResetAlert").gameObject.SetActive(false);    // J : 게임 초기화 경고창 비활성화
    }

    // J : 설정 데이터 초기화
    // J : SettingData를 삭제해도 설정창의 음량 조절 슬라이더값으로 인해 소리 데이터는 유지
    // J : =>슬라이더값을 1로 세팅
    private void ResetSetting()
    {
        slider.value = 1;
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
}
