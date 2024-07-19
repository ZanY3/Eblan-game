using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public GameObject fLight;
    public AudioClip fOnSound;
    public AudioSource source;
    public float charge;
    public TMP_Text chargeText;

    [Header("Battery")]
    public AudioClip batteryPickSound;

    private float startCharge;
    private bool canOn = true;
    private bool isOn = false;
    private GameObject battery;

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
        }
        if (isOn)
        {
            charge -= Time.deltaTime * 3;
            if (charge < 0)
                charge = 0;
        }
        if (canOn && charge != 0 && Input.GetKeyDown(KeyCode.F))
        {
            isOn = !isOn;
            fLight.SetActive(isOn);
            source.PlayOneShot(fOnSound);
        }
        chargeText.text = (int)charge + "%";
        await new WaitForSeconds(2f);
        battery = null;
    }

    public void ChargeFlight(float value)
    {
        float maxValueToPick = Mathf.Max(startCharge - value, 0);
        if (charge <= maxValueToPick && battery != null)
        {
            float randPitch = Random.Range(0.7f, 1.0f);
            source.pitch = randPitch;
            source.PlayOneShot(batteryPickSound);
            Destroy(battery.gameObject);
            battery = null;
            charge += value;
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
