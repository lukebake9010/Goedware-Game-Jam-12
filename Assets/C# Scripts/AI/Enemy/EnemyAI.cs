using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;

    protected void Awake()
    {
        if(agent == null) agent = GetComponent<NavMeshAgent>();
        LookAtPlayer();
    }


    public void SetDestination(Vector3 destination)
    {
        if (agent == null) return;
        agent.destination = destination;
    }

    public void LookAtPlayer()
    {
        if (agent == null) return;
        PlayerManager playerManager = PlayerManager.Instance;
        if(PlayerManager.Instance == null) return;
        Vector3 playerPosition = playerManager.gameObject.transform.position;
        Vector3 lookVector = playerPosition - gameObject.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(lookVector,Vector3.up);
        gameObject.transform.rotation = lookRotation;
    }

}
