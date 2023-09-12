using UnityEngine;

public class StoneController : WeaponContoller
{
    protected override void Start()
    {
        base.Start();
    }
    
    protected override void Attack()
    {
        base.Attack();

        GameObject spawnedStone = Instantiate(weaponPrefab);
        spawnedStone.transform.position = transform.position;
        spawnedStone.GetComponent<StoneBehaviour>().SetRawDirection(playerMovement.lastMovedVector);
        spawnedStone.GetComponent<StoneBehaviour>().DirectionChecker(playerMovement.MoveDir);
    }
}
