using System.Collections.Generic;
using UnityEngine;

public class Worm_circle : MonoBehaviour
{
    public Transform target; // ���� ���
    public float fSpeed = 0.1f; // �ε巯�� �̵� �ӵ�
    public int delay = 10; // ���󰡴� ������ (������ ����)

    private Queue<Vector3> positions = new Queue<Vector3>(); // ��ġ ���� ť

    public void UpdatePosition()
    {
        // Ÿ���� ���� ��ġ�� ť�� �߰�
        positions.Enqueue(target.position);

        // �����̺��� ť�� Ŀ���� ���� ��ġ�� ���󰡵��� ����
        if (positions.Count > delay)
        {
            Vector3 targetPosition = positions.Dequeue();

            // �ε巴�� �̵�
            transform.position = Vector3.Lerp(transform.position, targetPosition, fSpeed);
        }
    }
}