using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screamer : MonoBehaviour
{
    public GameObject screamerImage;
    public float screamerTime;

    private bool used = false;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private async void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player") && !used)
        {
            used = true;
            source.Play();
            screamerImage.SetActive(true);
            await new WaitForSeconds(screamerTime);
            source.Stop();
            screamerImage.SetActive(false);
        }
    }
}
