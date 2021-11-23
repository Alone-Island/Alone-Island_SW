using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningManager : MonoBehaviour
{
    public ScreenManager screenManager;
    public AIAction aiAction;

    public void Learning(int id)
    {
        switch (id)
        {
            case 100:
                Debug.Log(id);
                screenManager.FarmStudy();
                aiAction.GoToStudyPlace(-7, -7);
                break;
            case 200:
                Debug.Log(id);
                screenManager.HouseStudy();
                aiAction.GoToStudyPlace(10, 9);
                break;
            case 300:
                Debug.Log(id);
                screenManager.CraftStudy();
                aiAction.GoToStudyPlace(7, 5);
                break;
            case 400:
                Debug.Log(id);
                screenManager.EngineerStudy();
                aiAction.GoToStudyPlace(-5, 5);
                break;
            default:
                Debug.Log("fail learning");
                break;
        }
    }
}
