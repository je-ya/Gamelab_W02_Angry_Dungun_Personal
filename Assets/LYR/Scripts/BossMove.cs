using UnityEngine;
using System.Collections;

public class BossMove : MonoBehaviour
{
    public float speed = 5f; // �̵� �ӵ�
    private Vector3 targetPosition; // ������ ��ġ
    private bool isMoving = false; // �̵� ������ ����
    private bool isWaiting = false; // ��� ������ ����'
    CheckCenterOfMap centerOb;
    Vector3 centerPosion;
    float radius = 33;
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    void Start()
    {
        //MapCenter ������Ʈ�� ��ġ ��������
        centerOb = GameObject.FindAnyObjectByType<CheckCenterOfMap>();
        centerPosion = centerOb.GetComponent<Transform>().position;
        //Debug.Log(centerPosion); //���� Center ��ġ�� �������� Ȯ��

        xMax = centerPosion.x + radius;
        xMin = centerPosion.x - radius;
        yMax = centerPosion.y - radius;
        yMin = centerPosion.y + radius;


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
        //�ϵ��ڵ� �� �κ� - Center ������Ʈ �����, �� ������Ʈ�� �����ͼ� �װ� �ݰ� �󸶷� �����ϸ� �� �� ����
        //������Ʈ �������°� ������ ���� �� ��ũ��Ʈ �̿��ص� �ɱ�?
        //���࿡ Center ������Ʈ�� ��ã���� ���� �޼��� ���
        //�ݰ��� ������ ���� ���� �� �� �ֵ���, �׸��� public���� private�� SerializeField�ؼ� ��ũ��Ʈ���� ��ġ�� üũ �����ϵ���
        //�ϴ� �� ���� ���ڿ� �̷��� ���� �Ǵ°��� �����
        
        if (centerPosion != null )
        {
            float randomX = Random.Range(xMax, xMin);
            float randomY = Random.Range(yMax, yMin);
            targetPosition = new Vector3(randomX, randomY, 0);

        }
        else { Debug.Log("Center Ob cannot be found"); }
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
