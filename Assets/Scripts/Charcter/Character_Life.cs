using UnityEngine;

public class Character_Life : MonoBehaviour
{
    //오브젝트가 가지고 있는 스크립트에서 HP를 받아오고, 받아온 HP의 증감이나 죽음 판정을 하는 스크립트



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
            // 여기에 사망 처리 로직 추가
            // 영웅의 기상 할거면 넣으셈
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
