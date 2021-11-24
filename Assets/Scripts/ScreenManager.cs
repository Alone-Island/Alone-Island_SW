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

    public LearningManager learningManager;
    public TextMeshProUGUI learningTime;

    private int day = 1; // N : ��¥
    //private int bookNum = 0; // N : å ����
    [SerializeField] private TextMeshProUGUI calender; // N : ��¥ �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI book; // N : å ���� �ؽ�Ʈ
    [SerializeField] private int bookNum;

    public EndingManager endingManager;
    public GameManager gameManager;     // J : GameManager���� �Ϸ簡 �������� ������

    // Start is called before the first frame update
    void Start()
    {
        // N : ���� �ʱ�ȭ
        hungerStat.initStat(100, 100);
<<<<<<< Updated upstream
        happyStat.initStat(100, 100);
=======
        happyStat.initStat(50, 100);
>>>>>>> Stashed changes
        temperatureStat.initStat(100, 100);
        //dangerStat.initStat(100, 100);

        // N : ���� �ʱ�ȭ
        farmLv.initLv(1, 10);
        houseLv.initLv(1, 10);
        craftLv.initLv(1, 10);
        engineerLv.initLv(1, 15);
        heartLv.initLv(1, 20);

        // N : Ķ���� �ʱ�ȭ
        calender.text = "day 01";
        // N : å ���� �ʱ�ȭ
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

    // N : ��¥ ��ȭ
    public void dayAfter()
    {
        // N : Ķ���� ����
        day++;
        if (day < 10) calender.text = "day " + "0" + day.ToString();
        else calender.text = "day " + day.ToString();

        // N : 90�� ����
        if (day > 90) endingManager.timeOutEnding();

        // N : ���� ����
        hungerStat.fCurrValue = hungerStat.fCurrValue + farmLv.fCurrValue - 10;
        happyStat.fCurrValue = happyStat.fCurrValue + heartLv.fCurrValue - 5;
        temperatureStat.fCurrValue = temperatureStat.fCurrValue + craftLv.fCurrValue - 10;

        // N : ���� ó��
        if (hungerStat.fCurrValue <= 0) endingManager.failHungry();
        else if (happyStat.fCurrValue <= 0) endingManager.failLonely();
        else if (temperatureStat.fCurrValue <= 0) endingManager.failCold();

        // N : �Ϸ縶�� ȣ��
        Invoke("dayAfter", gameManager.day);
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
        //dangerStat.fCurrValue += 50;
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

        // N : ���� ó��
        if (engineerLv.fCurrValue >= engineerLv.maxValue)
        {
            if (heartLv.fCurrValue > 17.0f) endingManager.successPeople(); // N : ���� �ɷ��� ���� ���
            else endingManager.successAI(); // N : ���� �ɷ��� ���� ���
        }
    }

    // N : ���� ���� (���ɱ⸸ �ϴ� ��� n = 0)
    public void HeartStudy(int n)
    {
        if (n == 0) happyStat.fCurrValue += 5;
        //useBook();
        heartLv.fCurrValue += n;
        happyStat.fCurrValue += (10 * n);

        // N : ���� ó��
        if (happyStat.fCurrValue <= 0) endingManager.failLonely();
        else if (heartLv.fCurrValue >= heartLv.maxValue)
        {
            if (engineerLv.fCurrValue > 13.0f) endingManager.successPeople(); // N : ���� �ɷ��� ���� ���
            else endingManager.successTwo(); // N : ���� �ɷ��� ���� ���
        }
    }
}
