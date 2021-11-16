using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // C : UI 프로그래밍을 위해 추가 (Text 등)

// C : 전체적인 게임 진행 및 관리를 도와주는 스크립트
public class GameManager : MonoBehaviour
{
    public Text talkText;           // C : 대화창의 text
    public GameObject scanObject;   // C : 스캔된(조사한) game object
    public GameObject talkPanel;    // C : 대화창
    public bool isTPShow;           // C : talkPanel의 상태 저장 (보여주기 or 숨기기)
    public TalkManager talkManager; // C : GameManager에서 TalkManager의 함수를 호출할 수 있도록 talkManager 변수 생성
    public int talkIndex;           // C : 필요한 talkIndex를 저장하기 위한 변수 생성

    // C : 플레이어가 Object에 대해 조사 시(플레이어의 액션 발생 시) 적절한 내용을 포함한 대화창 띄워주기
    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;               // C : parameter로 들어온 스캔된 game object를 public 변수인 scanObject에 대입
        ObjectData objData = scanObject.GetComponent<ObjectData>();     // C : scanObject의 ObjectData instance 가져오기
        Talk(objData.id);                   // C : 필요한 talkPanel text 값 가져오기

        talkPanel.SetActive(isTPShow);      // C : talkPanel 숨기거나 보여주기
    }

    // C : 상황에 따라 적절하게 필요한 talkPanel text 값 대화창에 띄우기
    void Talk(int id)
    {
        string talkData = talkManager.GetTalkData(id, talkIndex);     // C : 조사한 object에 해당하는 talkData 중 talkIndex 위치의 string을 가져오기

        if (talkData == null)           // C : 해당하는 id의 talkData string들을 모두 가져왔다면
        {
            isTPShow = false;           // C : talkPanel의 show 상태 false로 저장
            talkIndex = 0;              // C : 다음 Talk()함수 사용을 위해 talkIndex를 0으로 초기화
            return;
        }
        
        talkText.text = talkData;       // C : talkPanel의 text를 talkData로 설정
        isTPShow = true;                // C : talkPanel의 show 상태 true로 저장 (해당하는 id의 talkData string이 아직 남아있음)
        talkIndex++;                    // C : 해당하는 id의 다음 talkData string을 가져오기 위해
    }
}
