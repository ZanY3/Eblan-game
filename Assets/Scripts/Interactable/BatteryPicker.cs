using UnityEngine;

public class BatteryPicker : MonoBehaviour
{
    public float chargeValue;
    private bool usable = false;
    private FlashlightController fLightController;

    private void Start()
    {
        fLightController = FindObjectOfType<FlashlightController>();
    }

    private void Update()
    {
        if (usable && Input.GetKeyDown(KeyCode.E))
        {
            fLightController.ChargeFlight(chargeValue);
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
