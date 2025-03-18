using UnityEngine;
using static SkillManager;

public class SkillEnemynomal : MonoBehaviour
{
    Skill[] SkillE;
    private int currentSkillIndex = 0; // ���� ��ų �ε��� ����

    void Start()
    {
        SkillE = new Skill[2]; // �迭 ũ�� �ʱ�ȭ
        SkillE[0] = SkillManager.Instance.GetSkillData("EnemyAttack");
        SkillE[1] = SkillManager.Instance.GetSkillData("EnemyAttackW");
    }

    public void EnemyAttackN()
    {
        // BattleManager�� ���� ��ų ������ ����
        BattleManager.Instance.SetESkill(SkillE[currentSkillIndex]);
        // ���� ���� ���� �ε��� ��ȯ
        currentSkillIndex = (currentSkillIndex + 1) % 2;
    }
}
