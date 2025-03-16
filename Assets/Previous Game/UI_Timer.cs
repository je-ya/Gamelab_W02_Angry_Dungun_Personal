using TMPro;
using UnityEngine;

public class UI_Timer : MonoBehaviour
{


    public TextMeshProUGUI timerText; // �ν����Ϳ��� ������ TMP UI
    private float timeElapsed = 0f;   // ��� �ð�
    private int startTime = 60;       // ���� �ð� (60��)
    private bool isRunning = false;

    private Color normalColor = Color.white; // �⺻ ���� (���)
    private Color warningColor = Color.red;  // 10�� ������ �� ���� (������)

    void Start()
    {
        if (timerText == null)
        {
            Debug.LogError("TimerText�� ������� �ʾҽ��ϴ�! �ν����Ϳ��� �������ּ���.");
            return;
        }

        timerText.color = normalColor; // ���� �� �⺻ ���� ����
        isRunning = true;
        UpdateTimerDisplay();
        Debug.Log("Ÿ�̸� ����!");
    }

    void Update()
    {
        if (isRunning)
        {
            timeElapsed += Time.deltaTime; // ��� �ð� ����
            int remainingTime = startTime - Mathf.FloorToInt(timeElapsed); // ���� �ð� ���

            if (remainingTime >= 0)
            {
                UpdateTimerDisplay();
                // 10�� ������ �� ���� ����
                if (remainingTime <= 10)
                {
                    timerText.color = warningColor;
                }
                else
                {
                    timerText.color = normalColor;
                }
            }
            else
            {
                isRunning = false;
                timerText.text = ""; // 0�ʰ� �Ǹ� �ؽ�Ʈ ���
                Debug.Log("Ÿ�̸� ����!");
            }
        }
    }

    void UpdateTimerDisplay()
    {
        int remainingTime = startTime - Mathf.FloorToInt(timeElapsed);
        timerText.text = remainingTime.ToString(); // �� �����θ� ǥ��
    }
}

