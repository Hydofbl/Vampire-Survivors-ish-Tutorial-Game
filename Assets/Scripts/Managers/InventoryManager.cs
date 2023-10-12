using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<WeaponController> WeaponSlots = new List<WeaponController>();
    public int[] WeaponLevels = new int[6];
    public List<PassiveItem> PassiveItemSlots = new List<PassiveItem>();
    public int[] passiveItemLevels = new int[6];

    public void AddWeapon(int slotId, WeaponController weapon)
    {
        WeaponSlots[slotId] = weapon;
    }

    public void AddPassiveItem(int slotId, PassiveItem passiveItem)
    {
        PassiveItemSlots[slotId] = passiveItem;
    }

    public void LevelUpWeapon(int  slotId)
    {

    }

    public void LevelUpPassiveItem(int slotId)
    {

    }
}
