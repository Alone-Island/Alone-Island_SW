using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    private Image panel;

    private void Awake()
    {
        panel = this.GetComponent<Image>(); // J : panel의 이미지 component 가져오기
    }
    private void Start()
    {
        StartCoroutine("FadeIn");   // J : 게임 시작 시 페이드인
    }

    public IEnumerator FadeIn()
    {
        float fadeCount = 1;    // J : 초기 알파값(검은 화면)
        while (fadeCount > 0)    // J : 알파값이 최대(1)가 될 때까지 반복
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f); // J : 0.01초마다 밝아지게->1초 후 완전히 밝아짐
            panel.color = new Color(0, 0, 0, fadeCount);    // J : 알파값 조정
        }
    }
}
