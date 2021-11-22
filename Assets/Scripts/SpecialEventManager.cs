using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // J : UI ���α׷����� ���� �߰� (Text ��)

public class SpecialEventManager : MonoBehaviour
{
    public TalkManager talkManager; // J : GameManager���� TalkManager�� �Լ��� ȣ���� �� �ֵ��� talkManager ���� ����
    public ScreenManager screenManager; // N : ���� ������ ���� ȣ��
    public GameObject talkPanel;    // J : ��ȭâ
    public Text talkText;           // J : ��ȭâ�� text
    public int talkIndex;           // J : talkIndex�� �����ϱ� ���� ����
    public bool AItalk = false;     // J : AI�� ����� �̺�Ʈ ��ȭ�� �ϴ��� ����
    public bool special = false;    // J : ����� �̺�Ʈ ���� �� ����
    public Text selectText0, selectText1, selectText2;
    public Button selectPanel0, selectPanel1, selectPanel2;

    List<Text> selectText;
    List<Button> selectPanel;
    int talkID;                     // J : TalkManager�κ��� talkData�� �������� ���� ����
    int firstRandomNum;             // J : ���� ����� �̺�Ʈ�� ���� ����1 (0 : ������ 2��, 1: ������ 3��)
    int secondRandomNum;            // J : ���� ����� �̺�Ʈ�� ���� ����2

    // J : Special Event �߻�
    public void Action() 
    {
        AItalk = true;  // J : JumpŰ�� ������ �� object scan�� �� �� ���� ��

        System.Random rand = new System.Random();
        firstRandomNum = rand.Next(2);      // J : 0-1������ ���� ���� (0 : ������ 2��, 1: ������ 3��)
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
            talkIndex = 0;      // J : talk index �ʱ�ȭ
            talkPanel.SetActive(false);  //������ �Լ� ���� ������ �ӽ� �ڵ�
            Select();
            return;
        }
        talkText.text = talkData;       // J : talkPanel�� text�� talkData�� ����
        talkIndex++;                    // J : �ش� talkID�� ���� talkData string�� �������� ����
    }

    void Select()
    {
        string selectData;        
        for (int selectIndex = 0; (selectData = talkManager.GetSelectData(talkID, selectIndex)) != null; selectIndex++)
        {
            selectText[selectIndex].text = selectData;
            selectPanel[selectIndex].gameObject.SetActive(true);
        }
    }

    public void Select1()
    {
        switch (firstRandomNum) 
        {
            case 0:
                switch (secondRandomNum)
                {
                    case 1:
                        // �Ϸ� �ٷ� ������
                        screenManager.HeartStudy(1); // N : ���� 1���� ���
                        break;
                    case 2:
                        // ������ ���
                        break;
                    case 3:
                        // ���� �ɷ� 1���� ���
                        break;
                    case 4:
                        // ����� ���
                        break;

                }
                break;
            case 1:
                switch (secondRandomNum)
                {
                    case 1:
                        // AI�� �������� ���ؼ� ���
                        break;
                    case 2:
                        // ������ ���
                        break;
                    case 3:
                        // �Ϸ� �ٷ� ������
                        screenManager.HeartStudy(2); // N : ���� 2���� ���

                        break;
                    case 4:
                        // ���߿� �߰�
                        break;
                }
                break;
        }
        SelectComplete();
    }

    public void Select2()
    {
        switch (firstRandomNum)
        {
            case 0:
                // ���� �ɷ� 1���� �϶�
                break;
            case 1:
                switch (secondRandomNum)
                {
                    case 1:
                        // ��ȭ����
                        break;
                    case 2:
                        // ���� �ɷ� 1���� �϶�
                        break;
                    case 3:
                        // ��ȭ����
                        break;
                    case 4:
                        // ���߿� �߰�
                        break;
                }
                break;
        }
        SelectComplete();
    }

    public void Select3()
    {
        switch (secondRandomNum)
        {
            case 1:
                // ���� �ɷ� 1���� �϶�
                break;
            case 2:
                // ���� ����
                break;
            case 3:
                // �˰��� �ɰ��� �ջ��� ���� AI ���� -> ���
                break;
            case 4:
                // ���߿� �߰�
                break;
        }
        SelectComplete();
    }

    void SelectComplete()
    {
        for (int i = 0; i < 3; i++)
            selectPanel[i].gameObject.SetActive(false);
        talkPanel.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        selectText = new List<Text>();
        selectText.Add(selectText0);
        selectText.Add(selectText1);
        selectText.Add(selectText2);

        selectPanel = new List<Button>();
        selectPanel.Add(selectPanel0);
        selectPanel.Add(selectPanel1);
        selectPanel.Add(selectPanel2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
