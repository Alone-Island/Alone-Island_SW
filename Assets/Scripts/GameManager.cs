using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // C : UI ���α׷����� ���� �߰� (Text ��)
using TMPro;            // J : TextMeshProUGUI�� ���� �߰�

// C : ��ü���� ���� ���� �� ������ �����ִ� ��ũ��Ʈ
public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI talkText;           // C : ��ȭâ�� text
    public GameObject scanObject;   // C : ��ĵ��(������) game object
    public GameObject talkPanel;    // C : ��ȭâ
    public bool isTPShow;           // C : talkPanel�� ���� ���� (�����ֱ� or �����)
    public TalkManager talkManager; // C : GameManager���� TalkManager�� �Լ��� ȣ���� �� �ֵ��� talkManager ���� ����
    public int talkIndex;           // C : �ʿ��� talkIndex�� �����ϱ� ���� ���� ����
    public int day = 20;            // J : �Ϸ�� 20��

    public SpecialEventManager specialManager; // J : GameManager���� SpecialEventManager�� �Լ��� ȣ���� �� �ֵ��� talkManager ���� ����
    public LearningManager learningManager;     // C :
    public ScreenManager screenManager; // N : å ���� �������� ����

    bool playerTalk = false;            // J : �÷��̾ ��ȭ�ϴ� �߿��� special event�� �����ϵ��� ���� ����
    public bool isEndingShow = false;         // N : ���� ���� (���� ī�� ��Ÿ�� ���ĺ���)
    public bool isTheEnd = false;         // N : ���� ���� ���� (���� ī�� ��Ÿ���� 2�� �ں���)
    int randomNum = 0;                  // C : AI���� ��ȭ ��, ������ ��ȭ ������ ����ϱ� ���� ���� ����
    public int dayTalk = 0;        // N : AI���� ��ȭ Ƚ��

    public TextMeshProUGUI alertText;           // N : �˸�â�� text

    // J :IEnumerator Ÿ��(WaitForSeconds)�� ��ȯ�ϴ� �Լ�
    private IEnumerator SpecialEvent(float delayTime)
    {
        yield return new WaitForSeconds(delayTime); // J : delayTime�� ��ٸ� �� �簳
        // J : �÷��̾��� ��ȭ�� ���� ������ ���
        while (true) {
            if (!playerTalk) {              // J : player�� ��ȭ ���� �ƴϸ�
                specialManager.Action();    // J : special event �ߵ�
                break;
            }
            yield return null;
        }
        StartCoroutine("SpecialEvent", day*3); // J : SpecialEvent �Լ� ȣ��
    }

    private void Start()
    {
        StartCoroutine("SpecialEvent", day*3); // J : SpecialEvent �Լ� ȣ��
    }

    

    // C : �÷��̾ Object�� ���� ���� ��(�÷��̾��� �׼� �߻� ��) ������ ������ ������ ��ȭâ ����ֱ�
    public void Action(GameObject scanObj)
    {
        playerTalk = true;                  // J : �÷��̾ ��ȭ�ϴ� �߿��� special event�� �����ϵ��� ����
        scanObject = scanObj;               // C : parameter�� ���� ��ĵ�� game object�� public ������ scanObject�� ����
        ObjectData objData = scanObject.GetComponent<ObjectData>();     // C : scanObject�� ObjectData instance ��������
        int talkId = objData.id;            // K : takl data�� id ���� ����, ����ó���� ���� �߰� ������

        if (objData.id == 1000 && randomNum == 0)      // C : objData�� AI�̰�, ��ȭ ù �����̸�
        {
            // N : �Ϸ翡 �ѹ� �̻� ��ȭ �õ�, J : or ���� ��� �˸�â�� ���� Ȱ��ȭ ������ ���
            if (dayTalk > 0)
            {
                talkId = 2000;
            }
            else
            {
                System.Random rand = new System.Random();
                randomNum = rand.Next(1, 11);                  // C : 1~10������ ������ ����
            }
        }

        if (objData.id >= 100 && objData.id <= 400)
        {
            if (learningManager.isAILearning) // K : �н��ϱ� ���縦 ������, AI �н����� ��� ����ó��
            {
                talkId = 500;
            } else if (screenManager.currBookNum() < 1) // K : �н��ϱ� ���縦 ������, å�� ���� ��� ����ó��
            {
                talkId = 600;
            }
        }
        Talk(talkId);                   // C : �ʿ��� talkPanel text �� ��������, K : ����ó���� ���� objData.id > talkId�� ����

        if (talkId == 1000) talkPanel.SetActive(isTPShow);      // C : talkPanel ����ų� �����ֱ�
        else
        {
            GameObject.Find("Alert").transform.Find("Alert Set").gameObject.SetActive(isTPShow); // N : �˸�â ����ֱ�
        }
    }

    // C : ��Ȳ�� ���� �����ϰ� �ʿ��� talkPanel text �� ��ȭâ�� ����
    void Talk(int id)
    {
        // C : ������ object�� �ش��ϴ� talkData �� talkIndex ��ġ�� string�� ��������
        string talkData = talkManager.GetTalkData(id + randomNum, talkIndex);

        if (talkData == null)           // C : �ش��ϴ� id�� talkData string���� ��� �����Դٸ�
        {
            if (id >= 100 && id <= 400)     // C :
            {
                learningManager.Learning(id);
            }
            else if (id == 1000) screenManager.HeartStudy(0);

            playerTalk = false;         // J : ���������� special event�� �ߵ��ϵ��� ����
            isTPShow = false;           // C : talkPanel�� show ���� false�� ����
            talkIndex = 0;              // C : ���� Talk()�Լ� ����� ���� talkIndex�� 0���� �ʱ�ȭ
            randomNum = 0;              // C : ���� Talk()�Լ� ����� ���� randomNum�� 0���� �ʱ�ȭ
            if (id == 1000) dayTalk++;  // N : �Ϸ� ��ȭ Ƚ�� ����
            return;
        }
        if (id == 1000) talkText.text = talkData;       // C : talkPanel�� text�� talkData�� ����
        else alertText.text = talkData;                 // N : �˸�â�� text�� talkData�� ����

        isTPShow = true;                // C : talkPanel�� show ���� true�� ���� (�ش��ϴ� id�� talkData string�� ���� ��������)
        talkIndex++;                    // C : �ش��ϴ� id�� ���� talkData string�� �������� ����
    }
}
