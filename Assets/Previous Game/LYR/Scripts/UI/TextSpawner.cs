using UnityEngine;
using TMPro;

using System.Collections;

public class TextSpawner : MonoBehaviour
{
    TextMeshProUGUI textMeshPro;
    float typingSpeed = 0.05f;

    string fullText;
    bool typingDone;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        fullText = textMeshPro.text;
        textMeshPro.text = "";
        typingDone = false;
        StartCoroutine(TypeText());
         
        
    }


    void Update()
    {
        // ���콺 ���� ��ư Ŭ�� ����
        if (typingDone == true &&Input.GetMouseButtonDown(0)) // 0�� ���� ��ư, 1�� ������, 2�� ���
        {
            
        }
    }
     
    IEnumerator TypeText()
    {
        foreach(char letter in fullText)
        {
            textMeshPro.text += letter; ;
            yield return new WaitForSeconds(typingSpeed);
        }
        typingDone = true;
    }



}
