using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookScript : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D coll)   // Ai �浹 ���� �Լ�
    {
        this.gameObject.SetActive(false);
        Debug.Log("å �浹");
    }
}
