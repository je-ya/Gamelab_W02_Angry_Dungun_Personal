using UnityEngine;
using static SkillManager;

public class SkillAttack : MonoBehaviour
{
    Skill SkillA;

    void Start()
    {

        SkillA = SkillManager.Instance.GetSkillData("IdleAttack");
        //Debug.Log($"이름: {SkillA.name}, 스킬 타입 : {SkillA.type}, 명중률: {SkillA.hitRate}, 데미지 보정: {SkillA.damageModi}, 스텟 증가량: {SkillA.increaseValue}");
    }

    public void OnAttackButtonClicked()
    {
        if (SkillA != null)
        {
            BattleManager.Instance.SetSkill(SkillA);
        }
    }

}
