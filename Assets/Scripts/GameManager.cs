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
    public TalkManager talkManager; // C : GameManager���� TalkManager�� �Լ��� ȣ���� �� �ֵ��� talkManager ���� ����
    public int talkIndex;           // C : �ʿ��� talkIndex�� �����ϱ� ���� ���� ����
    public int day = 20;            // J : �Ϸ�� 20��

    public SpecialEventManager specialManager; // J : GameManager���� SpecialEventManager�� �Լ��� ȣ���� �� �ֵ��� talkManager ���� ����

    bool playerTalk = false;            // J : �÷��̾ ��ȭ�ϴ� �߿��� special event�� �����ϵ��� ���� ����
    int randomNum = 0;                  // C : AI���� ��ȭ ��, ������ ��ȭ ������ ����ϱ� ���� ���� ����

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
        StartCoroutine("SpecialEvent", 15 * day); // J : SpecialEvent �Լ� ȣ��
    }

    private void Start()
    {
        StartCoroutine("SpecialEvent", 15 * day); // J : SpecialEvent �Լ� ȣ��
    }

    

    // C : �÷��̾ Object�� ���� ���� ��(�÷��̾��� �׼� �߻� ��) ������ ������ ������ ��ȭâ ����ֱ�
    public void Action(GameObject scanObj)
    {
        playerTalk = true;                  // J : �÷��̾ ��ȭ�ϴ� �߿��� special event�� �����ϵ��� ����
        scanObject = scanObj;               // C : parameter�� ���� ��ĵ�� game object�� public ������ scanObject�� ����
        ObjectData objData = scanObject.GetComponent<ObjectData>();     // C : scanObject�� ObjectData instance ��������

        if (objData.id == 1000 && randomNum == 0)      // C : objData�� AI�̰�, ��ȭ ù �����̸�
        {
            System.Random rand = new System.Random();
            randomNum = rand.Next(1, 11);                  // C : 1~10������ ������ ����
        }

        Talk(objData.id);                   // C : �ʿ��� talkPanel text �� ��������

        talkPanel.SetActive(isTPShow);      // C : talkPanel ����ų� �����ֱ�
    }

    // C : ��Ȳ�� ���� �����ϰ� �ʿ��� talkPanel text �� ��ȭâ�� ����
    void Talk(int id)
    {
        // C : ������ object�� �ش��ϴ� talkData �� talkIndex ��ġ�� string�� ��������
        string talkData = talkManager.GetTalkData(id + randomNum, talkIndex);

        if (talkData == null)           // C : �ش��ϴ� id�� talkData string���� ��� �����Դٸ�
        {
            playerTalk = false;         // J : ���������� special event�� �ߵ��ϵ��� ����
            isTPShow = false;           // C : talkPanel�� show ���� false�� ����
            talkIndex = 0;              // C : ���� Talk()�Լ� ����� ���� talkIndex�� 0���� �ʱ�ȭ
            randomNum = 0;              // C : ���� Talk()�Լ� ����� ���� randomNum�� 0���� �ʱ�ȭ
            return;
        }
        
        talkText.text = talkData;       // C : talkPanel�� text�� talkData�� ����
        isTPShow = true;                // C : talkPanel�� show ���� true�� ���� (�ش��ϴ� id�� talkData string�� ���� ��������)
        talkIndex++;                    // C : �ش��ϴ� id�� ���� talkData string�� �������� ����
    }
}
