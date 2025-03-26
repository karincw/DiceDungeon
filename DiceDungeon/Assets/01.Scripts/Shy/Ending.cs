using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public GameObject end;

    private void Start()
    {
        end.gameObject.SetActive(false);
    }

    public void EndOpen()
    {
        end.gameObject.SetActive(true);
    }

    public void End()
    {
        SceneManager.LoadScene(0);
    }
}
