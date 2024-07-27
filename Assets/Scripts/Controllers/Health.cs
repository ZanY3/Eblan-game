using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public AudioSource dieSource;
    public AudioClip dieSound;
    public AudioClip damageSound;
    public GameObject dieParticles;
    public GameObject damageParticles;

    public GameObject finishPortal;

    public void TakeDamage(float damage)
    {
        Instantiate(damageParticles, transform.position, Quaternion.identity);
        dieSource.PlayOneShot(damageSound);
        health -= damage;
        if(health <= 0)
        {
            float randPitch = Random.Range(0.5f, 1);
            dieSource.pitch = randPitch;
            dieSource.PlayOneShot(dieSound);
            Instantiate(dieParticles, transform.position, Quaternion.identity);
            finishPortal.SetActive(true);
            Destroy(gameObject);
        }
    }
}
