using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using TMPro;
using System.Xml;

public class Character : MonoBehaviour
{

    TextMeshProUGUI pDamage;


    public CharacterStats stats;
    [SerializeField]
    private float currentHp;
    [SerializeField]
    float currentStress;
    public float CurrentHp
    {
        get { return currentHp; }
        set { currentHp = value; }
    }

    public float Stress
    {
        get { return currentStress; }
        set { currentStress = value; }
    }

    public Action<Character> OnDamageTaken;

    private void Awake()
    {
        CurrentHp = stats.hp;
        Stress = 0;
    
    }

    public Transform triangle;


    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        pDamage = FindObjectOfType<pDamage>().GetComponent<TextMeshProUGUI>();


    }


    private void ClearText()
    {
        pDamage.text = "";
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Clamp(currentHp, 0, stats.hp);
        if (!gameObject.CompareTag("Player"))
        {
            pDamage.text = $"{damage.ToString("0")}";
            Invoke("ClearText", 0.5f);
        }


        StartCoroutine(Blink());
        Debug.Log($"{stats.characterName}�� {damage} �������� ����. ���� HP: {currentHp}/{stats.hp}");

        if (CurrentHp <= 0)
        {

            if (!gameObject.CompareTag("Player"))
            {
                StateManager.Instance.EndBattle();
                Destroy(gameObject);
            }
            else Destroy(gameObject);


        }
    }

    public void TakeHeal(float heal)
    {
        if (heal < 0) heal = 0;
        currentHp += heal;
        currentHp = Mathf.Clamp(currentHp, 0, stats.hp);
        Debug.Log($"{stats.characterName}�� {heal}��ŭ ȸ����. ���� HP: {currentHp}/{stats.hp}");
    }


    public void TakeSDamage(float damage)
    {
        currentStress += damage;
        currentStress = Mathf.Clamp(currentStress, 0, stats.stress);
        if (currentStress >=100)
        {
            TakeDamage(10f);
        }
        Debug.Log($"{stats.characterName}�� {damage} �������� ����. ���� stress: {currentStress}/{stats.stress}");


    }

    public void TakeSHeal(float heal)
    {
        if (heal < 0) heal = 0;
        currentStress -= heal;
        currentStress = Mathf.Clamp(currentHp, 0, stats.stress);
        Debug.Log($"{stats.characterName}�� {heal}��ŭ ȸ����. ���� stress: {currentStress}/{stats.stress}");
    }


    public void IncreaseStat(string stat, int amount)
    {
        switch (stat)
        {
            case "defense":
                stats.defense += amount;
                Debug.Log($"{stats.characterName}�� ������ {amount} �����Ͽ� {stats.defense}�� �Ǿ����ϴ�.");
                break;
            case "evasion":
                stats.evasion += amount;
                Debug.Log($"{stats.characterName}�� ȸ�ǰ� {amount} �����Ͽ� {stats.evasion}�� �Ǿ����ϴ�.");
                break;
        }
    }


    SpriteRenderer spriteRenderer;

    float blinkInterval = 0.1f;
    float blinkDuration = 0.5f;

    private IEnumerator Blink()
    {
        float elapsedTime = 0f;

        while (elapsedTime < blinkDuration)
        {
            // ��������Ʈ �������� ��
            spriteRenderer.enabled = false;

            // ������ ���ݸ�ŭ ���
            yield return new WaitForSeconds(blinkInterval);

            // ��������Ʈ �������� ��
            spriteRenderer.enabled = true;

            // �ٽ� ������ ���ݸ�ŭ ���
            yield return new WaitForSeconds(blinkInterval);

            // ��� �ð� ������Ʈ
            elapsedTime += blinkInterval * 2;
        }

        // ��ũ ȿ���� ���� �� ��������Ʈ �������� ���� ���·� ����
        spriteRenderer.enabled = true;
    }


}
