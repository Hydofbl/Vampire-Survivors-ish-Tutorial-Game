using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base script of all melee weapon beahaviours
/// </summary>
/// 
public class MeleeWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject WeaponData;
    public float destroyAfterSeconds;

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }
}
