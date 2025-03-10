using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌한 오브젝트의 태그가 "player"인지 확인
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Main3");
        }
    }
}
