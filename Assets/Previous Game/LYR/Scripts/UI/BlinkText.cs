using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BlinkText : MonoBehaviour
{
    TextMeshProUGUI blinkText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blinkText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Blink());
    }

    public IEnumerator Blink()
    {
        while(true)
        {

            blinkText.text = "> Game Start?";
            yield return new WaitForSeconds(.5f);
            blinkText.text = "  Game Start?";
            yield return new WaitForSeconds(.5f);
        }    
    }
}
