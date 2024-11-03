using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public Transform[] patrolPoints; // ѕатрульные точки
    public float patrolPauseDuration = 2f; // ѕауза между точками
    public AudioClip[] searchSounds;
    public AudioClip[] findSounds;
    public float soundInterval = 5f;

    public float pointStopDistance = 0.25f;

    [Header("Reached Player")]
    public GameObject gameUi;
    public GameObject loseUi;
    public string restartScene = "Level 1";
    public AudioSource reachedSource;
    public AudioClip jumpScareSound;
    public float timeBfrRestart = 1f;
    [HideInInspector] public bool isInLosePanel = false;

    [Header("Difficulty settings")]

    [Header("Hard")]
    public float hardViewDistance = 10f;
    public float hardSpeed = 3.5f;

    [Header("Normal")]
    public float normalViewDistance = 9f;
    public float normalSpeed = 3f;

    [Header("Easy")]
    public float easyViewDistance = 8f;
    public float easySpeed = 2.8f;

    private float viewDistance = 10f;
    private float speed = 3.5f;
    private DifficultyController difficultyController;
    private FirstPersonLook playerCamera;
    private FirstPersonMovement playerMovement;
    private AudioSource source;
    private Animator animator;
    private NavMeshAgent agent;
    private bool seePlayer = false;
    private int currentPatrolIndex = 0;
    private string currentState = "";
    private float lastSoundTime = 0f;
    private bool isPaused = false;
    private bool playedReachedSound = false;

    void Start()
    {
        playerMovement = FindAnyObjectByType<FirstPersonMovement>();
        difficultyController = FindAnyObjectByType<DifficultyController>();
        playerCamera = FindAnyObjectByType<FirstPersonLook>();
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        SetDifficultySettings();

        if (patrolPoints.Length > 0)
        {
            StartCoroutine(Patrol());
        }
    }

    void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance < 1.5f)
        {
            ReachedPlayer();
            agent.isStopped = true;
            Debug.Log("Jumpscare");
        }
        else
        {
            agent.isStopped = false;
            agent.speed = speed;

            // ќбновл€ем состо€ние видимости игрока
            seePlayer = distance < viewDistance && CanSeePlayer();

            if (seePlayer)
            {
                if (currentState != "SEEK")
                {
                    PlayRandomSound(findSounds);
                    currentState = "SEEK";
                }
                agent.destination = target.position;
            }
            else
            {
                if (currentState != "PATROL")
                {
                    PlayRandomSound(searchSounds);
                    currentState = "PATROL";
                }
            }
        }

        UpdateAnimator();
        PlaySoundsPeriodically();
    }

    private void SetDifficultySettings()
    {
        if (difficultyController != null)
        {
            switch (difficultyController.gameDifficulty)
            {
                case "Hard":
                    viewDistance = hardViewDistance;
                    speed = hardSpeed;
                    break;
                case "Normal":
                    viewDistance = normalViewDistance;
                    speed = normalSpeed;
                    break;
                case "Easy":
                    viewDistance = easyViewDistance;
                    speed = easySpeed;
                    break;
            }
        }
    }

    private void PlayRandomSound(AudioClip[] sounds)
    {
        if (sounds.Length == 0) return;
        int randomNum = Random.Range(0, sounds.Length);
        source.clip = sounds[randomNum];
        source.Play();
    }

    private bool CanSeePlayer()
    {
        RaycastHit hit;
        Vector3 directionToPlayer = (target.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, directionToPlayer, out hit, viewDistance))
        {
            if (hit.transform == target)
            {
                return true;
            }
        }
        return false;
    }

    private void PlaySoundsPeriodically()
    {
        if (Time.time - lastSoundTime >= soundInterval)
        {
            if (currentState == "SEEK")
            {
                PlayRandomSound(findSounds);
            }
            else if (currentState == "PATROL")
            {
                PlayRandomSound(searchSounds);
            }
            lastSoundTime = Time.time;
        }
    }

    private void UpdateAnimator()
    {
        float currentSpeed = agent.velocity.magnitude;

        if (currentSpeed <= 1)
        {
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", true);
        }
    }

    private IEnumerator Patrol()
    {
        while (true)
        {
            // ѕровер€ем, что враг не видит игрока и есть патрульные точки
            if (!seePlayer && patrolPoints.Length > 0)
            {
                agent.SetDestination(patrolPoints[currentPatrolIndex].position);

                // ∆дем, пока враг достигнет точки или пока не увидит игрока
                yield return new WaitUntil(() => agent.remainingDistance <= agent.stoppingDistance + pointStopDistance || seePlayer);

                if (seePlayer) continue; // ѕрерываем ожидание, если враг увидел игрока

                // ¬ключаем паузу на патрульной точке
                isPaused = true;
                float pauseEndTime = Time.time + patrolPauseDuration;

                while (Time.time < pauseEndTime)
                {
                    // ѕровер€ем видимость игрока каждый кадр во врем€ паузы
                    seePlayer = CanSeePlayer();
                    if (seePlayer)
                    {
                        isPaused = false;
                        break; // ѕрерываем паузу, если враг увидел игрока
                    }
                    yield return null;
                }

                if (seePlayer)
                {
                    // ѕрерываем патруль и начинаем преследование игрока
                    isPaused = false;
                    continue;
                }

                // ѕереходим к следующей патрульной точке
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
                isPaused = false;
            }
            yield return null;
        }
    }

    private async void ReachedPlayer()
    {
        if (!playedReachedSound)
        {
            reachedSource.PlayOneShot(jumpScareSound);
            playedReachedSound = true;
        }
        playerMovement.enabled = false;
        isInLosePanel = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        playerCamera.canFollow = false;
        gameUi.SetActive(false);
        source.enabled = false;
        await new WaitForSeconds(timeBfrRestart);
        loseUi.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Collided with wall");
            if (!seePlayer)
            {
                agent.SetDestination(patrolPoints[currentPatrolIndex].position);
            }
        }
    }
}
