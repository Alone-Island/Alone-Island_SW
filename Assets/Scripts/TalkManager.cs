using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// C : ��ȭ �����͸� ���� �� �����ϴ� ��ũ��Ʈ
public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;       // C : ��ȭ �����͸� �����ϴ� dictionary ����
    Dictionary<int, string[]> selectData;     // J : ������ �����͸� �����ϴ� dictionary ����

    void Awake()
    {
        // C : dictionary instance ����
        talkData = new Dictionary<int, string[]>();
        selectData = new Dictionary<int, string[]>();
        GenerateData();
    }

    // C : talkData ����
    void GenerateData()
    {
        // C : key�� 1000~1009�̸� AI���� dialogue data (Dr.Kim�� AI���� ��ȭ�� �� ��), �� ���� �н�
        talkData.Add(1001, new string[] { "�� ������ �����ϱ��~?" });
        talkData.Add(1002, new string[] { "�ڻ���� MBTI�� ������?", "���� ISTP����!" });
        talkData.Add(1003, new string[] { "ģ���� �� �־����� ���ھ��..", "���� �ڻ���� �ְ��� ģ�����ϴ�." });
        talkData.Add(1004, new string[] { "�ڻ���� ��ô� ���� ���� ���� �� �ñ��ؿ�! ", "���� ���� �� �԰ŵ��." });
        talkData.Add(1005, new string[] { "������ ���� �¾翭 ����������.", "������ �¾���� �� �������?", "������ �ʹ� ���� ���̿���Ф�" });
        talkData.Add(1006, new string[] { "���� ��� ��������ɱ��?" });
        talkData.Add(1007, new string[] { "�����̶� �����ϱ��?", "���� �����ϱ��?" });
        talkData.Add(1008, new string[] { "AI�� �ʹ� �ȶ������� ��� �ɱ��?" });
        talkData.Add(1009, new string[] { "�ڻ���� �����亸�� �� �ȶ��ϰ� ���ִ� ����̿���!" });
        talkData.Add(1010, new string[] { "�����̶� �����ϱ��? ������ ���� ���� �ɱ��?" });

        // C : key�� 100~400�̸� �н��ϱ⿡ ���� text data (100 - ���, 200 - ����, 300 - ����, 400 - ����)
        talkData.Add(100, new string[] { "��縦 �н��Ͻðڽ��ϱ�?", "��縦 �н��մϴ�." });
        talkData.Add(200, new string[] { "������ �н��Ͻðڽ��ϱ�?", "������ �н��մϴ�." });
        talkData.Add(300, new string[] { "������ �н��Ͻðڽ��ϱ�?", "������ �н��մϴ�." });
        talkData.Add(400, new string[] { "������ �н��Ͻðڽ��ϱ�?", "������ �н��մϴ�." });

        // J : key�� 10001~10013�̸� ����� �̺�Ʈ�� ���� text data (10001~10004 : ������ 2��, 10011~10013 : ������ 3��)
        talkData.Add(10001, new string[] { "���͸��� ���� ��Ҿ��Ф�", "�Ϸ縸 �ƹ��͵� ���ϰ� �;��.." });
        talkData.Add(10002, new string[] { "�ڻ���� ���� ���ο� ���Ÿ� ���Ծ��!" });
        talkData.Add(10003, new string[] { "���� �߻������� �ִ� �� ���ƿ�!", "��Ƽ� �����������?" });
        talkData.Add(10004, new string[] { "���� �߻������� �ִ� �� ���ƿ�!", "��Ƽ� �����������?" });
        talkData.Add(10011, new string[] { "�� �� �ʹ� �̻��� �ʾƿ�??" });
        talkData.Add(10012, new string[] { "(AI�� ���� ������)" });
        talkData.Add(10013, new string[] { "(������ �������� AI�� ���ƴ�. ��� �ұ�?)" });
        talkData.Add(10014, new string[] { "*���� �߰� ����*" });

        // J : key�� 10001~10013�̸� ����� �̺�Ʈ�� ���� ������ data
        selectData.Add(10001, new string[] { "�׷� �Ϸ� ����", "�ȵžȵ� ���� �ٷ� �� �ؾߵ�!" });
        selectData.Add(10002, new string[] { "����~ �Ծ��!", "������ ó������ ���Ŵ� ������! " });
        selectData.Add(10003, new string[] { "�󸶸��� ����! ���� ����!", "�߻������� �ʹ� ������. ��������" });
        selectData.Add(10004, new string[] { "�󸶸��� ����! ���� ����!", "�߻������� �ʹ� ������. ��������" });
        selectData.Add(10011, new string[] { "�ɺ��� �ʰ� �� ����", "���ڳ�~", "����..." });
        selectData.Add(10012, new string[] { "�ٷ� ���� ������!", "�˾Ƽ� ���ðž�", "�ʹ� �־�! ���������� ã�Ƽ� ���ؾ߰ڴ�!" });
        selectData.Add(10013, new string[] { "���峭 ���� �ִ��� �Ϸ絿�� ������ ���캸��", "���� �������� Ȯ������", "������ ġ���ְ� �ٽ� �۾��� ��������" });
        selectData.Add(10014, new string[] { "������1", "������2", "������3" });
    }

    // C : �ʿ��� TalkData�� return
    public string GetTalkData(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)       // C : talkIndex�� talkData[id]�� ������ index + 1�̸�
            return null;

        return talkData[id][talkIndex];     // C : �ʿ��� ������ id�� index�� ���� return
    }

    // J : �ʿ��� SelectData�� return
    public string GetSelectData(int id, int selectIndex)
    {
        if (selectIndex == selectData[id].Length)       // J : selectIndex�� selectData[id]�� ������ index + 1�̸�
            return null;

        return selectData[id][selectIndex];     // J : �ʿ��� ������ id�� index�� ���� return
    }
}
