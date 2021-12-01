using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// K : 텍스트에 효과를 주기 위한 매니저입니다.
public class TextManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI dialog; // K : text 오브젝트를 받아오기 위한 변수입니다. > using TMPro;          
    public GameManager manager;
    public EndingManager endingManager;
    public string[] fullText;

    private int endingCode = DataController.Instance.endingData.currentEndingCode;

    // K : synopsys의 텍스트들(여러 문장)의 배열입니다.
    private string[] synopsysFullText = { 
        "나는 로봇공학자 K...\nAI 로봇을 개발하기 위해\n연구실에서만 지낸 시간이 수년... ", 
        "드디어... 완성했다...\n나의 역작, AI 로봇 NJ-C!!!!\n이제 세상에 공개할 때가 왔...",
        "아니...뭐지??",
        "내가 살고 있던 마을은 섬이 되어있었다...",
        "통신은 모두 끊겨있고..\n사람들은 전혀 보이지 않는다!!\n갑자기 무인도에 혼자 남게 되다니?!?",
        "하지만 나에게는 NJ-C가 있어!\n아직 깡통에 불과하는 이 로봇을 학습시켜서\n이 섬에서 살아남아 보자! "
    };

    private string[] happySosoLifeFullText = {
        "벌써 90일이 지났어요.",
        "이런 하루하루도 나쁘지는 않네요.",
        "앞으로도 잘 지내봐요. 박사님."
    };    
    private string[] happyAIFullText = {
        "박사님! 박사님이 가르칠 로봇들이 잔뜩 생겼어요.",
        "제 공학 능력이 이렇게 성장했네요.",
        "앞으로 저희가 다같이 박사님을 도울게요.",
        "박사박사님!!님!! 박박사님!사님! 박사님!!"
    };    
    private string[] happyPeopleFullText = {
        "박사님!! \n제가 통신기를 만들었어요!!!",
        "사실 매일 조금씩 만들고 있었는데... \n드디어 완성이에요!",
        "지금 당장 통신기를 사용해봐요!!",
        "치...지직....치지지..직...여기는...",
    };    
    private string[] happyTwoFullText = {
        "박사님! 제가 열심히 꽃을 심었더니 우리 섬이 예뻐졌어요!",
        "박사님과의 하루하루가 정말 즐거워요",
        "박사님도 그러셨으면 좋겠어요"
    };


    string subText; // K : synopsys의 텍스트(한 문장) 일부를 저장하기 위한 변수입니다.
    int currentPoint = 0; // K : synopsysFullText에서 현재 포인터가 어디있는지 저장하기 위한 변수입니다.

    public bool isTyping = true; // K : 현재 글자가 화면에 타이핑되고 있는지 확인하기 위한 변수입니다.
    bool isSkipPart = false;

    void Start() {
        if (endingCode == 0)             // K : 엔딩 코드가 0인 경우가 초기화 상태이므로 게임 시작시 시놉시스 우선
        {
            fullText = synopsysFullText;
            
        }       
        else if(endingCode > 100)                                            // K : 엔딩코드가 100보다 큰 경우 해피엔딩
        {
            switch (endingCode)
            {
                case 101:
                    fullText = happySosoLifeFullText;
                    break;
                case 102:
                    fullText = happyTwoFullText;
                    break;
                case 103:
                    fullText = happyAIFullText;
                    break;
                case 104:
                    fullText = happyPeopleFullText;
                    break;
                default:
                    Debug.Log("해피에딩 에러");
                    break;
            }
        }
        StartCoroutine("TypingAction", 0);          // K : 스크립트의 시작과 동시에 시놉시스를 타이핑을 시작하는 코드입니다.
    }
    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))    // K : 스페이스바를 눌렀을 때
        {

            if (manager.isTheEnd)
            {
                if (endingCode < 100) // 배드에딩코드 < 100, 해피엔딩시 > 100
                {
                    SceneManager.LoadScene("GameMenu"); // K : 배드엔딩이 끝나고 바로 게임 메뉴로 돌아갑니다.
                }
                else
                {
                    SceneManager.LoadScene("EndingCredits"); // K : 해피엔딩시에만 엔딩크레딧을 보여줌
                }
            } else
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
    }

    public void GoToGameScreen() // K : 게임 신으로 가는 함수입니다.
    {
        // K : 모든 변수 초기화
        StopCoroutine("TypingAction");
        currentPoint = 0;
        subText = "";
        isTyping = true;
        isSkipPart = false;

        if (endingCode < 100)
        {
            SceneManager.LoadScene("MainGame"); // K : 시놉시스가 끝나고 게임 신으로 가게 하는 함수입니다. > using UnityEngine.SceneManagement;
        }
        else
        {
            endingManager.ShowHappyEndingCard(endingCode); // K : 해피엔딩 카드가 보이게 하는 함수를 호출합니다.
        }        
    }

    IEnumerator TypingAction() {
        if (currentPoint >= fullText.Length)    // K : 모든 텍스트들을 타이핑 했을 때
        {
            if (endingCode < 100)
            {
                Debug.Log("Game Start"); // K : 모든 텍스트를 출력 완료, 게임 플레이 신으로 이동
            } else
            {
                Debug.Log("Game Happy Ending Credits");
            }
            GoToGameScreen();
        }

        dialog.text = "";   // K : Text 오브젝트의 text 초기화
        isTyping = true;    // K : 텍스트 화면에 타이핑을 시작했기 때문에, isTyping true

        for (int i = 0; i < fullText[currentPoint].Length; i++) // K : 텍스트 한 문장의 한 글자 한 글자를 화면에 나타나게 하기 위한 반복문
        {
            yield return new WaitForSeconds(0.07f); // K : 텍스트 한 글자 한 글자 사이의 딜레이

            subText += fullText[currentPoint].Substring(0, i);  // K : 텍스트의 인덱스 0~i까지 자름
            dialog.text = subText;                                      // K : Text 오브젝트에 subText 적용 
            subText = "";                                               // K : subText 초기화

            if (isSkipPart)                                             // K : 부분 스킵이 true면
            {
                dialog.text = fullText[currentPoint];           // K : Text 오브젝트에 전체 텍스트 문장을 띄운다
                isSkipPart = false;                                     // K : isSkipPart 초기화
                isTyping = false;                                       // K : isTyping 초기화
                break;
            }
        }

        isTyping = false;   // isTyping 초기화
        currentPoint++;     // 텍스트 배열의 포인터 옮김
    }
}
