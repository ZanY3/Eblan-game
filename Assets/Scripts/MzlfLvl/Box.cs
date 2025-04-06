using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public float DetectionRadius = 5f;
    public float PullSpeed = 2f;

    private Rigidbody _rb;
    private Transform _playerTransform;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            _playerTransform = player.transform;
        }
    }

    void Update()
    {
        if (_playerTransform == null)
            return;

        float distance = Vector3.Distance(transform.position, _playerTransform.position);

        if (distance <= DetectionRadius && Input.GetKey(KeyCode.E))
        {
            Vector3 direction = (_playerTransform.position - transform.position).normalized;
            _rb.MovePosition(transform.position + direction * PullSpeed * Time.deltaTime);
        }
    }
}
