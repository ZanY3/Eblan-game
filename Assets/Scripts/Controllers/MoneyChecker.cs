using UnityEngine;

public class MoneyChecker : MonoBehaviour
{
    public GameObject keysObj;
    public AudioClip sucessSound;
    public GameObject sucessParticles;
    public Transform particlesPosition;

    private bool taked = false;
    private AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider collision)
    {
        if(collision.gameObject.CompareTag("Money") && !taked)
        {
            Destroy(collision.gameObject);
            Instantiate(sucessParticles.gameObject, particlesPosition.position, Quaternion.identity);
            taked = true;
            source.PlayOneShot(sucessSound);
            keysObj.SetActive(true);
        }
    }
}
