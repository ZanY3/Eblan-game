using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public GameObject fLight;
    public AudioClip fOnSound;
    public float charge;
    public TMP_Text chargeText;

    [Header("Battery")]
    public AudioClip batteryPickSound;

    private float startCharge;
    private bool canOn = true;
    private bool isOn = false;
    private GameObject battery;
    private AudioSource source;

    private void Start()
    {
        startCharge = charge;
        source = GetComponent<AudioSource>();
    }

    private async void Update()
    {
        if (charge <= 0)
        {
            isOn = false;
            fLight.SetActive(isOn);
            source.PlayOneShot(fOnSound);
            await new WaitForSeconds(0.3f);
            source.enabled = false;
        }
        if (isOn)
        {
            charge -= Time.deltaTime * 1.45f;
            if (charge < 0)
                charge = 0;
        }
        if (canOn && charge != 0 && Input.GetKeyDown(KeyCode.F))
        {
            source.enabled = true;
            isOn = !isOn;
            fLight.SetActive(isOn);
            source.PlayOneShot(fOnSound);
        }
        chargeText.text = (int)charge + "%";
    }

    public void ChargeFlight(float value)
    {
        if (battery != null)
        {
            charge += value;
            if (charge > startCharge)
                charge = startCharge;

            float randPitch = Random.Range(0.7f, 1.0f);
            source.pitch = randPitch;
            source.PlayOneShot(batteryPickSound);

            Destroy(battery);
            battery = null;
            canOn = true;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Battery"))
            battery = collision.gameObject;
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Battery"))
            battery = null;
    }
}
