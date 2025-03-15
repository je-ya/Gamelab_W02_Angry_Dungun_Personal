using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class BettleManager : MonoBehaviour
{
    public static BettleManager Instance => _instance;
    static BettleManager _instance;

    [System.Serializable]
    enum BattleState
    {
        IdleAttack,
        PlayerTurn,
        EnemyTurn
    }

    [SerializeField]
    BattleState state;

    float turnTimer = 0f;
    int currentCharacter;

    void Start()
    {
        Invoke("StartBattle", 1f);
    }

    void StartBattle()
    {
        state = BattleState.IdleAttack;
    }

    void Update()
    {
        switch (state)
        {
            case BattleState.PlayerTurn:
                PlayerTurn();
                break;
            case BattleState.EnemyTurn:
                EnemyTurn();
                break;
            case BattleState.IdleAttack:
                IdleAttack();
                break;
        }
    }

    void PlayerTurn()
    {
        if (currentCharacter < 4)
        {
            turnTimer += Time.deltaTime;
            if (turnTimer >= 2f)
            {
                Debug.Log("ĳ���� " + (currentCharacter + 1) + " ��ų ��� �Ϸ�");
                currentCharacter++;
                turnTimer = 0f;
            }
        }
        else
        {
            currentCharacter = 0;
            turnTimer = 0f;
            state = BattleState.EnemyTurn;
        }
    }

    void EnemyTurn()
    {
        turnTimer += Time.deltaTime;
        if (turnTimer >= 2f)
        {
            Debug.Log("�� �� ����");
            turnTimer = 0f;
            state = BattleState.IdleAttack;
        }
    }

    void IdleAttack()
    {
        turnTimer += Time.deltaTime;
        if (turnTimer >= 2f)
        {
            Debug.Log("���� ����");
            turnTimer = 0f;
            state = BattleState.PlayerTurn;
        }
    }

}
