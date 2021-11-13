using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    public Transform target;
    public Transform player;
    [SerializeField]
    GameObject p2;
    public State state;
    public bool arrivedAtTarget;

    RaycastHit hit;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        navMeshAgent.destination = target.position;
        arrivedAtTarget = navMeshAgent.remainingDistance < 1.5f;

        Vector3 rotation = p2.transform.position - transform.position;
        rotation = rotation.normalized;


        Physics.Raycast(transform.position, rotation, out hit, Mathf.Infinity);
        //Debug.Log(hit.transform);
    }
    private void OnDrawGizmos()
    {
        if (p2 != null  && hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject);
            if (hit.collider.gameObject == p2)
            {
                Gizmos.color = Color.blue;
            }
            else
            {
                Gizmos.color = Color.red;
            }

            Gizmos.DrawLine(p2.transform.position, transform.position);
        }

        
    }
}

public enum State { searching, idle, chasing, fleaing, attacking };