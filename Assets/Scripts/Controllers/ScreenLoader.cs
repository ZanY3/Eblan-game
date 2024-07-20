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
        // �������� ����������� �������� �����
        var operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            // �������� �������� �������� (�� 0 �� 1)
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            if (loadingBar != null)
                loadingBar.value = progress;

            // ���� �������� ���������, ���������� �����
            if (operation.progress >= 0.9f)
            {
                await new WaitForSeconds(1f);
                operation.allowSceneActivation = true;
            }

            await Task.Yield(); // ��������� ������������ ������ ���������� ���������� �� ��������� �����
        }
    }
}
