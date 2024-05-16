using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [System.Serializable]
    public class WeaponUpgrade
    {
        public int WeaponUpgradeIndex;
        public GameObject InitialWeapon;
        public WeaponScriptableObject WeaponData;
    }

    [System.Serializable]
    public class PassiveItemUpgrade
    {
        public int PassiveItemUpgradeIndex;
        public GameObject InitialPassiveItem;
        public PassiveItemScriptableObject PassiveItemData;
    }

    [System.Serializable]
    public class UpgradeUI
    {
        public TMP_Text UpgradeNameDisplay;
        public TMP_Text UpgradeDescriptionDisplay;
        public Image UpgradeIcon;
        public Button UpgradeButton;
    }

    public List<WeaponUpgrade> WeaponUpgradeOptions = new List<WeaponUpgrade>();
    public List<PassiveItemUpgrade> PassiveItemUpgradeOptions = new List<PassiveItemUpgrade>();
    public List<UpgradeUI> UpgradeUIOptions = new List<UpgradeUI>();

    private PlayerStats _playerStats;

    private void Start()
    {
        _playerStats = GetComponent<PlayerStats>();
    }

    public void AddWeapon(int slotId, WeaponController weapon)
    {
        WeaponSlots[slotId] = weapon;
        WeaponLevels[slotId] = weapon.WeaponData.Level;
        WeaponUISlots[slotId].enabled = true;
        WeaponUISlots[slotId].sprite = weapon.WeaponData.Icon;

        if(GameManager.Instance != null && GameManager.Instance.ChoosingUpgrade)
        {
            GameManager.Instance.EndLevelUp();
        }
    }

    public void AddPassiveItem(int slotId, PassiveItem passiveItem)
    {
        PassiveItemSlots[slotId] = passiveItem;
        passiveItemLevels[slotId] = passiveItem.PassiveItemData.Level;
        PassiveItemUISlots[slotId].enabled |= true;
        PassiveItemUISlots[slotId].sprite = passiveItem.PassiveItemData.Icon;

        if (GameManager.Instance != null && GameManager.Instance.ChoosingUpgrade)
        {
            GameManager.Instance.EndLevelUp();
        }
    }

    public void LevelUpWeapon(int  slotId, int upgradeIndex)
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

        WeaponUpgradeOptions[upgradeIndex].WeaponData = upgradedWeapon.GetComponent<WeaponController>().WeaponData ;

        if (GameManager.Instance != null && GameManager.Instance.ChoosingUpgrade)
        {
            GameManager.Instance.EndLevelUp();
        }
    }

    public void LevelUpPassiveItem(int slotId, int upgradeIndex)
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

        PassiveItemUpgradeOptions[upgradeIndex].PassiveItemData = upgradedPassiveItem.GetComponent<PassiveItem>().PassiveItemData;

        if (GameManager.Instance != null && GameManager.Instance.ChoosingUpgrade)
        {
            GameManager.Instance.EndLevelUp();
        }
    }

    void ApplyUpgradeOptions()
    {
        List<WeaponUpgrade> avaliableWeaponUpgrades = new List<WeaponUpgrade>(WeaponUpgradeOptions);
        List<PassiveItemUpgrade> avaliablePassiveItemUpgrades = new List<PassiveItemUpgrade>(PassiveItemUpgradeOptions);

        foreach(var upgradeOption in UpgradeUIOptions)
        {
            if(avaliableWeaponUpgrades.Count == 0 && avaliablePassiveItemUpgrades.Count == 0)
            {
                return;
            }

            // Choose upgrade type
            int upgradeType;

            if(avaliablePassiveItemUpgrades.Count == 0)
            {
                upgradeType = 1;
            }
            else if(avaliableWeaponUpgrades.Count == 0)
            {
                upgradeType = 2;
            }
            else
            {
                // Choose between weapon and passive item
                upgradeType = Random.Range(1, 3);
            }

            if (upgradeType == 1)
            {
                WeaponUpgrade chosenWeaponUpgrade = avaliableWeaponUpgrades[Random.Range(0, avaliableWeaponUpgrades.Count)];

                avaliableWeaponUpgrades.Remove(chosenWeaponUpgrade);

                if(chosenWeaponUpgrade != null)
                {
                    EnableUpgradeUI(upgradeOption);

                    bool newWeapon = false;

                    for (int i = 0; i < WeaponSlots.Count; i++)
                    {
                        if (WeaponSlots[i] != null && WeaponSlots[i].WeaponData == chosenWeaponUpgrade.WeaponData)
                        {
                            newWeapon = false;
                            
                            if(!newWeapon)
                            {
                                if(!chosenWeaponUpgrade.WeaponData.NextLevelPrefab)
                                {
                                    DisableUpgradeUI(upgradeOption);
                                    break;
                                }

                                upgradeOption.UpgradeButton.onClick.AddListener(() => LevelUpWeapon(i, chosenWeaponUpgrade.WeaponUpgradeIndex));
                                upgradeOption.UpgradeDescriptionDisplay.text = chosenWeaponUpgrade.WeaponData.NextLevelPrefab.GetComponent<WeaponController>().WeaponData.Description;
                                upgradeOption.UpgradeNameDisplay.text = chosenWeaponUpgrade.WeaponData.NextLevelPrefab.GetComponent<WeaponController>().WeaponData.name;
                            }
                            break;
                        }
                        else
                        {
                            newWeapon = true;
                        }
                    }

                    if(newWeapon)
                    {
                        upgradeOption.UpgradeButton.onClick.AddListener(() => _playerStats.SpawnWeapon(chosenWeaponUpgrade.InitialWeapon));
                        upgradeOption.UpgradeDescriptionDisplay.text = chosenWeaponUpgrade.WeaponData.Description;
                        upgradeOption.UpgradeNameDisplay.text = chosenWeaponUpgrade.WeaponData.name;
                    }

                    upgradeOption.UpgradeIcon.sprite = chosenWeaponUpgrade.WeaponData.Icon;
                }
            }
            else if(upgradeType == 2)
            {
                PassiveItemUpgrade chosenPassiveItemUpgrade = avaliablePassiveItemUpgrades[Random.Range(0, avaliablePassiveItemUpgrades.Count)];

                avaliablePassiveItemUpgrades.Remove(chosenPassiveItemUpgrade);

                if (chosenPassiveItemUpgrade != null)
                {
                    EnableUpgradeUI(upgradeOption);

                    bool newPassiveItem = false;

                    for (int i = 0; i < PassiveItemSlots.Count; i++)
                    {
                        if (PassiveItemSlots[i] != null && PassiveItemSlots[i].PassiveItemData == chosenPassiveItemUpgrade.PassiveItemData)
                        {
                            newPassiveItem = false;

                            if (!newPassiveItem)                
                            {
                                if(!chosenPassiveItemUpgrade.PassiveItemData.NextLevelPrefab)
                                {
                                    DisableUpgradeUI(upgradeOption);
                                    break;
                                }

                                upgradeOption.UpgradeButton.onClick.AddListener(() => LevelUpPassiveItem(i, chosenPassiveItemUpgrade.PassiveItemUpgradeIndex));
                                upgradeOption.UpgradeDescriptionDisplay.text = chosenPassiveItemUpgrade.PassiveItemData.NextLevelPrefab.GetComponent<PassiveItem>().PassiveItemData.Description;
                                upgradeOption.UpgradeNameDisplay.text = chosenPassiveItemUpgrade.PassiveItemData.NextLevelPrefab.GetComponent<PassiveItem>().PassiveItemData.name;

                            }
                            break;
                        }
                        else
                        {
                            newPassiveItem = true;
                        }
                    }

                    if (newPassiveItem)
                    {
                        upgradeOption.UpgradeButton.onClick.AddListener(() => _playerStats.SpawnPassiveItem(chosenPassiveItemUpgrade.InitialPassiveItem));
                        upgradeOption.UpgradeDescriptionDisplay.text = chosenPassiveItemUpgrade.PassiveItemData.Description;
                        upgradeOption.UpgradeNameDisplay.text = chosenPassiveItemUpgrade.PassiveItemData.name;
                    }
                    
                    upgradeOption.UpgradeIcon.sprite = chosenPassiveItemUpgrade.PassiveItemData.Icon;
                }
            }
        }
    }

    void RemoveUpgradeOptions()
    {
        foreach(var upgradeOption in UpgradeUIOptions)
        {
            upgradeOption.UpgradeButton.onClick.RemoveAllListeners();
            DisableUpgradeUI(upgradeOption); // For disabling all upgrade ui options
        }
    }

    public void RemoveAndApplyUpgrades()
    {
        RemoveUpgradeOptions();
        ApplyUpgradeOptions();
    }

    private void DisableUpgradeUI(UpgradeUI ui)
    {
        ui.UpgradeNameDisplay.transform.parent.gameObject.SetActive(false);
    }

    private void EnableUpgradeUI(UpgradeUI ui)
    {
        ui.UpgradeNameDisplay.transform.parent.gameObject.SetActive(true);
    }
}
