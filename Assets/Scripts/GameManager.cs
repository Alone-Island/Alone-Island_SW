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

    // C : �÷��̾ Object�� ���� ���� ��(�÷��̾��� �׼� �߻� ��) ������ ������ ������ ��ȭâ ����ֱ�
    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;               // C : parameter�� ���� ��ĵ�� game object�� public ������ scanObject�� ����
        ObjectData objData = scanObject.GetComponent<ObjectData>();     // C : scanObject�� ObjectData instance ��������
        Talk(objData.id);                   // C : �ʿ��� talkPanel text �� ��������

        talkPanel.SetActive(isTPShow);      // C : talkPanel ����ų� �����ֱ�
    }

    // C : ��Ȳ�� ���� �����ϰ� �ʿ��� talkPanel text �� ��ȭâ�� ����
    void Talk(int id)
    {
        string talkData = talkManager.GetTalkData(id, talkIndex);     // C : ������ object�� �ش��ϴ� talkData �� talkIndex ��ġ�� string�� ��������

        if (talkData == null)           // C : �ش��ϴ� id�� talkData string���� ��� �����Դٸ�
        {
            isTPShow = false;           // C : talkPanel�� show ���� false�� ����
            talkIndex = 0;              // C : ���� Talk()�Լ� ����� ���� talkIndex�� 0���� �ʱ�ȭ
            return;
        }
        
        talkText.text = talkData;       // C : talkPanel�� text�� talkData�� ����
        isTPShow = true;                // C : talkPanel�� show ���� true�� ���� (�ش��ϴ� id�� talkData string�� ���� ��������)
        talkIndex++;                    // C : �ش��ϴ� id�� ���� talkData string�� �������� ����
    }
}
