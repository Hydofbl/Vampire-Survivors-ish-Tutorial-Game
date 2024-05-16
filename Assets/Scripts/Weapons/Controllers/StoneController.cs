using UnityEngine;

public class StoneController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }
    
    protected override void Attack()
    {
        base.Attack();

        GameObject spawnedStone = Instantiate(WeaponData.WeaponPrefab);
        spawnedStone.transform.position = transform.position;
        spawnedStone.GetComponent<StoneBehaviour>().SetRawDirection(playerMovement.LastMovedVector);
        spawnedStone.GetComponent<StoneBehaviour>().DirectionChecker(playerMovement.LastMovedVector);
    }
}
