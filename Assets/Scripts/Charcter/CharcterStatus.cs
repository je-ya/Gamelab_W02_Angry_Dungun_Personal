using UnityEngine;
using TMPro;
using System;

//ĳ���� ������ �����ϴ� �߻� Ŭ����
public abstract class CharcterStatus : MonoBehaviour
{
    protected string charaterName;

    protected float hp;
    protected int attack;
    protected float criticalRate;
    protected int defense;
    protected int evasion;

 

    protected virtual void InitStats()
    {
        hp = 100;
        attack = 10;
        defense = 0;
        evasion = 0;
        criticalRate = 5f;
    }

}
