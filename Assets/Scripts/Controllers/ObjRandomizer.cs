using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRandomizer : MonoBehaviour
{
    public GameObject[] objects;
    public int objToSpawnLeft = 10;

    private bool[] isObjOn;
    
    private void Start()
    {
        if (isObjOn == null || isObjOn.Length != objects.Length)
        {
            isObjOn = new bool[objects.Length];
        }
    }

    private void Update()
    {
        if (objToSpawnLeft > 0)
        {
            // Собираем индексы неактивных обьектов
            List<int> inactiveKeys = new List<int>();
            for (int i = 0; i < objects.Length; i++)
            {
                if (!isObjOn[i])
                {
                    inactiveKeys.Add(i);
                }
            }

            // Проверяем, есть ли доступные неактивные обьекты
            if (inactiveKeys.Count > 0)
            {
                // Выбираем случайный неактивный ключ
                int randomIndex = Random.Range(0, inactiveKeys.Count);
                int randomObj = inactiveKeys[randomIndex];

                objects[randomObj].SetActive(true);
                isObjOn[randomObj] = true;
                objToSpawnLeft--;
            }
        }
    }
}
