using TMPro;
using UnityEngine;

public class UI_Timer : MonoBehaviour
{


    public TextMeshProUGUI timerText; // 인스펙터에서 연결할 TMP UI
    private float timeElapsed = 0f;   // 경과 시간
    private int startTime = 60;       // 시작 시간 (60초)
    private bool isRunning = false;

    private Color normalColor = Color.white; // 기본 색상 (흰색)
    private Color warningColor = Color.red;  // 10초 이하일 때 색상 (빨간색)

    void Start()
    {
        if (timerText == null)
        {
            Debug.LogError("TimerText가 연결되지 않았습니다! 인스펙터에서 연결해주세요.");
            return;
        }

        timerText.color = normalColor; // 시작 시 기본 색상 설정
        isRunning = true;
        UpdateTimerDisplay();
        Debug.Log("타이머 시작!");
    }

    void Update()
    {
        if (isRunning)
        {
            timeElapsed += Time.deltaTime; // 경과 시간 증가
            int remainingTime = startTime - Mathf.FloorToInt(timeElapsed); // 남은 시간 계산

            if (remainingTime >= 0)
            {
                UpdateTimerDisplay();
                // 10초 이하일 때 색상 변경
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
                timerText.text = ""; // 0초가 되면 텍스트 비움
                Debug.Log("타이머 종료!");
            }
        }
    }

    void UpdateTimerDisplay()
    {
        int remainingTime = startTime - Mathf.FloorToInt(timeElapsed);
        timerText.text = remainingTime.ToString(); // 초 단위로만 표시
    }
}

