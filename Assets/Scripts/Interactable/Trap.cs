using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float catchCd;
    public TMP_Text catchCdText;
    public AudioClip catchSound;
    public Rigidbody playerRb;

    private bool usedSound = false;
    private bool trapped = false;
    private bool used = false;
    private AudioSource source;

    private void Start()
    {
        catchCdText.text = null;
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(trapped)
        {
            catchCdText.text = ((int)catchCd).ToString();
            catchCd -= Time.deltaTime;
        }    
    }

    private void OnTriggerStay(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Catched();
        }
    }

    public async void Catched()
    {
        if(!usedSound)
            source.PlayOneShot(catchSound);
        trapped = true;
        used = true;
        playerRb.isKinematic = true;
        usedSound = true;
        await new WaitForSeconds(catchCd);
        Dismiss();
    }

    public void Dismiss()
    {
        trapped = false;
        playerRb.isKinematic = false;
        catchCdText.text = null;
        Destroy(gameObject);

    }
}
