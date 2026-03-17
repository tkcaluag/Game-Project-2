using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Enemy", order = 1)]

public class EnemyData : ScriptableObject
{
    public int health;
    public int maxHealth;
    public int damage;
    public float speed;
    public int ExperiencePoints;

}
