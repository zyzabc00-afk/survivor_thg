using UnityEngine;
using UnityEngine.AI;

public interface IEnemyMove
{
    void MoveToTarget(Transform transform, NavMeshAgent navMesh);
    void StopMoving(NavMeshAgent navMesh);
}