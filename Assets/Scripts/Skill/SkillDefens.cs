using UnityEngine;
using static SkillManager;

public class SkillDefens : MonoBehaviour
{
    Skill SkillD;

    void Start()
    {
        SkillD = SkillManager.Instance.GetSkillData("IdleDefenseUp");
    }

    public void OnDefenseButtonClicked()
    {
        if (SkillD != null)
        {
            BattleManager.Instance.SetSkill(SkillD);
        }
    }

}
