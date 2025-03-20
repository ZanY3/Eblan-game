using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StintFinalMiniGame : MonoBehaviour
{
    public GameObject GameUi;
    public GameObject MiniGameUi;
    public GameObject PressEClue;

    [Header("Cups")]
    public GameObject LeftCup;
    public GameObject RightCup;

    private FirstPersonLook playerCamera;
    private Pause _pause;
    private bool _inGame = false;
    private bool _usable = false;

    private void Start()
    {
        playerCamera = FindAnyObjectByType<FirstPersonLook>();
        _pause = FindAnyObjectByType<Pause>();
    }

    private void Update()
    {
        if (_usable && !_inGame && Input.GetKeyDown(KeyCode.E))
        {
            _pause.canPause = false;
            _inGame = true;
            GameUi.SetActive(false);
            MiniGameUi.SetActive(true);
            Cursor.visible = Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            playerCamera.canFollow = false;
            Time.timeScale = 0;
        }
    }

    public void RCupSelected()
    {
        //cum drink
        //end
    }
    public void LCupSelected()
    {
        //end
    }

    public void EndGame()
    {
        //end
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            _usable = true;
            PressEClue.SetActive(true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _usable = false;
            PressEClue.SetActive(false);
        }
    }

}
