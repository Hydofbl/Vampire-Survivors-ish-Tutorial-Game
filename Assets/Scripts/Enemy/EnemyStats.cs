using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject EnemyData;

    // CUrrent Stats
    [HideInInspector]
    public float CurrentMovementSpeed;
    [HideInInspector]
    public float CurrentHealth;
    [HideInInspector]
    public float CurrentDamage;

    void Awake()
    {
        CurrentMovementSpeed = EnemyData.MovementSpeed;
        CurrentHealth = EnemyData.MaxHealth;
        CurrentDamage = EnemyData.Damage;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        if(CurrentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        // Check if collided object is player, then deal damage using TakeDamage method
        if(other.gameObject.CompareTag("Player"))
        {
            if(other.gameObject.TryGetComponent(out PlayerStats player))
            {
                player.TakeDamage(CurrentDamage);
            }
        }
    }
}
