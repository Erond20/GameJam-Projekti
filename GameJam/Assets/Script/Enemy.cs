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

    public float health;
    public float maxHealth;
    public GameObject healthBarUI;
    public Slider slider;
    public float WanderSpeed = 4f;
    public float chaseSpeed = 7f;
    private Animator animator;


    public GameObject Player;
    public float Distance;

    public Transform player;

    public bool isAngered;


    public NavMeshAgent _agent;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        slider.value = calculateHealth();
        animator = GetComponentInChildren<Animator>();
    }
   
    // Update is called once per frame
    void Update()   
    {
        if (isAngered && Distance < 0)
        {
            AttackPlayer();
        }


        if (isAngered)
        {
            animator.SetBool("Aware", true);
            agent.speed = WanderSpeed;
        }
        else
        {
            animator.SetBool("Aware", false);
            agent.speed = chaseSpeed;
        }

        slider.value = calculateHealth();

        if(health <maxHealth)
        {

            healthBarUI.SetActive(true);
        }
        if(health <= 0)
        {
            Destroy(this.gameObject, 4f);
            animator.SetBool("IsDead", true);
        }
        if(health > maxHealth)
        {
            health = maxHealth;
        }

        Distance = Vector3.Distance(Player.transform.position, this.transform.position);

        if(Distance <=15)
        {
            isAngered = true;

        }
        if(Distance > 15f)
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

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
