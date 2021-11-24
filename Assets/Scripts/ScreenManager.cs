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

    private int day = 1; // N : 날짜
    //private int bookNum = 0; // N : 책 개수
    [SerializeField] private TextMeshProUGUI calender; // N : 날짜 텍스트
    [SerializeField] private TextMeshProUGUI book; // N : 책 개수 텍스트
    [SerializeField] private int bookNum;

    public EndingManager endingManager;
    public GameManager gameManager;     // J : GameManager에서 하루가 몇초인지 가져옴

    // Start is called before the first frame update
    void Start()
    {
        // N : 스탯 초기화
        hungerStat.initStat(100, 100);
<<<<<<< Updated upstream
        happyStat.initStat(100, 100);
=======
        happyStat.initStat(50, 100);
>>>>>>> Stashed changes
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
        } else
        {
            learningTime.text = "";
        }
    }

    // N : 날짜 변화
    public void dayAfter()
    {
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

        // N : 하루마다 호출
        Invoke("dayAfter", gameManager.day);
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
    }

    // N : 건축 배우기
    public void HouseStudy()
    {
        useBook();
        houseLv.fCurrValue++;
        //dangerStat.fCurrValue += 50;
    }

    // N : 공예 배우기
    public void CraftStudy()
    {
        useBook();
        craftLv.fCurrValue++;
        temperatureStat.fCurrValue += 50;
    }

    // N : 공학 배우기
    public void EngineerStudy()
    {
        useBook();
        engineerLv.fCurrValue++;

        // N : 엔딩 처리
        if (engineerLv.fCurrValue >= engineerLv.maxValue)
        {
            if (heartLv.fCurrValue > 17.0f) endingManager.successPeople(); // N : 공감 능력이 높은 경우
            else endingManager.successAI(); // N : 공감 능력이 낮은 경우
        }
    }

    // N : 공감 배우기 (말걸기만 하는 경우 n = 0)
    public void HeartStudy(int n)
    {
        if (n == 0) happyStat.fCurrValue += 5;
        //useBook();
        heartLv.fCurrValue += n;
        happyStat.fCurrValue += (10 * n);

        // N : 엔딩 처리
        if (happyStat.fCurrValue <= 0) endingManager.failLonely();
        else if (heartLv.fCurrValue >= heartLv.maxValue)
        {
            if (engineerLv.fCurrValue > 13.0f) endingManager.successPeople(); // N : 공학 능력이 높은 경우
            else endingManager.successTwo(); // N : 공학 능력이 낮은 경우
        }
    }
}
