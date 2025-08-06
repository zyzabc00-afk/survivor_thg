using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveController : MonoBehaviour
{
    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private Transform target;

    private NavMeshAgent navAgent;
    private IEnemyMove iEnemyMove;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();

        if (navAgent == null)
        {
            Debug.LogError("null nav agent");
        }

        if (enemyAI == null)
        {
            Debug.LogError("config enemy ai null");
        }

        iEnemyMove = new EnemyMoveService(enemyAI.chaseDistance);

        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (navAgent != null) navAgent.speed = enemyAI.moveSpeed;
    }

    void Update()
    {
        if (target == null || navAgent == null) return;

        iEnemyMove.MoveToTarget(target, navAgent);

        var pos = transform.rotation;
        pos.x = 0;
        pos.y = 0;
        pos.z = 0;
        transform.rotation = pos;
    }

    void OnDestroy()
    {
        iEnemyMove.StopMoving(navAgent);
    }
}