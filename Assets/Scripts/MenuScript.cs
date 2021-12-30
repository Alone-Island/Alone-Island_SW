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

    public GameObject endingCards;  // J : Scroll View->Viewport->Content
    public Sprite badCard;
    public Sprite happyCard;


    // J : 시작하기 버튼 onclick
    public void SelectStart()
    {
        Debug.Log("시작하기");
        if (true)//DataController.Instance.settingData.firstGame == 1) // J : 첫게임이면
            SceneManager.LoadScene("Synopsis"); // J : Synopsis scene으로 이동
        else
            SceneManager.LoadScene("MainGame"); // J : MainGame scene으로 이동
    }

    // J : 엔딩카드 버튼 onclick
    public void SelectCard()
    {
        Debug.Log("엔딩카드");
        scrollView.SetActive(true);

        Image card;

        // J : BadLine0 엔딩카드
        if (DataController.Instance.endingData.hungry == 1)
        {
            card = endingCards.transform.Find("BadLine0").transform.Find("hungry").GetComponent<Image>();
            card.sprite = badCard;
            endingCards.transform.Find("BadLine0").transform.Find("hungry").transform.Find("Image").gameObject.SetActive(false);
            endingCards.transform.Find("BadLine0").transform.Find("hungry").transform.Find("hungry").gameObject.SetActive(true);
        }
        if (DataController.Instance.endingData.lonely == 1)
        {
            card = endingCards.transform.Find("BadLine0").transform.Find("lonely").GetComponent<Image>();
            card.sprite = badCard;
            endingCards.transform.Find("BadLine0").transform.Find("lonely").transform.Find("Image").gameObject.SetActive(false);
            endingCards.transform.Find("BadLine0").transform.Find("lonely").transform.Find("lonely").gameObject.SetActive(true);
        }
        if (DataController.Instance.endingData.cold == 1)
        {
            card = endingCards.transform.Find("BadLine0").transform.Find("cold").GetComponent<Image>();
            card.sprite = badCard;
            endingCards.transform.Find("BadLine0").transform.Find("cold").transform.Find("Image").gameObject.SetActive(false);
            endingCards.transform.Find("BadLine0").transform.Find("cold").transform.Find("cold").gameObject.SetActive(true);
        }

        // J : BadLine1 엔딩카드
        if (DataController.Instance.endingData.poisonBerry == 1)
        {
            card = endingCards.transform.Find("BadLine1").transform.Find("poisonBerry").GetComponent<Image>();
            card.sprite = badCard;
            endingCards.transform.Find("BadLine1").transform.Find("poisonBerry").transform.Find("Image").gameObject.SetActive(false);
            endingCards.transform.Find("BadLine1").transform.Find("poisonBerry").transform.Find("poisonBerry").gameObject.SetActive(true);
        }
        if (DataController.Instance.endingData.error == 1)
        {
            card = endingCards.transform.Find("BadLine1").transform.Find("error").GetComponent<Image>();
            card.sprite = badCard;
            endingCards.transform.Find("BadLine1").transform.Find("error").transform.Find("Image").gameObject.SetActive(false);
            endingCards.transform.Find("BadLine1").transform.Find("error").transform.Find("error").gameObject.SetActive(true);
        }
        if (DataController.Instance.endingData.electric == 1)
        {
            card = endingCards.transform.Find("BadLine1").transform.Find("electric").GetComponent<Image>();
            card.sprite = badCard;
            endingCards.transform.Find("BadLine1").transform.Find("electric").transform.Find("Image").gameObject.SetActive(false);
            endingCards.transform.Find("BadLine1").transform.Find("electric").transform.Find("electric").gameObject.SetActive(true);
        }
        if (DataController.Instance.endingData.pig == 1)
        {
            card = endingCards.transform.Find("BadLine1").transform.Find("pig").GetComponent<Image>();
            card.sprite = badCard;
            endingCards.transform.Find("BadLine1").transform.Find("pig").transform.Find("Image").gameObject.SetActive(false);
            endingCards.transform.Find("BadLine1").transform.Find("pig").transform.Find("pig").gameObject.SetActive(true);
        }
        if (DataController.Instance.endingData.storm == 1)
        {
            card = endingCards.transform.Find("BadLine1").transform.Find("storm").GetComponent<Image>();
            card.sprite = badCard;
            endingCards.transform.Find("BadLine1").transform.Find("storm").transform.Find("Image").gameObject.SetActive(false);
            endingCards.transform.Find("BadLine1").transform.Find("storm").transform.Find("storm").gameObject.SetActive(true);
        }

        // J : BadLine2 엔딩카드
        if (DataController.Instance.endingData.space == 1)
        {
            card = endingCards.transform.Find("BadLine2").transform.Find("space").GetComponent<Image>();
            card.sprite = badCard;
            endingCards.transform.Find("BadLine2").transform.Find("space").transform.Find("Image").gameObject.SetActive(false);
            endingCards.transform.Find("BadLine2").transform.Find("space").transform.Find("space").gameObject.SetActive(true);
        }

        // J : HappyLine0 엔딩카드
        if (DataController.Instance.endingData.timeOut == 1)
        {
            card = endingCards.transform.Find("HappyLine0").transform.Find("timeOut").GetComponent<Image>();
            card.sprite = happyCard;
            endingCards.transform.Find("HappyLine0").transform.Find("timeOut").transform.Find("Image").gameObject.SetActive(false);
            endingCards.transform.Find("HappyLine0").transform.Find("timeOut").transform.Find("timeOut").gameObject.SetActive(true);
        }
        if (DataController.Instance.endingData.two == 1)
        {
            card = endingCards.transform.Find("HappyLine0").transform.Find("two").GetComponent<Image>();
            card.sprite = happyCard;
            endingCards.transform.Find("HappyLine0").transform.Find("two").transform.Find("Image").gameObject.SetActive(false);
            endingCards.transform.Find("HappyLine0").transform.Find("two").transform.Find("two").gameObject.SetActive(true);
        }
        if (DataController.Instance.endingData.AITown == 1)
        {
            card = endingCards.transform.Find("HappyLine0").transform.Find("AITown").GetComponent<Image>();
            card.sprite = happyCard;
            endingCards.transform.Find("HappyLine0").transform.Find("AITown").transform.Find("Image").gameObject.SetActive(false);
            endingCards.transform.Find("HappyLine0").transform.Find("AITown").transform.Find("AITown").gameObject.SetActive(true);
        }
        if (DataController.Instance.endingData.people == 1)
        {
            card = endingCards.transform.Find("HappyLine0").transform.Find("people").GetComponent<Image>();
            card.sprite = happyCard;
            endingCards.transform.Find("HappyLine0").transform.Find("people").transform.Find("Image").gameObject.SetActive(false);
            endingCards.transform.Find("HappyLine0").transform.Find("people").transform.Find("people").gameObject.SetActive(true);
        }
    }

    // J : 엔딩카드 나가기 버튼 onclick
    public void SelectCardQuit()
    {
        Debug.Log("엔딩카드 나가기");
        scrollView.SetActive(false);  // J : 게임방법창 비활성화
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

    // J : 게임종료 버튼 onclick
    public void SelectGameQuit()
    {
        Debug.Log("게임종료");
        Application.Quit(); // J : 프로그램 종료
    }
}
