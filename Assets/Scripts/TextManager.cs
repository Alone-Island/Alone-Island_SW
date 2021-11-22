using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// K : 텍스트에 효과를 주기 위한 매니저입니다.
public class TextManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI dialog; // K : text 오브젝트를 받아오기 위한 변수입니다.

    // K : synopsys의 텍스트들(여러 문장)의 배열입니다.
    public string[] synopsysFullText = { 
        "나는 로봇공학자 K...\nAI 로봇을 개발하기 위해\n연구실에서만 지낸 시간이 수년...", 
        "드디어... 완성했다...",
        "나의 역작, AI 로봇 NJ-C!!!!",
        "이제 세상에 공개할 때가 왔군",
        "아니...뭐지?",
        "연구실 밖의 상황을 살펴보니…",
        "통신은 모두 끊겨있고..\n사람들은 전혀 보이지 않는다!!",
        "설마 내가 이 세상 남은 유일한 인간인건가?!?",
        "이제 식량도 거의 다 떨어지고...\n이렇게 죽는건가...",
        "하지만 나에게는 NJ-C가 있어!",
        "아직 깡통에 불과하는 이 로봇을 학습시켜서\n이 황폐화된 세상에서 살아남아 보자!"
    };
    string subText; // K : synopsys의 텍스트(한 문장) 일부를 저장하기 위한 변수입니다.
    int currentPoint = 0; // K : synopsysFullText에서 현재 포인터가 어디있는지 저장하기 위한 변수입니다.

    bool isTyping = true; // K : 현재 글자가 화면에 타이핑되고 있는지 확인하기 위한 변수입니다.
    bool isSkipPart = false;

    void Start() {
        StartCoroutine("TypingAction", 0);  // K : 스크립트의 시작과 동시에 타이핑을 시작하는 코드입니다.
    }
    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))    // K : 스페이스바를 눌렀을 때
        {
            if (!isTyping)  // K : 현재 글자가 화면에 타이핑 되고 있지 않을 때
            {
                // 스페이스바를 눌렀을 때 현재 글자가 화면에 타이핑 되고 있지 않는다면, 다음 문장 타이핑하기 위한 코드
                StartCoroutine("TypingAction", 0); 
            }
            else
            {
                // 스페이스바를 눌렀을 때 현재 글자가 화면에 타이핑 되고 있다면, 부분 스킵을 위해 isSkipPart true로 변경하는 코드
                isSkipPart = true;
            }
        }
    }

    void GoToGameScreen() 
    {
        // 모든 변수 초기화
        currentPoint = 0;
        subText = "";
        isTyping = true;
        isSkipPart = false;

        SceneManager.LoadScene("SampleScene");
    }

    IEnumerator TypingAction() {
        if (currentPoint >= synopsysFullText.Length)    // 모든 텍스트들을 타이핑 했을 때
        {
            Debug.Log("게임 시작"); // K : 모든 텍스트를 출력 완료, 게임 플레이 신으로 이동
            GoToGameScreen();
        }

        dialog.text = "";   // Text 오브젝트의 text 초기화
        isTyping = true;    // 텍스트 화면에 타이핑을 시작했기 때문에, isTyping true

        for (int i = 0; i < synopsysFullText[currentPoint].Length; i++) // 텍스트 한 문장의 한 글자 한 글자를 화면에 나타나게 하기 위한 반복문
        {
            yield return new WaitForSeconds(0.15f); // 텍스트 한 글자 한 글자 사이의 딜레이

            subText += synopsysFullText[currentPoint].Substring(0, i);  // 텍스트의 인덱스 0~i까지 자름
            dialog.text = subText;                                      // Text 오브젝트에 subText 적용 
            subText = "";                                               // subText 초기화

            if (isSkipPart)                                             // 부분 스킵이 true면
            {
                dialog.text = synopsysFullText[currentPoint];           // Text 오브젝트에 전체 텍스트 문장을 띄운다
                isSkipPart = false;                                     // isSkipPart 초기화
                isTyping = false;                                       // isTyping 초기화
                break;
            }
        }

        isTyping = false;   // isTyping 초기화
        currentPoint++;     // 텍스트 배열의 포인터 옮김
    }
}
