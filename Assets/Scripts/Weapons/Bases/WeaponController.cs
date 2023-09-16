using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base script of all weapon controllers
/// </summary>

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public WeaponScriptableObject WeaponData;

    private float currentCooldown;

    protected PlayerMovement playerMovement;

    protected virtual void Start()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        currentCooldown = WeaponData.cooldownDuration;
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
        currentCooldown = WeaponData.cooldownDuration;
    }
}
