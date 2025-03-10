using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TextElement
{
    public string text;                     // ǥ���� �ؽ�Ʈ (�ѱ� ����)
    [Tooltip("�⺻ ��ġ�κ����� ������")]
    public Vector3 position = Vector3.zero; // �ؽ�Ʈ ��ġ (���������� ���)
    public bool useCustomPosition = false;  // ������ ��� ����
    [Tooltip("�⺻��: 0,0,0")]
    public Vector3 rotation;               // �ؽ�Ʈ ȸ��
    [Tooltip("�⺻��: 1")]
    public float scale = 1f;               // �ؽ�Ʈ ũ�� (�⺻�� 1)
    public ActionType actionToNext;        // ���� ��ҷ� �Ѿ�� ����
}

public enum ActionType
{
    SpaceBar,       // �ƹ� �ൿ ����
    MouseClick,  // ���콺 Ŭ��
    ClickToSetMoveable,
    ClickToSetShootable,
    EnterToTrigger
}

public class ScriptManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMeshPro;    // TextMeshProUGUI ������Ʈ

    [SerializeField]
    private List<TextElement> textElements; // �ؽ�Ʈ ��� ����Ʈ

    private int currentIndex = -1;          // ���� ǥ�� ���� �ؽ�Ʈ �ε���
    private bool typingDone;                // Ÿ���� �Ϸ� ����
    private const float typingSpeed = 0.05f; // ������ Ÿ���� �ӵ�
    private Vector3 defaultPosition;       // ������Ʈ�� �⺻ ��ġ

    private Aazz0200_Player playerScript;


    void Start()
    {
        Init();
    }

    void Update()
    {
        ApplyTrigger();
    }

    //�ʱ�ȭ
    void Init()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        playerScript = playerObject.GetComponent<Aazz0200_Player>();

        //TextMeshProUGUI ��������
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        //�⺻ ��ġ�� �ʱ�ȭ
        defaultPosition = textMeshPro.transform.localPosition;
        textMeshPro.text = "";      //������ �ؽ�Ʈ �ʱ�ȭ
        typingDone = false;     //Ÿ���� ���� �ʱ�ȭ

        if (textElements.Count > 0)
        {
            ShowNextText();     //�� ó�� �ؽ�Ʈ ǥ��
        }
    }

    
    void ApplyTrigger()
    {
        if (!typingDone || currentIndex < 0 || currentIndex >= textElements.Count) return;

        TextElement currentElement = textElements[currentIndex];

        switch (currentElement.actionToNext)
        {
            case ActionType.MouseClick:
                if (Input.GetMouseButtonDown(0))
                {
                    ShowNextText();
                }
                break;
            case ActionType.SpaceBar:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ShowNextText();
                }
                break;
            case ActionType.ClickToSetMoveable:
                if(Input.GetMouseButtonDown(0))
                {
                    playerScript.CanMove = true;
                    ShowNextText();
                }
                break;
            case ActionType.ClickToSetShootable:
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    playerScript.CanShoot = true;
                    ShowNextText();
                }    
                break;
            case ActionType.EnterToTrigger:
                

                break;
            default:
                break;
        }
    }

    //��Ҹ��� �ٲ�� transform �����ϱ�
    void ApplyTextProperties(int index)
    {
        if (index < 0 || index >= textElements.Count) return;

        TextElement element = textElements[index];
        textMeshPro.transform.localPosition = defaultPosition + (element.useCustomPosition ? element.position : Vector3.zero);
        textMeshPro.transform.localEulerAngles = element.rotation;
        textMeshPro.transform.localScale = Vector3.one * element.scale;
    }

    //���� �ؽ�Ʈ�� �Ѿ��
    void ShowNextText()
    {
        if (!typingDone && currentIndex >= 0) return;

        textMeshPro.text = "";

        currentIndex++;
        if (currentIndex >= textElements.Count)
        {
            currentIndex = textElements.Count;
            typingDone = true;
            return;
        }

        ApplyTextProperties(currentIndex);
        StartCoroutine(TypeText());
    }

    //�ؽ�Ʈ�� �ѱ��ھ� ������ �ϴ� �Լ�
    IEnumerator TypeText()
    {
        typingDone = false;
        TextElement element = textElements[currentIndex];
        string fullText = element.text;

        foreach (char letter in fullText)
        {
            textMeshPro.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        typingDone = true;
    }

    public int GetElementCount()
    {
        return textElements.Count;
    }
}