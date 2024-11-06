using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TvController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    private bool isPlaying = false;
    private bool usable = false;

    private void Update()
    {
        if(usable && Input.GetKeyUp(KeyCode.E))
        {
            isPlaying = !isPlaying;
        }
        if(isPlaying)
            videoPlayer.Play();

        else
            videoPlayer.Pause();
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            usable = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            usable = false;
        }
    }
}
