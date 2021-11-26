using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSpawnManager : MonoBehaviour
{
    // J : https://angliss.cc/random-gameobject-created/ ����
    public GameObject book;
    
    public GameObject farmLearning_;         // C :
    /*
    public GameObject houseLearningIcon;        // C :
    public GameObject craftLearningIcon;        // C :
    public GameObject labLearningIcon;          // C :
    */
    
    public BoxCollider2D farmLearningArea;
    /*
    public BoxCollider2D houseLearningArea;
    public BoxCollider2D craftLearningArea;
    public BoxCollider2D labLearningArea;
    */

    public GameManager gameManager;

    int count = 20;                  // J : �� å ����
    private BoxCollider2D area;     // J : �ڽ� �ݶ��̴��� ������ �������� ���� ����
    private List<GameObject> bookList = new List<GameObject>();


    //Vector3 farmLearningPosition = farmLearningIcon.transform.localPosition;
    // Debug.Log(farmLearningPosition);
    // Debug.Log(farmLearningIcon.transform.localScale);
    //RectTransform farmLearningIconSize = farmLearningIcon.GetComponent<RectTransform>();
    //Debug.Log(farmLearningIcon.GetComponent<RectTransform>().rect.width);

    // J : count��ŭ å ����
    void Start()
    {
        area = GetComponent<BoxCollider2D>();

        /*Vector3 farmLearningPosition = farmLearningIcon.transform.localPosition;
        Debug.Log(rect);*/
        //Debug.Log(farmLearningIcon.GetComponent<RectTransform>().rect.height);

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

        /*Vector3 farmLearningPosition = farmLearningIcon.transform.localPosition;
        //Debug.Log(typeof(farmLearningPosition));
        //Debug.Log(spawnPos.x >= (farmLearningPosition.x - farmLearningIcon.transform.localScale.x / 2));
        if (spawnPos.x >= farmLearningPosition.x - farmLearningIcon.transform.localScale.x / 2
            && spawnPos.x <= farmLearningPosition.x + farmLearningIcon.transform.localScale.x / 2
            && spawnPos.y >= farmLearningPosition.y - farmLearningIcon.transform.localScale.y / 2
            && spawnPos.y <= farmLearningPosition.y + farmLearningIcon.transform.localScale.y / 2)
        {
            //Debug.Log("hi");

            //GetRandomPosition();
        }
        if (spawnPos.x <= farmLearningPosition.x - farmLearningIcon.transform.localScale.x / 2
            || spawnPos.x >= farmLearningPosition.x + farmLearningIcon.transform.localScale.x / 2
            || spawnPos.y <= farmLearningPosition.y - farmLearningIcon.transform.localScale.y / 2
            || spawnPos.y >= farmLearningPosition.y + farmLearningIcon.transform.localScale.y / 2)
        {
           // Debug.Log("hello");

            //GetRandomPosition();
        }*/

        Vector3 farmLearningPosition = farmLearning_.transform.localPosition;
        
        Vector2 farmLearningSize = farmLearningArea.size;
        /*Vector2 houseLearningSize = houseLearningArea.size;
        Vector2 craftLearningSize = craftLearningArea.size;
        Vector2 labLearningSize = labLearningArea.size;*/
        float[] farmArea = new float[] {farmLearningPosition.x - farmLearningSize.x -(float)1,
                                           farmLearningPosition.x + farmLearningSize.x -(float)1,
                                           farmLearningPosition.y - farmLearningSize.y +(float)1,
                                           farmLearningPosition.y + farmLearningSize.y +(float)1};

        //Debug.Log("0 : " + farmArea[0] + ", 1: " + farmArea[1] + ", 2: " + farmArea[2] + ", 3:" + farmArea[3]);

        if (spawnPos.x >= farmArea[0] && spawnPos.x <= farmArea[1]
            && spawnPos.y >= farmArea[2] && spawnPos.y <= farmArea[3])
        {
            return GetRandomPosition();
            //Debug.Log("spawnPos.x : " + spawnPos.x + ", spawnPos.y : " + spawnPos.y);
        }

        return spawnPos;    // J : ���� ��ġ return
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
