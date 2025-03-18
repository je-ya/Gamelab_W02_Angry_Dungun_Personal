using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//버튼 클릭했을 때, 클릭 상태 유지되게 함
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
        //isSelected 상태 유지
        if (select && EventSystem.current.currentSelectedGameObject != gameObject)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
            //Debug.Log("Button Selected");
        }

        // 해당 조건에서만 
        if (select && _targetSelect)
        {
            EventSystem.current.SetSelectedGameObject(null);

            select = false;
            BattleManager.Instance.isSelect = select;

            _targetSelect = false;
        }
    }




}
