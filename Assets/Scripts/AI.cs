using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    //public Transform target;
    [SerializeField] GameObject player;
    //[SerializeField] GameObject moveTarget;
    public State state;
    public bool arrivedAtTarget;

    RaycastHit hit;
    Vector3 playerLastPos;

    bool checkPointsUptodate = false;
    List<DoorDoubleSlide> checkPoints = new List<DoorDoubleSlide>();
    DoorDoubleSlide lastCheckpoint;
    DoorDoubleSlide[] allCheckPoints;

    Vector3 lastPosition;
    int distanceCheckCount = 0;

    private void Awake()
    {
        lastPosition = transform.position;
        allCheckPoints = Object.FindObjectsOfType<DoorDoubleSlide>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        changeState(State.idle);
        changeState(State.searching);
        //int index = Random.Range(0,allCheckPoints.Length-1);
        //navMeshAgent.destination = allCheckPoints[index].transform.position;
        pickNewTarget();
    }


    private void Update()
    {
        if (player != null)
        {
            arrivedAtTarget = navMeshAgent.remainingDistance < 1.5f;

            if(distanceCheckCount > 60)
            {
                if(Vector3.Distance(lastPosition, transform.position) < .1)
                {
                    Debug.Log("AI Stuck");
                    changeState(State.idle);
                }
                lastPosition = transform.position;
                distanceCheckCount = 0;
            }
            distanceCheckCount++;

            Vector3 rotation = player.transform.position - transform.position;
            rotation = rotation.normalized;

            if (state == State.chasing)
            {
                Physics.Raycast(transform.position, rotation, out hit, Mathf.Infinity, 7);
            }
            else
            {
                Physics.Raycast(transform.position, rotation, out hit, Mathf.Infinity);
            }

            switch (state)
            {
                case State.searching:
                    
                    if (hit.collider != null && hit.collider.gameObject == player)
                    {
                        playerLastPos = player.transform.position;
                        changeState(State.chasing);
                        navMeshAgent.destination = player.transform.position;
                    }else if (arrivedAtTarget)
                    {
                        Debug.Log("Arrived");
                        changeState(State.idle);
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
                            changeState(State.idle);
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

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("We collided");
        //Debug.Log(collision.gameObject);
    }

        private void pickNewTarget()
    {

        Debug.Log("Pick new target");
        if (!checkPointsUptodate) {
            checkPoints.Clear();
            foreach (DoorDoubleSlide checkpoint in allCheckPoints)
            {
                if (lastCheckpoint == null || lastCheckpoint != checkpoint)
                {
                    Vector3 testpoint = checkpoint.transform.TransformPoint(checkpoint.GetComponent<BoxCollider>().center);
                    //Debug.Log("Checkpoint distance: " + Vector3.Distance(testpoint, transform.position));
                    if (Vector3.Distance(testpoint, transform.position) < 30 && Vector3.Distance(testpoint, transform.position) > 2)
                    {
                        //checkPoints.Add(checkpoint);
                        /*
                        Vector3 rotation = testpoint - transform.position;
                        rotation = rotation.normalized;


                        Physics.Raycast(transform.position, rotation, out hit, Mathf.Infinity);
                        Debug.Log(hit.collider.gameObject.tag);
                        if (hit.collider != null && hit.collider.gameObject.tag == "Checkpoint")
                        {
                        */
                            //Debug.Log("Valid Checkpoint");
                            NavMeshPath path = new NavMeshPath();
                            if (navMeshAgent.CalculatePath(testpoint, path))
                            {
                                if (path.status == NavMeshPathStatus.PathComplete)
                                {
                                    checkPoints.Add(checkpoint);
                                }
                            }
                        //}

                    }
                }
                
            }
            Debug.Log("Checkpoint Count: " + checkPoints.Count);
            int index = Random.Range(0, checkPoints.Count - 1);
            lastCheckpoint = checkPoints[index];
            navMeshAgent.destination = checkPoints[index].transform.TransformPoint(checkPoints[index].GetComponent<BoxCollider>().center);
            changeState(State.searching);
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

    private void changeState(State newState)
    {
        Debug.Log("New State: " + newState);
        state = newState;
    }
}

public enum State { searching, idle, chasing, fleaing, attacking };