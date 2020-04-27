using UnityEngine;
using UnityEngine.AI;

public class navAgent : MonoBehaviour
{
    [SerializeField] private Transform destination;
    private NavMeshAgent nevMeshAgent;

    private void Start()
    {
        nevMeshAgent = GetComponent<NavMeshAgent>();
        SetDestination();
    }

    private void SetDestination()
    {
        nevMeshAgent.SetDestination(destination.position);
    }
}
