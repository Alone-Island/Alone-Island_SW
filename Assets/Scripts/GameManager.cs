using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // C : UI ���α׷����� ���� �߰� (Text ��)

// C : ��ü���� ���� ���� �� ������ �����ִ� ��ũ��Ʈ
public class GameManager : MonoBehaviour
{
    public Text talkText;           // C : ��ȭâ�� text
    public GameObject scanObject;   // C : ��ĵ��(������) game object
    public GameObject talkPanel;    // C : ��ȭâ
    public bool isTPShow;           // C : talkPanel�� ���� ���� (�����ֱ� or �����)

    // C : �÷��̾ Object�� ���� ���� ��(�÷��̾��� �׼� �߻� ��) ������ ������ ������ ��ȭâ ����ֱ�
    public void Action(GameObject scanObj)
    {
        if (isTPShow)               // C : talkPanel�� �̹� �������� ������
        {
            isTPShow = false;       // C : talkPanel�� show ���� false�� ����
        }
        else                        // C : talkPanel�� ������ ������
        {
            isTPShow = true;
            scanObject = scanObj;           // C : parameter�� ���� ��ĵ�� game object�� public ������ scanObject�� ����
            talkText.text = "This is :" + scanObject.name + ".";      // C : ��ȭâ�� text�� scanObject�� �̸��� ������ ������ �������� ����
        }

        talkPanel.SetActive(isTPShow);      // C : talkPanel ����ų� �����ֱ�
    }
}
