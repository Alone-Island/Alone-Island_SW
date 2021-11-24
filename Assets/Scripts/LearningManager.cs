using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningManager : MonoBehaviour
{
    public ScreenManager screenManager;     // C :
    public AIAction aiAction;               // C :
    public bool isAILearning = false;         // K : AI�� �н������� Ȯ���ϴ� ����
    public int learningTime = 10;

    public void CompleateLearning() // K : 
    {
        isAILearning = false;
    }
    
    public void WaitingLearning() // K : AI �н� �ð��� ��ٸ��� �Լ�
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
        switch (id)
        {
            case 100:                       // C :
                Debug.Log(id);
                screenManager.FarmStudy();
                aiAction.GoToLearningPlace(-7, -7);
                isAILearning = true;
                Invoke("WaitingLearning", 1);
                break;
            case 200:                       // C :
                Debug.Log(id);
                screenManager.HouseStudy();
                aiAction.GoToLearningPlace(10, 9);
                isAILearning = true;
                Invoke("WaitingLearning", 1);
                break;
            case 300:                       // C :
                Debug.Log(id);
                screenManager.CraftStudy();
                aiAction.GoToLearningPlace(7, 5);
                isAILearning = true;
                Invoke("WaitingLearning", 1);
                break;
            case 400:                       // C :
                Debug.Log(id);
                screenManager.EngineerStudy();
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
