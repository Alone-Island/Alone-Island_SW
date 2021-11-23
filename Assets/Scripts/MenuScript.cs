using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject gameRule;

    // J : �����ϱ� ��ư onclick
    public void SelectStart()
    {
        Debug.Log("�����ϱ�");
        SceneManager.LoadScene("Synopsis"); // J : Synopsis scene���� �̵�
    }

    // J : �ҷ����� ��ư onclick
    public void SelectBring()
    {
        Debug.Log("�ҷ�����");
    }

    // J : ����ī�� ��ư onclick
    public void SelectCard()
    {
        Debug.Log("����ī��");
    }

    // J : ���ӹ�� ��ư onclick
    public void SelectRule()
    {
        Debug.Log("���ӹ��");
        gameRule.SetActive(true);   // J : ���ӹ��â Ȱ��ȭ
    }

    // J : ���ӹ�� ������ ��ư onclick
    public void SelectRuleQuit()
    {
        Debug.Log("���ӹ�� ������");
        gameRule.SetActive(false);  // J : ���ӹ��â ��Ȱ��ȭ
    }
}
