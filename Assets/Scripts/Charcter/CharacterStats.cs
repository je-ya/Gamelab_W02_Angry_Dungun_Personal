using UnityEngine;


//캐릭터 스텟을 정의

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Character/CharacterStats", order = 1)]
public class CharacterStats : ScriptableObject
{
    [Header("기본 정보")]
    public string characterName;

    [Header("스탯")]
    public float hp;
    public float stress = 200;
    public int attack;
    public int defense;
    public int evasion;
    public int criticalChance;
}



