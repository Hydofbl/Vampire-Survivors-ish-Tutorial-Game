using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{ 
    [SerializeField] private float movementSpeed;

    private Transform _playerTransform;  
    private SpriteRenderer _spriteRenderer;
    private Vector3 _lastPosition;

    void Start()
    {
        _playerTransform = FindObjectOfType<PlayerMovement>().transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _lastPosition = transform.position;
        transform.position = Vector2.MoveTowards(transform.position, _playerTransform.position, movementSpeed * Time.deltaTime);

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
}
