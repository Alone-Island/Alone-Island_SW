using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// C : 대화 데이터를 저장 및 관리하는 스크립트
public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;       // C : 대화 데이터를 저장하는 dictionary 변수

    void Awake()
    {
        // C : dictionary instance 생성
        talkData = new Dictionary<int, string[]>();
        GenerateTalkData();
    }

    // C : talkData 생성
    void GenerateTalkData()
    {
        // C : key가 1000~1009이면 AI와의 dialogue data (Dr.Kim이 AI에게 대화를 걸 때), 즉 공감 학습
        talkData.Add(1000, new string[] { "제 생일은 언제일까요~?" });
        talkData.Add(1001, new string[] { "박사님의 MBTI는 뭐에요?", "저는 ISTP에요!" });
        talkData.Add(1002, new string[] { "친구가 더 있었으면 좋겠어요..", "물론 박사님이 최고의 친구랍니다." });
        talkData.Add(1003, new string[] { "박사님이 드시는 밥은 무슨 맛일 지 궁금해요! ", "저는 쌀을 못 먹거든요." });
        talkData.Add(1004, new string[] { "저에게 밥은 태양열 에너지에요.", "차가운 태양빛은 왜 없을까요?", "여름에 너무 덥단 말이에요ㅠㅠ" });
        talkData.Add(1005, new string[] { "저는 어떻게 만들어진걸까요?" });
        talkData.Add(1006, new string[] { "생명이란 무엇일까요?", "저도 생명일까요?" });
        talkData.Add(1007, new string[] { "AI가 너무 똑똑해지면 어떻게 될까요?" });
        talkData.Add(1008, new string[] { "박사님은 제페토보다 더 똑똑하고 멋있는 사람이에요!" });
        talkData.Add(1009, new string[] { "죽음이란 무엇일까요? 죽으면 어디로 가는 걸까요?" });

        // C : key가 100~400이면 학습하기에 대한 text data (100 - 농사, 200 - 건축, 300 - 공예, 400 - 공학)
        talkData.Add(100, new string[] { "농사를 학습하시겠습니까?", "농사를 학습합니다." });
        talkData.Add(200, new string[] { "건축을 학습하시겠습니까?", "건축을 학습합니다." });
        talkData.Add(300, new string[] { "공예를 학습하시겠습니까?", "공예를 학습합니다." });
        talkData.Add(400, new string[] { "공학을 학습하시겠습니까?", "공학을 학습합니다." });
    }

    // C : 필요한 TalkData를 return
    public string GetTalkData(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)       // C : talkIndex가 talkData[id]의 마지막 index + 1이면
            return null;

        return talkData[id][talkIndex];     // C : 필요한 문장을 id와 index를 통해 return
    }
}
