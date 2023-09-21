using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBehaviour : ProjectileWeaponBehaviour
{

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        transform.position += direction * WeaponData.Speed * Time.deltaTime;        
    }
}
