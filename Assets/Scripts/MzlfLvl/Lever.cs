using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public AudioClip OnSound;
    public GameObject ClueTxt;
    public LeverDoor LeverDoor;

    private bool _isActive = true;
    private AudioSource _source;
    private Animator _anim;
    private bool _usable = false;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(_usable && Input.GetKeyDown(KeyCode.E) && _isActive)
        {
            _anim.SetTrigger("On");
            _source.PlayOneShot(OnSound);
            LeverDoor.LeversToOnLeft--;
            ClueTxt.SetActive(false);
            _isActive = false;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && _isActive)
        {
            ClueTxt.SetActive(true);
            _usable = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ClueTxt.SetActive(false);
            _usable = false;
        }
    }
}
