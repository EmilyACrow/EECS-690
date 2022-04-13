using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SuitAI : MonoBehaviour, INavigation
{
    [SerializeField] private UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] private LayerMask groundLayer, playerLayer;
    [SerializeField] private Transform player;
    public Waypoint target;

    //States
    enum State {Patrol, Idle, Alerted, Attacking};
    private State currentState;

    //Patrolling
    public Vector3 walkPoint;
    private bool waypointSet, doneIdling;
    public float walkPointRange = 4.0f;
    public float distanceToPoint = 0.0f;

    // Attacking
    [SerializeField] public float rateOfFire;
    public bool alreadyAttacked;

    // States
    [SerializeField] public float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        currentState = State.Idle;
        doneIdling = true;
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if(!playerInAttackRange && !playerInSightRange) 
            if(doneIdling) Patrol();
        if(!playerInAttackRange && playerInSightRange)  Chase();
        if(playerInAttackRange && playerInSightRange)   Attack();   
        if(waypointSet) distanceToPoint = (transform.position - target.transform.position).magnitude;
    }

    private void Patrol() {
        if(!waypointSet) ChooseNewWaypoint();
        if (waypointSet) agent.SetDestination(target.transform.position);

        Vector3 distanceToWalkPoint = transform.position - target.transform.position;

        //If distance to next point is <1, we have arrived
        if (distanceToWalkPoint.magnitude < 1.5f && waypointSet) {
            currentState = State.Idle;
            waypointSet = false;
            doneIdling = false;
            StartCoroutine(IdleFor(2.0f));
        }
    }

    IEnumerator IdleFor(float seconds) {
        yield return new WaitForSeconds(seconds);   
        doneIdling = true;
    }

    private void ChooseNewWaypoint() {
        if(target.getNextWaypoint() == null) {
            target = target.getPrevWaypoint();
        } else {
            target = target.getNextWaypoint();
        }
        waypointSet = true;
        currentState = State.Patrol;
        
        //Check if the new point is actually ground
        // if(Physics.Raycast(newWalkPoint, -transform.up, 2f, groundLayer)) {
        //     walkPoint = newWalkPoint;
        //     waypointSet = true;
        // }
        // else {
        //     Debug.Log("Raycast failed");
        // }
    }

    private void Chase() {
        agent.SetDestination(player.position);
    }

    private void Attack() {
        //Stop moving
        agent.SetDestination(transform.position);

        //Look at player
        transform.LookAt(player);

        //Attack player
        if(!alreadyAttacked) {
            //Create bullet
            //Add spread to shot
            //Reset attack after a delay
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), rateOfFire);
        }
    }

    private void ResetAttack() {
        alreadyAttacked = false;
    }

    private void CreateBullet(Quaternion angle, float velocity, float lifetime) {

    }


    public void MoveToPoint(Vector3 point) {

    }
}
