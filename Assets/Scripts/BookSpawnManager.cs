using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSpawnManager : MonoBehaviour
{
    // J : https://angliss.cc/random-gameobject-created/ ����
    public GameObject book;

    public GameObject farmObject;                       // C :
    public GameObject farmLearningObject;               // C :
    public GameObject houseObject;                      // C :
    public GameObject houseLearningObject;              // C :
    public GameObject craftObject;                      // C :
    public GameObject craftLearningObject;              // C :
    public GameObject labObject;                        // C :
    public GameObject labLearningObject;                // C :
    public GameObject playerObject;                     // C :
    private GameObject tempObject;                      // C :

    public GameManager gameManager;

    int count = 20;                  // J : �� å ����
    private BoxCollider2D area;     // J : �ڽ� �ݶ��̴��� ������ �������� ���� ����
    private List<GameObject> bookList = new List<GameObject>();

    // J : count��ŭ å ����
    void Start()
    {
        area = GetComponent<BoxCollider2D>();
        tempObject = new GameObject("tempObject");      // C :
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
        // C : �⺻���� ������ ��ġ �����ϴ� ����
        Vector2 basePosition = transform.position;  // J : ������Ʈ�� ��ġ
        Vector2 size = area.size;                   // J : box colider2d, �� ���� ũ�� ����

        // J : x, y�� ���� ��ǥ ���
        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = basePosition.y + Random.Range(-size.y / 2f, size.y / 2f);

        Vector3 spawnPos = new Vector3(posX, posY, 0);



        // C : book�� �����Ǹ� �ȵǴ� ��ġ�� �����ϴ� ����
        if (isLimit(tempObject, playerObject, spawnPos, 1))                     // C : ���� player ��ġ�� �����̿� book�� �������� �ʵ��� ����
        {
            return GetRandomPosition();
        }
        else if (isLimit(farmObject, farmLearningObject, spawnPos, 0))          // C :
        {
            return GetRandomPosition();
        }
        else if (isLimit(houseObject, houseLearningObject, spawnPos, 0))        // C :
        {
            return GetRandomPosition();
        }
        else if (isLimit(craftObject, craftLearningObject, spawnPos, 0))        // C :
        {
            return GetRandomPosition();
        }
        else if (isLimit(labObject, labLearningObject, spawnPos, 0))            // C :
        {
            return GetRandomPosition();
        }

        return spawnPos;    // J : ���� ��ġ return
    }


    // C : ������ book�� ���� ��ġ�� ���� ������ ������ true, �ƴϸ� false ��ȯ�ϴ� �Լ�
    // C : 
    private bool isLimit(GameObject baseObject, GameObject learningObject, Vector3 spawnPos, double margin)
    {
        // C : book�� �����Ǹ� �ȵǴ� ���� ���ϱ�
        Vector3 basePos = baseObject.transform.localPosition;
        Vector3 learningPos = learningObject.transform.localPosition;               // C :       
        Vector2 learningSize = learningObject.GetComponent<BoxCollider2D>().size;   // C :

        double[] learingArea = new double[] {learningPos.x - learningSize.x + basePos.x - 0.5 - margin,       // C :
                                           learningPos.x + learningSize.x + basePos.x + 0.5 + margin,
                                           learningPos.y - learningSize.y + basePos.y - 0.5 - margin,
                                           learningPos.y + learningSize.y + basePos.y + 0.5 + margin};

        // C : book�� �����Ǹ� �ȵǴ� ��ġ�� �ִ��� Ȯ��
        if (spawnPos.x >= learingArea[0] && spawnPos.x <= learingArea[1]              // C :
            && spawnPos.y >= learingArea[2] && spawnPos.y <= learingArea[3])
        {
            return true;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
