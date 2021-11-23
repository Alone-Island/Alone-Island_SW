using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// K : ���� ũ���� �ؽ�Ʈ�� ���� Ŭ���� �Դϴ�
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
