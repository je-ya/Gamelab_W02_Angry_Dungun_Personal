using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static SkillManager;

public class HpManager : MonoBehaviour
{

    public TextMeshProUGUI hpText;
    public GameObject chracter;
    float MaxHP;
    float cHP;


    private void Start()
    {
        MaxHP = chracter.GetComponent<Character>().stats.hp;
        
    }

    private void Update()
    {
        cHP = chracter.GetComponent<Character>().CurrentHp;
        hpText.text = $"{MaxHP}/{cHP.ToString("0")}";
    }

}
