using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public GameManager manager;
    public GameObject panel;            // N : 화면 어둡게

    // N : 스페이스바 입력 받는 시간을 주기 위해
    public void TheEnd()
    {
        manager.isTheEnd = true;
        DataController.Instance.endingData.currentEndingCode = 0;
    }

    // J : 모든 엔딩의 공통 코드
    private void ending()
    {
        panel.SetActive(true);
        manager.isEndingShow = true;
        DataController.Instance.settingData.firstGame = 0;
    }

    // N : 배고픔 스탯이 0인 경우
    public void failHungry()
    {
        Debug.Log("Hungry,,,");
        DataController.Instance.endingData.hungry = 1;

        ending();
        panel.transform.Find("Bad-Hungry").gameObject.SetActive(true);
        Invoke("TheEnd", 2.0f);
    }

    // N : 행복 스탯이 0인 경우
    public void failLonely()
    {
        Debug.Log("Lonely,,,");
        DataController.Instance.endingData.lonely = 1;

        ending();
        panel.transform.Find("Bad-Lonely").gameObject.SetActive(true);
        Invoke("TheEnd", 2.0f);
    }

    // N : 체온 스탯이 0인 경우
    public void failCold()
    {
        Debug.Log("Cold,,,");
        DataController.Instance.endingData.cold = 1;

        ending();
        panel.transform.Find("Bad-Frozen").gameObject.SetActive(true);
        Invoke("TheEnd", 2.0f);
    }

    // N : 이벤트 엔딩 endingCode 1-6
    public void suddenEnding(int endingCode)
    {
        ending();
        switch (endingCode)
        {
            case 1: // N : Bad Ending (독열매)
                Debug.Log("Poison Berry,,,");
                DataController.Instance.endingData.poisonBerry = 1;
                panel.transform.Find("Bad-Berry").gameObject.SetActive(true);
                break;
            case 2: // N : Bad Ending (AI가 이해하지 못함)
                Debug.Log("먼소리야,,,");
                DataController.Instance.endingData.error = 1;
                panel.transform.Find("Bad-Error").gameObject.SetActive(true);
                break;
            case 3: // J : Bad Ending (감전사)
                Debug.Log("감전사,,,");
                DataController.Instance.endingData.electric = 1;
                panel.transform.Find("Bad-Electric").gameObject.SetActive(true);
                break;
            case 4: // J : Bad Ending (멧돼지)
                Debug.Log("멧돼지");
                DataController.Instance.endingData.pig = 1;
                panel.transform.Find("Bad-Pi").gameObject.SetActive(true);
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

    // K : 해피 엔딩 endingCode 101-104
    public void ShowHappyEndingCard(int endingCode)
    {
        ending();
        switch (endingCode)
        {
            case 101:
                panel.transform.Find("Happy-SosoLife").gameObject.SetActive(true);
                break;
            case 102:
                panel.transform.Find("Happy-Two").gameObject.SetActive(true);
                break;
            case 103:
                panel.transform.Find("Happy-AI").gameObject.SetActive(true);
                break;
            case 104:
                panel.transform.Find("Happy-People").gameObject.SetActive(true);
                break;
            default:
                Debug.Log("해피에딩 에러");
                break;
        }
        
        Invoke("TheEnd", 2.0f);
    }

    // K : 해피 엔딩
    public void timeOutEnding()
    {        
        DataController.Instance.endingData.timeOut = 1;
        DataController.Instance.endingData.currentEndingCode = 101;
        Debug.Log("그냥 저냥 살았습니당 ~~");
        SceneManager.LoadScene("Synopsis");
    }

    public void successTwo()
    {
        Debug.Log("human과 ai는 단둘이 행복하게 살았답니다");
        DataController.Instance.endingData.two = 1;
        DataController.Instance.endingData.currentEndingCode = 102;
        SceneManager.LoadScene("Synopsis");
    }

    public void successAI()
    {
        Debug.Log("다른 ai를 만들어내서 ai들과 함께 살게 됨");
        DataController.Instance.endingData.AITown = 1;
        DataController.Instance.endingData.currentEndingCode = 103;
        SceneManager.LoadScene("Synopsis");
    }

    public void successPeople()
    {
        Debug.Log("통신기를 만들어서 다른 생존자들을 만남");
        DataController.Instance.endingData.people = 1;
        DataController.Instance.endingData.currentEndingCode = 104;
        SceneManager.LoadScene("Synopsis");
    }
}

