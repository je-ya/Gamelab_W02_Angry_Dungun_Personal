using UnityEngine;
using UnityEngine.UI;

public class GameInfoRightButton : MonoBehaviour
{
    Button _gameInfoExitButton;
    public Info1 _info1;
    public Info2 _info2;

    void Start()
    {
        _gameInfoExitButton = GetComponent<Button>();
        _gameInfoExitButton.onClick.AddListener(GameInfoRightButtonButtonClick);
    }

    /// <summary>
    /// �������� ����� �Լ� 
    /// </summary>
    void GameInfoRightButtonButtonClick()
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

