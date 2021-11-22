using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookScript : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D coll)   // Ai 충돌 감지 함수
    {
        this.gameObject.SetActive(false);
        Debug.Log("책 충돌");
    }
}
