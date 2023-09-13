using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float lookRadius = 12f;
    public GameObject player;
    Transform target;
    NavMeshAgent agent;
    public CharacterStats targetStats;
    public float health = 1000f;
    public GameObject destroyed;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = player.transform;
        targetStats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        float playerDistance = Vector3.Distance(target.position, transform.position);

        if(playerDistance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if(playerDistance <= agent.stoppingDistance)
            {
                FaceTarget();
                Attack();
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookAt = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAt, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void Attack()
    {
        if(targetStats != null)
        {
            targetStats.TakeHit(20);
        }
        
    }

    public void TakeDamage (float amount)
    {
        health -= amount;
        if(health <= 0f)
        {
            Death();
        }
    }

     public void Death()
     {
        Instantiate(destroyed, transform.position, transform.rotation);
        Destroy(gameObject);
     }
}
