using UnityEngine;

public class KHW_BossHandMove : MonoBehaviour
{
    [SerializeField] float moveCoolDown = 5f;      // �̵� ����
    [SerializeField] float elapsedTime = 0f;       // ��� �ð�
    [SerializeField] bool isMoved = false;         // �̵� �� ����
    [SerializeField] GameObject Hand;              // �� ������Ʈ
    [SerializeField] float moveTime = 3f;
    [SerializeField] Transform originTransform;
    [SerializeField] Transform moveTransform;      // ��ǥ ��ġ (���� ��ǥ)

    float moveTimer = 0f;                          // �̵� Ÿ�̸�
    bool moveToTarget = false;                     // �̵� ����
    Vector3 startWorldPos;                         // ���� ���� ��ġ

    private void Start()
    {
        // Hand�� �ʱ� ���� ��ġ ����
        startWorldPos = Hand.transform.position;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        // ��ٿ� üũ
        if (elapsedTime > moveCoolDown && !isMoved)
        {
            MoveHand();
        }

        // �̵� ���� �� �ε巴�� ó��
        if (isMoved)
        {
            moveTimer += Time.deltaTime;
            float t = moveTimer / moveTime;

            if (t <= 1f)
            {
                Vector3 fromPos = moveToTarget ? startWorldPos : moveTransform.position;
                Vector3 toPos = moveToTarget ? moveTransform.position : startWorldPos;
                Hand.transform.position = Vector3.Lerp(fromPos, toPos, t);
            }
            else
            {
                // �̵� �Ϸ�
                isMoved = false;
                moveTimer = 0f;
                elapsedTime = 0f;
                moveToTarget = !moveToTarget; // ���� ��ȯ
            }
        }
    }

    void MoveHand()
    {
        if (!isMoved)
        {
            isMoved = true;
            moveTimer = 0f;
        }
    }
}