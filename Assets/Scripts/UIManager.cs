using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private Character currentCharacter;
    
    public TextMeshProUGUI nameText, hpText, attackText, defenseText, evasionText, criticalText, stressText;
    public void UpdateCharacterUI(Character character)
    {
        if (character == null)
        {
            Debug.LogError("UpdateCharacterUI: ĳ���Ͱ� null�Դϴ�.");
            return;
        }

        currentCharacter = character;
        Debug.Log($"UI ������Ʈ: {character.stats.name}");


        if (nameText != null) nameText.text = (string.IsNullOrEmpty(character.stats.name) ? "Unknown" : character.stats.name);
        if (hpText != null) hpText.text = "HP: " + (character.CurrentHp > 0 ? character.CurrentHp.ToString("0") : "0");
        if (attackText != null) attackText.text = "����: " + (character.stats.attack > 0 ? character.stats.attack.ToString() : "0");
        if (defenseText != null) defenseText.text = "���: " + (character.stats.defense > 0 ? character.stats.defense.ToString() : "0");
        if (evasionText != null) evasionText.text = "ȸ��: " + (character.stats.evasion > 0 ? character.stats.evasion.ToString() : "0");
        if (criticalText != null) criticalText.text = "ġ��Ÿ: " + (character.stats.criticalChance > 0 ? character.stats.criticalChance.ToString() : "0");
        if (stressText != null) stressText.text = "��Ʈ����  " + (character.stats.stress > 0 ? character.Stress.ToString() : "0");
    }


    public TextMeshProUGUI EnemyText, EhpText, EattackText, EdefenseText, EevasionText, EcriticalText;
    public void UpdateEnemyUI(Character character)
    {
        if (character == null)
        {
            Debug.LogError("UpdateCharacterUI: ĳ���Ͱ� null�Դϴ�.");
            return;
        }

        currentCharacter = character;
        Debug.Log($"UI ������Ʈ: {character.stats.name}");


        if (EnemyText != null) EnemyText.text = (string.IsNullOrEmpty(character.stats.name) ? "Unknown" : character.stats.name);
        if (EhpText != null) EhpText.text = "HP: " + (character.CurrentHp > 0 ? character.CurrentHp.ToString("0") : "0");
        if (EattackText != null) EattackText.text = "����: " + (character.stats.attack > 0 ? character.stats.attack.ToString() : "0");
        if (EdefenseText != null) EdefenseText.text = "���: " + (character.stats.defense > 0 ? character.stats.defense.ToString() : "0");
        if (EevasionText != null) EevasionText.text = "ȸ��: " + (character.stats.evasion > 0 ? character.stats.evasion.ToString() : "0");
        if (EcriticalText != null) EcriticalText.text = "ġ��Ÿ: " + (character.stats.criticalChance > 0 ? character.stats.criticalChance.ToString() : "0");

    }


}
