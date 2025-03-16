using UnityEngine;
using UnityEngine.SceneManagement;

public class Trigger : MonoBehaviour
{

    public UnityEngine.Events.UnityEvent OnEnter;
    // public UnityEngine.Events.UnityEvent OnColEnter;
    //public UnityEngine.Events.UnityEvent OnEixt;
    // public LayerMask CheckLayer;


    public void ActivateTrigger()
    {
        OnEnter.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Aazz0200_Player>() != null)
            OnEnter.Invoke();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Aazz0200_Player>() != null)
            OnEnter.Invoke();
    }

    //�����浹ü ������ũ��ƮO
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Aazz0200_Player>() != null)
            OnEnter.Invoke();
    }




    public void StageNext()
    {
        if (SceneManager.GetActiveScene().buildIndex != SceneManager.GetAllScenes().Length - 2)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void SaveBefore()// �� �� ����
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
