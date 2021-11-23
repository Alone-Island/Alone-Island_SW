using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // J : UI 프로그래밍을 위해 추가 (Text 등)

public class SpecialEventManager : MonoBehaviour
{
    public TalkManager talkManager; // J : GameManager에서 TalkManager의 함수를 호출할 수 있도록 talkManager 변수 생성
    public ScreenManager screenManager; // N : 레벨 관리를 위해 호출
    public EndingManager endingManager; // N : 엔딩 처리를 위해 호출
    public GameObject talkPanel;    // J : 대화창
    public Text talkText;           // J : 대화창의 text
    public int talkIndex;           // J : talkIndex를 저장하기 위한 변수
    public bool AItalk = false;     // J : AI가 스페셜 이벤트 대화를 하는지 여부
    public Text selectText0, selectText1, selectText2;
    public Button selectButton0, selectButton1, selectButton2;

    List<Text> selectText;          // J : 선택지 텍스트를 관리하기 위한 리스트
    List<Button> selectButton;      // J : 선택지 버튼을 관리하기 위한 리스트
    int specialID;                  // J : TalkManager로부터 talkData를 가져오기 위한 변수
    int firstRandomNum;             // J : 랜덤 스페셜 이벤트를 위한 변수1 (0 : 선택지 2개, 1: 선택지 3개)
    int secondRandomNum;            // J : 랜덤 스페셜 이벤트를 위한 변수2

    // J : Special Event 발생
    public void Action() 
    {
        AItalk = true;  // J : Jump키를 눌렀을 때 object scan을 할 수 없게 함

        System.Random rand = new System.Random();
        
        int danger = (int)((10 - screenManager.houseLv.fCurrValue) * 5 + 50);   // J : 위험도 계산
        if (rand.Next(100) < danger)    // J : 위험도가 높아 재난 발생
            Disaster();
        else 
        {
            firstRandomNum = rand.Next(2);      // J : 0-1까지의 난수 생성 (0 : 선택지 2개, 1: 선택지 3개)
            secondRandomNum = rand.Next(1, 5);  // J : 1-4까지의 난수 생성

            specialID = 10000 + 10 * firstRandomNum + secondRandomNum; // J : talkData를 갖고 오기 위해 talkID 계산

            talkPanel.SetActive(true);  // J : 대화창 활성화
            Talk();                     // J : 대화 시작
        }
    }

    private void Disaster()
    {
        switch ((new System.Random()).Next(2))  // J : 각 재난은 50% 확률로 발생
        {
            case 0: // J : 쓰나미
                endingManager.suddenEnding(5);
                break;
            case 1: // J : 운석 충돌
                endingManager.suddenEnding(6);
                break;
        }
    }

    // J : 실행될 때마다 다음 문장으로 넘어감
    public void Talk() 
    {
        string talkData = talkManager.GetTalkData(specialID, talkIndex);   // J : TalkManager로부터 talkData를 가져오기
        if (talkData == null)   // J : 해당 talkID의 talkData를 모두 가져왔다면
        {
            AItalk = false;     // J : Jump키를 눌렀을 때 object scan을 할 수 있게 함
            talkIndex = 0;      // J : talk index 초기화
            talkPanel.SetActive(false);  //선택지 함수 구현 전까지 임시 코드
            Select();           // J : 선택지 화면에 보임
            return;
        }
        talkText.text = talkData;       // J : talkPanel의 text를 talkData로 설정
        talkIndex++;                    // J : 해당 talkID의 다음 talkData string을 가져오기 위해
    }

    // J : 선택지가 화면에 나타남
    void Select()
    {
        string selectData;        
        for (int selectIndex = 0; (selectData = talkManager.GetSelectData(specialID, selectIndex)) != null; selectIndex++) // J : selectData의 개수에 따라 selectButton이 보임
        {
            selectText[selectIndex].text = selectData;              // J : SelectButton의 text에 selectData대입
            selectButton[selectIndex].gameObject.SetActive(true);   // J : SelectButton 활성화
        }
    }


