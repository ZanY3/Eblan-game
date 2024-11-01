using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraIntroMove : MonoBehaviour
{
    public float moveDuration = 1.0f; // Время на анимацию опускания камеры
    public float moveDistance = -5.0f; // Расстояние опускания камеры

    private Vector3 startPosition; // Начальное положение камеры
    private Vector3 targetPosition; // Конечное положение камеры
    private float elapsedTime = 0.0f; // Таймер для анимации
    private bool isMoving = true; // Флаг, чтобы остановить движение после анимации

    void Start()
    {
        // Задаем начальное и конечное положение камеры
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(0, moveDistance, 0);
    }

    void Update()
    {
        if (isMoving)
        {
            // Плавно опускаем камеру
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;

            // Если время анимации прошло, останавливаем движение камеры
            if (elapsedTime >= moveDuration)
            {
                transform.position = targetPosition; // Устанавливаем точную позицию
                isMoving = false; // Останавливаем анимацию
            }
        }
    }
}
