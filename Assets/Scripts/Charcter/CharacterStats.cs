using UnityEngine;


//ĳ���� ������ ����

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Character/CharacterStats", order = 1)]
public class CharacterStats : ScriptableObject
{
    [Header("�⺻ ����")]
    public string characterName;

    [Header("����")]
    public float hp;
    public float stress = 200;
    public int attack;
    public int defense;
    public int evasion;
    public int criticalChance;
}



