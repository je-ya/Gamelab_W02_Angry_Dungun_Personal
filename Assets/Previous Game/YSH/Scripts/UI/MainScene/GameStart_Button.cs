using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStart_Button : MonoBehaviour
{
    Button _gameStartButton;
    void Start()
    {
        _gameStartButton = GetComponent<Button>();
        _gameStartButton.onClick.AddListener(StartButtonClick);
    }

    /// <summary>
    /// 눌렸을때 사용할 함수 
    /// </summary>
    void StartButtonClick()
    {
        // 씬 넘기고 
        SceneManager.LoadScene("InGame");
    }
    

}
