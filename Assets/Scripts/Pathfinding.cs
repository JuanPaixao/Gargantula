using UnityEngine;
using UnityEngine.AI;

public abstract class Pathfinding : MonoBehaviour
{
    Vector3 _pathDestination;
    NavMeshAgent _navMeshAgent;

    // Use this for initialization
    public void FollowTransform(Vector3 destination)
    {
        _pathDestination = destination;
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        if (_navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent not attached to " + gameObject.name);
        }
        else
        {
            SetDestination();
        }
    }

    public void SetDestination()
    {
        _navMeshAgent.SetDestination(_pathDestination);
    }
}
