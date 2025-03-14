using UnityEngine;
using TMPro;
using System;

public abstract class CharcterStatus : MonoBehaviour
{
    public string charaterName;
    protected  float maxHp;
    protected  float currentHp;

    protected int attack;
    protected float criticalRate;
    protected int defense;
    protected int evasion;

    protected virtual void Start()
    {
        currentHp = maxHp;
    }    

    protected virtual void InitStats()
    {
        maxHp = 100;
        attack = 10;
        defense = 0;
        evasion = 0;
        criticalRate = 5f;
    }




    void TakeDamage()
    {
        float damage = GetHitDamageForSkill();
        ReduceHP(damage);
    }

    void GetHeal()
    {
        float damage = GetHealDamageForSkill();
        IncreaseHp(damage);
    }


    float GetHitDamageForSkill()
    {
        return 0f;
    }

    float GetHealDamageForSkill()
    {
        return 0f;
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



    void IncreaseStress()
    {

    }








}
