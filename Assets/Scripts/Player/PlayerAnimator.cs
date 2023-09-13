using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement _playerMovement;
    private int _currentState;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();

        // Default starting state
        _currentState = Down;
    }

    void Update()
    {
        var state = GetState();

        if (_playerMovement.MoveDir.x != 0 || _playerMovement.MoveDir.y != 0)
        {
            _animator.SetBool("Move", true);
        }
        else
        {
            _animator.SetBool("Move", false);
        }

        if (state == _currentState)
            return;

        _animator.CrossFade(state, 0, 0);
        _currentState = state;
    }

    private int GetState()
    {
        int state = _currentState;
        float moveDirX = _playerMovement.MoveDir.x;
        float moveDirY = _playerMovement.MoveDir.y;

        if(moveDirX == 0f && moveDirY != 0f)
        {
            if(moveDirY > 0f)
            {
                state = Up;
            }
            else
            {
                state = Down;
            }
        }
        else if(moveDirX != 0f && moveDirY == 0f)
        {
            if(moveDirX > 0f)
            {
                state = Right;
            }
            else if(moveDirX < 0f)
            {
                state = Left;
            }
        }
        else if(moveDirX != 0f && moveDirY != 0f)
        {
            if(moveDirX > 0f)
            {
                state = Right;
            }
            else if(moveDirX < 0f)
            {
                state = Left;
            }
        }

        return state;
    }

    private static readonly int Left = Animator.StringToHash("PlayerLeftIdleAnim");
    private static readonly int Right = Animator.StringToHash("PlayerRightIdleAnim");
    private static readonly int Up = Animator.StringToHash("PlayerUpIdleAnim");
    private static readonly int Down = Animator.StringToHash("PlayerDownIdleAnim");
}
