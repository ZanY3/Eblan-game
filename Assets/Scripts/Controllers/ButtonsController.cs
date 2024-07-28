using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsController : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneName);
    }
    public void LoadLink(string link)
    {
        Application.OpenURL(link);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
