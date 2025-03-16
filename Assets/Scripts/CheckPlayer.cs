using UnityEngine;


//적이 캐릭터와 조우 했을 때, 전투가 발생
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
            //Debug.Log("플레이어와 닿음!");
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


