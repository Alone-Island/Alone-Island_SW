using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public GameManager manager;

    public GameObject panel;            // N : 화면 어둡게
    public GameObject badHungry;        // N : 배드 엔딩 (배고픔) 카드
    public GameObject badBerry;         // J : 배드 엔딩 (독열매) 카드
    public GameObject badElectric;      // J : 배드 엔딩 (감전사) 카드
    public GameObject badPig;           // J : 배드 엔딩 (멧돼지) 카드
    public GameObject happyAITown;      // J : 해피 엔딩 (AITown) 카드
    public GameObject happyPeople;      // J : 해피 엔딩 (통신기) 카드
    public GameObject happyTwo;         // J : 해피 엔딩 (단둘이) 카드

    // N : 스페이스바 입력 받는 시간을 주기 위해
    public void TheEnd()
    {
        manager.isTheEnd = true;
    }

    // N : 배고픔 스탯이 0인 경우
    public void failHungry()
    {
        Debug.Log("Hungry,,,");
        DataController.Instance.endingData.hungry = 1;

        panel.SetActive(true);
        badHungry.SetActive(true);

        manager.isEndingShow = true;
        Invoke("TheEnd", 2.0f);
    }

    // N : 행복 스탯이 0인 경우
    public void failLonely()
    {
        Debug.Log("Lonely,,,");
        DataController.Instance.endingData.lonely = 1;

        panel.SetActive(true);
        panel.transform.Find("Bad-Lonely").gameObject.SetActive(true);

        manager.isEndingShow = true;
        Invoke("TheEnd", 2.0f);
    }

    // N : 체온 스탯이 0인 경우
    public void failCold()
    {
        Debug.Log("Cold,,,");
        DataController.Instance.endingData.cold = 1;

        panel.SetActive(true);
        panel.transform.Find("Bad-Frozen").gameObject.SetActive(true);

        manager.isEndingShow = true;
        Invoke("TheEnd", 2.0f);
    }

    public void successPeople()
    {
        Debug.Log("통신기를 만들어서 다른 생존자들을 만남");
        DataController.Instance.endingData.people = 1;

        panel.SetActive(true);
        happyPeople.SetActive(true);

        manager.isEndingShow = true;
        Invoke("TheEnd", 2.0f);
    }

    public void successAI()
    {
        Debug.Log("다른 ai를 만들어내서 ai들과 함께 살게 됨");
        DataController.Instance.endingData.AITown = 1;

        panel.SetActive(true);
        happyAITown.SetActive(true);

        manager.isEndingShow = true;
        Invoke("TheEnd", 2.0f);
    }

    public void successTwo()
    {
        Debug.Log("human과 ai는 단둘이 행복하게 살았답니다");
        DataController.Instance.endingData.two = 1;

        panel.SetActive(true);
        happyTwo.SetActive(true);

        manager.isEndingShow = true;
        Invoke("TheEnd", 2.0f);
    }

    // N : 이벤트 엔딩
    public void suddenEnding(int endingCode)
    {
        panel.SetActive(true);  // J : 화면 어두워짐
        manager.isEndingShow = true;
        switch (endingCode)
        {
            case 1: // N : Bad Ending (독열매)
                Debug.Log("Poison Berry,,,");
                DataController.Instance.endingData.poisonBerry = 1;
                badBerry.SetActive(true);
                break;
            case 2: // N : Bad Ending (AI가 이해하지 못함)
                Debug.Log("먼소리야,,,");
                DataController.Instance.endingData.error = 1;
                panel.transform.Find("Bad-Error").gameObject.SetActive(true);
                break;
            case 3: // J : Bad Ending (감전사)
                Debug.Log("감전사,,,");
                DataController.Instance.endingData.electric = 1;
                badElectric.SetActive(true);
                break;
            case 4: // J : Bad Ending (멧돼지)
                Debug.Log("멧돼지");
                DataController.Instance.endingData.pig = 1;
                badPig.SetActive(true);
                break;
            case 5: // J : Bad Ending (쓰나미)
                Debug.Log("쓰나미");
                DataController.Instance.endingData.storm = 1;
                panel.transform.Find("Bad-Storm").gameObject.SetActive(true);
                break;
            case 6: // J : Bad Ending (운석충돌)
                Debug.Log("운석 충돌");
                DataController.Instance.endingData.space = 1;
                panel.transform.Find("Bad-Space").gameObject.SetActive(true);
                break;
        }
        Invoke("TheEnd", 2.0f);
    }

    public void timeOutEnding()
    {
        Debug.Log("그냥 저냥 살았습니당 ~~");
        DataController.Instance.endingData.timeOut = 1;

        panel.SetActive(true);
        panel.transform.Find("Happy-SosoLife").gameObject.SetActive(true);
        manager.isEndingShow = true;
        Invoke("TheEnd", 2.0f);
    }
}
