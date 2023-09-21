using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public EnemyScriptableObject EnemyData;

    private Transform _playerTransform;  
    private SpriteRenderer _spriteRenderer;
    private Vector3 _lastPosition;

    private Rigidbody2D _rb;

    void Start()
    {
        _playerTransform = FindObjectOfType<PlayerMovement>().transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _lastPosition = transform.position;

        // Moving to the right or stoping
        if((_lastPosition - transform.position).normalized.x <= 0)
        {
            _spriteRenderer.flipX = false;
        }
        // Moving to the left
        else
        {
            _spriteRenderer.flipX = true;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        _rb.velocity = (_playerTransform.position - transform.position).normalized * EnemyData.MovementSpeed;
    }
}
