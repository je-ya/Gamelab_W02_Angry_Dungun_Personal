using UnityEngine;
using static SkillManager;

public class SkillEnemynomal : MonoBehaviour
{
    Skill[] SkillE;
    private int currentSkillIndex = 0; // 현재 스킬 인덱스 추적

    void Start()
    {
        SkillE = new Skill[2]; // 배열 크기 초기화
        SkillE[0] = SkillManager.Instance.GetSkillData("EnemyAttack");
        SkillE[1] = SkillManager.Instance.GetSkillData("EnemyAttackW");
    }

    public void EnemyAttackN()
    {
        // BattleManager로 현재 스킬 데이터 전송
        BattleManager.Instance.SetESkill(SkillE[currentSkillIndex]);
        // 다음 턴을 위한 인덱스 전환
        currentSkillIndex = (currentSkillIndex + 1) % 2;
    }
}
