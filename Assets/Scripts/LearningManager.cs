using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningManager : MonoBehaviour
{
    public ScreenManager screenManager;     // C :
    public AIAction aiAction;               // C :
    public bool isAILearning = false;         // K : AI가 학습중인지 확인하는 변수
    
    public void CompleateLearning()
    {
        isAILearning = false;
    }

    public void Learning(int id)            // C :
    {
        switch (id)
        {
            case 100:                       // C :
                Debug.Log(id);
                screenManager.FarmStudy();
                aiAction.GoToLearningPlace(-7, -7);
                isAILearning = true;
                Invoke("CompleateLearning", 10);
                break;
            case 200:                       // C :
                Debug.Log(id);
                screenManager.HouseStudy();
                aiAction.GoToLearningPlace(10, 9);
                isAILearning = true;
                Invoke("CompleateLearning", 10);
                break;
            case 300:                       // C :
                Debug.Log(id);
                screenManager.CraftStudy();
                aiAction.GoToLearningPlace(7, 5);
                isAILearning = true;
                Invoke("CompleateLearning", 10);
                break;
            case 400:                       // C :
                Debug.Log(id);
                screenManager.EngineerStudy();
                aiAction.GoToLearningPlace(-5, 5);
                isAILearning = true;
                Invoke("CompleateLearning", 10);
                break;
            default:
                Debug.Log("fail learning");
                break;
        }
    }
}
