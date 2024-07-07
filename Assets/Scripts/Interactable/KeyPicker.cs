using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyPicker : MonoBehaviour
{
    public AudioClip keyPickSound;

    private GameObject key;
    private bool usable = false;
    private AudioSource source;
    private KeysController keysController;

    private void Start()
    {
        keysController = FindAnyObjectByType<KeysController>();
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(usable && Input.GetKeyUp(KeyCode.E))
        {
            float randPitch = Random.Range(0.7f, 1.0f);
            source.pitch = randPitch;
            source.PlayOneShot(keyPickSound);
            keysController.TakeKey();
            Destroy(key.gameObject);
            usable = false;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            key = collision.gameObject;
            usable = true;
        }

    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            usable = false;
            key = null;
        }
    }
}
