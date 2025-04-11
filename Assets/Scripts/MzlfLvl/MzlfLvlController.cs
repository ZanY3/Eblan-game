using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MzlfLvlController : MonoBehaviour
{
    [Header("Objects")]
    public GameObject GameUi;
    public GameObject LoseUi;
    public GameObject Box;
    public GameObject Obstacles;
    public Animator StartWallAnim;

    [Header("Sounds")]
    public AudioClip BoxDestroySound;
    public AudioClip DefeatSound;
    public AudioClip GarageOpenSound;
    public AudioClip BgMusic;
    public AudioClip StartSound;
    public AudioClip EndVoiceSound;
    public AudioClip EndSound;

    [Header("Other")]
    public ScreenLoader ScreenLoader;
    public string SceneToLoad;

    [Header("Timer")]
    public float GameTime;
    public TMP_Text TimerTxt;


    private bool _isStarted = false;
    private FirstPersonLook _playerCamera;
    private Pause _pause;
    private FirstPersonMovement _playerMovement;
    private AudioSource _source;

    private void Start()
    {
        Obstacles.SetActive(false);
        _source = GetComponent<AudioSource>();
        _source.PlayOneShot(StartSound);

        _playerCamera = FindAnyObjectByType<FirstPersonLook>();
        _pause = FindAnyObjectByType<Pause>();
        _playerMovement = FindAnyObjectByType<FirstPersonMovement>();

    }

    private void Update()
    {
        if(_isStarted)
        {
            int minutes; int seconds;
            minutes = (int)GameTime / 60;
            seconds = (int)GameTime - minutes * 60;

            TimerTxt.text = $"{minutes} мин {seconds} сек";

            if(GameTime <= 0)
            {
                BoxDestroy();
            }
            else
            {
                GameTime -= Time.deltaTime;
            }
        }
    }

    public async void Die()
    {
        _source.PlayOneShot(DefeatSound);
        await new WaitForSeconds(1);
        _playerMovement.enabled = false;
        _pause.canPause = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _playerCamera.canFollow = false;
        GameUi.SetActive(false);
        LoseUi.SetActive(true);


    }
    public void StartTheGame() //на кнопку открывается
    {
        _isStarted = true;
        Obstacles.SetActive(true);
        _source.PlayOneShot(GarageOpenSound);
        StartWallAnim.SetTrigger("Open");
        _source.clip = BgMusic;
        _source.Play();
    }

    public void BoxDestroy()
    {
        Destroy(Box);
        _source.PlayOneShot(BoxDestroySound);
        Die();
    }
    public async void EndGame()
    {
        _source.Stop();
        _source.PlayOneShot(EndSound);
        await new WaitForSeconds(1);
        _source.PlayOneShot(EndVoiceSound);
        await new WaitForSeconds(1.5f);
        ScreenLoader.LoadScene(SceneToLoad);

    }

}
