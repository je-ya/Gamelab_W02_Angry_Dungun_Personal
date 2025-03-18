using UnityEngine;


//ĳ���Ͱ� ���� �Ÿ��� �����̸� ��Ʈ������ �����ϴ� �̺�Ʈ�� �߻��ϰ� ��
//����� �αװ� �ִ� ĭ���� �ش� ĳ������ ��Ʈ���� ��ġ�� ���̴� �Լ� ȣ��
public class MoveStress : MonoBehaviour
{
    float playerLastXPoint;
    float threshold = 5f;
    Character character;
    int damage;

    void Start()
    {
        init();

    }

    void init()
    {
        playerLastXPoint = Mathf.Floor(transform.position.x / threshold) * threshold;
        damage = Random.Range(5, 9);
        character = GetComponent<Character>();
    }

    void Update()
    {
        float currentXpoint = Mathf.Floor(transform.position.x / threshold) * threshold;

        if (currentXpoint != playerLastXPoint)
        {
            //Debug.Log($"[{gameObject.name}] �̵� ���� - ����: {playerLastXPoint}, ����: {currentXpoint}");    //��Ʈ���� ����Ʈ ���� Ȯ�� �뵵
            if (currentXpoint > playerLastXPoint) // x+ ���� �̵�
            {
                //�ʿ��� ���� ����
                ForwardStressUp();

            }
            else if (currentXpoint < playerLastXPoint)
            {
                backwardStressUp();
            }
            playerLastXPoint = currentXpoint;
        }
    }
    public bool ForwardStressUp()
    {
        float increaseProbability = 15f;
        float chance = Random.Range(0f, 100f);
        if (chance <= increaseProbability)
        {
            character.TakeSDamage(damage);
            Debug.Log($"[{gameObject.name}]��Ʈ���� {damage}����!!");
            return true;
        }
        return false;
    }


    public bool backwardStressUp()
    {
        float increaseProbability = 35f;
        float chance = Random.Range(0f, 100f);


        if (chance <= increaseProbability)
        {
            character.TakeSDamage(damage);
            Debug.Log($"[{gameObject.name}]��Ʈ���� {damage}����!");
            return true;
        }
        return false;
    }

}

