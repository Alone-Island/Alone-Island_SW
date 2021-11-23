using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public GameObject panel; // N : 화면 어둡게
    public GameObject badHungry; // N : 배드 엔딩 (배고픔) 카드

    // N : 배고픔 스탯이 0인 경우
    public void failHungry()
    {
        Debug.Log("Hungry,,,");
        panel.SetActive(true);
        badHungry.SetActive(true);
    }

    // N : 행복 스탯이 0인 경우
    public void failLonely()
    {
        Debug.Log("Lonely,,,");
    }

    // N : 체온 스탯이 0인 경우
    public void failCold()
    {
        Debug.Log("Cold,,,");
    }

    public void successPeople()
    {
        Debug.Log("통신기를 만들어서 다른 생존자들을 만남");
    }

    public void successAI()
    {
        Debug.Log("다른 ai를 만들어내서 ai들과 함께 살게 됨");
    }

    public void successTwo()
    {
        Debug.Log("human과 ai는 단둘이 행복하게 살았답니다");
    }

    // N : 이벤트 엔딩
    public void suddenEnding(int endingCode)
    {
        switch (endingCode)
        {
            case 1: // N : Bad Ending (독열매)
                Debug.Log("Poison Berry,,,");
                break;
            case 2: // N : Bad Ending (AI가 이해하지 못함)
                Debug.Log("먼소리야,,,");
                break;
            case 3: // N : Bad Ending (AI 고장)
                Debug.Log("Broken,,,");
                break;
            case 4: // J : Bad Ending (쓰나미)
                Debug.Log("쓰나미");
                break;
            case 5: // J : Bad Ending (운석충돌)
                Debug.Log("운석 충돌");
                break;
        }
    }

    public void timeOutEnding()
    {
        Debug.Log("그냥 저냥 살았습니당 ~~");
    }
}
