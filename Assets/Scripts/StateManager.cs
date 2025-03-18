using UnityEngine;
using UnityEngine.UI;

//이동과 전투 상태를 관리
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
