using UnityEngine;
using UnityEngine.UI;

public class GetHpcomponet : MonoBehaviour
{
    public GameObject targetObject;


    private Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>(); // 슬라이더 컴포넌트를 자동으로 가져옴
    }

    void Start()
    {
        if (targetObject != null)
        {
            // targetObject에서 MyScript 컴포넌트를 가져와 값 참조
            Aazz0200_Life targetScript = targetObject.GetComponent<Aazz0200_Life>();
            if (targetScript != null)
            {
                // 슬라이더의 최대값과 현재값 설정
                slider.maxValue = targetScript.max; // Max 값을 슬라이더의 최대값으로
                slider.minValue = 0;                // 최소값은 0으로 (필요에 따라 변경 가능)
                slider.value = targetScript.now;    // now 값을 슬라이더의 현재값으로
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
                slider.value = targetScript.now; // now 값이 변경될 때마다 슬라이더 업데이트
            }
        }
    }


}


