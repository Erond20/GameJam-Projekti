
using UnityEngine;
using UnityEngine.AI;

public class AIScript : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public float healht;

    public LayerMask WhatIsGround, WhatIsPlayer;

    public Vector3 walkPoints;
    bool walkpointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool AlreadyAttacked;
    public GameObject projectile;

    public float sightrange, attackRange;
    public bool playerinsightRange, playerinAttackRange;

    private void Awake()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        playerinsightRange = Physics.CheckSphere(transform.position, sightrange, WhatIsPlayer);
        playerinAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatIsPlayer);

        if (!playerinsightRange && !playerinAttackRange) patrolling();
        if (playerinsightRange && !playerinAttackRange) ChasePlayer();
        if (playerinsightRange && playerinAttackRange) AttackPlayer();
    }

    private void patrolling()
    {
        if (walkpointSet) searchWalkPointSet();

        if (walkpointSet)
            agent.SetDestination(walkPoints);

        Vector3 distanceToWalkPoint = transform.position - walkPoints;

        if (distanceToWalkPoint.magnitude < 1f)
            walkpointSet = false;
    } 
    private void searchWalkPointSet()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoints = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoints, -transform.up, 2f, WhatIsGround))
            walkpointSet = true;
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);
        if (!AlreadyAttacked)
        {

            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            AlreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        AlreadyAttacked = false;
    }
    public void TakeDamage(int damage)
    {
        healht -= damage;

        if (healht <= 0) Invoke(nameof(DestroyEnemy), .5f);
            
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
