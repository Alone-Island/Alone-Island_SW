using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // J : UI ���α׷����� ���� �߰� (Text ��)

public class SpecialEventManager : MonoBehaviour
{
    public TalkManager talkManager; // J : GameManager���� TalkManager�� �Լ��� ȣ���� �� �ֵ��� talkManager ���� ����
    public GameObject talkPanel;    // J : ��ȭâ
    public Text talkText;           // J : ��ȭâ�� text
    public int talkIndex;           // J : talkIndex�� �����ϱ� ���� ����
    public bool AItalk = false;     // J : AI�� ����� �̺�Ʈ ��ȭ�� �ϴ��� ����
    public bool special = false;    // J : ����� �̺�Ʈ ���� �� ����

    int talkID;                     // J : TalkManager�κ��� talkData�� �������� ���� ����
    int firstRandomNum;             // J : ���� ����� �̺�Ʈ�� ���� ����1 (0 : ������ 2��, 1: ������ 3��)
    int secondRandomNum;            // J : ���� ����� �̺�Ʈ�� ���� ����2

    // J : Special Event �߻�
    public void Action() 
    {
        AItalk = true;  // J : JumpŰ�� ������ �� object scan�� �� �� ���� ��

        System.Random rand = new System.Random();
        firstRandomNum = rand.Next(1);      // J : 0-1������ ���� ���� (0 : ������ 2��, 1: ������ 3��)
        secondRandomNum = rand.Next(1, 5);  // J : 1-4������ ���� ����

        talkID = 10000 + 10 * firstRandomNum + secondRandomNum; // J : talkData�� ���� ���� ���� talkID ���

        talkPanel.SetActive(true);  // J : ��ȭâ Ȱ��ȭ
        Talk();                     // J : ��ȭ ����
    }

    // J : ����� ������ ���� �������� �Ѿ
    public void Talk() 
    {
        string talkData = talkManager.GetTalkData(talkID, talkIndex);   // J : TalkManager�κ��� talkData�� ��������
        if (talkData == null)   // J : �ش� talkID�� talkData�� ��� �����Դٸ�
        {
            AItalk = false;     // J : JumpŰ�� ������ �� object scan�� �� �� �ְ� ��
            talkPanel.SetActive(false);  //������ �Լ� ���� ������ �ӽ� �ڵ�
            //������ �Լ� ȣ��
        }
        talkText.text = talkData;       // J : talkPanel�� text�� talkData�� ����
        talkIndex++;                    // J : �ش� talkID�� ���� talkData string�� �������� ����
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
