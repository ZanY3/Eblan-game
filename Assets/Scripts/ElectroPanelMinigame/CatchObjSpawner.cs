using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchObjSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject objPrefab;

    public float spawnCd;

    private float startSpawnCd;

    private void Start()
    {
        startSpawnCd = spawnCd;
    }
    private void Update()
    {
        if(spawnCd <= 0)
        {
            int rand = Random.Range(0, spawnPoints.Length);
            GameObject obj = Instantiate(objPrefab, spawnPoints[rand].position, Quaternion.identity);
            obj.transform.SetParent(transform, true);
            obj.transform.localScale = Vector3.one;
            spawnCd = startSpawnCd;

        }
        spawnCd -= Time.deltaTime;
    }
}
