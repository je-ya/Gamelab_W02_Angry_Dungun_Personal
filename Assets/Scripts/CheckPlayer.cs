using Unity.VisualScripting;
using UnityEngine;

public class CheckPlayer : MonoBehaviour
{

    GameObject enemy;
    Transform player;
    float detectionDistance = 15f;
    float checkInterval = 0.2f; // 0.2�ʸ��� üũ
     Canvas trun;

    void Start()
    {

        Transform spawnerTransform = gameObject.transform;
        enemy = spawnerTransform.gameObject;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject trunOb = FindObjectOfType<Turn>().GetComponent<Turn>().gameObject;
        trun = trunOb.GetComponent<Canvas>();

        // �ݺ������� CheckDistance ȣ�� ����
        InvokeRepeating("CheckDistance", 0f, checkInterval);
    }

    void CheckDistance()
    {
        if (player != null && Vector2.Distance(transform.position, player.position) <= detectionDistance)
        {
            SetToBattle();
            CancelInvoke("CheckDistance"); // ���� ���� �� üũ �ߴ�
        }
    }


    private bool isBattleStarted = false; // ���� ���� ���θ� �����ϴ� ���� �߰�
    bool start = false;


    void SetToBattle()
    {
        if (!isBattleStarted) // ������ ���� ���۵��� �ʾ��� ���� ����
        {
            StateManager.Instance.StartBattle();
            enemy.SetActive(true);
            BattleManager.Instance.InitEnemy();
            BattleManager.Instance.UpdateUIForCurrentEnemy();
            isBattleStarted = true; // ���� ���� �÷��� ����
            trun.enabled = true;
            BattleManager.Instance.StartBattle();
        }
    }
}


