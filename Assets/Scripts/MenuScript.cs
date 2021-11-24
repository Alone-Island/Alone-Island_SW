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

    private bool play = true;   // J : 배경음악 재생중인지 여부

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
        AudioSource audioSource = BGM.GetComponent<AudioSource>();
        if (play)   // J : BGM 재생중이면 off
        {
            Debug.Log("BGM off");
            audioSource.gameObject.SetActive(false);    // J : BGM off
            // J : off Image 보이게
            onImage.SetActive(false);
            offImage.SetActive(true);
            play = false;
        }
        else        // J : BGM 재생중이면 on
        {
            Debug.Log("BGM on");
            audioSource.gameObject.SetActive(true);     // J : BGM on
            // J : on Image 보이게
            onImage.SetActive(true);
            offImage.SetActive(false);
            play = true;
        }
    }
}
