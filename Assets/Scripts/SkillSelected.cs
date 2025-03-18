using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//��ư Ŭ������ ��, Ŭ�� ���� �����ǰ� ��
public class SkillSelected : MonoBehaviour
{
    [SerializeField]
    bool _targetSelect;
    public bool TargetSelect
    {
        get { return _targetSelect; }
        set { _targetSelect = value; }
    }

    [SerializeField]
    bool select;

    Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickButton);
        select = false;
        _targetSelect = false;
    }

    void OnClickButton()
    {
        select = true;
        BattleManager.Instance.isSelect = select;
        BattleManager.Instance.SetCurrentSkillSelected(this);

    }



    void Update()
    {
        //isSelected ���� ����
        if (select && EventSystem.current.currentSelectedGameObject != gameObject)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
            //Debug.Log("Button Selected");
        }

        // �ش� ���ǿ����� 
        if (select && _targetSelect)
        {
            EventSystem.current.SetSelectedGameObject(null);

            select = false;
            BattleManager.Instance.isSelect = select;

            _targetSelect = false;
        }
    }




}
