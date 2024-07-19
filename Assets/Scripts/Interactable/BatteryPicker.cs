using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BatteryPicker : MonoBehaviour
{
    public float chargeValue;

    private bool usable = false;
    private FlashlightController fLigthController;

    private void Start()
    {
        fLigthController = FindAnyObjectByType<FlashlightController>();
    }

    private void Update()
    {
        if(usable && Input.GetKeyDown(KeyCode.E))
        {
            fLigthController.ChargeFlight(chargeValue);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Battery"))
        {
            usable = true;
        }

    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Battery"))
        {
            usable = false;
        }
    }
}
