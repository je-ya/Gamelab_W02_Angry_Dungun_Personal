using UnityEngine;
using System;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance => _instance;
    static StateManager _instance;

    bool inBattle = false;

    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartBattle()
    {
        inBattle = true;
    }
    
    public void EndBattle()
    {
        inBattle = false;
    }    

    public bool CheckBattleActive()
    {
        return inBattle;
    }    

}
