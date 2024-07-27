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
    private EndController endController;

    private void Start()
    {
        endController = FindAnyObjectByType<EndController>();
    }

    public void TakeDamage(float damage)
    {
        float randPitch = Random.Range(0.8f, 1);
        dieSource.pitch = randPitch;
        Instantiate(damageParticles, transform.position, Quaternion.identity);
        dieSource.PlayOneShot(damageSound);
        health -= damage;
        if(health <= 0)
        {
            endController.enemyesLeft--;
            dieSource.PlayOneShot(dieSound);
            Instantiate(dieParticles, transform.position, Quaternion.identity);
            finishPortal.SetActive(true);
            Destroy(gameObject);
        }
    }
}
