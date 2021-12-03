using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningManager : MonoBehaviour
{
    public ScreenManager screenManager;     // C :
    public AIAction aiAction;               // C :
    public bool isAILearning = false;         // K : AI가 학습중인지 확인하는 변수
   
    private float initLearningTime = 4;
    public float learningTime = 5;

    public GameObject craftTextObject;      // C :
    public GameObject farmTextObject;      // C :
    public GameObject houseTextObject;      // C :
    public GameObject engineerTextObject;      // C :
    public GameObject levelUp;            // C :
    private float levelUpEffectTime = 0;

    private int learningId;

    public void CompleateLearning() // K : 
    {
        isAILearning = false;
        learningTime = 5;

        switch (learningId)
        {
            case 100:                       // C :
                screenManager.FarmStudy();

                // C : levelUp animation 실행하기
                levelUp.transform.SetParent(farmTextObject.transform);
                levelUp.SetActive(true);

                break;
            case 200:                       // C :               
                screenManager.HouseStudy();
               
                // C : levelUp animation 실행하기
                levelUp.transform.SetParent(houseTextObject.transform);
                levelUp.SetActive(true);

                break;
            case 300:                       // C :
                screenManager.CraftStudy();

                // C : levelUp animation 실행하기
                levelUp.transform.SetParent(craftTextObject.transform);
                levelUp.SetActive(true);

                break;
            case 400:                       // C :
                screenManager.EngineerStudy();

                // C : levelUp animation 실행하기
                levelUp.transform.SetParent(engineerTextObject.transform);
                levelUp.SetActive(true);

                break;
            default:
                Debug.Log("fail learning level up");
                break;
        }
    }
    
    public void WaitingLearning() // K : AI 학습 시간을 기다리는 함수
    {
        learningTime--;
        if (learningTime == 0)
        {
            CompleateLearning();
        } else
        {
            Invoke("WaitingLearning", 1);
        }
    }

    public void Learning(int id)            // C :
    {
        learningId = id;
        if (!isAILearning && !aiAction.isAICollisionToPlayer) {
            switch (learningId)
            {
                case 100:                       // C :
                    Debug.Log(learningId);
                    learningTime = initLearningTime + screenManager.farmLv.fCurrValue;
                    aiAction.GoToLearningPlace(-7, -7);
                    isAILearning = true;                                        
                    Invoke("WaitingLearning", 1);               
                    break;
                case 200:                       // C :
                    Debug.Log(learningId);
                    learningTime = initLearningTime + screenManager.houseLv.fCurrValue;
                    aiAction.GoToLearningPlace(10, 9);
                    isAILearning = true;
                    Invoke("WaitingLearning", 1);

                    break;
                case 300:                       // C :
                    Debug.Log(learningId);
                    learningTime = initLearningTime + screenManager.craftLv.fCurrValue;
                    aiAction.GoToLearningPlace(5, 0);
                    isAILearning = true;
                    Invoke("WaitingLearning", 1);

                    break;
                case 400:                       // C :
                    Debug.Log(learningId);
                    learningTime = initLearningTime + screenManager.engineerLv.fCurrValue;
                    aiAction.GoToLearningPlace(-5, 5);
                    isAILearning = true;
                    Invoke("WaitingLearning", 1);

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
            levelUpEffectTime += Time.deltaTime;
            if (levelUpEffectTime > 2f)                      // C : 
            {
                levelUp.SetActive(false);
                levelUpEffectTime = 0;
            }
        }
        else
        {
            levelUpEffectTime = 0;
        }
    }
}
