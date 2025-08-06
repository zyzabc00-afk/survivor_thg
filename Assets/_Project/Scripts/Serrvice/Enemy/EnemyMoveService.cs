using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveService : IEnemyMove
{
    private readonly float chaseDistance;

    public EnemyMoveService(float chaseDistance)
    {
        this.chaseDistance = chaseDistance;
    }

    public void MoveToTarget(Transform target, NavMeshAgent navMesh)
    {
        if (target == null || navMesh == null) return;

        float distanceToTarget = Vector3.Distance(navMesh.transform.position, target.position);

        if (distanceToTarget < chaseDistance)
        {
            navMesh.SetDestination(target.position);
        }
        else
        {
            StopMoving(navMesh);
        }
    }

    public void StopMoving(NavMeshAgent navMesh)
    {
        if (navMesh != null)
        {
            // navMesh.ResetPath();
        }
    }
}