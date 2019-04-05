using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    NavMeshAgent nm;
    public Transform target;
    public enum ZombieState { idle, chasing };
    public float zombieArea = 10f;
    public Animator animator; 

    public ZombieState state = ZombieState.idle;

    void Start()
    {
        nm = GetComponent<NavMeshAgent>();
        StartCoroutine(UseBrain());
    }

    void Update()
    {

    }

    IEnumerator UseBrain()
    {
        while (true)
        {
            switch (state)
            {
                case ZombieState.idle:
                    float dist = Vector3.Distance(target.position, transform.position);
                    if (dist < zombieArea && animator != null)
                    {
                        state = ZombieState.chasing;
                        animator.SetBool("isChasing", true);
                    }
                    nm.SetDestination(transform.position);
                    break;
                case ZombieState.chasing:
                    dist = Vector3.Distance(target.position, transform.position);
                    if (dist > zombieArea && animator != null)
                    {
                        state = ZombieState.idle;
                        animator.SetBool("isChasing", false);
                    }
                    nm.SetDestination(target.position);
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}
