using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenManager : MonoBehaviour
{
    public PlayerStat hungerStat; // N : 배고픔 스탯
    public PlayerStat happyStat; // N : 행복 스탯
    public PlayerStat temperatureStat; // N : 체온 스탯
    public PlayerStat dangerStat; // N : 위험 스탯

    public AILevel farmLv; // N : 농사 레벨
    public AILevel houseLv; // N : 건축 레벨
    public AILevel craftLv; // N : 공예 레벨
    public AILevel engineerLv; // N : 공학 레벨
    public AILevel heartLv; // N : 공감 레벨

    public LearningManager learningManager;
    public TextMeshProUGUI learningTime;
    public TextMeshProUGUI learningTitle;

    private int day = 1; // N : 날짜
    //private int bookNum = 0; // N : 책 개수
    [SerializeField] private TextMeshProUGUI calender; // N : 날짜 텍스트
    [SerializeField] private TextMeshProUGUI book; // N : 책 개수 텍스트
    [SerializeField] private int bookNum;

    public EndingManager endingManager;
    public GameManager gameManager;     // J : GameManager에서 하루가 몇초인지 가져옴

    public GameObject heartTextObject;      // C :
    public GameObject levelUp;              // C :
    public GameObject levelDown;            // C :
    //private float time = 0;               // C :
    private float downTime = 0;             // C :

    // Start is called before the first frame update
    void Start()
    {
        // N : 스탯 초기화
        hungerStat.initStat(100, 100);
        happyStat.initStat(50, 100);
        temperatureStat.initStat(100, 100);
        //dangerStat.initStat(100, 100);

        // N : 레벨 초기화
        farmLv.initLv(1, 10);
        houseLv.initLv(1, 10);
        craftLv.initLv(1, 10);
        engineerLv.initLv(1, 15);
        heartLv.initLv(1, 20);

        // N : 캘린더 초기화
        calender.text = "day 01";
        // N : 책 개수 초기화
        book.text = "0 books";

        Invoke("dayAfter", gameManager.day);
    }

    // Update is called once per frame
    void Update()
    {
        // K :learningTime
        if(learningManager.isAILearning)
        {
            learningTime.text = learningManager.learningTime.ToString();
            learningTitle.alpha = 1;
        } else
        {
            learningTime.text = "";
            learningTitle.alpha = 0;
        }


        /*
        // C :
        if (levelUp.activeSelf == true)     // C :
        {
            time += Time.deltaTime;
            if (time > 2f)                      // C : 
            {
                levelUp.SetActive(false);
                time = 0;
            }
        }
        else
        {
            time = 0;
        }
        */

        // C :
        if (levelDown.activeSelf == true)     // C :
        {
            downTime += Time.deltaTime;
            if (downTime > 2f)                      // C : 
            {
                levelDown.SetActive(false);
                downTime = 0;
            }
        }
        else
        {
            downTime = 0;
        }
    }

    // N : 날짜 변화
    public void dayAfter()
    {
        if (gameManager.isEndingShow) return;
        // N : 캘린더 관리
        day++;
        if (day < 10) calender.text = "day " + "0" + day.ToString();
        else calender.text = "day " + day.ToString();

        // N : 90일 이후
        if (day > 90) endingManager.timeOutEnding();

        // N : 스탯 관리
        hungerStat.fCurrValue = hungerStat.fCurrValue + farmLv.fCurrValue - 10;
        happyStat.fCurrValue = happyStat.fCurrValue + heartLv.fCurrValue - 5;
        temperatureStat.fCurrValue = temperatureStat.fCurrValue + craftLv.fCurrValue - 10;

        // N : 엔딩 처리
        if (hungerStat.fCurrValue <= 0) endingManager.failHungry();
        else if (happyStat.fCurrValue <= 0) endingManager.failLonely();
        else if (temperatureStat.fCurrValue <= 0) endingManager.failCold();

        //N : AI와 대화 횟수 초기화
        gameManager.dayTalk = 0;

        // N : 하루마다 호출
        Invoke("dayAfter", gameManager.day);
    }

    public int currBookNum()
    {
        return bookNum;
    }

    // N : 책 줍기
    public void getBook()
    {
        bookNum++;
        book.text = bookNum.ToString() + " books";
    }

    // N : 책 쓰기
    public void useBook()
    {
        bookNum--;
        book.text = bookNum.ToString() + " books";
    }

    // N : 농사 배우기
    public void FarmStudy()
    {
        useBook();
        farmLv.fCurrValue++;
        hungerStat.fCurrValue += 50;

        if (farmLv.fCurrValue > 2)
        {
            if(farmLv.fCurrValue<5) GameObject.Find("Farm").transform.Find("Lv3-4").gameObject.SetActive(true); // N : 레벨 3-4
            else if (farmLv.fCurrValue < 7)
            {
                // N : 레벨 5-6
                GameObject.Find("Farm").transform.Find("Lv3-4").gameObject.SetActive(false);
                GameObject.Find("Farm").transform.Find("Lv5-6").gameObject.SetActive(true);
            }
            else if (farmLv.fCurrValue < 9)
            {
                // N : 레벨 7-8
                GameObject.Find("Farm").transform.Find("Lv5-6").gameObject.SetActive(false);
                GameObject.Find("Farm").transform.Find("Lv7-8").gameObject.SetActive(true);
            }
            else
            {
                // N : 레벨 9-10
                GameObject.Find("Farm").transform.Find("Lv7-8").gameObject.SetActive(false);
                GameObject.Find("Farm").transform.Find("Lv9-10").gameObject.SetActive(true);
            }
        }
    }

    // N : 건축 배우기
    public void HouseStudy()
    {
        useBook();
        houseLv.fCurrValue++;
        //dangerStat.fCurrValue += 50;

        if (houseLv.fCurrValue > 2)
        {
            if (houseLv.fCurrValue < 6) GameObject.Find("House").transform.Find("Lv3-5").gameObject.SetActive(true); // N : 레벨 3-4
            else if (houseLv.fCurrValue < 9)
            {
                // N : 레벨 5-6
                GameObject.Find("House").transform.Find("Lv3-5").gameObject.SetActive(false);
                GameObject.Find("House").transform.Find("Lv6-8").gameObject.SetActive(true);
            }
            else
            {
                // N : 레벨 9-10
                GameObject.Find("House").transform.Find("Lv6-8").gameObject.SetActive(false);
                GameObject.Find("House").transform.Find("Lv9-10").gameObject.SetActive(true);
            }
        }
    }

    // N : 공예 배우기
    public void CraftStudy()
    {
        useBook();
        craftLv.fCurrValue++;
        temperatureStat.fCurrValue += 50;

        if (craftLv.fCurrValue > 3)
        {
            if (craftLv.fCurrValue < 8)
            {
                GameObject.Find("Craft_Room").transform.Find("Lv1-3").gameObject.SetActive(false);
                GameObject.Find("Craft_Room").transform.Find("Lv4-7").gameObject.SetActive(true);
            }
            else
            {
                // N : 레벨 9-10
                GameObject.Find("Craft_Room").transform.Find("Lv4-7").gameObject.SetActive(false);
                GameObject.Find("Craft_Room").transform.Find("Lv8-10").gameObject.SetActive(true);
            }
        }
    }

    // N : 공학 배우기
    public void EngineerStudy()
    {
        useBook();
        engineerLv.fCurrValue++;

        // ~~
        if (engineerLv.fCurrValue > 2)
        {
            if (engineerLv.fCurrValue < 6) GameObject.Find("Lab").transform.Find("Lv3-5").gameObject.SetActive(true); // N : 레벨 3-4
            else if (engineerLv.fCurrValue < 9)
            {
                // N : 레벨 5-6
                GameObject.Find("Lab").transform.Find("Lv3-5").gameObject.SetActive(false);
                GameObject.Find("Lab").transform.Find("Lv6-8").gameObject.SetActive(true);
            }
            else
            {
                // N : 레벨 9-10
                GameObject.Find("Lab").transform.Find("Lv6-8").gameObject.SetActive(false);
                GameObject.Find("Lab").transform.Find("Lv9-10").gameObject.SetActive(true);
            }
        }

        // N : 엔딩 처리
        if (engineerLv.fCurrValue >= engineerLv.maxValue)
        {
            if (heartLv.fCurrValue > 17.0f) endingManager.successPeople(); // N : 공감 능력이 높은 경우
            else endingManager.successAI(); // N : 공감 능력이 낮은 경우
        }
    }

    // N : 공감 배우기 (말걸기 n = 0)
    public void HeartStudy(int n)
    {
        if (n == 0)
        {
            //useBook();
            heartLv.fCurrValue++;
            happyStat.fCurrValue += 5;
        }
        else
        {
            heartLv.fCurrValue += n;
            happyStat.fCurrValue += (5 * n);
        }

        // C : levelUp animation 실행하기
        if (n >= 0)
        {
            levelUp.transform.SetParent(heartTextObject.transform);
            levelUp.SetActive(true);
        }
        else
        {
            levelDown.SetActive(true);
        }


        if (heartLv.fCurrValue > 7)
        {
            if (heartLv.fCurrValue < 15) GameObject.Find("plant").transform.Find("Lv8-14").gameObject.SetActive(true); // N : 레벨 3-4
            else
            {
                // N : 레벨 9-10
                GameObject.Find("plant").transform.Find("Lv8-14").gameObject.SetActive(false);
                GameObject.Find("plant").transform.Find("Lv15-20").gameObject.SetActive(true);
            }
        }

        // N : 엔딩 처리
        if (happyStat.fCurrValue < 0 || heartLv.fCurrValue < 0) endingManager.failLonely();
        else if (heartLv.fCurrValue >= heartLv.maxValue)
        {
            if (engineerLv.fCurrValue > 13.0f) endingManager.successPeople(); // N : 공학 능력이 높은 경우
            else endingManager.successTwo(); // N : 공학 능력이 낮은 경우
        }
    }
}
