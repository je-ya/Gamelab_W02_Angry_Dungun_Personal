using UnityEngine;

public class Destroy_tail : MonoBehaviour
{

    void OnDestroy()
    {
        // 부모 오브젝트가 존재하면 파괴
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}