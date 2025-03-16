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
    SetMoveable,
    SetShootable,
    EnterToTrigger,
    TextAutoSkip,
    KillEnemy,
    SetDashable,
    NextRTrigger,
    TextAutoSkipAT,
    Clickenough,
    EnemyObON,
    UseItem1,
    RelicOn
}

public class ScriptManager : MonoBehaviour
{
    
    public TextMeshProUGUI textMeshPro;    // TextMeshProUGUI ������Ʈ

    [SerializeField]
    private List<TextElement> textElements; // �ؽ�Ʈ ��� ����Ʈ

    private int currentIndex = -1;          // ���� ǥ�� ���� �ؽ�Ʈ �ε���
    private bool typingDone;                // Ÿ���� �Ϸ� ����
    private const float typingSpeed = 0.05f; // ������ Ÿ���� �ӵ�
    private Vector3 defaultPosition;       // ������Ʈ�� �⺻ ��ġ


    bool isEnterTriggered;
    [SerializeField]
    bool enemyExist;
    Trigger trigger;

    private Aazz0200_Player playerScript;
    private PlayerDash dashScript;

    bool enoughTime;
    float holdTime = 1.0f;
    Coroutine clickCoroutine;

    Transform parentObject;

    GameObject playerObject;
    bool isRelicActive;

    void Start()
    {
        Init();
    }

    void Update()
    {
        ApplyTrigger();
        CheckEnemy();
        CheckMouseClick();
        CheckRelic();
    }

    //�ʱ�ȭ
    void Init()
    {
        playerObject = GameObject.FindWithTag("Player");
        playerScript = playerObject.GetComponent<Aazz0200_Player>();
        dashScript = playerObject.GetComponent<PlayerDash>();

        string objectName = gameObject.name;

        parentObject = transform.parent;

        trigger = GetComponent<Trigger>();

        enemyExist = true;

        if(objectName == "R1 Text Manager")
        {
            playerScript.CanMove = false;
            playerScript.CanShoot = false;
            dashScript.enabled = false;
        }


        if (trigger == null)
        {
            Debug.LogError("Trigger ��ũ��Ʈ�� ã�� �� �����ϴ�.");
        }

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
            case ActionType.MouseClick: //���콺 Ŭ�� �� ���� ��ũ��Ʈ ���
                if (Input.GetMouseButtonDown(0))
                {
                    ShowNextText();
                }
                break;
            case ActionType.SpaceBar: //�����̽��� �Է� �� ���� ��ũ��Ʈ ���
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ShowNextText();
                }
                break;
            case ActionType.SetMoveable: //���콺 Ŭ�� �� ĳ���� ������&���� ��ũ��Ʈ ���
                if(Input.GetMouseButtonDown(0))
                {
                    playerScript.CanMove = true;
                    ShowNextText();
                }
                break;
            case ActionType.SetShootable: //���콺 Ŭ�� �� �߻� ����&���� ��ũ��Ʈ ���
                if (Input.GetMouseButtonDown(0))
                {
                    playerScript.CanShoot = true;
                    ShowNextText();
                }    
                break;
            case ActionType.EnterToTrigger:  //Ʈ���ſ� ������ ���� ��ũ��Ʈ ���
                if (isEnterTriggered)
                {
                    ShowNextText();
                    isEnterTriggered = false; // �̺�Ʈ ó�� �� �÷��� �ʱ�ȭ
                }
                break;
            case ActionType.SetDashable:  //���콺 Ŭ�� �� �뽬 ����&���� ��ũ��Ʈ ���
                if (Input.GetMouseButtonDown(0))
                {
                    dashScript.enabled = true;
                    ShowNextText();
                }
                break;
            case ActionType.TextAutoSkip:  //Ÿ������ ������ 0.5�� �ڿ� ���� ��ũ��Ʈ ���
                if (typingDone == true)
                {
                    Invoke("ShowNextText", 1f);
                }
                break;
            case ActionType.TextAutoSkipAT:  //Ÿ������ ������ 0.5�� �ڿ� ���� ��ũ��Ʈ ���&������Ʈ�� Ʈ���� ����
                if (typingDone == true)
                {
                    Invoke("ShowNextText", 0.5f);
                    trigger.ActivateTrigger();
                }
                break;
            case ActionType.KillEnemy:  //���� Enemy�� �ڽ� ������Ʈ�� ������ ���� ��ũ��Ʈ ���
                if(enemyExist ==false) ShowNextText();
                break;
            case ActionType.NextRTrigger:  //���콺 Ŭ�� �� ���� ��ũ��Ʈ ��� & ������Ʈ�� Ʈ���� ����
                if (Input.GetMouseButtonDown(0))
                {
                    trigger.ActivateTrigger();
                    ShowNextText();
                }
                break;
            case ActionType.Clickenough:  //���콺 Ŭ���� ���� �ð� ���ӵǸ� ���� ��ũ��Ʈ ���
                if(enoughTime == true)
                {
                    ShowNextText();
                }    
                break;
            case ActionType.EnemyObON:
                if (Input.GetMouseButtonDown(0)) 
                {
                    EnemyOn();
                    ShowNextText();
                }
                break;
            case ActionType.UseItem1:
                if(Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ShowNextText();
                }
                break;
            case ActionType.RelicOn:
                if(isRelicActive == true)
                {
                    ShowNextText();
                }
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


    void CheckEnemy()
    {

        if (parentObject != null)
        {
            // �θ� ������Ʈ�� �ڽ� ������Ʈ �� "enemy"��� �̸��� ������Ʈ�� ã��
            Transform enemyObject = parentObject.Find("Enemy");

            if (enemyObject != null)
            {
                // "enemy" ������Ʈ�� �ڽ� ������Ʈ�� ������ Ȯ��
                if (enemyObject.childCount == 0)
                {
                    // Ʈ���� ������ �����Ǿ��� �� ������ �ڵ�
                    NoEnemy();
                }
            }
        }
    }


    void EnemyOn()
    {
        if (parentObject != null)
        {
            Transform enemyObject = parentObject.Find("Enemy");
            enemyObject.gameObject.SetActive(true);
        }    
    }    

    void NoEnemy()
    {
        enemyExist = false;
    }

    void CheckMouseClick()
    {
        if (Input.GetMouseButtonDown(0)) // Ŭ�� ����
        {
            clickCoroutine = StartCoroutine(CheckHoldTime());
        }

        if (Input.GetMouseButtonUp(0)) // Ŭ�� ����
        {
            if (clickCoroutine != null)
            {
                StopCoroutine(clickCoroutine);
            }
            enoughTime = false; // ������: �� �� false�� �ǵ�����
        }
    }
    IEnumerator CheckHoldTime()
    {
        yield return new WaitForSeconds(holdTime);
        enoughTime = true;
    }

    void CheckRelic()
    {
        Transform relicRotate = playerObject.transform.Find("Relic_Rotate");
        if (relicRotate != null)
        {
            Transform redRelic1 = relicRotate.Find("Red_Relic1");
            if (redRelic1 != null)
            {
                isRelicActive = redRelic1.gameObject.activeInHierarchy;
            }
        }
    }

    public int GetElementCount()
    {
        return textElements.Count;
    }
}