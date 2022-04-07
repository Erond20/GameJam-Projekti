using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;   
using UnityEngine.UI;   

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    bool alreadyAttacked;
    public float attackForce;
    public Transform firePoint;
    public GameObject projectile;
    public float timeBetweenAttacks;
    public float WanderSpeed = 4f;
    public float chaseSpeed = 7f;
    private Animator animator;
    public float enemyCooldown = 1;
    public float damage = 1;

    private bool playerInRange = false;
    private bool canAttack = true;
    private bool Attacking = false;

    private bool isAware = false;
    public float health;
    public float maxHealth;
    public GameObject healthBarUI;
    public Slider slider;


    public GameObject Player;
    public float Distance;

    public Transform player;

    public bool isAngered;
    public bool caughtPlayer = false;

    public NavMeshAgent _agent;


    Vector3 playerPosition = Vector3.zero;
    Vector3 m_playerposition;
    void Start()
    {
        playerPosition = Vector3.zero;
        health = maxHealth;
        slider.value = calculateHealth();
        animator = GetComponentInChildren<Animator>();
    }
   
    // Update is called once per frame
    void Update()
    {
        if (playerInRange && canAttack)
        {
            StartCoroutine(AttackCooldown());
        }
        if(isAngered)
        {
            animator.SetBool("Aware", true);
            agent.speed = chaseSpeed;
        }
        else
        {
            animator.SetBool("Aware", false);
            agent.speed = WanderSpeed;
        }
        slider.value = calculateHealth();

        if(health <maxHealth)
        {
            healthBarUI.SetActive(true);
        }
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        if(health > maxHealth)
        {
            health = maxHealth;
        }

        Distance = Vector3.Distance(Player.transform.position, this.transform.position);

        if(Distance <=5)
        {
            isAngered = true;
        }
        if(Distance > 5f)
        {
            isAngered = false;
        }

        if(isAngered)
        {
            _agent.isStopped = false;

            _agent.SetDestination(Player.transform.position);
        }
        if(!isAngered)
        {
            _agent.isStopped = true;
        }

    }
       float calculateHealth()
    {
        return health / maxHealth;
    }
    public void TakeDamage(int Damage)
    {
        health -= Damage;
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);
        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * attackForce, ForceMode.Impulse);
            //rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

   
   

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            playerInRange = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            playerInRange = false;
        }
    }
    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(enemyCooldown);
        canAttack = true;
    }

}
