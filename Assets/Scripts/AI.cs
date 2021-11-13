using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    public Transform target;
    public Transform player;
    public State state;
    public bool arrivedAtTarget;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        navMeshAgent.destination = target.position;
        arrivedAtTarget = navMeshAgent.remainingDistance < 1.5f;

        Vector3 rotation = player.position - transform.position;
        rotation = rotation.normalized;


        RaycastHit hit;
        Physics.Raycast(transform.position, rotation, out hit, Mathf.Infinity);
        Debug.Log(hit.transform);
    }
    private void OnDrawGizmos()
    {
        if (player != null)
        Gizmos.DrawLine(player.position, transform.position);
    }
}

public enum State { searching, idle, chasing, fleaing, attacking };