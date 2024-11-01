using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject gameUi;
    public GameObject pauseUi;
    public GameObject settingsUi;

    private FirstPersonLook playerCamera;
    private bool paused = false;

    private void Start()
    {
        playerCamera = FindAnyObjectByType<FirstPersonLook>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            Cursor.visible = Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            playerCamera.canFollow = false;
            paused = true;
            gameUi.SetActive(false);
            pauseUi.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public void UnPause()
    {
        Cursor.visible = Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerCamera.canFollow = true;
        Time.timeScale = 1f;
        paused = false;
        gameUi.SetActive(true);
        pauseUi.SetActive(false);
    }   
    public void OpenSettings()
    {
        pauseUi.SetActive(false);
        settingsUi.SetActive(true);
    }    
    public void CloseSettings()
    {
        pauseUi.SetActive(true);
        settingsUi.SetActive(false);
    }
}
