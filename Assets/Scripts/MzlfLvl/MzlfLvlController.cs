using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
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
    public PhysicalBtn PhysBtn;
    public string SceneToLoad;

    [Header("Timer")]
    public float GameTime;
    public TMP_Text TimerTxt;

    private bool _canSkipIntro = true;
    private bool _isStarted = false;
    private bool _playedEndSound = false;
    private FirstPersonLook _playerCamera;
    private Pause _pause;
    private FirstPersonMovement _playerMovement;
    private AudioSource _source;

    private async void Start()
    {
        Obstacles.SetActive(false);
        _source = GetComponent<AudioSource>();

        _playerCamera = FindAnyObjectByType<FirstPersonLook>();
        _pause = FindAnyObjectByType<Pause>();
        _playerMovement = FindAnyObjectByType<FirstPersonMovement>();
        TimerTxt.gameObject.SetActive(false);

        _source.clip = StartSound;
        _source.Play();
        await new WaitForSeconds(13);
        PhysBtn._isCanPress = true;
        _canSkipIntro = false;

    }

    private void Update()
    {
        if(_isStarted)
        {
            int minutes; int seconds;
            minutes = (int)GameTime / 60;
            seconds = (int)GameTime - minutes * 60;

            TimerTxt.text = $"{minutes} мин {seconds} сек";

            if(GameTime <= 0 && !_playedEndSound)
            {
                BoxDestroy();
                _playedEndSound = true;
            }
            else
            {
                GameTime -= Time.deltaTime;
            }
        }

        if(!_isStarted && Input.GetKeyDown(KeyCode.P))
        {
            SkipIntro();
        }
    }

    public void SkipIntro()
    {
        _source.Stop();
        PhysBtn._isCanPress = true;
    }

    public void StartTheGame() //на кнопку открывается
    {
        _isStarted = true;
        Obstacles.SetActive(true);
        _source.PlayOneShot(GarageOpenSound);
        StartWallAnim.SetTrigger("Open");
        _source.clip = BgMusic;
        _source.Play();
        TimerTxt.gameObject.SetActive(true);
    }

    public void BoxDestroy()
    {
        Destroy(Box);
        _source.PlayOneShot(BoxDestroySound);
        Die();
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
    public async void EndGame()
    {
        _source.Stop();
        _source.PlayOneShot(EndSound);
        await new WaitForSeconds(1);
        _source.PlayOneShot(EndVoiceSound);
        await new WaitForSeconds(1.5f);
        GameUi.SetActive(false);
        ScreenLoader.LoadScene(SceneToLoad);

    }


}
