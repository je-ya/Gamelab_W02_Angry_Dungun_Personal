using UnityEngine;
using UnityEngine.UI;

public class GetHpcomponet : MonoBehaviour
{
    public GameObject targetObject;


    private Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>(); // �����̴� ������Ʈ�� �ڵ����� ������
    }

    void Start()
    {
        if (targetObject != null)
        {
            // targetObject���� MyScript ������Ʈ�� ������ �� ����
            Aazz0200_Life targetScript = targetObject.GetComponent<Aazz0200_Life>();
            if (targetScript != null)
            {
                // �����̴��� �ִ밪�� ���簪 ����
                slider.maxValue = targetScript.max; // Max ���� �����̴��� �ִ밪����
                slider.minValue = 0;                // �ּҰ��� 0���� (�ʿ信 ���� ���� ����)
                slider.value = targetScript.now;    // now ���� �����̴��� ���簪����
            }
        }
    }


    void Update()
    {
        if (targetObject != null)
        {
            Aazz0200_Life targetScript = targetObject.GetComponent<Aazz0200_Life>();
            if (targetScript != null)
            {
                slider.value = targetScript.now; // now ���� ����� ������ �����̴� ������Ʈ
            }
        }
    }


}


