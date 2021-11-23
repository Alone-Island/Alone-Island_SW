using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject gameRule;

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
}
