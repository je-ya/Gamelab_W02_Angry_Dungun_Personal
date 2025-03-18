using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SkillManager;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance => _instance;
    static BattleManager _instance;

    public Button[] buttons;

    [System.Serializable]
    enum BattleState
    {
        IdleAttack,
        PlayerTurn,
        EnemyTurn
    }

    [SerializeField]
    BattleState state;

    
    public List<Character> playerCharacters;
    [SerializeField]
    private GameObject playerParent;

    public Character currentCForSkill;

    private int currentCharacterIndex = 0;
    int enemyIndex = 0;

    [SerializeField]
    private List<Character> EnemyCharacters;
    [SerializeField]
    private GameObject spwaner;


    private UIManager uiManager;

    bool playerTurnStart = true;

    float turnTimer = 0f;

    Character targetCharacter;

    Skill skill;
    Skill eSkill;

    [SerializeField]
    private bool _isSelect;
    public bool isSelect
    {
        get { return _isSelect; }
        set { _isSelect = value; }
    }

    private SkillSelected currentSkillSelected;
    float damage;
    int value;

    public TextMeshProUGUI trunText;
    int trun = 1;

    bool inBattle = false; 
    



    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        if (playerParent == null)
        {
            playerParent = FindObjectOfType<Player>().gameObject;
            if (playerParent == null)
            {
                Debug.LogError("GameManager: 'Player' 오브젝트를 찾을 수 없습니다.");
                return;
            }
        }
        if (spwaner == null)
        {
            spwaner = FindObjectOfType<YR_SpawnManager>().gameObject;
            if(spwaner == null)
            {
                Debug.LogError("GameManager: 'spwaner' 오브젝트를 찾을 수 없습니다.");
                return;
            }
        }
        InitCharacter();
        SetButtonsInteractable(false);

    }

    void InitCharacter()
    {
        playerCharacters = new List<Character>();
        foreach (Transform child in playerParent.transform)
        {
            Character character = child.GetComponent<Character>();
            if (character != null)
            {
                playerCharacters.Add(character);
                Debug.Log($"플레이어 캐릭터 추가: {character.stats.characterName}");
            }
        }
    }

    public void InitEnemy()
    {
        EnemyCharacters = new List<Character>();
        foreach (Transform child in spwaner.transform)
        {
            Character character = child.GetComponent<Character>();
            if (character != null)
            {
                EnemyCharacters.Add(character);
                Debug.Log($"적 캐릭터 추가: {character.stats.characterName}");
            }
        }
    }



    void Start()
    {
        
        _instance = this;
        _isSelect = false;

    }

    bool battleStart =false;

    public void StartBattle()
    {
        battleStart = true;
        state = BattleState.IdleAttack;

    }




    void Update()
    {
        inBattle = StateManager.Instance.CheckBattleActive();
        if (!inBattle)
        {
            SetButtonsInteractable(false);
        }
        if (battleStart)
        {
            switch (state)
            {
                case BattleState.PlayerTurn:
                    PlayerTurn();
                    break;
                case BattleState.EnemyTurn:
                    EnemyTurn();
                    break;
                case BattleState.IdleAttack:
                    IdleAttack();
                    break;
            }
            if (_isSelect == true && Input.GetMouseButtonDown(0)) DetectTarget();
        }
        trunText.text = ($"{trun}");
    }


    void PlayerTurn()
    {
        SetButtonsInteractable(true);
        if (playerTurnStart == true)
        {
            StartPlayerTurn();
            playerTurnStart = false;
        }
    }

    private void StartPlayerTurn()
    {
        if (playerCharacters == null || playerCharacters.Count == 0)
        {
            Debug.LogError("StartPlayerTurn: playerCharacters가 없습니다.");
            return;
        }

        currentCharacterIndex = 0;
        Debug.Log($"StartPlayerTurn: 첫 번째 캐릭터 인덱스 {currentCharacterIndex}");
        UpdateUIForCurrentCharacter();
    }

    private void UpdateUIForCurrentCharacter()
    {
        if (uiManager == null)
        {
            Debug.LogError("UpdateUIForCurrentCharacter: uiManager가 null입니다.");
            return;
        }

        if (currentCharacterIndex >= 0 && currentCharacterIndex < playerCharacters.Count)
        {
            Character currentChar = playerCharacters[currentCharacterIndex];
            currentCForSkill = currentChar;
            

            //Debug.Log($"UI 업데이트 요청: {currentChar.stats.name}, HP: {currentChar.stats.hp}");
            uiManager.UpdateCharacterUI(currentChar);
            
        }
        else
        {
            Debug.Log("플레이어 턴 종료!");
            
            state = BattleState.EnemyTurn;
        }
        
    }

    Character currentE;
    public void UpdateUIForCurrentEnemy()
    {
        currentE = EnemyCharacters[enemyIndex];
        uiManager.UpdateEnemyUI(currentE);
    }


    void EnemyTurn()
    {
        SetButtonsInteractable(false);

        turnTimer += Time.deltaTime;
        if (turnTimer >= 0.5f) // 적당한 대기 시간 후 플레이어 턴 시작
        {
            turnTimer = 0f;
            if (EnemyCharacters != null)
            {
                // 현재 적 캐릭터에서 SkillEnemynomal 스크립트 가져오기
                SkillEnemynomal enemyScript = currentE.GetComponent<SkillEnemynomal>();
                if (enemyScript != null)
                {
                    enemyScript.EnemyAttackN(); // 적이 BattleManager로 스킬 전송
                    Debug.Log($"EnemyTurn: 받은 스킬 - {eSkill.name}");
                    // 여기서 스킬을 사용한 추가 로직(타겟 설정, 데미지 적용 등)을 넣을 수 있음
                    foreach (Character player in playerCharacters)
                    {
                        if (player != null && player.stats.hp > 0) // 살아있는 플레이어만 대상
                        {
                            float damage = SkillManager.Instance.CalculateDamage(currentE, player, eSkill);
                            player.TakeDamage(damage);

                            Debug.Log($"{player.stats.characterName}에게 {damage} 데미지 적용");
                        }
                    }

                    skill = null; // 스킬 사용 후 초기화
                }
                else
                {
                    Debug.LogWarning($"Enemy {EnemyCharacters[enemyIndex].stats.characterName}에 SkillEnemynomal 스크립트가 없습니다.");
                }

                trun++;
                Debug.Log("적 턴 종료!");
                state = BattleState.IdleAttack;


            }
        }
        }

        void IdleAttack()
    {
        turnTimer += Time.deltaTime;
        if (turnTimer >= 0.5f) // 적당한 대기 시간 후 플레이어 턴 시작
        {
            turnTimer = 0f;
            Debug.Log("기본 공격 종료!");
            state = BattleState.PlayerTurn;
            playerTurnStart = true; // 플레이어 턴이 정상적으로 시작되도록 설정
        }
    }


    private void DetectTarget()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            Clickable clickable = hit.collider.GetComponent<Clickable>();
            if (clickable != null && clickable.character != null)
            {
                //Debug.Log($"마우스로 클릭된 대상: {clickable.character.stats.name}");
                OnTargetSelected(clickable.character);
            }
        }
    }

    public void SetCurrentSkillSelected(SkillSelected skillSelected) // SkillSelected 설정 메서드 추가
    {
        currentSkillSelected = skillSelected;
    }

    public void SetSkill(Skill selectedSkill)
    {
        skill = selectedSkill;
    }

    public void SetESkill(Skill selectedSkill)
    {
        eSkill = selectedSkill;
    }


    public void OnTargetSelected(Character target)
    {
        Debug.Log($"대상 선택됨: {target.stats.name}");
        targetCharacter = target;

        currentSkillSelected.TargetSelect = true;


        if (skill.type == "Attack")
        {
            damage = SkillManager.Instance.CalculateDamage(currentCForSkill, targetCharacter, skill);
            targetCharacter.TakeDamage(damage);
        }
        else if (skill.type == "Heal")
        {
            damage = SkillManager.Instance.CalculateHDamage(currentCForSkill, targetCharacter, skill);
            targetCharacter.TakeHeal(damage);
        }
        else if (skill.type == "DefenseUp")
        {
            value = skill.increaseValue;
            targetCharacter.IncreaseStat("defense", value);
        }
        else if (skill.type == "PowerUp")
        {
            value = skill.increaseValue;
        }
        else if (skill.type == "EvasionUp")
        {
            value = skill.increaseValue;
            targetCharacter.IncreaseStat("evasion", value);
        }
        else
        {
            Debug.Log("스킬 데이터 확인 바람");
        }
        value = 0;
        damage = 0;
        skill = null;
        currentCharacterIndex++;
        UpdateUIForCurrentCharacter();
        UpdateUIForCurrentEnemy();
    }

    public bool IsSelectingTarget()
    {
        return isSelect;
    }


    public void SetButtonsInteractable(bool isInteractable)
    {

        foreach (Button button in buttons)
        {
            button.interactable = isInteractable;
        }
    }






}
