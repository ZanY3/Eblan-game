using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroPanel : MonoBehaviour
{
    public GameObject gameUi;
    public GameObject miniGameUi;

    private Pause pause;
    private FirstPersonLook fpsLook;
    private FirstPersonMovement movement;
    private bool usable = false;
    private bool opened = false;

    private void Start()
    {
        fpsLook = FindAnyObjectByType<FirstPersonLook>();
        pause = FindAnyObjectByType<Pause>();
    }

    private void Update()
    {
        if (usable && !opened && Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 0f;
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

        } 
            
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
