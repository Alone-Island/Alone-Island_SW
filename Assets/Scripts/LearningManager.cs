using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningManager : MonoBehaviour
{
    public ScreenManager screenManager;     // C :
    public AIAction aiAction;               // C :

    public void Learning(int id)            // C :
    {
        switch (id)
        {
            case 100:                       // C :
                Debug.Log(id);
                screenManager.FarmStudy();
                aiAction.GoToStudyPlace(-7, -7);
                break;
            case 200:                       // C :
                Debug.Log(id);
                screenManager.HouseStudy();
                aiAction.GoToStudyPlace(10, 9);
                break;
            case 300:                       // C :
                Debug.Log(id);
                screenManager.CraftStudy();
                aiAction.GoToStudyPlace(7, 5);
                break;
            case 400:                       // C :
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
