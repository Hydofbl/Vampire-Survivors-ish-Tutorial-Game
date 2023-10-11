using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Serializable]
    public class Wave
    {
        public string WaveName;
        public List<EnemyGroup> EnemyGroups;
        public int WaveQuota;       // Total number of enemies to spawn in this wave
        public float SpawnInterval;
        public int SpawnCount;      // Numbers of enemies already spawned in this wave
    }

    [Serializable]
    public class EnemyGroup
    {
        public string EnemyNames;
        public GameObject EnemyPrefabs;
        public int EnemyCount;
        public int SpawnCount;
    }

    public List<Wave> Waves;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
