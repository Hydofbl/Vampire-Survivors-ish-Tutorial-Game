using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGem : MonoBehaviour, ICollectible
{
    public int ExperienceGranted;

    public void Collect()
    {
        // Keep trying to find player object for every gem is not good
        PlayerStats player = FindAnyObjectByType<PlayerStats>();
        player.IncreaseExperience(ExperienceGranted);
        Destroy(gameObject);
    }
}
