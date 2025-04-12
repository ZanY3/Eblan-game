using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhysicalBtn : MonoBehaviour
{
    public MzlfLvlController LvlController;
    public AudioClip BtnSound;
    public TMP_Text ClueTxt;
    public bool _isCanPress = false;

    private bool _usable = false;
    private AudioSource _source;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_usable && Input.GetKeyDown(KeyCode.E) && _isCanPress)
        {
            _source.PlayOneShot(BtnSound);
            LvlController.StartTheGame();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && _isCanPress)
        {
            ClueTxt.gameObject.SetActive(true);
            _usable = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ClueTxt.gameObject.SetActive(false);
            _usable = false;
        }
    }
}
