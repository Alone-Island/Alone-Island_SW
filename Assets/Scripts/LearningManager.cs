using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningManager : MonoBehaviour
{
    public ScreenManager screenManager;     // C :
    public AIAction aiAction;               // C :
    public bool isAILearning = false;         // K : AI가 학습중인지 확인하는 변수
    public int learningTime = 10;

    public GameObject craftTextObject;      // C :
    public GameObject farmTextObject;      // C :
    public GameObject houseTextObject;      // C :
    public GameObject engineerTextObject;      // C :
    public GameObject levelUp;            // C :
    private float time = 0;

    public void CompleateLearning() // K : 
    {
        isAILearning = false;
    }
    
    public void WaitingLearning() // K : AI 학습 시간을 기다리는 함수
    {
        learningTime--;
        if (learningTime == 0)
        {
            //Invoke("CompleateLearning", 1);
            CompleateLearning();
            learningTime = 10;
        } else
        {
            Invoke("WaitingLearning", 1);
        }
    }

    public void Learning(int id)            // C :
    {
        if (!isAILearning) {
            switch (id)
            {
                case 100:                       // C :
                    Debug.Log(id);
                    screenManager.FarmStudy();
                    aiAction.GoToLearningPlace(-7, -7);
                    isAILearning = true;
                    Invoke("WaitingLearning", 1);

                    // C : levelUp animation 실행하기
                    levelUp.transform.SetParent(farmTextObject.transform);
                    levelUp.SetActive(true);

                    break;
                case 200:                       // C :
                    Debug.Log(id);
                    screenManager.HouseStudy();
                    aiAction.GoToLearningPlace(10, 9);
                    isAILearning = true;
                    Invoke("WaitingLearning", 1);

                    // C : levelUp animation 실행하기
                    levelUp.transform.SetParent(houseTextObject.transform);
                    levelUp.SetActive(true);

                    break;
                case 300:                       // C :
                    Debug.Log(id);
                    screenManager.CraftStudy();
                    aiAction.GoToLearningPlace(7, 5);
                    isAILearning = true;
                    Invoke("WaitingLearning", 1);

                    // C : levelUp animation 실행하기
                    levelUp.transform.SetParent(craftTextObject.transform);
                    levelUp.SetActive(true);

                    break;
                case 400:                       // C :
                    Debug.Log(id);
                    screenManager.EngineerStudy();
                    aiAction.GoToLearningPlace(-5, 5);
                    isAILearning = true;
                    Invoke("WaitingLearning", 1);

                    // C : levelUp animation 실행하기
                    levelUp.transform.SetParent(engineerTextObject.transform);
                    levelUp.SetActive(true);

                    break;
                default:
                    Debug.Log("fail learning");
                    break;
            }
        }
    }

    void Update()
    {
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
    }
}
