using UnityEngine;

public class Destroy_tail : MonoBehaviour
{

    void OnDestroy()
    {
        // �θ� ������Ʈ�� �����ϸ� �ı�
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}