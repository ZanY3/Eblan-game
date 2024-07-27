using UnityEngine;

public class Door : MonoBehaviour
{
    public AudioClip doorOpenSound;

    private bool usable = false;
    private bool canOpen = true;

    private AudioSource source;
    private Animator animator;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (usable && Input.GetKeyUp(KeyCode.E) && canOpen)
        {
            animator.SetTrigger("Open");
            float randPitch = Random.Range(0.7f, 1.0f);
            source.pitch = randPitch;
            source.PlayOneShot(doorOpenSound);
            canOpen = false;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            usable = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            usable = false;
        }
    }
}
