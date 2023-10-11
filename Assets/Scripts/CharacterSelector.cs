using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public CharacterScriptableObject CharacterData;

    [Header("Stats")]
    [SerializeField] private TMP_Text statsText;

    public static CharacterSelector Instance;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Debug.Log("EXTRA " + this + " DELETED.");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static CharacterScriptableObject GetData()
    {
        return Instance.CharacterData;
    }

    public void SelectCharacter(CharacterScriptableObject character)
    {
        CharacterData = character;

        UpdateStatsText();
    }

    private void UpdateStatsText()
    {
        statsText.text =
                "Starting Weapon: " + CharacterData.StartingWeapon.name +
                "\n\nMax Health: " + CharacterData.MaxHealth.ToString() +
                "\n\nRecovery Rate: " + CharacterData.Recovery.ToString() +
                "\n\nMovement Speed: " + CharacterData.MovementSpeed.ToString() +
                "\n\nMight: " + CharacterData.MovementSpeed.ToString() +
                "\n\nProjectile Speed: " + CharacterData.ProjectileSpeed.ToString() +
                "\n\nMagnet Power: " + CharacterData.Magnet.ToString();
    }

    public void DestroySingleton()
    {
        Instance = null;
        Destroy(gameObject);
    }
}
