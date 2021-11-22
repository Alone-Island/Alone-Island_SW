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

    }

    // N : 체온 스탯이 0인 경우
    public void failCold()
    {

    }
}
