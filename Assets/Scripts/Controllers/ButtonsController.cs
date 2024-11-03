using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsController : MonoBehaviour
{
    public Camera mainCamera; // Ссылка на камеру
    public float cameraMoveDuration = 1.0f; // Время на анимацию опускания камеры
    public float cameraMoveDistance = -5.0f; // Расстояние опускания камеры

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1.0f;
        StartCoroutine(MoveCameraAndLoadScene(sceneName));
    }

    public void LoadLink(string link)
    {
        Application.OpenURL(link);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private System.Collections.IEnumerator MoveCameraAndLoadScene(string sceneName)
    {
        Time.timeScale = 1.0f;
        Vector3 startPosition = mainCamera.transform.position;
        Vector3 targetPosition = startPosition + new Vector3(0, cameraMoveDistance, 0);

        float elapsedTime = 0;

        // Плавно опускаем камеру
        while (elapsedTime < cameraMoveDuration)
        {
            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / cameraMoveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = targetPosition;

        // Загружаем сцену после завершения анимации
        SceneManager.LoadScene(sceneName);
    }
}
