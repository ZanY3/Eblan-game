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
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(20f);
        }
        bulletSource.PlayOneShot(bulletHitSound);
        Destroy(gameObject);
    }
}
