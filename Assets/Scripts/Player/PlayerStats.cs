using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private CharacterScriptableObject CharacterData;

    [Header("Player Stats")]
    // Current Stats
    private float _currentHealth;
    private float _currentRecovery;
    private float _currentMovementSpeed;
    private float _currentMight;
    private float _currentProjectileSpeed;
    private float _currentMagnet;

    #region Current Stats Properties
    public float CurrentHealth
    {
        get { return _currentHealth; }
        set
        {
            if (_currentHealth != value)
            {
                _currentHealth = value;

                if(GameManager.Instance != null)
                {
                    GameManager.Instance.CurrentHealthDisplay.text = "Health: " + _currentHealth;
                }
                // Other logics that needs to be executed
            }
        }
    }

    public float CurrentRecovery
    {
        get { return _currentRecovery; }
        set
        {
            if (_currentRecovery != value)
            {
                _currentRecovery = value;

                if (GameManager.Instance != null)
                {
                    GameManager.Instance.CurrentRecoveryDisplay.text = "Recovery: " + _currentRecovery;
                }
                // Other logics that needs to be executed
            }
        }
    }

    public float CurrentMovementSpeed
    {
        get { return _currentMovementSpeed; }
        set
        {
            if (_currentMovementSpeed != value)
            {
                _currentMovementSpeed = value;

                if (GameManager.Instance != null)
                {
                    GameManager.Instance.CurrentMoveSpeedDisplay.text = "Move Speed: " + _currentMovementSpeed;
                }
                // Other logics that needs to be executed
            }
        }
    }

    public float CurrentMight
    {
        get { return _currentMight; }
        set
        {
            if (_currentMight != value)
            {
                _currentMight = value;

                if (GameManager.Instance != null)
                {
                    GameManager.Instance.CurrentMightDisplay.text = "Might: " + _currentMight;
                }
                // Other logics that needs to be executed
            }
        }
    }

    public float CurrentProjectileSpeed
    {
        get { return _currentProjectileSpeed; }
        set
        {
            if (_currentProjectileSpeed != value)
            {
                _currentProjectileSpeed = value;

                if (GameManager.Instance != null)
                {
                    GameManager.Instance.CurrentProjectileSpeedDisplay.text = "Projectile Speed: " + _currentProjectileSpeed;
                }
                // Other logics that needs to be executed
            }
        }
    }

    public float CurrentMagnet
    {
        get { return _currentMagnet; }
        set
        {
            if (_currentMagnet != value)
            {
                _currentMagnet = value;

                if (GameManager.Instance != null)
                {
                    GameManager.Instance.CurrentMagnetDisplay.text = "Magnet: " + _currentMagnet;
                }
                // Other logics that needs to be executed
            }
        }
    }
    #endregion

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

    [Header("Inventory")]
    public int WeaponId;
    public int PassiveItemId;
    public GameObject SecondWeaponTest;
    public GameObject FirstPassiveItemTest, SecondPassiveItemTest;

    private InventoryManager _inventoryManager;

    [Header("UI")]
    public Image HealthBar;
    public Image ExpBar;
    public TMP_Text LevelText;

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
        //SpawnWeapon(SecondWeaponTest);
        //SpawnPassiveItem(FirstPassiveItemTest);
        SpawnPassiveItem(SecondPassiveItemTest);
    }

    private void Start()
    {
        ExperienceCap = LevelRanges[0].ExperienceCapIncrease;

        // Assign current stats
        GameManager.Instance.CurrentHealthDisplay.text = "Health: " + _currentHealth;
        GameManager.Instance.CurrentRecoveryDisplay.text = "Recovery: " + _currentRecovery;
        GameManager.Instance.CurrentMoveSpeedDisplay.text = "Move Speed: " + _currentMovementSpeed;
        GameManager.Instance.CurrentMightDisplay.text = "Might: " + _currentMight;
        GameManager.Instance.CurrentProjectileSpeedDisplay.text = "Projectile Speed: " + _currentProjectileSpeed;
        GameManager.Instance.CurrentMagnetDisplay.text = "Magnet: " + _currentMagnet;

        GameManager.Instance.AssignChosenCharacterUI(CharacterData);

        // Update health bar when game start.
        UpdateHealthBar();
        // Update experience bar when game start.
        UpdateExpBar();
        // Update level text when game start.
        UpdateLevelText();
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

        UpdateExpBar();
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

            UpdateLevelText();

            if(GameManager.Instance.CurrentState != GameManager.GameState.LevelUp)
            {
                GameManager.Instance.StartLevelUp();
            }

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

    void UpdateExpBar()
    {
        // Update experience bar fill amount
        ExpBar.fillAmount = (float)Experience / ExperienceCap;
    }

    void UpdateLevelText()
    {
        // Update Level text
        LevelText.text = "Lv. " + Level.ToString();
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

            UpdateHealthBar();
        }
    }

    void UpdateHealthBar()
    {
        HealthBar.fillAmount = CurrentHealth / CharacterData.MaxHealth;
    }
    public void Kill()
    {
        if(!GameManager.Instance.IsGameOver)
        {
            GameManager.Instance.AssignLevelReachedUI(Level);
            GameManager.Instance.AssignChosenWeaponsAndPassiveItems(_inventoryManager.WeaponUISlots, _inventoryManager.PassiveItemUISlots);
            GameManager.Instance.GameOver();
        }
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
