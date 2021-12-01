using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // J : UI ���α׷����� ���� �߰� (Text ��)
using TMPro;            // J : TextMeshProUGUI�� ���� �߰�

public class SpecialEventManager : MonoBehaviour
{
    public TalkManager talkManager;     // J : GameManager���� TalkManager�� �Լ��� ȣ���� �� �ֵ��� talkManager ���� ����
    public ScreenManager screenManager; // N : ���� ������ ���� ȣ��
    public EndingManager endingManager; // N : ���� ó���� ���� ȣ��
    public GameObject talkPanel;        // J : ��ȭâ
    public TextMeshProUGUI talkText;    // J : ��ȭâ�� text
    public int talkIndex;               // J : talkIndex�� �����ϱ� ���� ����
    public bool special = false;        // J : ����� �̺�Ʈ ���������� ����
    public bool AItalk = false;         // J : AI�� ����� �̺�Ʈ ��ȭ�� �ϴ��� ���� (������ ���� ��)
    public bool result = false;         // J : ��� �ؽ�Ʈ â�� �����ִ��� ���� (������ ���� ��)
    public TextMeshProUGUI selectText0, selectText1, selectText2;
    public Button selectButton0, selectButton1, selectButton2;

    List<TextMeshProUGUI> selectText;   // J : ������ �ؽ�Ʈ�� �����ϱ� ���� ����Ʈ
    List<Button> selectButton;          // J : ������ ��ư�� �����ϱ� ���� ����Ʈ
    int specialID;                      // J : TalkManager�κ��� talkData�� �������� ���� ����
    int firstRandomNum;                 // J : ���� ����� �̺�Ʈ�� ���� ����1 (0 : ������ 2��, 1: ������ 3��)
    int secondRandomNum;                // J : ���� ����� �̺�Ʈ�� ���� ����2
    int select;

    // J : Special Event �߻�
    public void Action() 
    {
        AItalk = true;  // J : JumpŰ�� ������ �� object scan�� �� �� ���� ��
        special = true;

        System.Random rand = new System.Random();
        
        int danger = (int)((10 - screenManager.houseLv.fCurrValue));   // J : ���赵 ���
        if (rand.Next(10000) < danger)    // J : ���赵�� ���� �糭 �߻�
            Disaster();
        else 
        {
            firstRandomNum = rand.Next(2);      // J : 0-1������ ���� ���� (0 : ������ 2��, 1: ������ 3��)
            secondRandomNum = rand.Next(1, 5);  // J : 1-4������ ���� ����

            specialID = 10000 + 10 * firstRandomNum + secondRandomNum; // J : talkData�� ���� ���� ���� talkID ���

            talkPanel.SetActive(true);  // J : ��ȭâ Ȱ��ȭ
            Talk();                     // J : ��ȭ ����
        }
    }

    private void Disaster()
    {
        switch ((new System.Random()).Next(2))  // J : �� �糭�� 50% Ȯ���� �߻�
        {
            case 0: // J : ������
                endingManager.suddenEnding(5);
                break;
            case 1: // J : � �浹
                endingManager.suddenEnding(6);
                break;
        }
    }

    // J : ����� ������ ���� �������� �Ѿ
    public void Talk() 
    {
        string talkData = talkManager.GetTalkData(specialID, talkIndex);   // J : TalkManager�κ��� talkData�� ��������
        if (talkData == null)   // J : �ش� talkID�� talkData�� ��� �����Դٸ�
        {
            AItalk = false;     // J : JumpŰ�� ������ �� object scan�� �� �� �ְ� ��
            talkIndex = 0;      // J : talk index �ʱ�ȭ
            Select();           // J : ������ ȭ�鿡 ����
            return;
        }
        talkText.text = talkData;       // J : talkPanel�� text�� talkData�� ����
        talkIndex++;                    // J : �ش� talkID�� ���� talkData string�� �������� ����
    }

    // J : ������ Ŭ�� �� ȣ��, ����� ������ ���� �������� �Ѿ
    public void ResultTalk()
    {
        result = true;  // J : ��� �ؽ�Ʈ�� �����ִ� ����
        string talkData = talkManager.GetResultData(specialID * 10 + select, talkIndex);   // J : TalkManager�κ��� resultData�� ��������
        if (talkData == null)   // J : �ش� talkID�� resultData�� ��� �����Դٸ�
        {
            result = false;     // J : ��� �ؽ�Ʈ ����
            special = false;    // J : ����� �̺�Ʈ ����
            talkIndex = 0;      // J : talk index �ʱ�ȭ
            Result();           // J : ��� �ݿ�
            return;
        }
        talkText.text = talkData;       // J : talkPanel�� text�� resultData�� ����
        talkIndex++;                    // J : �ش� talkID�� ���� resultData string�� �������� ����
    }

    // J : �������� ȭ�鿡 ��Ÿ��
    void Select()
    {
        string selectData;        
        for (int selectIndex = 0; (selectData = talkManager.GetSelectData(specialID, selectIndex)) != null; selectIndex++) // J : selectData�� ������ ���� selectButton�� ����
        {
            selectText[selectIndex].text = selectData;              // J : SelectButton�� text�� selectData����
            selectButton[selectIndex].gameObject.SetActive(true);   // J : SelectButton Ȱ��ȭ
        }
    }

