using UnityEngine;
using System.Collections;

public class BossMove : MonoBehaviour
{
    public float speed = 5f; // �̵� �ӵ�
    private Vector3 targetPosition; // ������ ��ġ
    private bool isMoving = false; // �̵� ������ ����
    private bool isWaiting = false; // ��� ������ ����

    void Start()
    {
        // �ʱ� ������ ����
        SetNewTargetPosition();

        // 10�� �Ŀ� ���
        StartCoroutine(StartMovingAfterDelay(10f));
    }

    void Update()
    {
        if (isMoving && !isWaiting)
        {
            // �������� �̵�
            MoveToTarget();
        }
    }

    void SetNewTargetPosition()
    {

        float randomX = Random.Range(-130,-70);
        float randomY = Random.Range(-30,30);


        targetPosition = new Vector3(randomX, randomY, 0);
    }

    void MoveToTarget()
    {
        // �������� �̵�
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // �������� �����ߴ��� Ȯ��
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // ���� �� 2�� ���
            StartCoroutine(WaitAtDestination(2f));
        }
    }

    IEnumerator StartMovingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isMoving = true; // 10�� �� �̵� ����
    }

    IEnumerator WaitAtDestination(float waitTime)
    {
        isWaiting = true; // ��� ���� ����
        yield return new WaitForSeconds(waitTime); // 2�� ���

        // ���ο� ������ ����
        SetNewTargetPosition();
        isWaiting = false; // ��� ���� ����
    }
}
