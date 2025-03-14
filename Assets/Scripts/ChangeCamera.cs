using Unity.VisualScripting;
using UnityEngine;
using System;
using static System.TimeZoneInfo;

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
        if(mainCamera != null && battleCamera != null)
        {
            mainCamera.enabled = !mainCamera.enabled; // mainCamera 비활성화
            battleCamera.enabled = !battleCamera.enabled; // battleCamera 활성화
        }
    }


}
