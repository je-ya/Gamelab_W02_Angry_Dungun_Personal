using UnityEngine;

public class Character_Life : MonoBehaviour
{
    //������Ʈ�� ������ �ִ� ��ũ��Ʈ���� HP�� �޾ƿ���, �޾ƿ� HP�� �����̳� ���� ������ �ϴ� ��ũ��Ʈ



    protected float maxHp;
    protected float currentHp;
    void Start()
    {
        currentHp = maxHp;
    }



    void ReduceHP(float damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);

        if (currentHp <= 0)
        {
            // ���⿡ ��� ó�� ���� �߰�
            // ������ ��� �ҰŸ� ������
        }

    }


    void IncreaseHp(float damage)
    {
        currentHp += damage;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
    }


    void TakeDamage(float damage)
    {
        //float damage = GetHitDamageForSkill();
        ReduceHP(damage);
    }

    void GetHeal(float damage)
    {
        //float damage = GetHealDamageForSkill();
        IncreaseHp(damage);
    }



    void IncreaseStress()
    {

    }



    /*    float GetHitDamageForSkill()
        {
            return 0f;
        }

        float GetHealDamageForSkill()
        {
            return 0f;
        }*/





}
