using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base script of all weapon controllers
/// </summary>

public class WeaponContoller : MonoBehaviour
{
    [Header("Weapon Stats")]
    public GameObject weaponPrefab;
    public float damage;
    public float speed;
    public float cooldownDuration;
    public int pierce;

    private float currentCooldown;

    protected PlayerMovement playerMovement;

    protected virtual void Start()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        currentCooldown = cooldownDuration;
    }

    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;

        if(currentCooldown <= 0f)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        currentCooldown = cooldownDuration;
    }
}
