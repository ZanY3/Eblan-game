using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ElectroPanel : MonoBehaviour
{
    public GameObject gameUi;
    public GameObject miniGameUi;
    public AudioClip miniGamePassSound;
    public bool stopTime = true;

    private Enemy enemy;
    private bool finished = false;
    private Pause pause;
    private FirstPersonLook fpsLook;
    private FirstPersonMovement movement;
    private bool usable = false;
    private bool opened = false;
    private AudioSource source;
    private float enemySpeed;

    private void Start()
    {
        movement = FindAnyObjectByType<FirstPersonMovement>();
        enemy = FindAnyObjectByType<Enemy>();
        source = GetComponent<AudioSource>();
        fpsLook = FindAnyObjectByType<FirstPersonLook>();
        pause = FindAnyObjectByType<Pause>();
    }

    private void Update()
    {
        if (usable && !opened && Input.GetKeyDown(KeyCode.E) && !finished)
        {
            if(stopTime)
                Time.timeScale = 0f;
            else
            {
                enemy.GetComponent<Enemy>().enabled = false;
            }

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
            enemy.GetComponent<Enemy>().enabled = transform;
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
        enemy.GetComponent<Enemy>().enabled = true;
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
