using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Object", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField]
    private GameObject weaponPrefab;
    public GameObject WeaponPrefab { get => weaponPrefab; private set => weaponPrefab = value; }

    // Base stats for weapons
    [SerializeField]
    public float damage;
    public float Damage { get => damage; private set => damage = value; }

    [SerializeField]
    public float speed;
    public float Speed { get => speed; private set => speed = value; }

    [SerializeField]
    public float cooldownDuration;
    public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value; }

    [SerializeField]
    public int pierce;
    public int Pierce { get => pierce; private set => pierce = value; }
}
