using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private CharacterScriptableObject CharacterData;

    [Header("Player Stats")]
    // Current Stats
    public float CurrentHealth;
    [HideInInspector]
    public float CurrentRecovery;
    [HideInInspector]
    public float CurrentMovementSpeed;
    [HideInInspector]
    public float CurrentMight;
    [HideInInspector]
    public float CurrentProjectileSpeed;
    [HideInInspector]
    public float CurrentMagnet;

    // Experience and level of the player
    [Header("Experience/Level")]
    public int Experience = 0;
    public int Level = 1;
    public int ExperienceCap;

    public List<LevelRange> LevelRanges;

    // Class for defining a level range and the corresponding experience cap increase for that range   
    [System.Serializable]
    public class LevelRange
    {
        public int MinLevel;
        public int MaxLevel;
        public int ExperienceCapIncrease;
    }

    [Header("I-Frames")]
    public float InvincibilityDuration;
    private float _invincibilityTimer;
    private bool _isInvincible;

    private InventoryManager _inventoryManager;
    [Header("Inventory")]
    public int WeaponId;
    public int PassiveItemId;
    public GameObject SecondWeaponTest;
    public GameObject FirstPassiveItemTest, SecondPassiveItemTest;

    private void Awake()
    {
        CharacterData = CharacterSelector.GetData();
        CharacterSelector.Instance.DestroySingleton();

        _inventoryManager = GetComponent<InventoryManager>();

        // Variable assignments
        CurrentHealth = CharacterData.MaxHealth;
        CurrentRecovery = CharacterData.Recovery;
        CurrentMovementSpeed = CharacterData.MovementSpeed;
        CurrentMight = CharacterData.Might;
        CurrentProjectileSpeed = CharacterData.ProjectileSpeed;
        CurrentMagnet = CharacterData.Magnet;

        // Spawn the starting weapon
        SpawnWeapon(CharacterData.StartingWeapon);
        SpawnWeapon(SecondWeaponTest);
        SpawnPassiveItem(FirstPassiveItemTest);
        SpawnPassiveItem(SecondPassiveItemTest);
    }

    private void Start()
    {
        ExperienceCap = LevelRanges[0].ExperienceCapIncrease;
    }

    private void Update()
    {
        if(_invincibilityTimer > 0)
        {
            _invincibilityTimer -= Time.deltaTime;
        }
        else if(_isInvincible)
        {
            _isInvincible = false;
        }

        Recover();
    }

    public void IncreaseExperience(int amount)
    {
        Experience += amount;

        LevelUpChecker();
    }

    private void LevelUpChecker()
    {

        if (Experience >= ExperienceCap)
        {
            Level++;
            Experience -= ExperienceCap;

            int experienceCapIncrease = 0;

            // looking for all ranges is not good
            foreach(LevelRange range in LevelRanges)
            {
                if(Level >= range.MinLevel && Level <= range.MaxLevel)
                {
                    experienceCapIncrease = range.ExperienceCapIncrease;
                    break;
                }
            }

            ExperienceCap += experienceCapIncrease;

            // Check if current experience is still higher than experiencecap
            LevelUpChecker();
        }
    }

    public void RestoreHealth(int amount)
    {
        // Only restore health if it's value below the maximum health
        if(CurrentHealth < CharacterData.MaxHealth)
        {
            // If current health's value become higher than maximum health after restoring it, equals it to the maximum health's.
            // If it's not, just add restored amount to the current health
            CurrentHealth = CurrentHealth + amount > CharacterData.MaxHealth ? CharacterData.MaxHealth : CurrentHealth + amount;
        }
    }

    public void TakeDamage(float amount)
    {
        if(!_isInvincible)
        {
            CurrentHealth -= amount;

            _invincibilityTimer = InvincibilityDuration;
            _isInvincible = true;

            if (CurrentHealth <= 0)
            {
                Kill();
            }
        }
    }

    public void Kill()
    {
        //Destroy(gameObject);
    }

    void Recover()
    {
        if(CurrentHealth < CharacterData.MaxHealth)
        {
            CurrentHealth += CurrentRecovery * Time.deltaTime;

            if(CurrentHealth > CharacterData.MaxHealth)
            {
                CurrentHealth = CharacterData.MaxHealth;
            }
        }
    }

    // Spawns weapon's controllers
    public void SpawnWeapon(GameObject weapon)
    {
        // Starts from 0
        if(WeaponId >= _inventoryManager.WeaponSlots.Count - 1)
        {
            // slots already full
            return;
        }

        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform, false);
        _inventoryManager.AddWeapon(WeaponId, spawnedWeapon.GetComponent<WeaponController>());

        WeaponId++;
    }

    public void SpawnPassiveItem(GameObject passiveItem)
    {
        // Starts from 0
        if (PassiveItemId >= _inventoryManager.PassiveItemSlots.Count - 1)
        {
            // slots already full
            return;
        }

        GameObject spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnedPassiveItem.transform.SetParent(transform, false);
        _inventoryManager.AddPassiveItem(PassiveItemId, spawnedPassiveItem.GetComponent<PassiveItem>());

        PassiveItemId++;
    }
}
