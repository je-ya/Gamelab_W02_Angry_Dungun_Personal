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
            Debug.LogError("UpdateCharacterUI: 캐릭터가 null입니다.");
            return;
        }

        currentCharacter = character;
        Debug.Log($"UI 업데이트: {character.stats.name}");


        if (nameText != null) nameText.text = (string.IsNullOrEmpty(character.stats.name) ? "Unknown" : character.stats.name);
        if (hpText != null) hpText.text = "HP: " + (character.CurrentHp > 0 ? character.CurrentHp.ToString("0") : "0");
        if (attackText != null) attackText.text = "공격: " + (character.stats.attack > 0 ? character.stats.attack.ToString() : "0");
        if (defenseText != null) defenseText.text = "방어: " + (character.stats.defense > 0 ? character.stats.defense.ToString() : "0");
        if (evasionText != null) evasionText.text = "회피: " + (character.stats.evasion > 0 ? character.stats.evasion.ToString() : "0");
        if (criticalText != null) criticalText.text = "치명타: " + (character.stats.criticalChance > 0 ? character.stats.criticalChance.ToString() : "0");
        if (stressText != null) stressText.text = "스트레스  " + (character.stats.stress > 0 ? character.Stress.ToString() : "0");
    }


    public TextMeshProUGUI EnemyText, EhpText, EattackText, EdefenseText, EevasionText, EcriticalText;
    public void UpdateEnemyUI(Character character)
    {
        if (character == null)
        {
            Debug.LogError("UpdateCharacterUI: 캐릭터가 null입니다.");
            return;
        }

        currentCharacter = character;
        Debug.Log($"UI 업데이트: {character.stats.name}");


        if (EnemyText != null) EnemyText.text = (string.IsNullOrEmpty(character.stats.name) ? "Unknown" : character.stats.name);
        if (EhpText != null) EhpText.text = "HP: " + (character.CurrentHp > 0 ? character.CurrentHp.ToString("0") : "0");
        if (EattackText != null) EattackText.text = "공격: " + (character.stats.attack > 0 ? character.stats.attack.ToString() : "0");
        if (EdefenseText != null) EdefenseText.text = "방어: " + (character.stats.defense > 0 ? character.stats.defense.ToString() : "0");
        if (EevasionText != null) EevasionText.text = "회피: " + (character.stats.evasion > 0 ? character.stats.evasion.ToString() : "0");
        if (EcriticalText != null) EcriticalText.text = "치명타: " + (character.stats.criticalChance > 0 ? character.stats.criticalChance.ToString() : "0");

    }


}
