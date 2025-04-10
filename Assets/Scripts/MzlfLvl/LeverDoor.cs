using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverDoor : MonoBehaviour
{
    public int LeversToOnLeft;
    public AudioClip OpenSound;

    private AudioSource _source;
    private Animator _anim;
    private bool _used = false;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(LeversToOnLeft <= 0 && !_used)
        {
            _source.PlayOneShot(OpenSound);
            _anim.SetTrigger("Open");
            _used = true;
        }
    }
}
