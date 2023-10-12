using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Animator BlackScreenAnim;

    public static UIManager Instance;

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void StartTransition()
    {
        BlackScreenAnim.enabled = true;

        // _animator.CrossFade(state, 0, 0);
    }

    public void EndTransition()
    {
        BlackScreenAnim.enabled = true;

        // _animator.CrossFade(state, 0, 0);
    }

    private static readonly int Left = Animator.StringToHash("PlayerLeftIdleAnim");
    private static readonly int Right = Animator.StringToHash("PlayerRightIdleAnim");
}