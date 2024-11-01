using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMenuRotation : MonoBehaviour
{
    public float sensitivity = 0.5f; // Чувствительность поворота
    public float maxRotation = 10f; // Максимальный угол поворота

    private Quaternion startRotation;

    void Start()
    {
        // Сохраняем начальный поворот камеры
        startRotation = transform.rotation;
    }

    void Update()
    {
        // Получаем положение мыши в долях экрана (от -0.5 до 0.5)
        float mouseX = (Input.mousePosition.x / Screen.width - 0.5f) * 2;
        float mouseY = (Input.mousePosition.y / Screen.height - 0.5f) * 2;

        // Рассчитываем углы поворота вокруг осей
        float rotationX = Mathf.Clamp(mouseY * maxRotation * sensitivity, -maxRotation, maxRotation);
        float rotationY = Mathf.Clamp(mouseX * maxRotation * sensitivity, -maxRotation, maxRotation);

        // Устанавливаем поворот камеры
        transform.rotation = startRotation * Quaternion.Euler(rotationX, -rotationY, 0);
    }
}

