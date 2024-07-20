using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMusicController : MonoBehaviour
{
    public AudioClip[] music;

    private AudioSource source;
    private GameObject destroySource;

    private void Start()
    {
        destroySource = GameObject.FindGameObjectWithTag("DestroySource");
        if (destroySource != null && destroySource != gameObject)
        {
            Destroy(destroySource);
        }

        source = GetComponent<AudioSource>();
        int random = Random.Range(0, music.Length);
        source.PlayOneShot(music[random]);
    }

    private void Update()
    {
        if (!source.isPlaying)
        {
            int random = Random.Range(0, music.Length);
            source.PlayOneShot(music[random]);
        }
    }
}
