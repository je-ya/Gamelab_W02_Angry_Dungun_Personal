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
        Debug.Log($"{stats.characterName}이 {damage} 데미지를 받음. 남은 HP: {currentHp}/{stats.hp}");

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
        Debug.Log($"{stats.characterName}이 {heal}만큼 회복됨. 현재 HP: {currentHp}/{stats.hp}");
    }


    public void TakeSDamage(float damage)
    {
        currentStress += damage;
        currentStress = Mathf.Clamp(currentStress, 0, stats.stress);
        if (currentStress >=100)
        {
            TakeDamage(10f);
        }
        Debug.Log($"{stats.characterName}이 {damage} 데미지를 받음. 현재 stress: {currentStress}/{stats.stress}");


    }

    public void TakeSHeal(float heal)
    {
        if (heal < 0) heal = 0;
        currentStress -= heal;
        currentStress = Mathf.Clamp(currentHp, 0, stats.stress);
        Debug.Log($"{stats.characterName}이 {heal}만큼 회복됨. 현재 stress: {currentStress}/{stats.stress}");
    }


    public void IncreaseStat(string stat, int amount)
    {
        switch (stat)
        {
            case "defense":
                stats.defense += amount;
                Debug.Log($"{stats.characterName}의 방어력이 {amount} 증가하여 {stats.defense}가 되었습니다.");
                break;
            case "evasion":
                stats.evasion += amount;
                Debug.Log($"{stats.characterName}의 회피가 {amount} 증가하여 {stats.evasion}가 되었습니다.");
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
            // 스프라이트 렌더러를 끔
            spriteRenderer.enabled = false;

            // 지정된 간격만큼 대기
            yield return new WaitForSeconds(blinkInterval);

            // 스프라이트 렌더러를 켬
            spriteRenderer.enabled = true;

            // 다시 지정된 간격만큼 대기
            yield return new WaitForSeconds(blinkInterval);

            // 경과 시간 업데이트
            elapsedTime += blinkInterval * 2;
        }

        // 블링크 효과가 끝난 후 스프라이트 렌더러를 켜진 상태로 유지
        spriteRenderer.enabled = true;
    }


}
