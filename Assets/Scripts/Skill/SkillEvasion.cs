using UnityEngine;
using static SkillManager;

public class SkillEvasion : MonoBehaviour
{

    Skill SkillE;

    void Start()
    {
        SkillE = SkillManager.Instance.GetSkillData("IdleEvasionUp");
    }


    public void OnEvasionButtonClicked()
    {
        if (SkillE != null)
        {
            BattleManager.Instance.SetSkill(SkillE);
        }
    }


}
