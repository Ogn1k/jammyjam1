using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void ChangeScene(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void DisableObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void EnableObject(GameObject obj)
    {
        obj.SetActive(true);
    }
}
