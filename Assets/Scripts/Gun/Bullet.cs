using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float destroyTime = 5f;
    public AudioClip bulletHitSound;

    private AudioSource bulletSource;

    private void Start()
    {
        bulletSource = GameObject.FindGameObjectWithTag("BulletSource").GetComponent<AudioSource>();
        Destroy(gameObject, destroyTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        bulletSource.PlayOneShot(bulletHitSound);
        Destroy(gameObject);
    }
}
