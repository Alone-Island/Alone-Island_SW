using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HappyEnding : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI dialog; // K : text ������Ʈ�� �޾ƿ��� ���� �����Դϴ�. > using TMPro;          

    // K : synopsys�� �ؽ�Ʈ��(���� ����)�� �迭�Դϴ�.
    public string[] HappyEndingFullText = {
        "�ڻ��!!\n ���� ��ű⸦ ��������!!",
        "��ű�?",
        "��� ���� ���ݾ� ����� �־��µ�...\n���� �ϼ��̿���!",
        "NJ-C! ���� ��ű⸦ ����غ���!",
        "ġ...����....ġ����..��...�����",
    };


    string subText; // K : synopsys�� �ؽ�Ʈ(�� ����) �Ϻθ� �����ϱ� ���� �����Դϴ�.
    int currentPoint = 0; // K : synopsysFullText���� ���� �����Ͱ� ����ִ��� �����ϱ� ���� �����Դϴ�.

    public bool isTyping = true; // K : ���� ���ڰ� ȭ�鿡 Ÿ���εǰ� �ִ��� Ȯ���ϱ� ���� �����Դϴ�.
    bool isSkipPart = false;

    void Start()
    {
        StartCoroutine("TypingAction", 0);          // K : ��ũ��Ʈ�� ���۰� ���ÿ� Ÿ������ �����ϴ� �ڵ��Դϴ�.
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))    // K : �����̽��ٸ� ������ ��
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

    public void GoToGameScreen(string screenName) // K : ���� ������ ���� �Լ��Դϴ�.
    {
        // K : ��� ���� �ʱ�ȭ
        currentPoint = 0;
        subText = "";
        isTyping = true;
        isSkipPart = false;

        SceneManager.LoadScene(screenName); // K : ���� ������ ���� �ϴ� �Լ��Դϴ�. > using UnityEngine.SceneManagement;
    }

    IEnumerator TypingAction()
    {
        if (currentPoint >= HappyEndingFullText.Length)    // K : ��� �ؽ�Ʈ���� Ÿ���� ���� ��
        {
            Debug.Log("Game Start"); // K : ��� �ؽ�Ʈ�� ��� �Ϸ�, ���� �÷��� ������ �̵�
            GoToGameScreen("EndingCredits");
        }

        dialog.text = "";   // K : Text ������Ʈ�� text �ʱ�ȭ
        isTyping = true;    // K : �ؽ�Ʈ ȭ�鿡 Ÿ������ �����߱� ������, isTyping true

        for (int i = 0; i < HappyEndingFullText[currentPoint].Length; i++) // K : �ؽ�Ʈ �� ������ �� ���� �� ���ڸ� ȭ�鿡 ��Ÿ���� �ϱ� ���� �ݺ���
        {
            yield return new WaitForSeconds(0.15f); // K : �ؽ�Ʈ �� ���� �� ���� ������ ������

            subText += HappyEndingFullText[currentPoint].Substring(0, i);  // K : �ؽ�Ʈ�� �ε��� 0~i���� �ڸ�
            dialog.text = subText;                                      // K : Text ������Ʈ�� subText ���� 
            subText = "";                                               // K : subText �ʱ�ȭ

            if (isSkipPart)                                             // K : �κ� ��ŵ�� true��
            {
                dialog.text = HappyEndingFullText[currentPoint];           // K : Text ������Ʈ�� ��ü �ؽ�Ʈ ������ ����
                isSkipPart = false;                                     // K : isSkipPart �ʱ�ȭ
                isTyping = false;                                       // K : isTyping �ʱ�ȭ
                break;
            }
        }

        isTyping = false;   // isTyping �ʱ�ȭ
        currentPoint++;     // �ؽ�Ʈ �迭�� ������ �ű�
    }
}
