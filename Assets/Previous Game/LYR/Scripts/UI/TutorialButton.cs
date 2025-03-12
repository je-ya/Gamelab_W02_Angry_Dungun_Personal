using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialButton : MonoBehaviour
{
    Button _gameStartButton;
    void Start()
    {
        _gameStartButton = GetComponent<Button>();
        _gameStartButton.onClick.AddListener(StartButtonClick);
    }

    /// <summary>
    /// �������� ����� �Լ� 
    /// </summary>
    void StartButtonClick()
    {
        // �� �ѱ�� 
        SceneManager.LoadScene("Tutorial");
    }


}
