using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingApple : Pickup, ICollectible
{
    public int HealthToRestore;

    public void Collect()
    {
        // Keep trying to find player object for every gem is not good
        PlayerStats player = FindAnyObjectByType<PlayerStats>();
        player.RestoreHealth(HealthToRestore);
    }
}
