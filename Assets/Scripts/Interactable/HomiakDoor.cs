using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HomiakDoor : MonoBehaviour
{
    public GameObject gameUi;
    public GameObject homiakUi;
    public TMP_Text scoreText;

    [Header("Sounds")]
    public AudioClip clickSound;
    public AudioClip finishSound;

    [Header("Door sounds")]
    public Animator doorAnimator;
    public AudioSource doorSource;
    public AudioClip doorOpenClip;

    private FirstPersonLook playerCamera;
    private int score;
    private AudioSource source;
    private bool usable = false;
    private bool opened = false;

    private void Start()
    {
        playerCamera = FindAnyObjectByType<FirstPersonLook>();
        scoreText.text = "0";
        source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(usable && !opened && Input.GetKeyDown(KeyCode.E))
        {
            source.Play();
            gameUi.gameObject.SetActive(false);
            homiakUi.gameObject.SetActive(true);
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = CursorLockMode.None;
            playerCamera.canFollow = false;
            Time.timeScale = 0;
        }
    }

    public void Clicked()
    {
        source.PlayOneShot(clickSound);
        score++;
        scoreText.text = score.ToString();
        if(score >= 30)
        {
            Time.timeScale = 1;
            playerCamera.canFollow = true;
            source.PlayOneShot(finishSound);
            opened = true;
            doorAnimator.SetTrigger("Open");
            doorSource.PlayOneShot(doorOpenClip);
            gameUi.gameObject.SetActive(true);
            homiakUi.gameObject.SetActive(false);
            source.Stop();
             
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            usable = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            usable = false;
        }
    }
}
