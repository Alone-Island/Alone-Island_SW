using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// K : 엔딩 크레딧 텍스트를 위한 클래스 입니다
public class EndingCreditsManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        Vector2 pos = transform.position;
        pos.y += 0.015f;
        transform.position = pos;

        if(transform.position.y > 15)
        {
            SceneManager.LoadScene("GameMenu");
        }
    }
}
