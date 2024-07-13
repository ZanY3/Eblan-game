using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float viewDistance;
    public float wanderDistance;
    public float speed;
    public Transform target;

    private Rigidbody rb;
    private NavMeshAgent agent;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        var currentSpeed = agent.velocity.magnitude;
        if (target == null) return;
        var distance = Vector3.Distance(transform.position, target.position);
        if (distance < 1.5f)
        {
            Debug.Log("Die");
        }
        else if (currentSpeed == 0)
        {
            Debug.Log(gameObject.name + " is idle");
        }
        else if (currentSpeed < 4)
        {
            Debug.Log(gameObject.name + " is walk");
        }
        else
        {
            Debug.Log(gameObject.name + " is run");
        }
        agent.speed = speed;

        if (distance < viewDistance)
        {
            // SEEK
            agent.destination = target.position;
        }
        else
        {
            // SEARCH
            if (agent.velocity == Vector3.zero)
            {
                var offset = Random.insideUnitSphere * wanderDistance;
                agent.destination = transform.position + offset;
            }
        }
    }
}
