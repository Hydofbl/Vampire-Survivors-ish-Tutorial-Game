using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base script of all projectile weapon beahaviours
/// </summary>

public class ProjectileWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject WeaponData;
    protected Vector3 direction;
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

    public void SetRawDirection(Vector3 rawDir)
    {
        direction = rawDir;
    }

    public void DirectionChecker(Vector3 dir)
    {
        float dirX = dir.x;
        float dirY = dir.y;
    
        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        // Change scale
        // if any direction value is zero then multiply that scale with 1,
        // else multiply that scale with direction value
        scale.x *= dirX == 0 ? 1 : dirX;
        scale.y *= dirY == 0 ? 1 : dirY;

        // if direction is diagonal
        if (dirX != 0 && dirY != 0)
        {
            // Rotate
            rotation.z += dirX * dirY * 45f;
        }

        // if direction is vertical
        if(dirX == 0 && dirY != 0)
        {
            // Rotate
            rotation.z += 90f;
        }

        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        // Reference the script from the collided collider and deal damage using TakeDamage method by currentDamage
        if (other.CompareTag("Enemy"))
        {
            if(other.TryGetComponent(out EnemyStats enemy))
            {
                enemy.TakeDamage(GetCurrentDamage());
                ReducePierce();
            }
        }
        else if (other.CompareTag("Prop"))
        {
            if (other.TryGetComponent(out BreakableProp prop))
            {
                prop.TakeDamage(GetCurrentDamage());
                ReducePierce();
            }
        }
    }

    void ReducePierce()
    {
        currentPierce--;

        if(currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
