using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if other game object has the ICollectible interface
        if(other.TryGetComponent(out ICollectible collectible))
        {
            collectible.Collect();
        }
    }
}
