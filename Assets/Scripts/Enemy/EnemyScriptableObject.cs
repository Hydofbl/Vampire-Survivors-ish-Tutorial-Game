using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Object", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    // Base stats for enemies
    public float MovementSpeed;
    public float MaxHealth;
    public float Damage;
}
