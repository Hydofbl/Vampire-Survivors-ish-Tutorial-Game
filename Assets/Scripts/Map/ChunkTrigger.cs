using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    private MapController _mapController;
    public GameObject TargetMap;

    private void Start()
    {
        _mapController = FindAnyObjectByType<MapController>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            _mapController.CurrentChunk = TargetMap;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(_mapController.CurrentChunk == TargetMap)
            {
                _mapController.CurrentChunk = null;
            }
        }
    }
}
