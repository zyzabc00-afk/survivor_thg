using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig", order = 1)]
public class EnemyAI:ScriptableObject
{
    public float chaseDistance = 15f;
    public float moveSpeed = 1f;
}