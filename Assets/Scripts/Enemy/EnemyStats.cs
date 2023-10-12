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

    public float DespawnDistance = 20f;
    Transform _player;

    void Awake()
    {
        CurrentMovementSpeed = EnemyData.MovementSpeed;
        CurrentHealth = EnemyData.MaxHealth;
        CurrentDamage = EnemyData.Damage;
    }

    private void Start()
    {
        _player = FindAnyObjectByType<PlayerStats>().transform;
    }

    private void Update()
    {
        if(Vector2.Distance(transform.position, _player.position) >= DespawnDistance)
        {
            ReturnEnemy();
        }
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

    private void OnDestroy()
    {
        // if scene is not loaded (exiting Play mode), pass this part.
        if (!gameObject.scene.isLoaded)
        {
            return;
        }

        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        enemySpawner.OnEnemyKilled();
    }

    private void ReturnEnemy()
    {
        EnemySpawner enemySpawner = FindAnyObjectByType<EnemySpawner>();
        transform.position = _player.position + enemySpawner.RelativeSpawnPoints[Random.Range(0, enemySpawner.RelativeSpawnPoints.Count)].position;
    }
}