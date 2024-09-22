using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;

    private void Awake()
    {
        if(agent == null) agent = GetComponent<NavMeshAgent>();
    }


    public void SetDestination(Vector3 destination)
    {
        if (agent == null) return;
        agent.destination = destination;
    }

}
