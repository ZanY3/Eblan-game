using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StintFinalMiniGame : MonoBehaviour
{
    public GameObject GameUi;
    public GameObject MiniGameUi;
    public GameObject CumDrinkBtn;
    public GameObject PressEClue;

    [Header("Cups")]
    public GameObject LeftCup;
    public GameObject RightCup;

    [Header("Audio")]
    public AudioClip DrinkSounds;
    public AudioClip StartTutorVoice;
    public AudioClip CumSelectVoice;
    public AudioClip GoodSelectVoice;

    private FirstPersonLook playerCamera;
    private Pause _pause;
    private bool _inGame = false;
    private bool _usable = false;
    private bool _selected = false;
    private AudioSource _source;

    private void Start()
    {
        playerCamera = FindAnyObjectByType<FirstPersonLook>();
        _pause = FindAnyObjectByType<Pause>();
    }

    private void Update()
    {
        if (_usable && !_inGame && Input.GetKeyDown(KeyCode.E) && !_selected)
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

    public void OpenCups()
    {
        _selected = true;
        RightCup.GetComponent<Animator>().SetTrigger("Open");
        LeftCup.GetComponent<Animator>().SetTrigger("Open");
    }
    public async void CumCupSelected()
    {
        if (!_selected)
        {
            OpenCups();
            _source.PlayOneShot(CumSelectVoice);
            await new WaitForSeconds(8.5f);
            CumDrinkBtn.SetActive(true);
        }
    }
    public void Drink()
    {
        _source.PlayOneShot(DrinkSounds);
        EndGame();
    }
    public async void GoodCupSelected()
    {
        if (!_selected)
        {
            OpenCups();
            _source.PlayOneShot(GoodSelectVoice);
            await new WaitForSeconds(4);
            EndGame();
        }
    }

    public void EndGame()
    {
        _pause.canPause = true;
        GameUi.SetActive(true);
        Cursor.visible = Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        playerCamera.canFollow = true;
        Time.timeScale = 1;
        Destroy(gameObject);
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
