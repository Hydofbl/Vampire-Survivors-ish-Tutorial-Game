using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private EnemyStats _enemyStats;
    private Transform _playerTransform;  
    private SpriteRenderer _spriteRenderer;
    private Vector3 _lastPosition;

    private Rigidbody2D _rb;

    void Start()
    {
        _playerTransform = FindObjectOfType<PlayerMovement>().transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _enemyStats = GetComponent<EnemyStats>();
    }

    void Update()
    {
        // On the right side of the player
        if ((_playerTransform.position - transform.position).normalized.x <= 0)
        {
            _spriteRenderer.flipX = true;
        }
        // On the left side of the player
        else
        {
            _spriteRenderer.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        _rb.velocity = (_playerTransform.position - transform.position).normalized * _enemyStats.CurrentMovementSpeed;
    }
}
