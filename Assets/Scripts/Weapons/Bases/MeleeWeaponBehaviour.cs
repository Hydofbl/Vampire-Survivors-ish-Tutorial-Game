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
        currentDamage = WeaponData.Damage;
        currentSpeed = WeaponData.Speed;
        currentCooldownDuration = WeaponData.CooldownDuration;
        currentPierce = WeaponData.Pierce;
    }


    public float GetCurrentDamage()
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().CurrentMight;
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
                enemy.TakeDamage(GetCurrentDamage());
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent(out BreakableProp prop))
            {
                prop.TakeDamage(GetCurrentDamage());
            }
        }
    }
}
