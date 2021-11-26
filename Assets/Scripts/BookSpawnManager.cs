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
    
    public BoxCollider2D farmLearningArea;      // C :
    public BoxCollider2D houseLearningArea;     // C :
    public BoxCollider2D craftLearningArea;     // C :
    public BoxCollider2D labLearningArea;       // C :

    public GameManager gameManager;

    int count = 20;                  // J : 찍어낼 책 개수
    private BoxCollider2D area;     // J : 박스 콜라이더의 사이즈 가져오기 위한 변수
    private List<GameObject> bookList = new List<GameObject>();


    // J : count만큼 책 스폰
    void Start()
    {
        area = GetComponent<BoxCollider2D>();

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
        Vector3 farmPosition = farmObject.transform.localPosition;                      // C :
        Vector3 farmLearningPosition = farmLearningObject.transform.localPosition;      // C :       
        Vector2 farmLearningSize = farmLearningArea.size;                               // C :
        double[] farmArea = new double[] {farmLearningPosition.x - farmLearningSize.x + farmPosition.x - 0.5,       // C :
                                           farmLearningPosition.x + farmLearningSize.x + farmPosition.x + 0.5,
                                           farmLearningPosition.y - farmLearningSize.y + farmPosition.y - 0.5,
                                           farmLearningPosition.y + farmLearningSize.y + farmPosition.y + 0.5};
        
        Vector3 housePosition = houseObject.transform.localPosition;                      // C :
        Vector3 houseLearningPosition = houseLearningObject.transform.localPosition;      // C :       
        Vector2 houseLearningSize = houseLearningArea.size;                               // C :
        double[] houseArea = new double[] {houseLearningPosition.x - houseLearningSize.x + housePosition.x - 0.5,       // C :
                                           houseLearningPosition.x + houseLearningSize.x + housePosition.x + 0.5,
                                           houseLearningPosition.y - houseLearningSize.y + housePosition.y - 0.5,
                                           houseLearningPosition.y + houseLearningSize.y + housePosition.y + 0.5};
        
        Vector3 craftPosition = craftObject.transform.localPosition;                      // C :
        Vector3 craftLearningPosition = craftLearningObject.transform.localPosition;      // C :       
        Vector2 craftLearningSize = craftLearningArea.size;                               // C :
        double[] craftArea = new double[] {craftLearningPosition.x - craftLearningSize.x + craftPosition.x - 0.5,       // C :
                                           craftLearningPosition.x + craftLearningSize.x + craftPosition.x + 0.5,
                                           craftLearningPosition.y - craftLearningSize.y + craftPosition.y - 0.5,
                                           craftLearningPosition.y + craftLearningSize.y + craftPosition.y + 0.5};

        Vector3 labPosition = labObject.transform.localPosition;                        // C :
        Vector3 labLearningPosition = labLearningObject.transform.localPosition;        // C :       
        Vector2 labLearningSize = labLearningArea.size;                                 // C :
        double[] labArea = new double[] {labLearningPosition.x - labLearningSize.x + labPosition.x - 0.5,       // C :
                                           labLearningPosition.x + labLearningSize.x + labPosition.x + 0.5,
                                           labLearningPosition.y - labLearningSize.y + labPosition.y - 0.5,
                                           labLearningPosition.y + labLearningSize.y + labPosition.y + 0.5};

        // C : 
        if (spawnPos.x >= farmArea[0] && spawnPos.x <= farmArea[1]              // C :
            && spawnPos.y >= farmArea[2] && spawnPos.y <= farmArea[3])
        {
            return GetRandomPosition();
        }
        else if (spawnPos.x >= houseArea[0] && spawnPos.x <= houseArea[1]       // C :
            && spawnPos.y >= houseArea[2] && spawnPos.y <= houseArea[3])
        {
            return GetRandomPosition();
        }
        else if (spawnPos.x >= craftArea[0] && spawnPos.x <= craftArea[1]       // C :
           && spawnPos.y >= craftArea[2] && spawnPos.y <= craftArea[3])
        {
            return GetRandomPosition();
        }
        else if (spawnPos.x >= labArea[0] && spawnPos.x <= labArea[1]           // C :
           && spawnPos.y >= labArea[2] && spawnPos.y <= labArea[3])
        {
            return GetRandomPosition();
        }

        return spawnPos;    // J : 랜덤 위치 return
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
