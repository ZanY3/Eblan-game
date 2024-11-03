using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
using JetBrains.Annotations;

public class ScreenLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public bool isHideUi = false;
    public GameObject hideUi;

    public void LoadScene(string sceneName)
    {
        if(isHideUi)
            hideUi.gameObject.SetActive(false);
        LoadSceneAsync(sceneName);
    }

    private async void LoadSceneAsync(string sceneName)
    {
        Time.timeScale = 1f;
        loadingScreen.SetActive(true);
        // �������� ����������� �������� �����
        var operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            // �������� �������� �������� (�� 0 �� 1)
            float progress = Mathf.Clamp01(operation.progress / 0.9f);


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
