using UnityEngine;
using UnityEngine.AI;

public class Enemy_Old : MonoBehaviour
{
    public float viewDistance = 10f;
    public float wanderDistance = 5f;
    public float speed = 3.5f;
    public Transform target;
    public AudioClip[] searchSounds;
    public AudioClip[] findSounds;
    public float soundInterval = 5f; // Интервал между воспроизведением звуков

    [Header("Reached Player")]
    public GameObject gameUi;
    public GameObject panelUi;
    public string restartScene = "Level 1";
    public AudioSource reachedSource;
    public AudioClip jumpScareSound;
    public float timeBfrRestart = 1f;

    [Header("Difficulty changes")]

    [Header("Hard")]
    public float hardViewDistance = 10f;
    public float normalViewDistance = 9f;
    public float easyViewDistance = 8f;

    [Header("Normal")]
    public float hardWanderDistance = 5f;
    public float normalWanderDistance = 4f;
    public float easyWanderDistance = 3f;

    [Header("Easy")]
    public float hardSpeed = 3.5f;
    public float normalSpeed = 3f;
    public float easySpeed = 2.8f;

    private DifficultyController difficultyController;

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
    private float lastSoundTime = 0f; // Время последнего воспроизведения звука

    void Start()
    {
        #region settings
        difficultyController = FindAnyObjectByType<DifficultyController>();
        playerMovement = FindAnyObjectByType<FirstPersonMovement>();
        playerCamera = FindAnyObjectByType<FirstPersonLook>();
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        #endregion
        #region difficulty
        if(difficultyController != null)
        {
            if (difficultyController.gameDifficulty == "Hard")
            {
                viewDistance = hardViewDistance;
                wanderDistance = hardWanderDistance;
                speed = hardSpeed;
            }

            else if (difficultyController.gameDifficulty == "Normal")
            {
                viewDistance = normalViewDistance;
                wanderDistance = normalWanderDistance;
                speed = normalSpeed;
            }

            else if (difficultyController.gameDifficulty == "Easy")
            {
                viewDistance = easyViewDistance;
                wanderDistance = easyWanderDistance;
                speed = easySpeed;
            }
        }
        #endregion
    }

    void Update()
    {
        if (target == null) return;

        var distance = Vector3.Distance(transform.position, target.position);

        if (distance < 2f)
        {
            // Достиг игрока
            ReachedPlayer();
            speed = 0;
            agent.isStopped = true;
            Debug.Log("Jumpscare");
        }
        else
        {
            agent.isStopped = false;
            agent.speed = speed;

            if (distance < viewDistance && CanSeePlayer())
            {
                // ВИДИТ ИГРОКА
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
                // НЕ ВИДИТ ИГРОКА
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

        // Периодически проигрывать звуки
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

    private bool CanSeePlayer()
    {
        RaycastHit hit;
        Vector3 directionToPlayer = (target.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, directionToPlayer, out hit, viewDistance))
        {
            if (hit.transform == target)
            {
                return true; // Враг видит игрока
            }
        }
        return false; // Враг не видит игрока
    }

    void OnDrawGizmos() //для наглядности
    {
        if (target == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, target.position);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        // Рисуем сферу радиуса блуждания
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, wanderDistance);
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

    private void Wander()
    {
        Vector3 wanderTarget = transform.position + Random.insideUnitSphere * wanderDistance;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(wanderTarget, out hit, wanderDistance, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    private void CheckIfStuck()
    {
        if (Vector3.Distance(transform.position, lastPosition) < 0.1f)
        {
            stuckTime += Time.deltaTime;
            if (stuckTime > 2.5f)
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
        Cursor.visible = Cursor.visible = true;
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
