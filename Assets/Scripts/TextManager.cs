using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// K : �ؽ�Ʈ�� ȿ���� �ֱ� ���� �Ŵ����Դϴ�.
public class TextManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI dialog; // K : text ������Ʈ�� �޾ƿ��� ���� �����Դϴ�. > using TMPro;          
    public GameManager manager;
    public EndingManager endingManager;
    public string[] fullText;

    private int endingCode = DataController.Instance.endingData.currentEndingCode;

    // K : synopsys�� �ؽ�Ʈ��(���� ����)�� �迭�Դϴ�.
    private string[] synopsysFullText = { 
        "���� �κ������� K...\nAI �κ��� �����ϱ� ����\n�����ǿ����� ���� �ð��� ����... ", 
        "����... �ϼ��ߴ�...\n���� ����, AI �κ� NJ-C!!!!\n���� ���� ������ ���� ��...",
        "�ƴ�...����??",
        "���� ��� �ִ� ������ ���� �Ǿ��־���...",
        "����� ��� �����ְ�..\n������� ���� ������ �ʴ´�!!\n���ڱ� ���ε��� ȥ�� ���� �Ǵٴ�?!?",
        "������ �����Դ� NJ-C�� �־�!\n���� ���뿡 �Ұ��ϴ� �� �κ��� �н����Ѽ�\n�� ������ ��Ƴ��� ����! "
    };

    private string[] happySosoLifeFullText = {
        "���� 90���� �������.",
        "�̷� �Ϸ��Ϸ絵 �������� �ʳ׿�.",
        "�����ε� �� ��������. �ڻ��."
    };    
    private string[] happyAIFullText = {
        "�ڻ��! �ڻ���� ����ĥ �κ����� �ܶ� ������.",
        "�� ���� �ɷ��� �̷��� �����߳׿�.",
        "������ ���� �ٰ��� �ڻ���� ����Կ�.",
        "�ڻ�ڻ��!!��!! �ڹڻ��!���! �ڻ��!!"
    };    
    private string[] happyPeopleFullText = {
        "�ڻ��!! \n���� ��ű⸦ ��������!!!",
        "��� ���� ���ݾ� ����� �־��µ�... \n���� �ϼ��̿���!",
        "���� ���� ��ű⸦ ����غ���!!",
        "ġ...����....ġ����..��...�����...",
    };    
    private string[] happyTwoFullText = {
        "�ڻ��! ���� ������ ���� �ɾ����� �츮 ���� ���������!",
        "�ڻ�԰��� �Ϸ��Ϸ簡 ���� ��ſ���",
        "�ڻ�Ե� �׷������� ���ھ��"
    };


    string subText; // K : synopsys�� �ؽ�Ʈ(�� ����) �Ϻθ� �����ϱ� ���� �����Դϴ�.
    int currentPoint = 0; // K : synopsysFullText���� ���� �����Ͱ� ����ִ��� �����ϱ� ���� �����Դϴ�.

    public bool isTyping = true; // K : ���� ���ڰ� ȭ�鿡 Ÿ���εǰ� �ִ��� Ȯ���ϱ� ���� �����Դϴ�.
    bool isSkipPart = false;

    void Start() {
        if (endingCode == 0)             // K : ���� �ڵ尡 0�� ��찡 �ʱ�ȭ �����̹Ƿ� ���� ���۽� �ó�ý� �켱
        {
            fullText = synopsysFullText;
            
        }       
        else if(endingCode > 100)                                            // K : �����ڵ尡 100���� ū ��� ���ǿ���
        {
            switch (endingCode)
            {
                case 101:
                    fullText = happySosoLifeFullText;
                    break;
                case 102:
                    fullText = happyTwoFullText;
                    break;
                case 103:
                    fullText = happyAIFullText;
                    break;
                case 104:
                    fullText = happyPeopleFullText;
                    break;
                default:
                    Debug.Log("���ǿ��� ����");
                    break;
            }
        }
        StartCoroutine("TypingAction", 0);          // K : ��ũ��Ʈ�� ���۰� ���ÿ� �ó�ý��� Ÿ������ �����ϴ� �ڵ��Դϴ�.
    }
    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))    // K : �����̽��ٸ� ������ ��
        {

            if (manager.isTheEnd)
            {
                if (endingCode < 100) // ��忡���ڵ� < 100, ���ǿ����� > 100
                {
                    SceneManager.LoadScene("GameMenu"); // K : ��忣���� ������ �ٷ� ���� �޴��� ���ư��ϴ�.
                }
                else
                {
                    SceneManager.LoadScene("EndingCredits"); // K : ���ǿ����ÿ��� ����ũ������ ������
                }
            } else
            {
                if (!isTyping)  // K : ���� ���ڰ� ȭ�鿡 Ÿ���� �ǰ� ���� ���� ��
                {
                    // K : �����̽��ٸ� ������ �� ���� ���ڰ� ȭ�鿡 Ÿ���� �ǰ� ���� �ʴ´ٸ�, ���� ���� Ÿ�����ϱ� ���� �ڵ�
                    StartCoroutine("TypingAction", 0);
                }
                else
                {
                    // K : �����̽��ٸ� ������ �� ���� ���ڰ� ȭ�鿡 Ÿ���� �ǰ� �ִٸ�, �κ� ��ŵ�� ���� isSkipPart true�� �����ϴ� �ڵ�
                    isSkipPart = true;
                }
            }            
        }
    }

    public void GoToGameScreen() // K : ���� ������ ���� �Լ��Դϴ�.
    {
        // K : ��� ���� �ʱ�ȭ
        StopCoroutine("TypingAction");
        currentPoint = 0;
        subText = "";
        isTyping = true;
        isSkipPart = false;

        if (endingCode < 100)
        {
            SceneManager.LoadScene("MainGame"); // K : �ó�ý��� ������ ���� ������ ���� �ϴ� �Լ��Դϴ�. > using UnityEngine.SceneManagement;
        }
        else
        {
            endingManager.ShowHappyEndingCard(endingCode); // K : ���ǿ��� ī�尡 ���̰� �ϴ� �Լ��� ȣ���մϴ�.
        }        
    }

    IEnumerator TypingAction() {
        if (currentPoint >= fullText.Length)    // K : ��� �ؽ�Ʈ���� Ÿ���� ���� ��
        {
            if (endingCode < 100)
            {
                Debug.Log("Game Start"); // K : ��� �ؽ�Ʈ�� ��� �Ϸ�, ���� �÷��� ������ �̵�
            } else
            {
                Debug.Log("Game Happy Ending Credits");
            }
            GoToGameScreen();
        }

        dialog.text = "";   // K : Text ������Ʈ�� text �ʱ�ȭ
        isTyping = true;    // K : �ؽ�Ʈ ȭ�鿡 Ÿ������ �����߱� ������, isTyping true

        for (int i = 0; i < fullText[currentPoint].Length; i++) // K : �ؽ�Ʈ �� ������ �� ���� �� ���ڸ� ȭ�鿡 ��Ÿ���� �ϱ� ���� �ݺ���
        {
            yield return new WaitForSeconds(0.07f); // K : �ؽ�Ʈ �� ���� �� ���� ������ ������

            subText += fullText[currentPoint].Substring(0, i);  // K : �ؽ�Ʈ�� �ε��� 0~i���� �ڸ�
            dialog.text = subText;                                      // K : Text ������Ʈ�� subText ���� 
            subText = "";                                               // K : subText �ʱ�ȭ

            if (isSkipPart)                                             // K : �κ� ��ŵ�� true��
            {
                dialog.text = fullText[currentPoint];           // K : Text ������Ʈ�� ��ü �ؽ�Ʈ ������ ����
                isSkipPart = false;                                     // K : isSkipPart �ʱ�ȭ
                isTyping = false;                                       // K : isTyping �ʱ�ȭ
                break;
            }
        }

        isTyping = false;   // isTyping �ʱ�ȭ
        currentPoint++;     // �ؽ�Ʈ �迭�� ������ �ű�
    }
}
