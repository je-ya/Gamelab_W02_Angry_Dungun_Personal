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
                Debug.LogError("GameManager: 'Player' ������Ʈ�� ã�� �� �����ϴ�.");
                return;
            }
        }
        if (spwaner == null)
        {
            spwaner = FindObjectOfType<YR_SpawnManager>().gameObject;
            if(spwaner == null)
            {
                Debug.LogError("GameManager: 'spwaner' ������Ʈ�� ã�� �� �����ϴ�.");
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
                Debug.Log($"�÷��̾� ĳ���� �߰�: {character.stats.characterName}");
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
                Debug.Log($"�� ĳ���� �߰�: {character.stats.characterName}");
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
            Debug.LogError("StartPlayerTurn: playerCharacters�� �����ϴ�.");
            return;
        }

        currentCharacterIndex = 0;
        Debug.Log($"StartPlayerTurn: ù ��° ĳ���� �ε��� {currentCharacterIndex}");
        UpdateUIForCurrentCharacter();
    }

    private void UpdateUIForCurrentCharacter()
    {
        if (uiManager == null)
        {
            Debug.LogError("UpdateUIForCurrentCharacter: uiManager�� null�Դϴ�.");
            return;
        }

        if (currentCharacterIndex >= 0 && currentCharacterIndex < playerCharacters.Count)
        {
            Character currentChar = playerCharacters[currentCharacterIndex];
            currentCForSkill = currentChar;
            

            //Debug.Log($"UI ������Ʈ ��û: {currentChar.stats.name}, HP: {currentChar.stats.hp}");
            uiManager.UpdateCharacterUI(currentChar);
            
        }
        else
        {
            Debug.Log("�÷��̾� �� ����!");
            
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
        if (turnTimer >= 0.5f) // ������ ��� �ð� �� �÷��̾� �� ����
        {
            turnTimer = 0f;
            if (EnemyCharacters != null)
            {
                // ���� �� ĳ���Ϳ��� SkillEnemynomal ��ũ��Ʈ ��������
                SkillEnemynomal enemyScript = currentE.GetComponent<SkillEnemynomal>();
                if (enemyScript != null)
                {
                    enemyScript.EnemyAttackN(); // ���� BattleManager�� ��ų ����
                    Debug.Log($"EnemyTurn: ���� ��ų - {eSkill.name}");
                    // ���⼭ ��ų�� ����� �߰� ����(Ÿ�� ����, ������ ���� ��)�� ���� �� ����
                    foreach (Character player in playerCharacters)
                    {
                        if (player != null && player.stats.hp > 0) // ����ִ� �÷��̾ ���
                        {
                            float damage = SkillManager.Instance.CalculateDamage(currentE, player, eSkill);
                            player.TakeDamage(damage);

                            Debug.Log($"{player.stats.characterName}���� {damage} ������ ����");
                        }
                    }

                    skill = null; // ��ų ��� �� �ʱ�ȭ
                }
                else
                {
                    Debug.LogWarning($"Enemy {EnemyCharacters[enemyIndex].stats.characterName}�� SkillEnemynomal ��ũ��Ʈ�� �����ϴ�.");
                }

                trun++;
                Debug.Log("�� �� ����!");
                state = BattleState.IdleAttack;


            }
        }
        }

        void IdleAttack()
    {
        turnTimer += Time.deltaTime;
        if (turnTimer >= 0.5f) // ������ ��� �ð� �� �÷��̾� �� ����
        {
            turnTimer = 0f;
            Debug.Log("�⺻ ���� ����!");
            state = BattleState.PlayerTurn;
            playerTurnStart = true; // �÷��̾� ���� ���������� ���۵ǵ��� ����
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
                //Debug.Log($"���콺�� Ŭ���� ���: {clickable.character.stats.name}");
                OnTargetSelected(clickable.character);
            }
        }
    }

    public void SetCurrentSkillSelected(SkillSelected skillSelected) // SkillSelected ���� �޼��� �߰�
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
        Debug.Log($"��� ���õ�: {target.stats.name}");
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
            Debug.Log("��ų ������ Ȯ�� �ٶ�");
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
