using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 1.0f; // Скорость вращения
    public Vector3 rotationAngle = new Vector3(0, 90, 0); // Угол поворота

    [Header("Sound Settings")]
    public AudioClip rotationSound; // Звук при вращении
    public bool playSoundOnEachRotation = true; // Флаг для звука

    private bool isRotating = false; // Флаг вращения
    private Quaternion startRotation; // Начальное положение
    private Quaternion targetRotation; // Целевое вращение
    private bool isRotated = false; // Текущее состояние вращения
    private AudioSource audioSource; // Компонент AudioSource

    // Статическая переменная для отслеживания активного объекта вращения
    private static RotateObject activeRotatingObject;

    void Start()
    {
        // Сохраняем начальное вращение объекта
        startRotation = transform.rotation;

        // Добавляем AudioSource, если он не добавлен заранее
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = rotationSound;
    }

    void Update()
    {
        if (isRotating)
        {
            // Плавно поворачиваем объект к целевому вращению
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Если объект достиг целевого вращения, останавливаем вращение
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                isRotating = false;
            }
        }
    }

    // Метод для запуска вращения
    public void StartRotation()
    {
        if (!isRotating)
        {
            // Если уже есть активный объект, вернуть его в исходное положение
            if (activeRotatingObject != null && activeRotatingObject != this)
            {
                activeRotatingObject.ResetRotation();
            }

            // Устанавливаем текущий объект как активный для вращения
            activeRotatingObject = this;

            // Устанавливаем целевое вращение в зависимости от текущего состояния
            targetRotation = isRotated ? startRotation : startRotation * Quaternion.Euler(rotationAngle);

            // Воспроизводим звук, если это включено
            if (playSoundOnEachRotation && rotationSound != null)
            {
                audioSource.Play();
            }

            // Обновляем состояние вращения и запускаем его
            isRotated = !isRotated;
            isRotating = true;
        }
    }

    // Метод для возврата объекта в начальное положение
    public void ResetRotation()
    {
        // Устанавливаем целевое вращение на начальное положение
        targetRotation = startRotation;
        isRotated = false;
        isRotating = true;

        // Очищаем активный вращающийся объект
        if (activeRotatingObject == this)
        {
            activeRotatingObject = null;
        }
    }
}
