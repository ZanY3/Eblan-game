using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    public Transform player;
    public float speed = 3.5f;

    [Header("Reached player")]
    public GameObject gameUi;
    public GameObject panelUi;
    public string restartScene = "Level 3";
    public AudioSource reachedSource;
    public AudioClip jumpScareSound;
    public float timeBfrRestart = 1f;

    private Weapon weapon;
    private FirstPersonMovement playerMovement;
    private FirstPersonLook playerCamera;


    private NavMeshAgent navMeshAgent;

    void Start()
    {
        weapon = FindAnyObjectByType<Weapon>();
        playerMovement = FindAnyObjectByType<FirstPersonMovement>();
        playerCamera = FindAnyObjectByType<FirstPersonLook>();

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
    }

    void Update()
    {
        if (player != null)
        {
            navMeshAgent.SetDestination(player.position);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ReachedPlayer();
        }
    }
    private async void ReachedPlayer()
    {
        reachedSource.PlayOneShot(jumpScareSound);
        weapon.enabled = false;
        playerMovement.canMove = false;
        playerCamera.canFollow = false;
        Cursor.visible = Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameUi.gameObject.SetActive(false);
        await new WaitForSeconds(timeBfrRestart);
        reachedSource.enabled = false;
        panelUi.gameObject.SetActive(true);

    }
}
