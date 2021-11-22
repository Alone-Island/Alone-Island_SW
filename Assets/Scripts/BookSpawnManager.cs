using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSpawnManager : MonoBehaviour
{
    // J : https://angliss.cc/random-gameobject-created/ ����
    public GameObject book;
    public GameManager gameManager;

    int count = 3;                  // J : �� å ����
    private BoxCollider2D area;     // J : �ڽ� �ݶ��̴��� ������ �������� ���� ����
    private List<GameObject> bookList = new List<GameObject>();

    // J : count��ŭ å ����
    void Start()
    {
        area = GetComponent<BoxCollider2D>();

        StartCoroutine("Spawn");
    }

    // J : ���� ������Ʈ�� �����Ͽ� scene�� �߰�
    private IEnumerator Spawn()
    {
        for (int i = 0; i < count; i++) // J : count��ŭ å ����
        {
            Vector3 spawnPos = GetRandomPosition(); // J :���� ��ġ return

            // J : ����, ��ġ, ȸ������ �Ű������� �޾� ������Ʈ ����
            // J : Quaternion.identity <- ȸ���� 0
            GameObject instance = Instantiate(book, spawnPos, Quaternion.identity);
            bookList.Add(instance); // J : ������Ʈ ������ ���� ����Ʈ�� add
        }
        area.enabled = false;       // J : BoxCollider2D ����
        yield return new WaitForSeconds(gameManager.day);   // J : �Ϸ� ����

        for (int i = 0; i < count; i++) // J : å ����
            Destroy(bookList[i].gameObject);

        bookList.Clear();           // J : bookList ����
        area.enabled = true;        // J : BoxCollider2D �ѱ�
        StartCoroutine("Spawn");    // J : å �ٽ� ����
    }

    // J : �� ���� ������ ��ġ�� return
    private Vector2 GetRandomPosition()
    {
        Vector2 basePosition = transform.position;  // J : ������Ʈ�� ��ġ
        Vector2 size = area.size;                   // J : box colider2d, �� ���� ũ�� ����

        // J : x, y�� ���� ��ǥ ���
        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = basePosition.y + Random.Range(-size.y / 2f, size.y / 2f);

        Vector3 spawnPos = new Vector3(posX, posY, 0);

        return spawnPos;    // J : ���� ��ġ return
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
