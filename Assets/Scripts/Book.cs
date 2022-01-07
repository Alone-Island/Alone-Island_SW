using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// J : Book Prefab Script
public class Book : MonoBehaviour
{
    private GameObject player;          // J : Dr.Kim(�÷��̾�) ������Ʈ
    private Vector2 playerPos, bookPos; // J : �÷��̾��� ��ġ, å(�ڽ�)�� ��ġ
    private int radius = 2;             // J : å�� ���̴� ����
    private float fadeCount = 0;        // J : �ʱ� ���İ�
    private bool inside;                // J : å�� �÷��̾��� �ݰ� ���� ����
    private bool outside;               // J : å�� �÷��̾��� �ݰ� �ܿ� ����

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Dr.Kim");     // J : �÷��̾� ������Ʈ
        inside = false; outside = false;        // J : ���� �ʱ�ȭ
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        bookPos = this.gameObject.transform.position;
        if (CalculateDistance() < radius)   // J : å�� �÷��̾��� �ݰ� ���� �ִ� ���
        {
            if (!inside)    // J : �ݰ� �ܿ� �ִٰ� ���ο� ���� ���
            {
                inside = true; outside = false;
                this.gameObject.GetComponent<Renderer>().enabled = true;    // J : å�� ���̵���
                StopCoroutine("FadeOut");   // J : ���̵� �ƿ� ���̾��ٸ� �ߴ�
                StartCoroutine("FadeIn");   // J : ���̵� �� ����
            }
        }
        else   // J : å�� �÷��̾��� �ݰ� �ܿ� �ִ� ���
        {
            if (!outside)   // J : �ݰ� ���� �ִٰ� �ܺη� ���� ���
            {
                outside = true; inside = false;
                StopCoroutine("FadeIn");    // J : ���̵� �� ���̾��ٸ� �ߴ�
                StartCoroutine("FadeOut");  // J : ���̵� �ƿ� ����
            }
        }
    }

    // J : �÷��̾�� å ������ �Ÿ� ����
    double CalculateDistance()
    {
        double distance = Math.Sqrt(Math.Pow(playerPos.x - bookPos.x, 2) + Math.Pow(playerPos.y - bookPos.y, 2));
        return distance;
    }

    private IEnumerator FadeIn()
    {
        while (true)
        {
            if (fadeCount >= 1) // J : ���İ��� �ִ�(1)�� �� ������ �ݺ�
                break;

            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f); // J : 0.01�ʸ��� ��������->1�� �� ������ ����
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, fadeCount);    // J : ���İ� ����
        }
    }

    public IEnumerator FadeOut()
    {
        while (fadeCount > 0)    // J : ���İ��� �ּ�(0)�� �� ������ �ݺ�
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f); // J : 0.01�ʸ��� �帴����->1�� �� ������ �Ⱥ���
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, fadeCount);    // J : ���İ� ����
        }
    }
}
