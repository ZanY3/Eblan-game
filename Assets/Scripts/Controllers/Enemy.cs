using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float viewDistance = 10f;
    public float wanderDistance = 5f;
    public float speed = 3.5f;
    public Transform target;
    public AudioClip[] searchSounds;
    public AudioClip[] findSounds;
    public float soundInterval = 5f; // »нтервал между воспроизведением звуков

    [Header("Reached Player")]
    public GameObject gameUi;
    public GameObject panelUi;
    public string restartScene = "Level 1";
    public AudioSource reachedSource;
    public AudioClip jumpScareSound;
    public float timeBfrRestart = 1f;

    private FirstPersonLook playerCamera;
    private FirstPersonMovement playerMovement;
    private AudioSource source;
    private Animator animator;
    private Rigidbody rb;
    private NavMeshAgent agent;
    private bool seePlayer = false;
    private Vector3 lastPosition;
    private float stuckTime = 0;
    private string currentState = "";
    private float lastSoundTime = 0f; // ¬рем€ последнего воспроизведени€ звука

    void Start()
    {
        playerMovement = FindAnyObjectByType<FirstPersonMovement>();
        playerCamera = FindAnyObjectByType<FirstPersonLook>();
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    void Update()
    {
        if (target == null) return;

        var distance = Vector3.Distance(transform.position, target.position);

        if (distance < 2f)
        {
            //DIE
            ReachedPlayer();
            speed = 0;
            agent.isStopped = true;
            Debug.Log("Jumpscare");
        }
        else
        {
            agent.isStopped = false;
            agent.speed = speed;

            if (distance < viewDistance)
            {
                // SEEK
                if (currentState != "SEEK")
                {
                    PlayRandomSound(findSounds);
                    currentState = "SEEK";
                }
                seePlayer = true;
                agent.destination = target.position;
            }
            else
            {
                // SEARCH
                if (currentState != "SEARCH")
                {
                    PlayRandomSound(searchSounds);
                    currentState = "SEARCH";
                }
                seePlayer = false;
                if (agent.remainingDistance < 0.5f)
                {
                    Wander();
                }
            }
        }

        CheckIfStuck();
        UpdateAnimator();

        // ѕериодически проигрывать звуки
        if (Time.time - lastSoundTime >= soundInterval)
        {
            if (currentState == "SEEK")
            {
                PlayRandomSound(findSounds);
            }
            else if (currentState == "SEARCH")
            {
                PlayRandomSound(searchSounds);
            }
            lastSoundTime = Time.time;
        }
    }

    private void PlayRandomSound(AudioClip[] sounds)
    {
        if (sounds.Length == 0) return;

        int randomNum = Random.Range(0, sounds.Length);
        AudioClip randomClip = sounds[randomNum];
        source.clip = randomClip;
        source.Play();
    }

    private void UpdateAnimator()
    {
        var currentSpeed = agent.velocity.magnitude;

        if (currentSpeed == 0)
        {
            PlayAnimation("Idle");
        }
        else if (currentSpeed < 4)
        {
            PlayAnimation("Run&Walk");
        }
        else
        {
            PlayAnimation("Run&Walk");
        }
    }

    private void Wander()
    {
        Vector3 wanderTarget = transform.position + Random.insideUnitSphere * wanderDistance;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(wanderTarget, out hit, wanderDistance, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    private void PlayAnimation(string animationName)
    {
        if (animator.HasState(0, Animator.StringToHash(animationName)))
        {
            animator.Play(animationName);
        }
        else
        {
            Debug.LogWarning($"Animation '{animationName}' does not exist in the Animator.");
        }
    }

    private void CheckIfStuck()
    {
        if (Vector3.Distance(transform.position, lastPosition) < 0.1f)
        {
            stuckTime += Time.deltaTime;
            if (stuckTime > 1f) // если застр€л более 1 секунды
            {
                Wander();
                stuckTime = 0;
            }
        }
        else
        {
            stuckTime = 0;
        }
        lastPosition = transform.position;
    }

    private async void ReachedPlayer()
    {
        reachedSource.PlayOneShot(jumpScareSound);
        playerMovement.canMove = false;
        playerCamera.canFollow = false;
        Cursor.visible = !Cursor.visible;
        Cursor.lockState = CursorLockMode.None;
        gameUi.gameObject.SetActive(false);
        source.enabled = false;
        await new WaitForSeconds(timeBfrRestart);
        reachedSource.enabled = false;
        panelUi.gameObject.SetActive(true);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Collided with wall");
            if (!seePlayer)
            {
                Wander();
            }
        }
    }

}
