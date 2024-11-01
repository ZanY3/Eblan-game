using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMenuMovement : MonoBehaviour
{
    public float sensitivity = 0.05f; // Чувствительность движения
    public float maxOffset = 2.0f; // Максимальное отклонение камеры по каждой оси

    private Vector3 startPos;

    void Start()
    {
        // Сохраняем начальную позицию камеры
        startPos = transform.position;
    }

    void Update()
    {
        // Получаем положение мыши в долях экрана (от 0 до 1)
        float mouseX = Input.mousePosition.x / Screen.width - 0.5f;
        float mouseY = Input.mousePosition.y / Screen.height - 0.5f;

        // Вычисляем новое положение камеры, добавляя смещение
        float offsetX = mouseX * sensitivity * maxOffset;
        float offsetY = mouseY * sensitivity * maxOffset;

        transform.position = startPos + new Vector3(offsetX, offsetY, 0);
    }
}

