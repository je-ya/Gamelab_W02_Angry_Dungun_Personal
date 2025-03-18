using UnityEngine;
using static SkillManager;

public class SkillPower : MonoBehaviour
{
    Skill SkillP;
    void Start()
    {
        SkillP = SkillManager.Instance.GetSkillData("IdlePowerUp");
    }

    public void OnPowerButtonClicked()
    {
        if (SkillP != null)
        {
            BattleManager.Instance.SetSkill(SkillP);
        }
    }

}
