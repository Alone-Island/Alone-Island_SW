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

    public BoxCollider2D farmLearningArea;      // C :
    public BoxCollider2D houseLearningArea;     // C :
    public BoxCollider2D craftLearningArea;     // C :
    public BoxCollider2D labLearningArea;       // C :

    public GameManager gameManager;

    int count = 20;                  // J : �� å ����
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
        // C : �⺻���� ������ ��ġ �����ϴ� ����
        Vector2 basePosition = transform.position;  // J : ������Ʈ�� ��ġ
        Vector2 size = area.size;                   // J : box colider2d, �� ���� ũ�� ����

        // J : x, y�� ���� ��ǥ ���
        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = basePosition.y + Random.Range(-size.y / 2f, size.y / 2f);

        Vector3 spawnPos = new Vector3(posX, posY, 0);



        // C : book�� �����Ǹ� �ȵǴ� ��ġ�� �����ϴ� ����
        // C : ���� player ��ġ�� �����̿� book�� �������� �ʵ��� ����
        Vector3 playerPos = playerObject.transform.localPosition;               // C :       
        Vector2 playerSize = playerObject.GetComponent<BoxCollider2D>().size;
        double[] limitArea = new double[] {playerPos.x - playerSize.x - 1,       // C :
                                           playerPos.x + playerSize.x + 1,
                                           playerPos.y - playerSize.y - 1,
                                           playerPos.y + playerSize.y + 1};
        if (spawnPos.x >= limitArea[0] && spawnPos.x <= limitArea[1]              // C :
            && spawnPos.y >= limitArea[2] && spawnPos.y <= limitArea[3])
        {
            return GetRandomPosition();
        }


        if (isLimit(farmObject, farmLearningObject, farmLearningArea, spawnPos))                // C :
        {
            return GetRandomPosition();
        }
        else if (isLimit(houseObject, houseLearningObject, houseLearningArea, spawnPos))        // C :
        {
            return GetRandomPosition();
        }
        else if (isLimit(craftObject, craftLearningObject, craftLearningArea, spawnPos))        // C :
        {
            return GetRandomPosition();
        }
        else if (isLimit(labObject, labLearningObject, labLearningArea, spawnPos))              // C :
        {
            return GetRandomPosition();
        }

        return spawnPos;    // J : ���� ��ġ return
    }


    // C : ������ book�� ���� ��ġ�� ���� ������ ������ true, �ƴϸ� false ��ȯ�ϴ� �Լ�
    // C : 
    private bool isLimit(GameObject baseObject, GameObject learningObject, BoxCollider2D learningColl, Vector3 spawnPos)
    {
        // C : book�� �����Ǹ� �ȵǴ� ���� ���ϱ�
        Vector3 basePos = baseObject.transform.localPosition;                       // C :
        Vector3 learningPos = learningObject.transform.localPosition;               // C :       
        Vector2 learningSize = learningColl.size;                                   // C :
        double[] learingArea = new double[] {learningPos.x - learningSize.x + basePos.x - 0.5,       // C :
                                           learningPos.x + learningSize.x + basePos.x + 0.5,
                                           learningPos.y - learningSize.y + basePos.y - 0.5,
                                           learningPos.y + learningSize.y + basePos.y + 0.5};

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
