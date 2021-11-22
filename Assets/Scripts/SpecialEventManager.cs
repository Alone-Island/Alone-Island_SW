using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // J : UI 프로그래밍을 위해 추가 (Text 등)

public class SpecialEventManager : MonoBehaviour
{
    public TalkManager talkManager; // J : GameManager에서 TalkManager의 함수를 호출할 수 있도록 talkManager 변수 생성
    public ScreenManager screenManager; // N : 레벨 관리를 위해 호출
    public GameObject talkPanel;    // J : 대화창
    public Text talkText;           // J : 대화창의 text
    public int talkIndex;           // J : talkIndex를 저장하기 위한 변수
    public bool AItalk = false;     // J : AI가 스페셜 이벤트 대화를 하는지 여부
    public bool special = false;    // J : 스페셜 이벤트 진행 중 여부
    public Text selectText0, selectText1, selectText2;
    public Button selectPanel0, selectPanel1, selectPanel2;

    List<Text> selectText;
    List<Button> selectPanel;
    int talkID;                     // J : TalkManager로부터 talkData를 가져오기 위한 변수
    int firstRandomNum;             // J : 랜덤 스페셜 이벤트를 위한 변수1 (0 : 선택지 2개, 1: 선택지 3개)
    int secondRandomNum;            // J : 랜덤 스페셜 이벤트를 위한 변수2

    // J : Special Event 발생
    public void Action() 
    {
        AItalk = true;  // J : Jump키를 눌렀을 때 object scan을 할 수 없게 함

        System.Random rand = new System.Random();
        firstRandomNum = rand.Next(2);      // J : 0-1까지의 난수 생성 (0 : 선택지 2개, 1: 선택지 3개)
        secondRandomNum = rand.Next(1, 5);  // J : 1-4까지의 난수 생성

        talkID = 10000 + 10 * firstRandomNum + secondRandomNum; // J : talkData를 갖고 오기 위해 talkID 계산

        talkPanel.SetActive(true);  // J : 대화창 활성화
        Talk();                     // J : 대화 시작
    }

    // J : 실행될 때마다 다음 문장으로 넘어감
    public void Talk() 
    {
        string talkData = talkManager.GetTalkData(talkID, talkIndex);   // J : TalkManager로부터 talkData를 가져오기
        if (talkData == null)   // J : 해당 talkID의 talkData를 모두 가져왔다면
        {
            AItalk = false;     // J : Jump키를 눌렀을 때 object scan을 할 수 있게 함
            talkIndex = 0;      // J : talk index 초기화
            talkPanel.SetActive(false);  //선택지 함수 구현 전까지 임시 코드
            Select();
            return;
        }
        talkText.text = talkData;       // J : talkPanel의 text를 talkData로 설정
        talkIndex++;                    // J : 해당 talkID의 다음 talkData string을 가져오기 위해
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
                        // 하루 바로 지나기
                        screenManager.HeartStudy(1); // N : 공감 1레벨 상승
                        break;
                    case 2:
                        // 독열매 사망
                        break;
                    case 3:
                        // 공감 능력 1레벨 상승
                        break;
                    case 4:
                        // 멧돼지 사망
                        break;

                }
                break;
            case 1:
                switch (secondRandomNum)
                {
                    case 1:
                        // AI가 이해하지 못해서 사망
                        break;
                    case 2:
                        // 감전사 사망
                        break;
                    case 3:
                        // 하루 바로 지나기
                        screenManager.HeartStudy(2); // N : 공감 2레벨 상승

                        break;
                    case 4:
                        // 나중에 추가
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
                // 공감 능력 1레벨 하락
                break;
            case 1:
                switch (secondRandomNum)
                {
                    case 1:
                        // 변화없음
                        break;
                    case 2:
                        // 공감 능력 1레벨 하락
                        break;
                    case 3:
                        // 변화없음
                        break;
                    case 4:
                        // 나중에 추가
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
                // 공감 능력 1레벨 하락
                break;
            case 2:
                // 구조 성공
                break;
            case 3:
                // 알고보니 심각한 손상을 입은 AI 고장 -> 사망
                break;
            case 4:
                // 나중에 추가
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
