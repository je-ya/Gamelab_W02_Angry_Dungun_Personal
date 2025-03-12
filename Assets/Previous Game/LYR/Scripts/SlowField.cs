using UnityEngine;

public class SlowField : MonoBehaviour
{
    public float slowSpeed = 2f; // ���ǿ��� ������ ���� �ӵ�
    public bool slowOn = false;

    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� �÷��̾����� Ȯ�� (�±� ���)
        if (other.CompareTag("Player"))
        {
            slowOn = true;
            Aazz0200_Player player = other.GetComponent<Aazz0200_Player>();
            if (player != null)
            {

                Debug.Log("�÷��̾� �ӵ��� ���������ϴ�!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Aazz0200_Player player = other.GetComponent<Aazz0200_Player>();
            if (player != null)
            {

                Debug.Log("�÷��̾� �ӵ��� �������� ���ƿԽ��ϴ�!");
            }
        }
    }

}