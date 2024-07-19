using UnityEngine;

public class BatteryPicker : MonoBehaviour
{
    public float chargeValue;

    private bool picked = false;
    private bool usable = false;
    private FlashlightController fLigthController;

    private void Start()
    {
        fLigthController = FindObjectOfType<FlashlightController>();
    }

    private async void Update()
    {
        if (usable && !picked && Input.GetKeyDown(KeyCode.E))
        {
            picked = true;
            fLigthController.ChargeFlight(chargeValue);
            await new WaitForSeconds(0.15f);
            picked = false;
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
