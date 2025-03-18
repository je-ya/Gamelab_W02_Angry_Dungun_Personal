using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//버튼 위에 마우스가 올라갔을 때, 툴팁 표시

public class ToolTip3 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    UI_Skill_Info3 _ui_Skill3_Info;

    [SerializeField] private string tooltipText; // 툴팁에 표시할 텍스트
    private Text tooltipTextComponent; // 툴팁 텍스트 컴포넌트 (패널 안에 텍스트가 있을 경우)

    void Start()
    {
        _ui_Skill3_Info = GameObject.FindAnyObjectByType<UI_Skill_Info3>();
    }

    // 마우스가 버튼 위에 올라갔을 때 호출
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_ui_Skill3_Info != null)
        {
            _ui_Skill3_Info.gameObject.GetComponent<Canvas>().enabled = true;

        }
    }

    // 마우스가 버튼을 떠났을 때 호출
    public void OnPointerExit(PointerEventData eventData)
    {
        if (_ui_Skill3_Info != null)
        {
            _ui_Skill3_Info.gameObject.GetComponent<Canvas>().enabled = false; // 툴팁 비활성화
        }
    }
}


