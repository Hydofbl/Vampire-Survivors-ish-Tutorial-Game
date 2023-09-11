using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [Header("Map Info")]
    public List<GameObject> TerrainChunks;
    public GameObject Player;
    public float CheckerRadius;
    public LayerMask TerrainMask;
    public Transform ChunksParent;
    public GameObject CurrentChunk;

    [Space]
    public float ChunkSize;

    private PlayerMovement _playerMovement;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    private GameObject latestChunk;

    public float maxOpDist;
    private float opDist;

    public bool StopChunkOptimizer;
    public float OptimizerCooldown;

    void Start()
    {
        _playerMovement = Player.gameObject.GetComponent<PlayerMovement>();
        StartCoroutine(ChunkOptimizer());
    }

    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    void ChunkChecker()
    {
        if(!CurrentChunk)
        {
            return;
        }

        // Horizontal or Vertical Spawning
        if(_playerMovement.MoveDir.x == 0 || _playerMovement.MoveDir.y == 0)
        {
            Vector3 noTerraincheckPoint = CurrentChunk.transform.position + new Vector3(ChunkSize * _playerMovement.MoveDir.x, ChunkSize * _playerMovement.MoveDir.y, 0);

            if (!Physics2D.OverlapCircle(noTerraincheckPoint, CheckerRadius, TerrainMask))
            {
                SpawnChunk(noTerraincheckPoint);
            }
        }
        // Diagonal Spawning
        else
        {
            Vector3 noTerraincheckPoint = CurrentChunk.transform.position + new Vector3(ChunkSize * _playerMovement.MoveDir.x, ChunkSize * _playerMovement.MoveDir.y, 0);
            Vector3 noTerraincheckPointCorner1 = CurrentChunk.transform.position + new Vector3(ChunkSize * _playerMovement.MoveDir.x, 0, 0);
            Vector3 noTerraincheckPointCorner2 = CurrentChunk.transform.position + new Vector3(0, ChunkSize * _playerMovement.MoveDir.y, 0);

            if (!Physics2D.OverlapCircle(noTerraincheckPoint, CheckerRadius, TerrainMask))
            {
                SpawnChunk(noTerraincheckPoint);
            }

            if (!Physics2D.OverlapCircle(noTerraincheckPointCorner1, CheckerRadius, TerrainMask))
            {
                SpawnChunk(noTerraincheckPointCorner1);
            }

            if (!Physics2D.OverlapCircle(noTerraincheckPointCorner2, CheckerRadius, TerrainMask))
            {
                SpawnChunk(noTerraincheckPointCorner2);
            }
        }
    }

    void SpawnChunk(Vector3 noTerrainPosition)
    {
        int rand = Random.Range(0, TerrainChunks.Count);
        latestChunk = Instantiate(TerrainChunks[rand], noTerrainPosition, Quaternion.identity, ChunksParent);
        spawnedChunks.Add(latestChunk);
    }

    IEnumerator ChunkOptimizer()
    {
        while(!StopChunkOptimizer)
        {
            foreach (var chunk in spawnedChunks)
            {
                opDist = Vector3.Distance(Player.transform.position, chunk.transform.position);

                if (opDist > maxOpDist)
                {
                    chunk.SetActive(false);
                }
                else
                {
                    chunk.SetActive(true);
                }
            }

            yield return new WaitForSeconds(OptimizerCooldown);
        }
    }
}
