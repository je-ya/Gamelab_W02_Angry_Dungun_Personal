using UnityEngine;
using static SkillManager;


public class SkillHeal : MonoBehaviour
{
    Skill skillH;

    void Start()
    {
        skillH = SkillManager.Instance.GetSkillData("IdleHeal");

    }

    public void OnHealButtonClicked()
    {
        if (skillH != null)
        {
            BattleManager.Instance.SetSkill(skillH);
        }
    }
}
