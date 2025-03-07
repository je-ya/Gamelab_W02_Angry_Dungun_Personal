using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Aazz0200_GameManager : MonoBehaviour
{
    public float playtime;
    public float EndTime = 150;
    public Slider ui;
    public GameObject Victory;


    void Update()
    {

    }

    public void To_Main()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }


}
