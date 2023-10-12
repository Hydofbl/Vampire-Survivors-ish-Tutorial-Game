using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : MonoBehaviour
{
    protected PlayerStats playerStats;
    public PassiveItemScriptableObject PassiveItemData;

    protected virtual void ApplyModifier()
    {
        // Apply the boost
    }

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        ApplyModifier();
    }

    void Update()
    {
        
    }
}
