using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public GameObject fLight;
    public AudioClip fOnSound;
    public AudioSource source;

    private bool isOn = false;
    private bool isTaked = true;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(isTaked && Input.GetKeyDown(KeyCode.F))
        {
            isOn = !isOn;
            fLight.SetActive(isOn);
            source.PlayOneShot(fOnSound);
        }
    }
}
