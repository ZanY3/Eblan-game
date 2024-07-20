using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ElevatorDoor : MonoBehaviour
{
    public TMP_Text cantEnterText;
    public GameObject elevatorBg;
    public AudioClip elevatorSound;
    public AudioClip errorSound;
    public float timeBtwLoad = 2f;

    private AudioSource source;
    private bool usable = false;
    private bool canEnter = false;
    private KeysController keyController;
    private ScreenLoader screenLoader;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        keyController = FindAnyObjectByType<KeysController>();
        screenLoader = FindAnyObjectByType<ScreenLoader>();
    }

    private async void Update()
    {
        if(keyController.count >= 10)
            canEnter = true;

        if(usable && canEnter && Input.GetKeyDown(KeyCode.E))
        {
            StartLoading();
        }
        else if(usable && Input.GetKeyDown(KeyCode.E))
        {
            source.PlayOneShot(errorSound);
            cantEnterText.gameObject.SetActive(true);
            await new WaitForSeconds(1.5f);
            cantEnterText.gameObject.SetActive(false);
        }

    }

    public async void StartLoading()
    {
        source.PlayOneShot(elevatorSound);
        elevatorBg.SetActive(true);
        await new WaitForSeconds(timeBtwLoad);
        screenLoader.LoadScene("Menu");
    }

    private void OnTriggerStay(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
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
