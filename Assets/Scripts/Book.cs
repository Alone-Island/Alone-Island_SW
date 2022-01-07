using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// J : Book Prefab Script
public class Book : MonoBehaviour
{
    private GameObject player;          // J : Dr.Kim(�÷��̾�) ������Ʈ
    private Vector2 playerPos, bookPos; // J : �÷��̾��� ��ġ, å(�ڽ�)�� ��ġ
    private int radius = 2;             //  J : å�� ���̴� ����

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
        if (CalculateDistance() < radius)   // J : å�� �÷��̾��� �ݰ� ���� �ִ� ���
        {
            this.gameObject.GetComponent<Renderer>().enabled = true;    // J : å�� ����
        }
        else   // J : å�� �÷��̾��� �ݰ� �ܿ� �ִ� ���
        {
            this.gameObject.GetComponent<Renderer>().enabled = false;   // J : å�� ������ ����
        }
    }

    // J : �÷��̾�� å ������ �Ÿ� ����
    double CalculateDistance()
    {
        double distance = Math.Sqrt(Math.Pow(playerPos.x - bookPos.x, 2) + Math.Pow(playerPos.y - bookPos.y, 2));
        return distance;
    }
}
