using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Object", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    // Base stats for enemies
    [SerializeField]
    private float movementSpeed;
    public float MovementSpeed { get => movementSpeed; private set => movementSpeed = value; }

    [SerializeField]
    private float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }

    [SerializeField]
    private float damage;
    public float Damage { get => damage; private set => damage = value; }
}
