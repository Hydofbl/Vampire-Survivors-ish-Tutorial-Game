using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string WaveName;
        public List<EnemyGroup> EnemyGroups;
        public int WaveQuota;       // Total number of enemies to spawn in this wave
        public float SpawnInterval;
        public int SpawnCount;      // Numbers of enemies already spawned in this wave
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string EnemyNames;
        public GameObject EnemyPrefab;
        public int EnemyCount;
        public int SpawnCount;
    }

    [SerializeField] private Transform parentTransform;

    [Header("Enemy Spawning")]
    public List<Wave> Waves;
    public int CurrentWaveCount;

    [Header("Spawner Attributes")]
    float spawnTimer;
    public int EnemiesAlive;
    public int MaxEnemyAllowed;
    public bool MaxEnemiesReached = false;
    public float WaveInterval;

    private bool isWaveActive = false;

    [Header("Spawn Positions")]
    public List<Transform> RelativeSpawnPoints;

    private Transform _player;

    void Start()
    {
        _player = FindAnyObjectByType<PlayerStats>().transform;
        CalculateWaveQuota();
    }

    void Update()
    {
        // Check if the wave has ended and the next wave should start
        if(CurrentWaveCount < Waves.Count && Waves[CurrentWaveCount].SpawnCount == 0 && !isWaveActive)
        {
            StartCoroutine(BeginNextWave());
        }

        spawnTimer += Time.deltaTime;
    
        if(spawnTimer >= Waves[CurrentWaveCount].SpawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }

        Debug.Log(CurrentWaveCount);
    }

    IEnumerator BeginNextWave()
    {
        isWaveActive = true;

        yield return new WaitForSeconds(WaveInterval);

        //////// If WaveInterval is a small number, the spawner passes to the next wave without spawning all enemies

        // For 5 waves, CurrentWaveCount starts from 0 to 4
        if (CurrentWaveCount < Waves.Count - 1)
        {
            isWaveActive = false;
            CurrentWaveCount++;
            CalculateWaveQuota();
        }
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;

        foreach(var enemyGroup in Waves[CurrentWaveCount].EnemyGroups)
        {
            currentWaveQuota += enemyGroup.EnemyCount;
        }

        Waves[CurrentWaveCount].WaveQuota = currentWaveQuota;
        Debug.LogWarning(currentWaveQuota);
    }

    void SpawnEnemies()
    {
        // Check if the minimum number of enemies in the wave have been spawned
        if (Waves[CurrentWaveCount].SpawnCount < Waves[CurrentWaveCount].WaveQuota && !MaxEnemiesReached)
        {
            // Spawn each type of enemy until the quota is filled
            foreach(var enemyGroup in Waves[CurrentWaveCount].EnemyGroups)
            {
                // Check if the minimum number of enemies of this type have been spawned
                if(enemyGroup.SpawnCount < enemyGroup.EnemyCount)
                {
                    Instantiate(enemyGroup.EnemyPrefab, _player.position + RelativeSpawnPoints[Random.Range(0, RelativeSpawnPoints.Count)].position, Quaternion.identity, parentTransform);

                    enemyGroup.SpawnCount++;
                    Waves[CurrentWaveCount].SpawnCount++;
                    EnemiesAlive++;

                    if (EnemiesAlive >= MaxEnemyAllowed)
                    {
                        MaxEnemiesReached = true;
                        return;
                    }
                }
            }
        }
    }

    public void OnEnemyKilled()
    {
        EnemiesAlive--;

        // Reset the MaxEnemiesReached flag if the number of enemies alive has dropped below the maximum amount
        if (MaxEnemiesReached && EnemiesAlive < MaxEnemyAllowed)
        {
            MaxEnemiesReached = false;
        }
    }

}
