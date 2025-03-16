using UnityEngine;


//���� ĳ���Ϳ� ���� ���� ��, ������ �߻�
public class CheckPlayer : MonoBehaviour
{
    ChangeCamera cameraController;
    GameObject enemy;

    void Start()
    {
        cameraController = FindObjectOfType<ChangeCamera>();
        Transform spawnerTransform = transform.Find("Spwaner");
        enemy = spawnerTransform.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            //Debug.Log("�÷��̾�� ����!");
            SetToBettle();
        }
    }

    void SetToBettle()
    {
        StateManager.Instance.StartBattle();
        enemy.SetActive(true);
        cameraController.SwitchCamera();
    }

}


