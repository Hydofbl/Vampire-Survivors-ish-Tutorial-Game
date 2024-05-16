using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Original Movement Direction
    public Vector2 MoveDir;
    [HideInInspector]
    // Using for movement
    public Vector2 MoveDirRaw;
    [HideInInspector]
    // Using for calculating projectile's direction
    public Vector2 LastMovedVector;

    private Rigidbody2D _rb;
    private PlayerStats _playerStats;

    private float _lastVerticalVector;
    private float _lastHorizontalVector;

    private void Start()
    {
        _playerStats = GetComponent<PlayerStats>();
        _rb = GetComponent<Rigidbody2D>();
        LastMovedVector = Vector2.right;
    }

    private void Update()
    {
        InputManagement();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void InputManagement()
    {
        if(GameManager.Instance.IsGameOver)
        {
            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        MoveDir = new Vector2(moveX, moveY);
        MoveDirRaw = MoveDir.normalized;

        if (MoveDir.x != 0)
        {
            _lastHorizontalVector = MoveDir.x;
            LastMovedVector = new Vector2(_lastHorizontalVector, 0f);
        }

        if(MoveDir.y != 0)
        {
            _lastVerticalVector = MoveDir.y;
            LastMovedVector = new Vector2(0f, _lastVerticalVector);
        }

        if(MoveDir.x != 0 && MoveDir.y != 0)
        {
            LastMovedVector = new Vector2(_lastHorizontalVector, _lastVerticalVector);
        }
    }

    private void Move()
    {
        if (GameManager.Instance.IsGameOver)
        {
            return;
        }

        _rb.velocity = new Vector2(MoveDirRaw.x * _playerStats.CurrentMovementSpeed, MoveDirRaw.y * _playerStats.CurrentMovementSpeed);
    }
}
