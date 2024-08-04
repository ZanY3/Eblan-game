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
            // �������� ������� ���������� ��������
            List<int> inactiveKeys = new List<int>();
            for (int i = 0; i < objects.Length; i++)
            {
                if (!isObjOn[i])
                {
                    inactiveKeys.Add(i);
                }
            }

            // ���������, ���� �� ��������� ���������� �������
            if (inactiveKeys.Count > 0)
            {
                // �������� ��������� ���������� ����
                int randomIndex = Random.Range(0, inactiveKeys.Count);
                int randomObj = inactiveKeys[randomIndex];

                objects[randomObj].SetActive(true);
                isObjOn[randomObj] = true;
                objToSpawnLeft--;
            }
        }
    }
}
