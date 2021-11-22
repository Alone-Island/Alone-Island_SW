using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenManager : MonoBehaviour
{
    public PlayerStat hungerStat; // N : ����� ����
    public PlayerStat happyStat; // N : �ູ ����
    public PlayerStat temperatureStat; // N : ü�� ����
    public PlayerStat dangerStat; // N : ���� ����

    public AILevel farmLv; // N : ��� ����
    public AILevel houseLv; // N : ���� ����
    public AILevel craftLv; // N : ���� ����
    public AILevel engineerLv; // N : ���� ����
    public AILevel heartLv; // N : ���� ����

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

    //�Ϸ��� ��ȭ
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

    // N : å �ݱ�
    public void getBook()
    {
        bookNum++;
        book.text = bookNum.ToString() + " books";
    }

    // N : å ����
    public void useBook()
    {
        bookNum--;
        book.text = bookNum.ToString() + " books";
    }

    // N : ��� ����
    public void FarmStudy()
    {
        useBook();
        farmLv.fCurrValue++;
        hungerStat.fCurrValue += 50;
    }

    // N : ���� ����
    public void HouseStudy()
    {
        useBook();
        houseLv.fCurrValue++;
        dangerStat.fCurrValue += 50;
    }

    // N : ���� ����
    public void CraftStudy()
    {
        useBook();
        craftLv.fCurrValue++;
        temperatureStat.fCurrValue += 50;
    }

    // N : ���� ����
    public void EngineerStudy()
    {
        useBook();
        engineerLv.fCurrValue++;
    }

    // N : ���� ����
    public void HeartStudy()
    {
        useBook();
        heartLv.fCurrValue++;
        happyStat.fCurrValue += 50;
    }
}
