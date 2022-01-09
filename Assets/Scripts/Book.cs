using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// J : Book Prefab Script
public class Book : MonoBehaviour
{
    private GameObject player;          // J : Dr.Kim(플레이어) 오브젝트
    private Vector2 playerPos, bookPos; // J : 플레이어의 위치, 책(자신)의 위치
    private int radius = 3;             // J : 책이 보이는 범위
    private float fadeCount = 0;        // J : 초기 알파값
    private float fadeInterval = 0.003f;// J : 페이드 시간 간격(0.01이면 1초 소요)
    private bool inside;                // J : 책이 플레이어의 반경 내에 있음
    private bool outside;               // J : 책이 플레이어의 반경 외에 있음

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Dr.Kim");     // J : 플레이어 오브젝트
        inside = false; outside = false;        // J : 변수 초기화
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        bookPos = this.gameObject.transform.position;
        if (CalculateDistance() < radius)   // J : 책이 플레이어의 반경 내에 있는 경우
        {
            if (!inside)    // J : 반경 외에 있다가 내부에 들어온 경우
            {
                inside = true; outside = false;
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, fadeCount);    // J : 초기 알파값 적용
                this.gameObject.GetComponent<Renderer>().enabled = true;    // J : 책이 보이도록
                StopCoroutine("FadeOut");   // J : 페이드 아웃 중이었다면 중단
                StartCoroutine("FadeIn");   // J : 페이드 인 시작
            }
        }
        else   // J : 책이 플레이어의 반경 외에 있는 경우
        {
            if (!outside)   // J : 반경 내에 있다가 외부로 나간 경우
            {
                outside = true; inside = false;
                StopCoroutine("FadeIn");    // J : 페이드 인 중이었다면 중단
                StartCoroutine("FadeOut");  // J : 페이드 아웃 시작
            }
        }
    }

    // J : 플레이어와 책 사이의 거리 리턴
    double CalculateDistance()
    {
        double distance = Math.Sqrt(Math.Pow(playerPos.x - bookPos.x, 2) + Math.Pow(playerPos.y - bookPos.y, 2));
        return distance;
    }

    private IEnumerator FadeIn()
    {
        while (true)
        {
            if (fadeCount >= 1) // J : 알파값이 최대(1)가 될 때까지 반복
                break;

            fadeCount += 0.01f;
            yield return new WaitForSeconds(fadeInterval); // J : fadeInterval 초마다 선명해짐 -> fadeInterval*100초 후 완전히 보임
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, fadeCount);    // J : 알파값 조정
        }
    }

    public IEnumerator FadeOut()
    {
        while (fadeCount > 0)    // J : 알파값이 최소(0)가 될 때까지 반복
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(fadeInterval); // J : fadeInterval 초마다 흐릿해짐 -> fadeInterval*100초 후 완전히 안보임
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, fadeCount);    // J : 알파값 조정
        }
    }
}
