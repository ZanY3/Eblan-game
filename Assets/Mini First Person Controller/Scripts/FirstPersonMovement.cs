using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    [Header("Stamina")]
    public float maxStamina = 100f;
    public float staminaDepletionRate = 10f;
    public float staminaRegenRate = 5f;
    public RectTransform staminaBar;

    private float startStaminaBarSize;
    private float currentStamina;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        currentStamina = maxStamina;
        startStaminaBarSize = staminaBar.sizeDelta.x;
    }

    void FixedUpdate()
    {
        staminaBar.sizeDelta = new Vector2(startStaminaBarSize * (currentStamina / maxStamina), staminaBar.sizeDelta.y);
        if (canRun && Input.GetKey(runningKey) && currentStamina > 0)
        {
            IsRunning = true;
            currentStamina -= staminaDepletionRate * Time.fixedDeltaTime;
            currentStamina = Mathf.Max(currentStamina, 0);
        }
        else
        {
            IsRunning = false;
            currentStamina += staminaRegenRate * Time.fixedDeltaTime;
            currentStamina = Mathf.Min(currentStamina, maxStamina);
        }
        float targetMovingSpeed = IsRunning ? runSpeed : speed;

        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }
}