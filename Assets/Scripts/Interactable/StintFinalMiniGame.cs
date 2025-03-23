using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StintFinalMiniGame : MonoBehaviour
{
    [Header("Ui")]
    public GameObject GameUi;
    public GameObject MiniGameUi;
    public GameObject CumDrinkBtn;
    public GameObject PressEClue;

    [Header("Objects to off")]
    public GameObject Player;
    public Enemy Enemy;

    [Header("Cups")]
    public GameObject LeftCup;
    public GameObject RightCup;

    [Header("Audio")]
    public AudioClip DrinkSounds;
    public AudioClip StartTutorVoice;
    public AudioClip CumSelectVoice;
    public AudioClip GoodSelectVoice;

    private FirstPersonLook _playerCamera;
    private Pause _pause;
    private bool _inGame = false;
    private bool _usable = false;
    private bool _selected = false;
    private bool _canSelect = false;
    private AudioSource _source;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
        _playerCamera = FindAnyObjectByType<FirstPersonLook>();
        _pause = FindAnyObjectByType<Pause>();
    }

    private async void Update()
    {
        if (_usable && !_inGame && Input.GetKeyDown(KeyCode.E) && !_selected)
        {
            Enemy.enabled = false;
            Player.GetComponent<FirstPersonMovement>().enabled = false;
            Player.GetComponent<Crouch>().enabled = false;
            Player.GetComponent<FlashlightController>().enabled = false;
            _pause.canPause = false;
            _inGame = true;
            GameUi.SetActive(false);
            MiniGameUi.SetActive(true);
            Cursor.visible = Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            _playerCamera.canFollow = false;
            _source.PlayOneShot(StartTutorVoice);
            await new WaitForSeconds(12f);
            _canSelect = true;
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
        if (!_selected && _canSelect)
        {
            OpenCups();
            _source.PlayOneShot(CumSelectVoice);
            await new WaitForSeconds(8.5f);
            CumDrinkBtn.SetActive(true);
        }
    }
    public async void Drink()
    {
        _source.PlayOneShot(DrinkSounds);
        await new WaitForSeconds(1.4f);
        EndGame();
    }
    public async void GoodCupSelected()
    {
        if (!_selected && _canSelect)
        {
            OpenCups();
            _source.PlayOneShot(GoodSelectVoice);
            await new WaitForSeconds(4);
            EndGame();
        }
    }

    public void EndGame()
    {
        Player.GetComponent<FirstPersonMovement>().enabled = transform;
        Player.GetComponent<Crouch>().enabled = true;
        Player.GetComponent<FlashlightController>().enabled = true;
        PressEClue.SetActive(false);
        _pause.canPause = true;
        GameUi.SetActive(true);
        MiniGameUi.SetActive(false);
        Cursor.visible = Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        _playerCamera.canFollow = true;
        Enemy.enabled = true;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _usable = true;
            PressEClue.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _usable = false;
            PressEClue.SetActive(false);
        }
    }

}
