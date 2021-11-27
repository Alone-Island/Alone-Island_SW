using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject gameRule;     // J : 게임방법 창
    public GameObject scrollView;   // J : 엔딩카드창

    public GameObject BGM;          // J : 배경음악 on/off를 위해 BGM object 가져옴
    public GameObject onImage;      // J : BGM on 이미지
    public GameObject offImage;     // J : BGM off 이미지
    AudioSource audioSource;        // J : BGM

    public GameObject endingCards;  // J : Scroll View->Viewport->Content
    public Sprite badCard;

    private void Start()
    {
        audioSource = BGM.GetComponent<AudioSource>();
        // J : 게임 데이터에서 가져와 초기 세팅
        if (DataController.Instance.settingData.BGMSound == 1)
            audioPlay();

        else if (DataController.Instance.settingData.BGMSound == 0)
            audioStop();
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
        scrollView.SetActive(true);

        if (DataController.Instance.endingData.hungry == 1)
        {

            Image card = endingCards.transform.Find("BadLine0").transform.Find("hungry").GetComponent<Image>();
            card.sprite = badCard;
            endingCards.transform.Find("BadLine0").transform.Find("hungry").transform.Find("Image").gameObject.SetActive(false);
            endingCards.transform.Find("BadLine0").transform.Find("hungry").transform.Find("hungry").gameObject.SetActive(true);
        }
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
            audioStop();
            DataController.Instance.settingData.BGMSound = 0;
        }
        else        // J : BGM 재생중이면 on
        {
            Debug.Log("BGM on");
            audioPlay();
            DataController.Instance.settingData.BGMSound = 1;
        }
    }

    private void audioPlay()
    {
        audioSource.Play();     // J : BGM on
        // J : on Image 보이게
        onImage.SetActive(true);
        offImage.SetActive(false);
    }

    private void audioStop()
    {
        audioSource.Stop();     // J : BGM off
        // J : off Image 보이게
        onImage.SetActive(false);
        offImage.SetActive(true);
    }
}
