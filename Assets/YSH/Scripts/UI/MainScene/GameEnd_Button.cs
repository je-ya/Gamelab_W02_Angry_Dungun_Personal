using UnityEngine;
using UnityEngine.UI;

public class GameEnd_Button : MonoBehaviour
{
    Button _gameEndButton;
    void Start()
    {
        _gameEndButton = GetComponent<Button>();
        _gameEndButton.onClick.AddListener(EndButtonClick);
    }

    /// <summary>
    /// 눌렸을때 사용할 함수 
    /// </summary>
    void EndButtonClick()
    {
        // 게임 종료 시키고 
        Application.Quit();
    }
}
