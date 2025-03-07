using UnityEngine;
using UnityEngine.UI;

public class GameInfo_ExitButton : MonoBehaviour
{
    Button _gameInfoExitButton;
    GameInfo_UI _gameInfoUI;
    void Start()
    {
        _gameInfoUI = GameObject.FindAnyObjectByType<GameInfo_UI>();
        _gameInfoExitButton = GetComponent<Button>();
        _gameInfoExitButton.onClick.AddListener(GameInfoExitButtonButtonClick);
    }

    /// <summary>
    /// �������� ����� �Լ� 
    /// </summary>
    void GameInfoExitButtonButtonClick()
    {
        _gameInfoUI.gameObject.GetComponent<Canvas>().enabled = false;
    }
}
