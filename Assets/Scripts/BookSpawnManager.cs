using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSpawnManager : MonoBehaviour
{
    // J : https://angliss.cc/random-gameobject-created/ 참조
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

    int count = 20;                  // J : 찍어낼 책 개수
    private BoxCollider2D area;     // J : 박스 콜라이더의 사이즈 가져오기 위한 변수
    private List<GameObject> bookList = new List<GameObject>();


    //Vector3 farmLearningPosition = farmLearningIcon.transform.localPosition;
    // Debug.Log(farmLearningPosition);
    // Debug.Log(farmLearningIcon.transform.localScale);
    //RectTransform farmLearningIconSize = farmLearningIcon.GetComponent<RectTransform>();
    //Debug.Log(farmLearningIcon.GetComponent<RectTransform>().rect.width);

    // J : count만큼 책 스폰
    void Start()
    {
        area = GetComponent<BoxCollider2D>();

        /*Vector3 farmLearningPosition = farmLearningIcon.transform.localPosition;
        Debug.Log(rect);*/
        //Debug.Log(farmLearningIcon.GetComponent<RectTransform>().rect.height);

        StartCoroutine("Spawn");
    }

    // J : 게임 오브젝트를 복제하여 scene에 추가
    private IEnumerator Spawn()
    {
        for (int i = 0; i < count; i++) // J : count만큼 책 생성
        {
            Vector3 spawnPos = GetRandomPosition(); // J :랜덤 위치 return

            // J : 원본, 위치, 회전값을 매개변수로 받아 오브젝트 복제
            // J : Quaternion.identity <- 회전값 0
            GameObject instance = Instantiate(book, spawnPos, Quaternion.identity);
            bookList.Add(instance); // J : 오브젝트 관리를 위해 리스트에 add
        }
        area.enabled = false;       // J : BoxCollider2D 끄기
        yield return new WaitForSeconds(gameManager.day);   // J : 하루 지남

        for (int i = 0; i < count; i++) // J : 책 삭제
            Destroy(bookList[i].gameObject);

        bookList.Clear();           // J : bookList 비우기
        area.enabled = true;        // J : BoxCollider2D 켜기
        StartCoroutine("Spawn");    // J : 책 다시 스폰
    }

    // J : 맵 내의 랜덤한 위치를 return
    private Vector2 GetRandomPosition()
    {
        Vector2 basePosition = transform.position;  // J : 오브젝트의 위치
        Vector2 size = area.size;                   // J : box colider2d, 즉 맵의 크기 벡터

        // J : x, y축 랜덤 좌표 얻기
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

        return spawnPos;    // J : 랜덤 위치 return
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
