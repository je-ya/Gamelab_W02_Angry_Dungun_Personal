using UnityEngine;
using static SkillManager;

public class SkillAttack : MonoBehaviour
{
    Skill SkillA;

    void Start()
    {

        SkillA = SkillManager.Instance.GetSkillData("IdleAttack");
        //Debug.Log($"�̸�: {SkillA.name}, ��ų Ÿ�� : {SkillA.type}, ���߷�: {SkillA.hitRate}, ������ ����: {SkillA.damageModi}, ���� ������: {SkillA.increaseValue}");
    }

    public void OnAttackButtonClicked()
    {
        if (SkillA != null)
        {
            BattleManager.Instance.SetSkill(SkillA);
        }
    }

}
