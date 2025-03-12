using UnityEngine;
using UnityEngine.UI;

public class GameInfo_Button : MonoBehaviour
{
    Button _gameInfoButton;
    GameInfo_UI _gameInfoUI;
    void Start()
    {
        _gameInfoButton = GetComponent<Button>();
        _gameInfoUI = GameObject.FindAnyObjectByType<GameInfo_UI>();
        _gameInfoButton.onClick.AddListener(InfoButtonClick);
    }

    /// <summary>
    /// 눌렸을때 사용할 함수 
    /// </summary>
    void InfoButtonClick()
    {
        // 게임에 대한 설명을 가진 UI SetActive 시키기 
        _gameInfoUI.gameObject.GetComponent<Canvas>().enabled = true;

    }
}
