using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base script of all projectile weapon beahaviours
/// </summary>

public class ProjectileWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject WeaponData;
    protected Vector3 direction;
    public float destroyAfterSeconds;

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void SetRawDirection(Vector3 rawDir)
    {
        direction = rawDir;
    }

    public void DirectionChecker(Vector3 dir)
    {
        float dirX = dir.x;
        float dirY = dir.y;
    
        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        // Change scale
        // if any direction value is zero then multiply that scale with 1,
        // else multiply that scale with direction value
        scale.x *= dirX == 0 ? 1 : dirX;
        scale.y *= dirY == 0 ? 1 : dirY;

        // if direction is diagonal
        if (dirX != 0 && dirY != 0)
        {
            // Rotate
            rotation.z += dirX * dirY * 45f;
        }

        // if direction is vertical
        if(dirX == 0 && dirY != 0)
        {
            // Rotate
            rotation.z += 90f;
        }

        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
