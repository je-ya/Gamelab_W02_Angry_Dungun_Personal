using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TextElement
{
    public string text;                     // 표시할 텍스트 (한글 지원)
    [Tooltip("기본 위치로부터의 오프셋")]
    public Vector3 position = Vector3.zero; // 텍스트 위치 (오프셋으로 사용)
    public bool useCustomPosition = false;  // 오프셋 사용 여부
    [Tooltip("기본값: 0,0,0")]
    public Vector3 rotation;               // 텍스트 회전
    [Tooltip("기본값: 1")]
    public float scale = 1f;               // 텍스트 크기 (기본값 1)
    public ActionType actionToNext;        // 다음 요소로 넘어가는 조건
}

public enum ActionType
{
    SpaceBar,       // 아무 행동 없음
    MouseClick,  // 마우스 클릭
    ClickToSetMoveable,
    ClickToSetShootable,
    EnterToTrigger
}

public class ScriptManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMeshPro;    // TextMeshProUGUI 컴포넌트

    [SerializeField]
    private List<TextElement> textElements; // 텍스트 요소 리스트

    private int currentIndex = -1;          // 현재 표시 중인 텍스트 인덱스
    private bool typingDone;                // 타이핑 완료 여부
    private const float typingSpeed = 0.05f; // 고정된 타이핑 속도
    private Vector3 defaultPosition;       // 오브젝트의 기본 위치

    private Aazz0200_Player playerScript;


    void Start()
    {
        Init();
    }

    void Update()
    {
        ApplyTrigger();
    }

    //초기화
    void Init()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        playerScript = playerObject.GetComponent<Aazz0200_Player>();

        //TextMeshProUGUI 가져오기
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        //기본 위치로 초기화
        defaultPosition = textMeshPro.transform.localPosition;
        textMeshPro.text = "";      //가져온 텍스트 초기화
        typingDone = false;     //타이핑 상태 초기화

        if (textElements.Count > 0)
        {
            ShowNextText();     //맨 처음 텍스트 표시
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

    //요소마다 바꿔둔 transform 적용하기
    void ApplyTextProperties(int index)
    {
        if (index < 0 || index >= textElements.Count) return;

        TextElement element = textElements[index];
        textMeshPro.transform.localPosition = defaultPosition + (element.useCustomPosition ? element.position : Vector3.zero);
        textMeshPro.transform.localEulerAngles = element.rotation;
        textMeshPro.transform.localScale = Vector3.one * element.scale;
    }

    //다음 텍스트로 넘어가기
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

    //텍스트가 한글자씩 나오게 하는 함수
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