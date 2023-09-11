using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Rigidbody2D _rb;
    
    [HideInInspector]
    public Vector2 MoveDir;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
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
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        MoveDir = new Vector2(moveX, moveY).normalized;
    }

    private void Move()
    {
        _rb.velocity = new Vector2(MoveDir.x * moveSpeed, MoveDir.y * moveSpeed);
    }
}
