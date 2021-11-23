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
    public AudioSource keyboardAudio;
    public AudioSource enterAudio;

    // K : synopsys�� �ؽ�Ʈ��(���� ����)�� �迭�Դϴ�.
    public string[] synopsysFullText = { 
        "���� �κ������� K...\nAI �κ��� �����ϱ� ����\n�����ǿ����� ���� �ð��� ����... ", 
        "����... �ϼ��ߴ�... ",
        "���� ����, AI �κ� NJ-C!!!! ",
        "���� ���� ������ ���� ��...",
        "�ƴ�...����? ",
        "������ ���� ��Ȳ�� ���캸�ϡ� ",
        "����� ��� �����ְ�..\n������� ���� ������ �ʴ´�!! ",
        "���� ���� �� ���� ���� ������ �ΰ��ΰǰ�?!? ",
        "���� �ķ��� ���� �� ��������...\n�̷��� �״°ǰ�... ",
        "������ �����Դ� NJ-C�� �־�! ",
        "���� ���뿡 �Ұ��ϴ� �� �κ��� �н����Ѽ�\n�� Ȳ��ȭ�� ���󿡼� ��Ƴ��� ����! "
    };
    string subText; // K : synopsys�� �ؽ�Ʈ(�� ����) �Ϻθ� �����ϱ� ���� �����Դϴ�.
    int currentPoint = 0; // K : synopsysFullText���� ���� �����Ͱ� ����ִ��� �����ϱ� ���� �����Դϴ�.

    bool isTyping = true; // K : ���� ���ڰ� ȭ�鿡 Ÿ���εǰ� �ִ��� Ȯ���ϱ� ���� �����Դϴ�.
    bool isSkipPart = false;

    void Start() {
        StartCoroutine("TypingAction", 0);  // K : ��ũ��Ʈ�� ���۰� ���ÿ� Ÿ������ �����ϴ� �ڵ��Դϴ�.
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

    public void GoToGameScreen() // K : ���� ������ ���� �Լ��Դϴ�.
    {
        // K : ��� ���� �ʱ�ȭ
        currentPoint = 0;
        subText = "";
        isTyping = true;
        isSkipPart = false;

        SceneManager.LoadScene("MainGame"); // K : ���� ������ ���� �ϴ� �Լ��Դϴ�. > using UnityEngine.SceneManagement;
    }

    IEnumerator TypingAction() {
        if (currentPoint >= synopsysFullText.Length)    // K : ��� �ؽ�Ʈ���� Ÿ���� ���� ��
        {
            Debug.Log("���� ����"); // K : ��� �ؽ�Ʈ�� ��� �Ϸ�, ���� �÷��� ������ �̵�
            GoToGameScreen();
        }

        dialog.text = "";   // K : Text ������Ʈ�� text �ʱ�ȭ
        isTyping = true;    // K : �ؽ�Ʈ ȭ�鿡 Ÿ������ �����߱� ������, isTyping true

        for (int i = 0; i < synopsysFullText[currentPoint].Length; i++) // K : �ؽ�Ʈ �� ������ �� ���� �� ���ڸ� ȭ�鿡 ��Ÿ���� �ϱ� ���� �ݺ���
        {
            yield return new WaitForSeconds(0.15f); // K : �ؽ�Ʈ �� ���� �� ���� ������ ������

            subText += synopsysFullText[currentPoint].Substring(0, i);  // K : �ؽ�Ʈ�� �ε��� 0~i���� �ڸ�
            dialog.text = subText;                                      // K : Text ������Ʈ�� subText ���� 
            subText = "";                                               // K : subText �ʱ�ȭ

            if (isSkipPart)                                             // K : �κ� ��ŵ�� true��
            {
                dialog.text = synopsysFullText[currentPoint];           // K : Text ������Ʈ�� ��ü �ؽ�Ʈ ������ ����
                isSkipPart = false;                                     // K : isSkipPart �ʱ�ȭ
                isTyping = false;                                       // K : isTyping �ʱ�ȭ
                break;
            }
        }

        isTyping = false;   // isTyping �ʱ�ȭ
        currentPoint++;     // �ؽ�Ʈ �迭�� ������ �ű�
    }
}
