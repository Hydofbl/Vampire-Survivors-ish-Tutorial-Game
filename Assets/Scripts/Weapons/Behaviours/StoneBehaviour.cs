using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBehaviour : ProjectileWeaponBehaviour
{
    StoneController _stoneController;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _stoneController = FindAnyObjectByType<StoneController>();
    }

    void Update()
    {
        transform.position += direction * _stoneController.speed * Time.deltaTime;        
    }
}
