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
    
    public TextMeshProUGUI textMeshPro;    // TextMeshProUGUI 컴포넌트

    [SerializeField]
    private List<TextElement> textElements; // 텍스트 요소 리스트

    private int currentIndex = -1;          // 현재 표시 중인 텍스트 인덱스
    private bool typingDone;                // 타이핑 완료 여부
    private const float typingSpeed = 0.05f; // 고정된 타이핑 속도
    private Vector3 defaultPosition;       // 오브젝트의 기본 위치


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

    //초기화
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
            Debug.LogError("Trigger 스크립트를 찾을 수 없습니다.");
        }

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
            case ActionType.MouseClick: //마우스 클릭 시 다음 스크립트 출력
                if (Input.GetMouseButtonDown(0))
                {
                    ShowNextText();
                }
                break;
            case ActionType.SpaceBar: //스페이스바 입력 시 다음 스크립트 출력
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ShowNextText();
                }
                break;
            case ActionType.SetMoveable: //마우스 클릭 시 캐릭터 움직임&다음 스크립트 출력
                if(Input.GetMouseButtonDown(0))
                {
                    playerScript.CanMove = true;
                    ShowNextText();
                }
                break;
            case ActionType.SetShootable: //마우스 클릭 시 발사 가능&다음 스크립트 출력
                if (Input.GetMouseButtonDown(0))
                {
                    playerScript.CanShoot = true;
                    ShowNextText();
                }    
                break;
            case ActionType.EnterToTrigger:  //트리거에 닿으면 다음 스크립트 출력
                if (isEnterTriggered)
                {
                    ShowNextText();
                    isEnterTriggered = false; // 이벤트 처리 후 플래그 초기화
                }
                break;
            case ActionType.SetDashable:  //마우스 클릭 시 대쉬 가능&다음 스크립트 출력
                if (Input.GetMouseButtonDown(0))
                {
                    dashScript.enabled = true;
                    ShowNextText();
                }
                break;
            case ActionType.TextAutoSkip:  //타이핑이 끝나고 0.5초 뒤에 다음 스크립트 출력
                if (typingDone == true)
                {
                    Invoke("ShowNextText", 1f);
                }
                break;
            case ActionType.TextAutoSkipAT:  //타이핑이 끝나고 0.5초 뒤에 다음 스크립트 출력&오브젝트의 트리거 실행
                if (typingDone == true)
                {
                    Invoke("ShowNextText", 0.5f);
                    trigger.ActivateTrigger();
                }
                break;
            case ActionType.KillEnemy:  //맵의 Enemy의 자식 오브젝트가 없으면 다음 스크립트 출력
                if(enemyExist ==false) ShowNextText();
                break;
            case ActionType.NextRTrigger:  //마우스 클릭 시 다음 스크립트 출력 & 오브젝트의 트리거 실행
                if (Input.GetMouseButtonDown(0))
                {
                    trigger.ActivateTrigger();
                    ShowNextText();
                }
                break;
            case ActionType.Clickenough:  //마우스 클릭이 일정 시간 지속되면 다음 스크립트 출력
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


    void CheckEnemy()
    {

        if (parentObject != null)
        {
            // 부모 오브젝트의 자식 오브젝트 중 "enemy"라는 이름의 오브젝트를 찾음
            Transform enemyObject = parentObject.Find("Enemy");

            if (enemyObject != null)
            {
                // "enemy" 오브젝트의 자식 오브젝트가 없는지 확인
                if (enemyObject.childCount == 0)
                {
                    // 트리거 조건이 충족되었을 때 실행할 코드
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
        if (Input.GetMouseButtonDown(0)) // 클릭 시작
        {
            clickCoroutine = StartCoroutine(CheckHoldTime());
        }

        if (Input.GetMouseButtonUp(0)) // 클릭 종료
        {
            if (clickCoroutine != null)
            {
                StopCoroutine(clickCoroutine);
            }
            enoughTime = false; // 선택적: 뗄 때 false로 되돌리기
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