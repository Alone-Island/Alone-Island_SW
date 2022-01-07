using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// J : Book Prefab Script
public class Book : MonoBehaviour
{
    private GameObject player;          // J : Dr.Kim(플레이어) 오브젝트
    private Vector2 playerPos, bookPos; // J : 플레이어의 위치, 책(자신)의 위치
    private int radius = 2;             //  J : 책이 보이는 범위

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Dr.Kim");     // J : 
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        bookPos = this.gameObject.transform.position;
        if (CalculateDistance() < radius)   // J : 책이 플레이어의 반경 내에 있는 경우
        {
            this.gameObject.GetComponent<Renderer>().enabled = true;    // J : 책이 보임
        }
        else   // J : 책이 플레이어의 반경 외에 있는 경우
        {
            this.gameObject.GetComponent<Renderer>().enabled = false;   // J : 책이 보이지 않음
        }
    }

    // J : 플레이어와 책 사이의 거리 리턴
    double CalculateDistance()
    {
        double distance = Math.Sqrt(Math.Pow(playerPos.x - bookPos.x, 2) + Math.Pow(playerPos.y - bookPos.y, 2));
        return distance;
    }
}
