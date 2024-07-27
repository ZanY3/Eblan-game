using UnityEngine;
using UnityEngine.SceneManagement;

public class EndController : MonoBehaviour
{
    public int enemyesLeft = 2;
    public string loadScene = "EndTrailer";

    private async void Update()
    {
        if(enemyesLeft <= 0)
        {
            await new WaitForSeconds(3f);
            SceneManager.LoadScene(loadScene);
        }
    }
}
