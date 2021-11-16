using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // C : UI 프로그래밍을 위해 추가 (Text 등)

// C : 전체적인 게임 진행 및 관리를 도와주는 스크립트
public class GameManager : MonoBehaviour
{
    public Text talkText;           // C : 대화창의 text
    public GameObject scanObject;   // C : 스캔된(조사한) game object

    // C : 플레이어가 Object에 대해 조사 시(플레이어의 액션 발생 시) 적절한 내용을 포함한 대화창 띄워주기
    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;       // C : parameter로 들어온 스캔된 game object를 public 변수인 scanObject에 대입
        talkText.text = "This is :" + scanObject.name + ".";      // C : 대화창의 text를 scanObject의 이름을 포함한 적절한 내용으로 설정
    }
}
