using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HappyEnding : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI dialog; // K : text 오브젝트를 받아오기 위한 변수입니다. > using TMPro;          

    // K : synopsys의 텍스트들(여러 문장)의 배열입니다.
    public string[] HappyEndingFullText = {
        "박사님!!\n 제가 통신기를 만들었어요!!",
        "통신기?",
        "사실 매일 조금씩 만들고 있었는데...\n드디어 완성이에요!",
        "NJ-C! 당장 통신기를 사용해보자!",
        "치...지직....치지지..직...여기는",
    };


    string subText; // K : synopsys의 텍스트(한 문장) 일부를 저장하기 위한 변수입니다.
    int currentPoint = 0; // K : synopsysFullText에서 현재 포인터가 어디있는지 저장하기 위한 변수입니다.

    public bool isTyping = true; // K : 현재 글자가 화면에 타이핑되고 있는지 확인하기 위한 변수입니다.
    bool isSkipPart = false;

    void Start()
    {
        StartCoroutine("TypingAction", 0);          // K : 스크립트의 시작과 동시에 타이핑을 시작하는 코드입니다.
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))    // K : 스페이스바를 눌렀을 때
        {
            if (!isTyping)  // K : 현재 글자가 화면에 타이핑 되고 있지 않을 때
            {
                // K : 스페이스바를 눌렀을 때 현재 글자가 화면에 타이핑 되고 있지 않는다면, 다음 문장 타이핑하기 위한 코드
                StartCoroutine("TypingAction", 0);
            }
            else
            {
                // K : 스페이스바를 눌렀을 때 현재 글자가 화면에 타이핑 되고 있다면, 부분 스킵을 위해 isSkipPart true로 변경하는 코드
                isSkipPart = true;
            }
        }
    }

    public void GoToGameScreen(string screenName) // K : 게임 신으로 가는 함수입니다.
    {
        // K : 모든 변수 초기화
        currentPoint = 0;
        subText = "";
        isTyping = true;
        isSkipPart = false;

        SceneManager.LoadScene(screenName); // K : 게임 신으로 가게 하는 함수입니다. > using UnityEngine.SceneManagement;
    }

    IEnumerator TypingAction()
    {
        if (currentPoint >= HappyEndingFullText.Length)    // K : 모든 텍스트들을 타이핑 했을 때
        {
            Debug.Log("Game Start"); // K : 모든 텍스트를 출력 완료, 게임 플레이 신으로 이동
            GoToGameScreen("EndingCredits");
        }

        dialog.text = "";   // K : Text 오브젝트의 text 초기화
        isTyping = true;    // K : 텍스트 화면에 타이핑을 시작했기 때문에, isTyping true

        for (int i = 0; i < HappyEndingFullText[currentPoint].Length; i++) // K : 텍스트 한 문장의 한 글자 한 글자를 화면에 나타나게 하기 위한 반복문
        {
            yield return new WaitForSeconds(0.15f); // K : 텍스트 한 글자 한 글자 사이의 딜레이

            subText += HappyEndingFullText[currentPoint].Substring(0, i);  // K : 텍스트의 인덱스 0~i까지 자름
            dialog.text = subText;                                      // K : Text 오브젝트에 subText 적용 
            subText = "";                                               // K : subText 초기화

            if (isSkipPart)                                             // K : 부분 스킵이 true면
            {
                dialog.text = HappyEndingFullText[currentPoint];           // K : Text 오브젝트에 전체 텍스트 문장을 띄운다
                isSkipPart = false;                                     // K : isSkipPart 초기화
                isTyping = false;                                       // K : isTyping 초기화
                break;
            }
        }

        isTyping = false;   // isTyping 초기화
        currentPoint++;     // 텍스트 배열의 포인터 옮김
    }
}
