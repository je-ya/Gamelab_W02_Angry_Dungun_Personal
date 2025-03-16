using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//��ư Ŭ������ ��, Ŭ�� ���� �����ǰ� ��
public class CustomButtonController : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerClickHandler
{
    private Button button;
    [SerializeField]
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

        //��ư�� ���� ������ �� ȣ��
        //���࿡ ������Ʈ�� �����ؼ� ȣ��Ǿ�����, ������ �׼� ����
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // ��ư Ŭ�� �� ���� ���·� ����
        //EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public GameObject skillbutton;

    void Update()
    {
        //isSelected ���� ����
        if (isSelected && EventSystem.current.currentSelectedGameObject != gameObject)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }

        // Ư�� ���ǿ����� ���� 
        if (isSelected && Input.GetKeyDown(KeyCode.Space))
        {
            EventSystem.current.SetSelectedGameObject(null);
            isSelected = false;
        }
    }
}
