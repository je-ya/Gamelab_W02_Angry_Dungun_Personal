using System.Collections;
using TMPro;
using UnityEngine;


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
        while (true)
        {

            blinkText.text = "> Game Start?";
            yield return new WaitForSeconds(.5f);
            blinkText.text = "  Game Start?";
            yield return new WaitForSeconds(.5f);
        }
    }
}
