using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroPanel : MonoBehaviour
{
    public GameObject gameUi;
    public GameObject miniGameUi;
    public AudioClip miniGamePassSound;

    private bool finished = false;
    private Pause pause;
    private FirstPersonLook fpsLook;
    private FirstPersonMovement movement;
    private bool usable = false;
    private bool opened = false;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        fpsLook = FindAnyObjectByType<FirstPersonLook>();
        pause = FindAnyObjectByType<Pause>();
    }

    private void Update()
    {
        if (usable && !opened && Input.GetKeyDown(KeyCode.E) && !finished)
        {
            Time.timeScale = 0f;
            Cursor.visible = Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            miniGameUi.SetActive(true);
            gameUi.SetActive(false);
            pause.canPause = false;
            opened = true;
            fpsLook.enabled = false;
            movement.enabled = false;

        }
        else if (opened && Input.GetKeyDown(KeyCode.Backspace))
        {
            Time.timeScale = 1f;
            miniGameUi.SetActive(false);
            gameUi.SetActive(true);
            pause.canPause = true;
            opened = false;
            fpsLook.enabled = true;
            movement.enabled = true;
            Cursor.visible = Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        } 
            
    }
    public void EndMiniGame()
    {
        Time.timeScale = 1f;
        finished = true;
        source.PlayOneShot(miniGamePassSound);
        miniGameUi.SetActive(false);
        gameUi.SetActive(true);
        pause.canPause = true;
        opened = false;
        fpsLook.enabled = true;
        movement.enabled = true;
        Cursor.visible = Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            usable = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            usable = false;
        }
    }
}
