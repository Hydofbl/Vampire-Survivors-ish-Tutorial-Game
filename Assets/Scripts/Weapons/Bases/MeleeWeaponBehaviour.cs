using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base script of all melee weapon beahaviours
/// </summary>
/// 
public class MeleeWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject WeaponData;
    public float destroyAfterSeconds;

    // Current Stats
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;

    private void Awake()
    {
        currentDamage = WeaponData.damage;
        currentSpeed = WeaponData.speed;
        currentCooldownDuration = WeaponData.cooldownDuration;
        currentPierce = WeaponData.pierce;
    }

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            if(other.TryGetComponent(out EnemyStats enemy))
            {
                enemy.TakeDamage(currentDamage);
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent(out BreakableProp prop))
            {
                prop.TakeDamage(currentDamage);
            }
        }
    }
}
