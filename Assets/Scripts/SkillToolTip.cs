using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//��ư ���� ���콺�� �ö��� ��, ���� ǥ��
public class SkillToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    UI_Skill1_Info _ui_Skill1_Info;

    [SerializeField] private string tooltipText; // ������ ǥ���� �ؽ�Ʈ
    private Text tooltipTextComponent; // ���� �ؽ�Ʈ ������Ʈ (�г� �ȿ� �ؽ�Ʈ�� ���� ���)

    void Start()
    {
        _ui_Skill1_Info = GameObject.FindAnyObjectByType<UI_Skill1_Info>();
    }

    // ���콺�� ��ư ���� �ö��� �� ȣ��
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_ui_Skill1_Info != null)
        {
            _ui_Skill1_Info.gameObject.GetComponent<Canvas>().enabled = true;

        }
    }

    // ���콺�� ��ư�� ������ �� ȣ��
    public void OnPointerExit(PointerEventData eventData)
    {
        if (_ui_Skill1_Info != null)
        {
            _ui_Skill1_Info.gameObject.GetComponent<Canvas>().enabled = false; // ���� ��Ȱ��ȭ
        }
    }
}
