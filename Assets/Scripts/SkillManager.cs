using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.TextCore.Text;

//스킬 매니저, 스킬 목록은 json파일 사용, 스킬을 추가할거면, json파일에 데이터를 추가

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }

    public void ExecuteSkill(string skillName, Character user, Character target)
    {
        Debug.Log($"{skillName} 스킬 실행: {user.stats.characterName} -> {target.stats.characterName}");
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
            //Debug.Log("JSON 내용: " + jsonData.text); // 디버깅용

            // JSON을 SkillData 형태로 변환
            SkillData skillData = JsonUtility.FromJson<SkillData>(jsonData.text);
            if (skillData != null && skillData.skills != null)
            {
                foreach (Skill skill in skillData.skills)
                {
                    skillDataMap[skill.name] = skill;
                }
                Debug.Log("스킬 데이터 로드 완료!");
            }
            else
            {
                Debug.LogError("JSON 파싱 실패: 데이터가 null입니다.");
            }
        }
        else
        {
            Debug.LogError("skillInfo.json 파일을 찾을 수 없습니다.");
        }
    }

    void PrintSkillData()
    {
        Debug.Log("===== 로드된 스킬 목록 =====");
        if (skillDataMap.Count == 0)
        {
            Debug.Log("스킬 데이터가 없습니다.");
            return;
        }

        foreach (var skillPair in skillDataMap)
        {
            Skill skill = skillPair.Value;
            Debug.Log($"이름: {skill.name}, 스킬 타입 : {skill.type}, 명중률: {skill.hitRate}, 데미지 보정: {skill.damageModi}, 스텟 증가량: {skill.increaseValue}");
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
                    Debug.Log("크리티컬!");
                    tmpText.text = "크리티컬!";
                    Invoke("ClearText", 0.5f);
                }
                else
                damage += (userStats.stats.attack * (skill.damageModi * 0.01f) * ((100 - targetStats.stats.defense) * 0.01f));
            }
        }
        else if (valueR > persent && valueR < skill.hitRate)
        {
            Debug.Log("회피");
            float increaseProbability = 15f;
            float chance = Random.Range(0f, 100f);
            if (chance <= increaseProbability)
            {
                user.TakeSDamage(damage);
                Debug.Log($"[{gameObject.name}]스트레스 {damage}증가!!");
            }
            tmpText.text = "회피";
            Invoke("ClearText", 0.5f); // 2초 후 ClearText 호출
        }
        else
        {
            Debug.Log("빗나감");
            float increaseProbability = 15f;
            float chance = Random.Range(0f, 100f);
            if (chance <= increaseProbability)
            {
                user.TakeSDamage(damage);
                Debug.Log($"[{gameObject.name}]스트레스 {damage}증가!!");
            }

            tmpText.text = "빗나감";
            Invoke("ClearText", 0.5f); // 2초 후 ClearText 호출
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




    //스킬 타입이랑 스킬 이름 딕셔너리(타입, 리스트(이름))
    //private Dictionary<string, List<string>> skillCategorie = new Dictionary<string, List<string>>
    //{
    //    {"Attack", new List<string>{"IdleAttack"} },
    //    {"Heal", new List<string>{"IdleHeal"} },
    //    {"DefenseUp", new List<string>{"IdleDefenseUp"} },
    //    {"PowerUp", new List<string>{"IdlePowerUp"} },
    //    {"EvasionUp", new List<string>{"IdleEvasionUp"} }
    //};

    //스킬 이름을 입력하면 어떤 타입인지 확인해주는 함수
    //public string CheckUsedSkill(string skillName)
    //{
    //    foreach (var category in skillCategorie)
    //    {
    //        if (category.Value.Contains(skillName)) // skillName이 해당 카테고리에 존재하는지 확인
    //        {
    //            return category.Key; // 해당 카테고리 반환
    //        }
    //    }

    //    Debug.LogWarning($"스킬 {skillName}이(가) 어떤 카테고리에도 속하지 않습니다.");
    //    return "Unknown"; // 해당하는 카테고리가 없을 경우
    //}


    //Action으로 대체하기 전에 쓰려고 했던 함수
    /*    void UseSkill(string skillName)
        {
            // 스킬 타입 확인
            string skillType = CheckUsedSkill(skillName);

            // skillDataMap에서 해당 스킬의 정보 가져오기
            if (!skillDataMap.TryGetValue(skillName, out Skill skill))
            {
                Debug.LogError($"스킬 '{skillName}'을(를) 찾을 수 없습니다.");
                return;
            }

            // 이 시점에 있는 가지고 있는 스킬의 정보 :
            // skillName = skill.name : 스킬 이름
            // skillType : 스킬 타입
            // skill.hitRate : float 스킬 명중률
            // skill.damageModi : float 스킬 데미지 배율
            // skill.effectValue : float 스텟 증가량
            // 그럼 이 정보들을 캐릭터나 적 스텟 스크립트에서 사용해야 함.
            // 그러면 해당 스크립트에 있는 함수들을 불러오면 되는걸까?
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
            else { Debug.Log("스킬 타입 오류!!"); }


        }*/


}

