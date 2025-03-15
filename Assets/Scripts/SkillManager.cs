using System.Collections.Generic;
using UnityEngine;

//��ų �Ŵ���, ��ų ����� json���� ���, ��ų�� �߰��ҰŸ�, json���Ͽ� �����͸� �߰��ϰ�,
//skillCategorie ��ųʸ��� �ش� ��ų �̸� ������ ���� �߰�

public class SkillManager : MonoBehaviour
{
    private void Awake()
    {
        LoadSkillDataFromJson();
    }

    private void Start()
    {

        PrintSkillData();
    }


    [System.Serializable]
    public class Skill
    {
        public string name;
        public string type;
        public float hitRate;
        public float damageModi;
        public float increaseValue;
    }

    [System.Serializable]
    public class SkillData
    {
        public Skill[] skills;
    }

    Dictionary<string, Skill> skillDataMap = new Dictionary<string, Skill>();

    void LoadSkillDataFromJson()
    {
        TextAsset jsonData = Resources.Load<TextAsset>("skillInfo");
        if (jsonData != null)
        {
            //Debug.Log("JSON ����: " + jsonData.text); // ������

            // JSON�� SkillData ���·� ��ȯ
            SkillData skillData = JsonUtility.FromJson<SkillData>(jsonData.text);
            if (skillData != null && skillData.skills != null)
            {
                foreach (Skill skill in skillData.skills)
                {
                    skillDataMap[skill.name] = skill;
                }
                Debug.Log("��ų ������ �ε� �Ϸ�!");
            }
            else
            {
                Debug.LogError("JSON �Ľ� ����: �����Ͱ� null�Դϴ�.");
            }
        }
        else
        {
            Debug.LogError("skillInfo.json ������ ã�� �� �����ϴ�.");
        }
    }

    void PrintSkillData()
    {
        Debug.Log("===== �ε�� ��ų ��� =====");
        if (skillDataMap.Count == 0)
        {
            Debug.Log("��ų �����Ͱ� �����ϴ�.");
            return;
        }

        foreach (var skillPair in skillDataMap)
        {
            Skill skill = skillPair.Value;
            Debug.Log($"�̸�: {skill.name}, ��ų Ÿ�� : {skill.type}, ���߷�: {skill.hitRate}, ������ ����: {skill.damageModi}, ���� ������: {skill.increaseValue}");
        }
        Debug.Log("==========================");
    }

    //��ų Ÿ���̶� ��ų �̸� ��ųʸ�(Ÿ��, ����Ʈ(�̸�))
    //private Dictionary<string, List<string>> skillCategorie = new Dictionary<string, List<string>>
    //{
    //    {"Attack", new List<string>{"IdleAttack"} },
    //    {"Heal", new List<string>{"IdleHeal"} },
    //    {"DefenseUp", new List<string>{"IdleDefenseUp"} },
    //    {"PowerUp", new List<string>{"IdlePowerUp"} },
    //    {"EvasionUp", new List<string>{"IdleEvasionUp"} }
    //};

    //��ų �̸��� �Է��ϸ� � Ÿ������ Ȯ�����ִ� �Լ�
    //public string CheckUsedSkill(string skillName)
    //{
    //    foreach (var category in skillCategorie)
    //    {
    //        if (category.Value.Contains(skillName)) // skillName�� �ش� ī�װ��� �����ϴ��� Ȯ��
    //        {
    //            return category.Key; // �ش� ī�װ� ��ȯ
    //        }
    //    }

    //    Debug.LogWarning($"��ų {skillName}��(��) � ī�װ����� ������ �ʽ��ϴ�.");
    //    return "Unknown"; // �ش��ϴ� ī�װ��� ���� ���
    //}


    //Action���� ��ü�ϱ� ���� ������ �ߴ� �Լ�
    /*    void UseSkill(string skillName)
        {
            // ��ų Ÿ�� Ȯ��
            string skillType = CheckUsedSkill(skillName);

            // skillDataMap���� �ش� ��ų�� ���� ��������
            if (!skillDataMap.TryGetValue(skillName, out Skill skill))
            {
                Debug.LogError($"��ų '{skillName}'��(��) ã�� �� �����ϴ�.");
                return;
            }

            // �� ������ �ִ� ������ �ִ� ��ų�� ���� :
            // skillName = skill.name : ��ų �̸�
            // skillType : ��ų Ÿ��
            // skill.hitRate : float ��ų ���߷�
            // skill.damageModi : float ��ų ������ ����
            // skill.effectValue : float ���� ������
            // �׷� �� �������� ĳ���ͳ� �� ���� ��ũ��Ʈ���� ����ؾ� ��.
            // �׷��� �ش� ��ũ��Ʈ�� �ִ� �Լ����� �ҷ����� �Ǵ°ɱ�?
            if (skillType == "Attack")
            {

            }else if(skillType == "Heal")
            {

            }
            else if (skillType == "DefenseUp")
            {

            }
            else if (skillType == "PowerUp")
            {

            }
            else if (skillType == "EvasionUp")
            {

            }
            else { Debug.Log("��ų Ÿ�� ����!!"); }


        }*/


}

