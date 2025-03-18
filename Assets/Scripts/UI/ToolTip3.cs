using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//��ư ���� ���콺�� �ö��� ��, ���� ǥ��

public class ToolTip3 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    UI_Skill_Info3 _ui_Skill3_Info;

    [SerializeField] private string tooltipText; // ������ ǥ���� �ؽ�Ʈ
    private Text tooltipTextComponent; // ���� �ؽ�Ʈ ������Ʈ (�г� �ȿ� �ؽ�Ʈ�� ���� ���)

    void Start()
    {
        _ui_Skill3_Info = GameObject.FindAnyObjectByType<UI_Skill_Info3>();
    }

    // ���콺�� ��ư ���� �ö��� �� ȣ��
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_ui_Skill3_Info != null)
        {
            _ui_Skill3_Info.gameObject.GetComponent<Canvas>().enabled = true;

        }
    }

    // ���콺�� ��ư�� ������ �� ȣ��
    public void OnPointerExit(PointerEventData eventData)
    {
        if (_ui_Skill3_Info != null)
        {
            _ui_Skill3_Info.gameObject.GetComponent<Canvas>().enabled = false; // ���� ��Ȱ��ȭ
        }
    }
}


