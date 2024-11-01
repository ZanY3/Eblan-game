using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform cameraTransform; // Ссылка на камеру
    public Vector3 offset = new Vector3(0.2f, -0.2f, 0.5f); // Смещение предмета относительно камеры
    public float followSpeed = 5.0f; // Скорость, с которой предмет следует за камерой
    public float rotationSpeed = 5.0f; // Скорость, с которой предмет поворачивается за камерой

    void Update()
    {
        // Расчет целевой позиции предмета с учётом смещения относительно камеры
        Vector3 targetPosition = cameraTransform.position + cameraTransform.TransformDirection(offset);
        
        // Плавное смещение предмета к целевой позиции
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Плавный поворот предмета, чтобы он смотрел в том же направлении, что и камера
        Quaternion targetRotation = cameraTransform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