    // J : ��� �ؽ�Ʈ�� ��� ������ �� ȣ���, ��� �ݿ�
    void Result()
    {
        switch (select) // J : ���° �������� Ŭ���ߴ���
        {
            case 0:
                switch (firstRandomNum)
                {
                    case 0:     // J : �������� 2���� ���
                        switch (secondRandomNum)
                        {
                            case 1: // J : ���͸��� ���� ��Ҿ��Ф� "�Ϸ縸 �ƹ��͵� ���ϰ� �;��..
                                screenManager.dayTime = 20;
                                screenManager.HeartStudy(1); // N : ���� 1���� ���
                                break;
                            case 2: // J : �ڻ���� ���� ���ο� ���Ÿ� ���Ծ��!
                                endingManager.suddenEnding(1); // N : Bad Ending (������)
                                break;
                            case 3: // J : ���� �߻������� �ִ� �� ���ƿ�! ��Ƽ� �����������?
                                screenManager.HeartStudy(1); // J : ���� 1���� ���
                                break;
                            case 4: // J : ���� �߻������� �ִ� �� ���ƿ�! ��Ƽ� �����������?
                                endingManager.suddenEnding(4);  // J : ����� ���
                                break;

                        }
                        break;
                    case 1:     // J : �������� 3���� ���
                        switch (secondRandomNum)
                        {
                            case 1: // J : �� �� �ʹ� �̻��� �ʾƿ�??
                                endingManager.suddenEnding(2); // N : Bad Ending (AI�� �������� ����)
                                break;
                            case 2: // J : (AI�� ���� ������)
                                endingManager.suddenEnding(3);  // ������ ���
                                break;
                            case 3: // J : (������ �������� AI�� ���ƴ�. ��� �ұ�?)
                                screenManager.dayTime = 20;
                                screenManager.HeartStudy(2); // N : ���� 2���� ���
                                break;
                            case 4: // J : *���� �߰� ����*
                                    // ���߿� �߰�
                                break;
                        }
                        break;
                }
                break;
            case 1:
                switch (firstRandomNum)
                {
                    case 0:     // J : �������� 2���� ���
                        screenManager.HeartStudy(-1); // J : ���� 1���� �϶�
                        break;
                    case 1:     // J : �������� 3���� ���
                        switch (secondRandomNum)
                        {
                            case 1: // J : �� �� �ʹ� �̻��� �ʾƿ�??
                                    // J : ��ȭ����
                                break;
                            case 2: // J : (AI�� ���� ������)
                                screenManager.HeartStudy(-1); // J : ���� 1���� �϶�
                                break;
                            case 3: // J : (������ �������� AI�� ���ƴ�. ��� �ұ�?)
                                    // J : ��ȭ����
                                break;
                            case 4: // J : *���� �߰� ����*
                                    // ���߿� �߰�
                                break;
                        }
                        break;
                }
                break;
            case 2:
                switch (secondRandomNum)
                {
                    case 1: // J : �� �� �ʹ� �̻��� �ʾƿ�??
                        screenManager.HeartStudy(-1); // J : ���� 1���� �϶�
                        break;
                    case 2: // J : (AI�� ���� ������)
                            // J : ���� ����
                        break;
                    case 3: // J : (������ �������� AI�� ���ƴ�. ��� �ұ�?)
                        endingManager.suddenEnding(3); // N : Bad Ending (AI ����)
                        break;
                    case 4: // J : *���� �߰� ����*
                            // ���߿� �߰�
                        break;
                }
                break;
        }
        talkPanel.SetActive(false);     // J : ����� �̺�Ʈ ��ȭâ ��Ȱ��ȭ
    }


    // J : SelectButton0�� Ŭ������ �� ȣ��Ǵ� �Լ�
    public void Select0()
    {
        select = 0;
        ResultTalk();
        SelectComplete();   // J :������ �Ϸ�Ǹ� ������ ��Ȱ��ȭ
    }

    // J : SelectButton1�� Ŭ������ �� ȣ��Ǵ� �Լ�
    public void Select1()
    {
        select = 1;
        ResultTalk();
        SelectComplete();   // J :������ �Ϸ�Ǹ� ������ ��Ȱ��ȭ
    }

    // J : SelectButton2�� Ŭ������ �� ȣ��Ǵ� �Լ�
    public void Select2()
    {
        select = 2;
        ResultTalk();
        SelectComplete();   // J :������ �Ϸ�Ǹ� ������ ��Ȱ��ȭ
    }

    // J :������ �Ϸ�Ǹ� ȣ��, ��ȭâ�� ������ ��Ȱ��ȭ
    void SelectComplete()
    {
        for (int i = 0; i < 3; i++)
            selectButton[i].gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        selectText = new List<TextMeshProUGUI>();
        selectText.Add(selectText0);
        selectText.Add(selectText1);
        selectText.Add(selectText2);

        selectButton = new List<Button>();
        selectButton.Add(selectButton0);
        selectButton.Add(selectButton1);
        selectButton.Add(selectButton2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
