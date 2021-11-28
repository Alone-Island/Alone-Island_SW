using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // C : UI 프로그래밍을 위해 추가 (Text 등)
using TMPro;            // J : TextMeshProUGUI를 위해 추가

// C : 전체적인 게임 진행 및 관리를 도와주는 스크립트
public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI talkText;           // C : 대화창의 text
    public GameObject scanObject;   // C : 스캔된(조사한) game object
    public GameObject talkPanel;    // C : 대화창
    public bool isTPShow;           // C : talkPanel의 상태 저장 (보여주기 or 숨기기)
    public TalkManager talkManager; // C : GameManager에서 TalkManager의 함수를 호출할 수 있도록 talkManager 변수 생성
    public int talkIndex;           // C : 필요한 talkIndex를 저장하기 위한 변수 생성
    public int day = 20;            // J : 하루는 20초

    public SpecialEventManager specialManager; // J : GameManager에서 SpecialEventManager의 함수를 호출할 수 있도록 talkManager 변수 생성
    public LearningManager learningManager;     // C :
    public ScreenManager screenManager; // N : 책 개수 가져오기 위해

    bool playerTalk = false;            // J : 플레이어가 대화하는 중에는 special event를 유예하도록 변수 생성
    public bool isEndingShow = false;         // N : 엔딩 여부 (엔딩 카드 나타난 직후부터)
    public bool isTheEnd = false;         // N : 게임 종료 여부 (엔딩 카드 나타나고 2초 뒤부터)
    int randomNum = 0;                  // C : AI와의 대화 시, 랜덤한 대화 내용을 출력하기 위한 변수 생성
    public int dayTalk = 0;        // N : AI와의 대화 횟수

    public TextMeshProUGUI alertText;           // N : 알림창의 text

    // J :IEnumerator 타입(WaitForSeconds)를 반환하는 함수
    private IEnumerator SpecialEvent(float delayTime)
    {
        yield return new WaitForSeconds(delayTime); // J : delayTime을 기다린 후 재개
        // J : 플레이어의 대화가 끝날 때까지 대기
        while (true) {
            if (!playerTalk) {              // J : player가 대화 중이 아니면
                specialManager.Action();    // J : special event 발동
                break;
            }
            yield return null;
        }
        StartCoroutine("SpecialEvent", day*3); // J : SpecialEvent 함수 호출
    }

    private void Start()
    {
        StartCoroutine("SpecialEvent", day*3); // J : SpecialEvent 함수 호출
    }

    

    // C : 플레이어가 Object에 대해 조사 시(플레이어의 액션 발생 시) 적절한 내용을 포함한 대화창 띄워주기
    public void Action(GameObject scanObj)
    {
        playerTalk = true;                  // J : 플레이어가 대화하는 중에는 special event를 유예하도록 설정
        scanObject = scanObj;               // C : parameter로 들어온 스캔된 game object를 public 변수인 scanObject에 대입
        ObjectData objData = scanObject.GetComponent<ObjectData>();     // C : scanObject의 ObjectData instance 가져오기
        int talkId = objData.id;            // K : takl data의 id 지정 변수, 예외처리를 위해 추가 설정함

        if (objData.id == 1000 && randomNum == 0)      // C : objData가 AI이고, 대화 첫 시작이면
        {
            // N : 하루에 한번 이상 대화 시도, J : or 어제 띄운 알림창이 아직 활성화 상태인 경우
            if (dayTalk > 0)
            {
                talkId = 2000;
            }
            else
            {
                System.Random rand = new System.Random();
                randomNum = rand.Next(1, 11);                  // C : 1~10까지의 난수를 대입
            }
        }

        if (objData.id >= 100 && objData.id <= 400)
        {
            if (learningManager.isAILearning) // K : 학습하기 조사를 했을때, AI 학습중인 경우 예외처리
            {
                talkId = 500;
            } else if (screenManager.currBookNum() < 1) // K : 학습하기 조사를 했을때, 책이 없는 경우 예외처리
            {
                talkId = 600;
            }
        }
        Talk(talkId);                   // C : 필요한 talkPanel text 값 가져오기, K : 예외처리를 위해 objData.id > talkId로 수정

        if (talkId == 1000) talkPanel.SetActive(isTPShow);      // C : talkPanel 숨기거나 보여주기
        else
        {
            GameObject.Find("Alert").transform.Find("Alert Set").gameObject.SetActive(isTPShow); // N : 알림창 띄워주기
        }
    }

    // C : 상황에 따라 적절하게 필요한 talkPanel text 값 대화창에 띄우기
    void Talk(int id)
    {
        // C : 조사한 object에 해당하는 talkData 중 talkIndex 위치의 string을 가져오기
        string talkData = talkManager.GetTalkData(id + randomNum, talkIndex);

        if (talkData == null)           // C : 해당하는 id의 talkData string들을 모두 가져왔다면
        {
            if (id >= 100 && id <= 400)     // C :
            {
                learningManager.Learning(id);
            }
            else if (id == 1000) screenManager.HeartStudy(0);

            playerTalk = false;         // J : 정상적으로 special event가 발동하도록 설정
            isTPShow = false;           // C : talkPanel의 show 상태 false로 저장
            talkIndex = 0;              // C : 다음 Talk()함수 사용을 위해 talkIndex를 0으로 초기화
            randomNum = 0;              // C : 다음 Talk()함수 사용을 위해 randomNum을 0으로 초기화
            if (id == 1000) dayTalk++;  // N : 하루 대화 횟수 증가
            return;
        }
        if (id == 1000) talkText.text = talkData;       // C : talkPanel의 text를 talkData로 설정
        else alertText.text = talkData;                 // N : 알림창의 text를 talkData로 설정

        isTPShow = true;                // C : talkPanel의 show 상태 true로 저장 (해당하는 id의 talkData string이 아직 남아있음)
        talkIndex++;                    // C : 해당하는 id의 다음 talkData string을 가져오기 위해
    }
}
