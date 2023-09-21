using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject EnemyData;

    // CUrrent Stats
    private float _currentMovementSpeed;
    private float _currentHealth;
    private float _currentDamage;

    void Awake()
    {
        _currentMovementSpeed = EnemyData.MovementSpeed;
        _currentHealth = EnemyData.MaxHealth;
        _currentDamage = EnemyData.Damage;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if(_currentHealth <= 0)
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
                player.TakeDamage(_currentDamage);
            }
        }
    }
}
