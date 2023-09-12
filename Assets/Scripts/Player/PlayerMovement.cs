using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Rigidbody2D _rb;
    
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public Vector2 MoveDirRaw;
    public Vector2 MoveDir;
    [HideInInspector]
    public Vector2 lastMovedVector;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        lastMovedVector = Vector2.right;
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

        MoveDir = new Vector2(moveX, moveY);
        MoveDirRaw = MoveDir.normalized;

        if (MoveDir.x != 0)
        {
            lastHorizontalVector = MoveDir.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f);
        }

        if(MoveDir.y != 0)
        {
            lastVerticalVector = MoveDir.y;
            lastMovedVector = new Vector2(0f, lastVerticalVector);
        }

        if(MoveDir.x != 0 && MoveDir.y != 0)
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector);
        }
    }

    private void Move()
    {
        _rb.velocity = new Vector2(MoveDirRaw.x * moveSpeed, MoveDirRaw.y * moveSpeed);
    }
}
