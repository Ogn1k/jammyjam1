using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void ChangeScene(Object scene)
    {
        if (scene != null)
        {
            SceneManager.LoadScene(scene.name);
        }
        else
        {
            print("no scene");
        }
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
