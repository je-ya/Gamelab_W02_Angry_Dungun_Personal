using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // �浹�� ������Ʈ�� �±װ� "player"���� Ȯ��
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Main3");
        }
    }
}
