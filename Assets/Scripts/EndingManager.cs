using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public GameObject panel;
    public GameObject badHungry;

    public void failHungry()
    {
        Debug.Log("Hungry,,,");
        panel.SetActive(true);
        badHungry.SetActive(true);
    }

    public void failLonely()
    {

    }

    public void failCold()
    {

    }
}
