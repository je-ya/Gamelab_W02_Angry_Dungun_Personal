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
                Debug.LogError($"{gameObject.name}에 Character 컴포넌트가 없습니다.");
            }
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // 원래 색상 저장

    }

    void OnMouseEnter()
    {
        if (BattleManager.Instance != null && BattleManager.Instance.IsSelectingTarget())
        {

            if (spriteRenderer != null)
            {
                spriteRenderer.color = highlightColor; // 하이라이트 색상으로 변경
            }
        }

        //Debug.Log($"{character.stats.characterName}위에 마우스가 올라감!");
    }



    void OnMouseExit()
    {
        if (BattleManager.Instance != null && BattleManager.Instance.IsSelectingTarget())
        {

            if (spriteRenderer != null)
            {
                spriteRenderer.color = originalColor; // 원래 색상으로 복원
            }
        }

        //Debug.Log($"{character.stats.characterName}에서 마우스가 나감!");
    }


    void OnMouseDown()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor; // 원래 색상으로 복원
        }
    }

}