    // J : SelectButton0을 클릭했을 때 호출되는 함수
    public void Select0()
    {
        switch (firstRandomNum) 
        {
            case 0:     // J : 선택지가 2개인 경우
                switch (secondRandomNum)
                {
                    case 1: // J : 배터리가 많이 닳았어요ㅠㅠ "하루만 아무것도 안하고 싶어요..
                        // 하루 바로 지나기
                        screenManager.HeartStudy(1); // N : 공감 1레벨 상승
                        break;
                    case 2: // J : 박사님을 위해 새로운 열매를 따왔어요!
                        endingManager.suddenEnding(1); // N : Bad Ending (독열매)
                        break;
                    case 3: // J : 저기 야생동물이 있는 것 같아요! 잡아서 구워먹을까요?
                        screenManager.HeartStudy(1); // J : 공감 1레벨 상승
                        break;
                    case 4: // J : 저기 야생동물이 있는 것 같아요! 잡아서 구워먹을까요?
                        endingManager.suddenEnding(4);  // J : 멧돼지 사망
                        break;

                }
                break;
            case 1:     // J : 선택지가 3개인 경우
                switch (secondRandomNum)
                {
                    case 1: // J : 이 꽃 너무 이쁘지 않아요??
                        endingManager.suddenEnding(2); // N : Bad Ending (AI가 이해하지 못함)
                        break;
                    case 2: // J : (AI가 물에 빠졌다)
                        endingManager.suddenEnding(3);  // 감전사 사망
                        break;
                    case 3: // J : (나무가 쓰러져서 AI가 다쳤다. 어떻게 할까?)
                        // 하루 바로 지나기
                        screenManager.HeartStudy(2); // N : 공감 2레벨 상승
                        break;
                    case 4: // J : *추후 추가 예정*
                        // 나중에 추가
                        break;
                }
                break;
        }
        SelectComplete();   // J :선택이 완료되면 대화창과 선택지 비활성화
    }

    // J : SelectButton1을 클릭했을 때 호출되는 함수
    public void Select1()
    {
        switch (firstRandomNum)
        {
            case 0:     // J : 선택지가 2개인 경우
                // 공감 능력 1레벨 하락
                break;
            case 1:     // J : 선택지가 3개인 경우
                switch (secondRandomNum)
                {
                    case 1: // J : 이 꽃 너무 이쁘지 않아요??
                        // 변화없음
                        break;
                    case 2: // J : (AI가 물에 빠졌다)
                        screenManager.HeartStudy(-1); // J : 공감 1레벨 하락
                        break;
                    case 3: // J : (나무가 쓰러져서 AI가 다쳤다. 어떻게 할까?)
                        // 변화없음
                        break;
                    case 4: // J : *추후 추가 예정*
                        // 나중에 추가
                        break;
                }
                break;
        }
        SelectComplete();   // J :선택이 완료되면 대화창과 선택지 비활성화
    }

    // J : SelectButton2을 클릭했을 때 호출되는 함수
    public void Select2()
    {
        switch (secondRandomNum)
        {
            case 1: // J : 이 꽃 너무 이쁘지 않아요??
                // 공감 능력 1레벨 하락
                break;
            case 2: // J : (AI가 물에 빠졌다)
                // 구조 성공
                break;
            case 3: // J : (나무가 쓰러져서 AI가 다쳤다. 어떻게 할까?)
                // 알고보니 심각한 손상을 입은 AI 고장 -> 사망
                endingManager.suddenEnding(3); // N : Bad Ending (AI 고장)
                break;
            case 4: // J : *추후 추가 예정*
                // 나중에 추가
                break;
        }
        SelectComplete();   // J :선택이 완료되면 대화창과 선택지 비활성화
    }

    // J :선택이 완료되면 호출, 대화창과 선택지 비활성화
    void SelectComplete()
    {
        for (int i = 0; i < 3; i++)
            selectButton[i].gameObject.SetActive(false);
        talkPanel.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        selectText = new List<Text>();
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
