using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SuitAI : MonoBehaviour, INavigation
{
    [SerializeField] private UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] private LayerMask groundLayer, playerLayer;
    [SerializeField] private Transform player;

    //Patrolling
    public Vector3 walkPoint;
    private bool walkPointSet, idling;
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
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if(!playerInAttackRange && !playerInSightRange) Patrol();
        if(!playerInAttackRange && playerInSightRange)  Chase();
        if(playerInAttackRange && playerInSightRange)   Attack();   
        if(walkPointSet) distanceToPoint = (transform.position - walkPoint).magnitude;
    }

    private void Patrol() {
        if(!walkPointSet) ChooseNewWalkPoint();
        if (walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //If distance to next point is <1, we have arrived
        if (distanceToWalkPoint.magnitude < 1f && walkPointSet) {
            walkPointSet = false;
        }
    }

    private void Idle() {

    }

    private void ChooseNewWalkPoint() {
        float newX = transform.position.x + Random.Range(-walkPointRange, walkPointRange);
        float newZ = transform.position.z + Random.Range(-walkPointRange, walkPointRange);
        Vector3 newWalkPoint = new Vector3(newX, transform.position.y, newZ);
        
        //Check if the new point is actually ground
        if(Physics.Raycast(newWalkPoint, -transform.up, 2f, groundLayer)) {
            walkPoint = newWalkPoint;
            walkPointSet = true;
        }
         else {
             Debug.Log("Raycast failed");
         }
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
