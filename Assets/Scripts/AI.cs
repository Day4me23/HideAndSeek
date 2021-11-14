using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    public Transform target;
    [SerializeField] GameObject player;
    [SerializeField] GameObject moveTarget;
    public State state;
    public bool arrivedAtTarget;

    RaycastHit hit;
    Vector3 playerLastPos;

    bool checkPointsUptodate = false;
    List<DoorDoubleSlide> checkPoints = new List<DoorDoubleSlide>();
    DoorDoubleSlide[] allCheckPoints;

    private void Awake()
    {
        allCheckPoints = Object.FindObjectsOfType<DoorDoubleSlide>();
        foreach(DoorDoubleSlide cp in allCheckPoints)
        {
            Debug.Log(cp);
            Debug.Log(cp.transform.position);
        }
        navMeshAgent = GetComponent<NavMeshAgent>();
        state = State.idle;
        state = State.searching;
        int index = Random.Range(0,allCheckPoints.Length-1);
        navMeshAgent.destination = allCheckPoints[index].transform.position;

    }


    private void Update()
    {
        if (player != null)
        {
            arrivedAtTarget = navMeshAgent.remainingDistance < 1.5f;

            Vector3 rotation = player.transform.position - transform.position;
            rotation = rotation.normalized;


            Physics.Raycast(transform.position, rotation, out hit, Mathf.Infinity);

            switch (state)
            {
                case State.searching:
                    if (hit.collider != null && hit.collider.gameObject == player)
                    {
                        playerLastPos = player.transform.position;
                        state = State.chasing;
                        navMeshAgent.destination = player.transform.position;
                    }else if (arrivedAtTarget)
                    {
                        Debug.Log("Arrived");
                        state = State.idle;
                    }
                    break;
                case State.chasing:
                    if (hit.collider != null && hit.collider.gameObject == player)
                    {
                        navMeshAgent.destination = player.transform.position;
                        playerLastPos = player.transform.position;
                    }
                    else
                    {
                        navMeshAgent.destination = playerLastPos;
                        if (arrivedAtTarget)
                        {
                            state = State.idle;
                        }
                    }
                    break;
                case State.idle:
                    pickNewTarget();
                    break;
            }
        }



        //Debug.Log(hit.transform);
    }

    private void pickNewTarget()
    {
        

        if (!checkPointsUptodate) {
            checkPoints.Clear();
            foreach (DoorDoubleSlide checkpoint in allCheckPoints)
            {
                
                if (Vector3.Distance(checkpoint.transform.position, transform.position) < 30 && Vector3.Distance(checkpoint.transform.position, transform.position) > 2)
                {
                    checkPoints.Add(checkpoint);
                    /*
                    NavMeshPath path = new NavMeshPath();
                    if (navMeshAgent.CalculatePath(checkpoint.transform.position, path)){
                        if (path.status == NavMeshPathStatus.PathComplete)
                        {
                            checkPoints.Add(checkpoint);
                        }
                    }
                    */
                }
                
            }
            int index = Random.Range(0, checkPoints.Count - 1);
            navMeshAgent.destination = checkPoints[index].transform.position;
            state = State.searching;
        }
        else
        {

        }
    }

    private void OnDrawGizmos()
    {
        if (player != null  && hit.collider != null)
        {
            //Debug.Log(hit.collider.gameObject);
            if (hit.collider.gameObject == player)
            {
                Gizmos.color = Color.blue;
            }
            else
            {
                Gizmos.color = Color.red;
            }

            Gizmos.DrawLine(player.transform.position, transform.position);
        }

        
    }
}

public enum State { searching, idle, chasing, fleaing, attacking };