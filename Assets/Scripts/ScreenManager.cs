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
    public TextMeshProUGUI learningTitle;

    private int day = 1; // N : ��¥
    //private int bookNum = 0; // N : å ����
    [SerializeField] private TextMeshProUGUI calender; // N : ��¥ �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI book; // N : å ���� �ؽ�Ʈ
    [SerializeField] private int bookNum;

    public EndingManager endingManager;
    public GameManager gameManager;     // J : GameManager���� �Ϸ簡 �������� ������

    public GameObject heartTextObject;      // C :
    public GameObject levelUp;              // C :
    public GameObject levelDown;            // C :
    //private float time = 0;               // C :
    private float downTime = 0;             // C :

    // Start is called before the first frame update
    void Start()
    {
        // N : ���� �ʱ�ȭ
        hungerStat.initStat(100, 100);
        happyStat.initStat(50, 100);
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

    // N : ��¥ ��ȭ
    public void dayAfter()
    {
        if (gameManager.isEndingShow) return;
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

        //N : AI�� ��ȭ Ƚ�� �ʱ�ȭ
        gameManager.dayTalk = 0;

        // N : �Ϸ縶�� ȣ��
        Invoke("dayAfter", gameManager.day);
    }

    public int currBookNum()
    {
        return bookNum;
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

        if (farmLv.fCurrValue > 2)
        {
            if(farmLv.fCurrValue<5) GameObject.Find("Farm").transform.Find("Lv3-4").gameObject.SetActive(true); // N : ���� 3-4
            else if (farmLv.fCurrValue < 7)
            {
                // N : ���� 5-6
                GameObject.Find("Farm").transform.Find("Lv3-4").gameObject.SetActive(false);
                GameObject.Find("Farm").transform.Find("Lv5-6").gameObject.SetActive(true);
            }
            else if (farmLv.fCurrValue < 9)
            {
                // N : ���� 7-8
                GameObject.Find("Farm").transform.Find("Lv5-6").gameObject.SetActive(false);
                GameObject.Find("Farm").transform.Find("Lv7-8").gameObject.SetActive(true);
            }
            else
            {
                // N : ���� 9-10
                GameObject.Find("Farm").transform.Find("Lv7-8").gameObject.SetActive(false);
                GameObject.Find("Farm").transform.Find("Lv9-10").gameObject.SetActive(true);
            }
        }
    }

    // N : ���� ����
    public void HouseStudy()
    {
        useBook();
        houseLv.fCurrValue++;
        //dangerStat.fCurrValue += 50;

        if (houseLv.fCurrValue > 2)
        {
            if (houseLv.fCurrValue < 6) GameObject.Find("House").transform.Find("Lv3-5").gameObject.SetActive(true); // N : ���� 3-4
            else if (houseLv.fCurrValue < 9)
            {
                // N : ���� 5-6
                GameObject.Find("House").transform.Find("Lv3-5").gameObject.SetActive(false);
                GameObject.Find("House").transform.Find("Lv6-8").gameObject.SetActive(true);
            }
            else
            {
                // N : ���� 9-10
                GameObject.Find("House").transform.Find("Lv6-8").gameObject.SetActive(false);
                GameObject.Find("House").transform.Find("Lv9-10").gameObject.SetActive(true);
            }
        }
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

    // N : ���� ���� (���ɱ� n = 0)
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

        // C : levelUp animation �����ϱ�
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
            if (heartLv.fCurrValue < 15) GameObject.Find("plant").transform.Find("Lv8-14").gameObject.SetActive(true); // N : ���� 3-4
            else
            {
                // N : ���� 9-10
                GameObject.Find("plant").transform.Find("Lv8-14").gameObject.SetActive(false);
                GameObject.Find("plant").transform.Find("Lv15-20").gameObject.SetActive(true);
            }
        }

        // N : ���� ó��
        if (happyStat.fCurrValue < 0 || heartLv.fCurrValue < 0) endingManager.failLonely();
        else if (heartLv.fCurrValue >= heartLv.maxValue)
        {
            if (engineerLv.fCurrValue > 13.0f) endingManager.successPeople(); // N : ���� �ɷ��� ���� ���
            else endingManager.successTwo(); // N : ���� �ɷ��� ���� ���
        }
    }
}
