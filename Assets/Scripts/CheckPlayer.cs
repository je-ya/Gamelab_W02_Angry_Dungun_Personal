using Unity.VisualScripting;
using UnityEngine;

public class CheckPlayer : MonoBehaviour
{

    GameObject enemy;
    Transform player;
    float detectionDistance = 15f;
    float checkInterval = 0.2f; // 0.2초마다 체크
     Canvas trun;

    void Start()
    {

        Transform spawnerTransform = gameObject.transform;
        enemy = spawnerTransform.gameObject;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject trunOb = FindObjectOfType<Turn>().GetComponent<Turn>().gameObject;
        trun = trunOb.GetComponent<Canvas>();

        // 반복적으로 CheckDistance 호출 시작
        InvokeRepeating("CheckDistance", 0f, checkInterval);
    }

    void CheckDistance()
    {
        if (player != null && Vector2.Distance(transform.position, player.position) <= detectionDistance)
        {
            SetToBattle();
            CancelInvoke("CheckDistance"); // 전투 시작 후 체크 중단
        }
    }


    private bool isBattleStarted = false; // 전투 시작 여부를 추적하는 변수 추가
    bool start = false;


    void SetToBattle()
    {
        if (!isBattleStarted) // 전투가 아직 시작되지 않았을 때만 실행
        {
            StateManager.Instance.StartBattle();
            enemy.SetActive(true);
            BattleManager.Instance.InitEnemy();
            BattleManager.Instance.UpdateUIForCurrentEnemy();
            isBattleStarted = true; // 전투 시작 플래그 설정
            trun.enabled = true;
            BattleManager.Instance.StartBattle();
        }
    }
}


