using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.TextCore.Text;

//��ų �Ŵ���, ��ų ����� json���� ���, ��ų�� �߰��ҰŸ�, json���Ͽ� �����͸� �߰�

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }

    public void ExecuteSkill(string skillName, Character user, Character target)
    {
        Debug.Log($"{skillName} ��ų ����: {user.stats.characterName} -> {target.stats.characterName}");
    }




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadSkillDataFromJson();
        }

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
        public int increaseValue;
    }

    [System.Serializable]
    public class SkillData
    {
        public Skill[] skills;
    }

    Dictionary<string, Skill> skillDataMap = new Dictionary<string, Skill>();

    void LoadSkillDataFromJson()
    {
        UnityEngine.TextAsset jsonData = Resources.Load<UnityEngine.TextAsset>("skillInfo");
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


    public Skill GetSkillData(string skillName)
    {
        if (skillDataMap.TryGetValue(skillName, out Skill skill))
        {
            return skill;
        }
        Debug.LogWarning($"Skill '{skillName}' not found in skillDataMap.");
        return null;
    }

    public TextMeshProUGUI tmpText;

    public float CalculateDamage(Character user, Character target, Skill skill)
    {
        float damage = 0f;
        Character userStats = user;
        Character targetStats = target;
        Skill skillStats = skill;
        float persent = skill.hitRate - target.stats.evasion;
        int valueR = UnityEngine.Random.Range(0, 100);
        int valueCR = UnityEngine.Random.Range(0, 100);
        if (valueR <= persent)
        {
            if (userStats != null)
            {
                if(valueCR <= user.stats.criticalChance) {
                    damage += (userStats.stats.attack * (skill.damageModi * 0.01f) * ((100 - targetStats.stats.defense) * 0.01f)*1.5f);
                    Debug.Log("ũ��Ƽ��!");
                    tmpText.text = "ũ��Ƽ��!";
                    Invoke("ClearText", 0.5f);
                }
                else
                damage += (userStats.stats.attack * (skill.damageModi * 0.01f) * ((100 - targetStats.stats.defense) * 0.01f));
            }
        }
        else if (valueR > persent && valueR < skill.hitRate)
        {
            Debug.Log("ȸ��");
            float increaseProbability = 15f;
            float chance = Random.Range(0f, 100f);
            if (chance <= increaseProbability)
            {
                user.TakeSDamage(damage);
                Debug.Log($"[{gameObject.name}]��Ʈ���� {damage}����!!");
            }
            tmpText.text = "ȸ��";
            Invoke("ClearText", 0.5f); // 2�� �� ClearText ȣ��
        }
        else
        {
            Debug.Log("������");
            float increaseProbability = 15f;
            float chance = Random.Range(0f, 100f);
            if (chance <= increaseProbability)
            {
                user.TakeSDamage(damage);
                Debug.Log($"[{gameObject.name}]��Ʈ���� {damage}����!!");
            }

            tmpText.text = "������";
            Invoke("ClearText", 0.5f); // 2�� �� ClearText ȣ��
        }

        return damage;
    }


    public float CalculateHDamage(Character user, Character target, Skill skill)
    {
        float damage = 0f;
        Character userStats = user;
        Character targetStats = target;
        Skill skillStats = skill;

        if (userStats != null)
        {
            damage += (userStats.stats.attack * (skill.damageModi * 0.01f));
        }


        return damage;
    }

    private void ClearText()
    {
        tmpText.text = "";
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

