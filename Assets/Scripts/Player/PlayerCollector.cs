using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private PlayerStats _playerStats;
    private CircleCollider2D _CollectingArea;

    public float PullSpeed;

    private void Start()
    {
        _playerStats = FindObjectOfType<PlayerStats>();
        _CollectingArea = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        _CollectingArea.radius = _playerStats.CurrentMagnet;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if other game object has the ICollectible interface
        if(other.TryGetComponent(out ICollectible collectible))
        {
            // Pulling Objects
            // Gets the rigidbody2d component of the item and applies force to it in the force direction
            // TO DO: Instead of applying force, we can use tweening methods.
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            Vector2 forceDir = (transform.position - other.transform.position).normalized;   
            rb.AddForce(forceDir * PullSpeed);

            collectible.Collect();
        }
    }
}
