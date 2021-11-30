using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSpawnManager : MonoBehaviour
{
    // J : https://angliss.cc/random-gameobject-created/ 참조
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

    int count = 20;                  // J : 찍어낼 책 개수
    private BoxCollider2D area;     // J : 박스 콜라이더의 사이즈 가져오기 위한 변수
    private List<GameObject> bookList = new List<GameObject>();

    // J : count만큼 책 스폰
    void Start()
    {
        area = GetComponent<BoxCollider2D>();
        tempObject = new GameObject("tempObject");      // C :
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
        // C : 기본적인 랜덤한 위치 생성하는 과정
        Vector2 basePosition = transform.position;  // J : 오브젝트의 위치
        Vector2 size = area.size;                   // J : box colider2d, 즉 맵의 크기 벡터

        // J : x, y축 랜덤 좌표 얻기
        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = basePosition.y + Random.Range(-size.y / 2f, size.y / 2f);

        Vector3 spawnPos = new Vector3(posX, posY, 0);



        // C : book이 생성되면 안되는 위치를 제한하는 과정
        if (isLimit(tempObject, playerObject, spawnPos, 1))                     // C : 현재 player 위치의 가까이에 book이 생성되지 않도록 설정
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

        return spawnPos;    // J : 랜덤 위치 return
    }


    // C : 생성한 book의 랜덤 위치가 제한 지역에 있으면 true, 아니면 false 반환하는 함수
    // C : 
    private bool isLimit(GameObject baseObject, GameObject learningObject, Vector3 spawnPos, double margin)
    {
        // C : book이 생성되면 안되는 범위 구하기
        Vector3 basePos = baseObject.transform.localPosition;
        Vector3 learningPos = learningObject.transform.localPosition;               // C :       
        Vector2 learningSize = learningObject.GetComponent<BoxCollider2D>().size;   // C :

        double[] learingArea = new double[] {learningPos.x - learningSize.x + basePos.x - 0.5 - margin,       // C :
                                           learningPos.x + learningSize.x + basePos.x + 0.5 + margin,
                                           learningPos.y - learningSize.y + basePos.y - 0.5 - margin,
                                           learningPos.y + learningSize.y + basePos.y + 0.5 + margin};

        // C : book이 생성되면 안되는 위치에 있는지 확인
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
