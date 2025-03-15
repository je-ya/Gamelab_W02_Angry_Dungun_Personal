using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomButtonController : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerClickHandler
{
    private Button button;
    private bool isSelected = false;


    void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        isSelected = true;
        Debug.Log("Button Selected");
    }

    public void OnDeselect(BaseEventData eventData)
    {
        //버튼이 선택 해제될 때 호출
        //만약에 오브젝트를 선택해서 호출되었으면, 데미지 액션 실행
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 버튼 클릭 시 선택 상태로 강제 설정
        EventSystem.current.SetSelectedGameObject(gameObject);
    }


    void Update()
    {
        if (isSelected && EventSystem.current.currentSelectedGameObject != gameObject)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }

        // 특정 조건에서만 해제 
        if (isSelected && Input.GetKeyDown(KeyCode.Space))
        {
            EventSystem.current.SetSelectedGameObject(null);
            isSelected = false;
        }
    }
}
