using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    private Image panel;

    private void Awake()
    {
        panel = this.GetComponent<Image>(); // J : panel�� �̹��� component ��������
    }
    private void Start()
    {
        StartCoroutine("FadeIn");   // J : ���� ���� �� ���̵���
    }

    public IEnumerator FadeIn()
    {
        float fadeCount = 1;    // J : �ʱ� ���İ�(���� ȭ��)
        while (fadeCount > 0)    // J : ���İ��� �ִ�(1)�� �� ������ �ݺ�
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f); // J : 0.01�ʸ��� �������->1�� �� ������ �����
            panel.color = new Color(0, 0, 0, fadeCount);    // J : ���İ� ����
        }
    }
}
