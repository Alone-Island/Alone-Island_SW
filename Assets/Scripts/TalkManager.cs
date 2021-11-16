using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// C : ��ȭ �����͸� ���� �� �����ϴ� ��ũ��Ʈ
public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;       // C : ��ȭ �����͸� �����ϴ� dictionary ����

    void Awake()
    {
        // C : dictionary instance ����
        talkData = new Dictionary<int, string[]>();
        GenerateTalkData();
    }

    // C : talkData ����
    void GenerateTalkData()
    {
        // C : key�� 1000~1009�̸� AI���� dialogue data (Dr.Kim�� AI���� ��ȭ�� �� ��), �� ���� �н�
        talkData.Add(1000, new string[] { "�� ������ �����ϱ��~?" });
        talkData.Add(1001, new string[] { "�ڻ���� MBTI�� ������?", "���� ISTP����!" });
        talkData.Add(1002, new string[] { "ģ���� �� �־����� ���ھ��..", "���� �ڻ���� �ְ��� ģ�����ϴ�." });
        talkData.Add(1003, new string[] { "�ڻ���� ��ô� ���� ���� ���� �� �ñ��ؿ�! ", "���� ���� �� �԰ŵ��." });
        talkData.Add(1004, new string[] { "������ ���� �¾翭 ����������.", "������ �¾���� �� �������?", "������ �ʹ� ���� ���̿���Ф�" });
        talkData.Add(1005, new string[] { "���� ��� ��������ɱ��?" });
        talkData.Add(1006, new string[] { "�����̶� �����ϱ��?", "���� �����ϱ��?" });
        talkData.Add(1007, new string[] { "AI�� �ʹ� �ȶ������� ��� �ɱ��?" });
        talkData.Add(1008, new string[] { "�ڻ���� �����亸�� �� �ȶ��ϰ� ���ִ� ����̿���!" });
        talkData.Add(1009, new string[] { "�����̶� �����ϱ��? ������ ���� ���� �ɱ��?" });

        // C : key�� 100~400�̸� �н��ϱ⿡ ���� text data (100 - ���, 200 - ����, 300 - ����, 400 - ����)
        talkData.Add(100, new string[] { "��縦 �н��Ͻðڽ��ϱ�?", "��縦 �н��մϴ�." });
        talkData.Add(200, new string[] { "������ �н��Ͻðڽ��ϱ�?", "������ �н��մϴ�." });
        talkData.Add(300, new string[] { "������ �н��Ͻðڽ��ϱ�?", "������ �н��մϴ�." });
        talkData.Add(400, new string[] { "������ �н��Ͻðڽ��ϱ�?", "������ �н��մϴ�." });
    }

    // C : �ʿ��� TalkData�� return
    public string GetTalkData(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)       // C : talkIndex�� talkData[id]�� ������ index + 1�̸�
            return null;

        return talkData[id][talkIndex];     // C : �ʿ��� ������ id�� index�� ���� return
    }
}
