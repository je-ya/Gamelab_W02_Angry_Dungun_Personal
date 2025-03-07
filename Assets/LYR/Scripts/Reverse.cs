using UnityEngine;

public class Reverse : MonoBehaviour
{
    public GameObject targetObject;

    void Start()
    {
        // ������Ʈ�� ��Ȱ��ȭ�Ǿ� �ִ��� Ȯ��
        if (!targetObject.activeSelf)
        {
            Debug.Log("��� ������Ʈ�� ��Ȱ��ȭ�Ǿ� �ֽ��ϴ�.");
        }

        // Collider2D ������Ʈ ��������
        Collider2D collider = targetObject.GetComponent<Collider2D>();

        if (collider != null)
        {
            // ���� ��ǥ ���� Bounds ��������
            Bounds bounds = collider.bounds;

            // Min (���� �Ʒ�)�� Max (������ ��) ��ǥ
            Vector2 min = bounds.min;
            Vector2 max = bounds.max;

            // ��� ���
            Debug.Log($"Min X: {min.x}, Min Y: {min.y}");
            Debug.Log($"Max X: {max.x}, Max Y: {max.y}");
        }
        else
        {
            Debug.Log("Collider2D�� �����ϴ�.");
        }
    }
}