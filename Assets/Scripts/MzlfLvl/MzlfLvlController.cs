using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class MzlfLvlController : MonoBehaviour
{
    [Header("Objects")]
    public GameObject GameUi;
    public GameObject LoseUi;
    public GameObject Box;

    [Header("Sounds")]
    public AudioClip BoxDestroySound;
    public AudioClip DefeatSound;

    private FirstPersonLook _playerCamera;
    private Pause _pause;
    private FirstPersonMovement _playerMovement;
    private AudioSource _source;

    private void Start()
    {
        _playerCamera = FindAnyObjectByType<FirstPersonLook>();
        _pause = FindAnyObjectByType<Pause>();
        _playerMovement = FindAnyObjectByType<FirstPersonMovement>();

        _source = GetComponent<AudioSource>();
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
    public void BoxDestroy()
    {
        Destroy(Box);
        _source.PlayOneShot(BoxDestroySound);
        //effects;
        Die();
    }

}
