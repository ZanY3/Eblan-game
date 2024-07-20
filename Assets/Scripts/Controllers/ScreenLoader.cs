using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;

public class ScreenLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;

    public void LoadScene(string sceneName)
    {
        LoadSceneAsync(sceneName);
    }

    private async void LoadSceneAsync(string sceneName)
    {
        loadingScreen.SetActive(true);
        // Начинаем асинхронную загрузку сцены
        var operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            // Получаем прогресс загрузки (от 0 до 1)
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            if (loadingBar != null)
                loadingBar.value = progress;

            // Если загрузка завершена, активируем сцену
            if (operation.progress >= 0.9f)
            {
                await new WaitForSeconds(1f);
                operation.allowSceneActivation = true;
            }

            await Task.Yield(); // Позволяет асинхронному методу продолжить выполнение на следующем кадре
        }
    }
}
