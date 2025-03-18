using System;
using UnityEngine;

public class Clickable : MonoBehaviour
{

    public Character character;
    Action<Character> Onclick;
    SpriteRenderer spriteRenderer;
    Color originalColor;
    Color highlightColor = new Color(111 / 255f, 111 / 255f, 111 / 255f);

    void Awake()
    {
        if (character == null)
        {
            character = GetComponent<Character>();
            if (character == null)
            {
                Debug.LogError($"{gameObject.name}�� Character ������Ʈ�� �����ϴ�.");
            }
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // ���� ���� ����

    }

    void OnMouseEnter()
    {
        if (BattleManager.Instance != null && BattleManager.Instance.IsSelectingTarget())
        {

            if (spriteRenderer != null)
            {
                spriteRenderer.color = highlightColor; // ���̶���Ʈ �������� ����
            }
        }

        //Debug.Log($"{character.stats.characterName}���� ���콺�� �ö�!");
    }



    void OnMouseExit()
    {
        if (BattleManager.Instance != null && BattleManager.Instance.IsSelectingTarget())
        {

            if (spriteRenderer != null)
            {
                spriteRenderer.color = originalColor; // ���� �������� ����
            }
        }

        //Debug.Log($"{character.stats.characterName}���� ���콺�� ����!");
    }


    void OnMouseDown()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor; // ���� �������� ����
        }
    }

}
