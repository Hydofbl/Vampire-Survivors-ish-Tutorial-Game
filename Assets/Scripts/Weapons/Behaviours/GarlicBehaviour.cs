using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicBehaviour : MeleeWeaponBehaviour
{
    List<GameObject> _markedEnemies;

    protected override void Start()
    {
        base.Start();
        _markedEnemies = new List<GameObject>();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Prop"))
        {
            if (!_markedEnemies.Contains(other.gameObject) && other.TryGetComponent(out EnemyStats enemy))
            {
                enemy.TakeDamage(currentDamage);

                // Mark the enemy so it doesn't take another instance of damage from garlic
                _markedEnemies.Add(other.gameObject);
            }
        }
        else if (other.CompareTag("Prop"))
        {
            if (!_markedEnemies.Contains(other.gameObject) && other.TryGetComponent(out BreakableProp prop))
            {
                prop.TakeDamage(currentDamage);

                // Mark the prop so it doesn't take another instance of damage from garlic
                _markedEnemies.Add(other.gameObject);
            }
        }
    }
}
