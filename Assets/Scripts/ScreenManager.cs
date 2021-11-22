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

    private int day = 1;
    private int bookNum = 0;
    [SerializeField] private TextMeshProUGUI calender;
    [SerializeField] private TextMeshProUGUI book;

    public EndingManager endingManager;

    // Start is called before the first frame update
    void Start()
    {
        hungerStat.initStat(10, 100);
        happyStat.initStat(50, 100);
        temperatureStat.initStat(100, 100);
        //dangerStat.initStat(100, 100);

        farmLv.initLv(1, 10);
        houseLv.initLv(1, 10);
        craftLv.initLv(1, 10);
        engineerLv.initLv(1, 10);
        heartLv.initLv(1, 10);

        calender.text = "day 01";
        book.text = "0 books";

        Invoke("dayAfter", 20.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //하루의 변화
    public void dayAfter()
    {
        day++;
        if (day < 10) calender.text = "day " + "0" + day.ToString();
        else calender.text = "day " + day.ToString();

        hungerStat.fCurrValue -= 10;
        happyStat.fCurrValue -= 5;
        temperatureStat.fCurrValue -= 10;
        //dangerStat.fCurrValue -= 10;

        if (hungerStat.fCurrValue < 1) endingManager.failHungry();
        else if (happyStat.fCurrValue <= 0) endingManager.failLonely();
        else if (temperatureStat.fCurrValue <= 0) endingManager.failCold();

        Invoke("dayAfter", 20.0f);
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
        dangerStat.fCurrValue += 50;
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
    }

    // N : 공감 배우기
    public void HeartStudy()
    {
        useBook();
        heartLv.fCurrValue++;
        happyStat.fCurrValue += 50;
    }
}
