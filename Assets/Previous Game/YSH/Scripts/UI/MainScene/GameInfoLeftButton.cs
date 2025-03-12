using UnityEngine;
using UnityEngine.UI;

public class GameInfoLeftButton : MonoBehaviour
{
    Button _gameInfoExitButton;
    public Info1 _info1;
    public Info2 _info2;

    void Start()
    {
        _gameInfoExitButton = GetComponent<Button>();
        _gameInfoExitButton.onClick.AddListener(GameInfoLeftButtonButtonClick);
    }

    /// <summary>
    /// �������� ����� �Լ� 
    /// </summary>
    void GameInfoLeftButtonButtonClick()
    {
        if (_info1.gameObject.activeSelf)
        {
            _info1.gameObject.SetActive(false);
            _info2.gameObject.SetActive(true);
        }
        else
        {
            _info1.gameObject.SetActive(true);
            _info2.gameObject.SetActive(false);
        }
    }
}
