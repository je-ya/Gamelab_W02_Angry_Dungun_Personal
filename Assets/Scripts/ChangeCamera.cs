using UnityEngine;

//이동에서 전투로 전환될 때 카메라 전환
public class ChangeCamera : MonoBehaviour
{

    Camera mainCamera;
    Camera battleCamera;

    void Start()
    {
        init();
    }


    void init()
    {
        mainCamera = Camera.main;
        battleCamera = FindAnyObjectByType<BattleCamera>().GetComponent<Camera>();

        if (mainCamera != null) mainCamera.enabled = true;
        if (battleCamera != null) battleCamera.enabled = false;
    }

    public void SwitchCamera()
    {
        if (mainCamera != null && battleCamera != null)
        {
            mainCamera.enabled = !mainCamera.enabled; // mainCamera 비활성화
            battleCamera.enabled = !battleCamera.enabled; // battleCamera 활성화
        }
    }


}
