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
        panel.SetActive(true);
        badHungry.SetActive(true);
        manager.isEndingShow = true;
        Invoke("TheEnd", 2.0f);
    }

    // N : 행복 스탯이 0인 경우
    public void failLonely()
    {
        Debug.Log("Lonely,,,");
        panel.SetActive(true);
        manager.isEndingShow = true;
        Invoke("TheEnd", 2.0f);
    }

    // N : 체온 스탯이 0인 경우
    public void failCold()
    {
        Debug.Log("Cold,,,");
        panel.SetActive(true);
        manager.isEndingShow = true;
        Invoke("TheEnd", 2.0f);
    }

    public void successPeople()
    {
        Debug.Log("통신기를 만들어서 다른 생존자들을 만남");
        panel.SetActive(true);
        happyPeople.SetActive(true);
        manager.isEndingShow = true;
        Invoke("TheEnd", 2.0f);
    }

    public void successAI()
    {
        Debug.Log("다른 ai를 만들어내서 ai들과 함께 살게 됨");
        panel.SetActive(true);
        happyAITown.SetActive(true);
        manager.isEndingShow = true;
        Invoke("TheEnd", 2.0f);
    }

    public void successTwo()
    {
        Debug.Log("human과 ai는 단둘이 행복하게 살았답니다");
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
                badBerry.SetActive(true);
                break;
            case 2: // N : Bad Ending (AI가 이해하지 못함)
                Debug.Log("먼소리야,,,");
                break;
            case 3: // J : Bad Ending (감전사)
                Debug.Log("감전사,,,");
                badElectric.SetActive(true);
                break;
            case 4: // J : Bad Ending (멧돼지)
                Debug.Log("멧돼지");
                badPig.SetActive(true);
                break;
            case 5: // J : Bad Ending (쓰나미)
                Debug.Log("쓰나미");
                break;
            case 6: // J : Bad Ending (운석충돌)
                Debug.Log("운석 충돌");
                break;
        }
        Invoke("TheEnd", 2.0f);
    }

    public void timeOutEnding()
    {
        Debug.Log("그냥 저냥 살았습니당 ~~");
        panel.SetActive(true);
        manager.isEndingShow = true;
        Invoke("TheEnd", 2.0f);
    }
}
