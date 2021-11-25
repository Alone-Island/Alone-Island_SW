using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject gameRule; // J : 게임방법 창
    public GameObject BGM;      // J : 배경음악 on/off를 위해 BGM object 가져옴
    public GameObject onImage;  // J : BGM on 이미지
    public GameObject offImage; // J : BGM off 이미지

    AudioSource audioSource;    // J : BGM

    private void Start()
    {
        audioSource = BGM.GetComponent<AudioSource>();
        // J : 게임 데이터에서 가져와 초기 세팅
        if (DataController.Instance.settingData.BGMSound == 1)
            audioSource.Play();     // J : BGM on
        else if (DataController.Instance.settingData.BGMSound == 0)
            audioSource.Stop();     // J : BGM off
    }

    // J : 시작하기 버튼 onclick
    public void SelectStart()
    {
        Debug.Log("시작하기");
        SceneManager.LoadScene("Synopsis"); // J : Synopsis scene으로 이동
    }

    // J : 불러오기 버튼 onclick
    public void SelectBring()
    {
        Debug.Log("불러오기");
    }

    // J : 엔딩카드 버튼 onclick
    public void SelectCard()
    {
        Debug.Log("엔딩카드");
    }

    // J : 게임방법 버튼 onclick
    public void SelectRule()
    {
        Debug.Log("게임방법");
        gameRule.SetActive(true);   // J : 게임방법창 활성화
    }

    // J : 게임방법 나가기 버튼 onclick
    public void SelectRuleQuit()
    {
        Debug.Log("게임방법 나가기");
        gameRule.SetActive(false);  // J : 게임방법창 비활성화
    }

    // J : 오디오 버튼 onclick
    public void SelectAudio()
    {
        if (DataController.Instance.settingData.BGMSound == 1)   // J : BGM 재생중이면 off
        {
            Debug.Log("BGM off");
            audioSource.Stop();     // J : BGM off
            // J : off Image 보이게
            onImage.SetActive(false);
            offImage.SetActive(true);
            DataController.Instance.settingData.BGMSound = 0;
        }
        else        // J : BGM 재생중이면 on
        {
            Debug.Log("BGM on");
            audioSource.Play();     // J : BGM on
            // J : on Image 보이게
            onImage.SetActive(true);
            offImage.SetActive(false);
            DataController.Instance.settingData.BGMSound = 1;
        }
    }
}
