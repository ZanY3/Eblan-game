using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MzlfLvlController : MonoBehaviour
{
    public GameObject Box;
    public AudioClip BoxDestroySound;
    public AudioClip DefeatSound;

    private AudioSource _source;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    public void Die()
    {
        _source.PlayOneShot(DefeatSound);

    }
    public void BoxDestroy()
    {
        Destroy(Box);
        _source.PlayOneShot(BoxDestroySound);
        //effects;
        Die();
    }

}
