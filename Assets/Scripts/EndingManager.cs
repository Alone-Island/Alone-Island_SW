using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public GameObject panel; // N : ȭ�� ��Ӱ�
    public GameObject badHungry; // N : ��� ���� (�����) ī��

    // N : ����� ������ 0�� ���
    public void failHungry()
    {
        Debug.Log("Hungry,,,");
        panel.SetActive(true);
        badHungry.SetActive(true);
    }

    // N : �ູ ������ 0�� ���
    public void failLonely()
    {

    }

    // N : ü�� ������ 0�� ���
    public void failCold()
    {

    }
}
