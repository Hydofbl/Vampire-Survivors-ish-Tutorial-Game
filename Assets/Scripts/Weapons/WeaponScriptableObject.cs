using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Object", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    public GameObject weaponPrefab;
    // Base stats for weapons
    public float damage;
    public float speed;
    public float cooldownDuration;
    public int pierce;
}
