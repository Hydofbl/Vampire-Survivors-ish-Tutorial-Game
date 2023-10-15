using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<WeaponController> WeaponSlots = new List<WeaponController>();
    public int[] WeaponLevels = new int[6];
    public List<Image> WeaponUISlots = new List<Image>();
    public List<PassiveItem> PassiveItemSlots = new List<PassiveItem>();
    public int[] passiveItemLevels = new int[6];
    public List<Image> PassiveItemUISlots = new List<Image>();

    public void AddWeapon(int slotId, WeaponController weapon)
    {
        WeaponSlots[slotId] = weapon;
        WeaponLevels[slotId] = weapon.WeaponData.Level;
        WeaponUISlots[slotId].enabled = true;
        WeaponUISlots[slotId].sprite = weapon.WeaponData.Icon;
    }

    public void AddPassiveItem(int slotId, PassiveItem passiveItem)
    {
        PassiveItemSlots[slotId] = passiveItem;
        passiveItemLevels[slotId] = passiveItem.PassiveItemData.Level;
        PassiveItemUISlots[slotId].enabled |= true;
        PassiveItemUISlots[slotId].sprite = passiveItem.PassiveItemData.Icon;
    }

    public void LevelUpWeapon(int  slotId)
    {
        WeaponController weapon = WeaponSlots[slotId];

        if(!weapon.WeaponData.NextLevelPrefab)
        {
            Debug.LogError("NO NEXT LEVEL FOR " + weapon.name);
            return;
        }

        GameObject upgradedWeapon = Instantiate(weapon.WeaponData.NextLevelPrefab, transform.position, Quaternion.identity);
        upgradedWeapon.transform.SetParent(transform);
        AddWeapon(slotId, upgradedWeapon.GetComponent<WeaponController>());
        Destroy(weapon.gameObject);
        WeaponLevels[slotId] = upgradedWeapon.GetComponent<WeaponController>().WeaponData.Level;
    }

    public void LevelUpPassiveItem(int slotId)
    {
        PassiveItem passiveItem = PassiveItemSlots[slotId];

        if (!passiveItem.PassiveItemData.NextLevelPrefab)
        {
            Debug.LogError("NO NEXT LEVEL FOR " + passiveItem.name);
            return;
        }

        GameObject upgradedPassiveItem = Instantiate(passiveItem.PassiveItemData.NextLevelPrefab, transform.position, Quaternion.identity);
        upgradedPassiveItem.transform.SetParent(transform);
        AddPassiveItem(slotId, upgradedPassiveItem.GetComponent<PassiveItem>());
        Destroy(passiveItem.gameObject);
        passiveItemLevels[slotId] = upgradedPassiveItem.GetComponent<PassiveItem>().PassiveItemData.Level;
    }
}
