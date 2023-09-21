using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject CharacterData;

    // Current Stats
    private float _currentHealth;
    private float _currentRecovery;
    private float _currentMovementSpeed;
    private float _currentMight;
    private float _currentProjectileSpeed;

    // Experience and level of the player
    [Header("Experience/Level")]
    public int Experience = 0;
    public int Level = 1;
    public int ExperienceCap;

    [Header("I-Frames")]
    public float InvincibilityDuration;
    private float _invincibilityTimer;
    private bool _isInvincible;

    // Class for defining a level range and the corresponding experience cap increase for that range   
    [System.Serializable]
    public class LevelRange
    {
        public int MinLevel;
        public int MaxLevel;
        public int ExperienceCapIncrease;
    }

    public List<LevelRange> LevelRanges;

    private void Awake()
    {
        // Variable assignments
        _currentHealth = CharacterData.MaxHealth;
        _currentRecovery = CharacterData.Recovery;
        _currentMovementSpeed = CharacterData.MovementSpeed;
        _currentMight = CharacterData.Might;
        _currentProjectileSpeed = CharacterData.ProjectileSpeed;
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
        if(_currentHealth < CharacterData.MaxHealth)
        {
            // If current health's value become higher than maximum health after restoring it, equals it to the maximum health's.
            // If it's not, just add restored amount to the current health
            _currentHealth = _currentHealth + amount > CharacterData.MaxHealth ? CharacterData.MaxHealth : _currentHealth + amount;
        }
    }

    public void TakeDamage(float amount)
    {
        if(!_isInvincible)
        {
            _currentHealth -= amount;

            _invincibilityTimer = InvincibilityDuration;
            _isInvincible = true;

            if (_currentHealth <= 0)
            {
                Kill();
            }
        }
    }

    public void Kill()
    {
        //Destroy(gameObject);
    }
}
