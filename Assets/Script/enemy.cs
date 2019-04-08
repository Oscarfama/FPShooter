using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    NavMeshAgent nm;
    public Transform target;
    public enum ZombieState { idle, chasing, attack };
    public float zombieArea = 10f;
    public float AreaToAttack = 2f;
    public Animator animator;
    public int Damage = 1;
    public AudioSource[] sounds;

    public ZombieState state = ZombieState.idle;

    void Start()
    {
        nm = GetComponent<NavMeshAgent>();
        StartCoroutine(Think());
    }

    void Update()
    {

    }

    IEnumerator Think()
    {
        while (true)
        {
            float dist = Vector3.Distance(target.position, transform.position);
            switch (state)
            {
                case ZombieState.idle:
                    sounds[0].Stop();
                    sounds[1].Stop();
                    if (dist < zombieArea && animator != null)
                    {
                        state = ZombieState.chasing;
                        animator.SetBool("isChasing", true);
                    }
                    else if (dist < AreaToAttack && animator != null)
                    {
                        state = ZombieState.attack;
                        animator.SetBool("isAttacking", true);
                    }
                    nm.SetDestination(transform.position);
                    break;
                case ZombieState.chasing:
                   sounds[0].Play();
                    if (dist > zombieArea && animator != null)
                    {
                        state = ZombieState.idle;
                        animator.SetBool("isChasing", false);
                    }
                    else if (dist < AreaToAttack && animator != null)
                    {
                        sounds[0].Stop();
                        state = ZombieState.attack;
                        animator.SetBool("isAttacking", true);
                    }
                    nm.SetDestination(target.position);
                    break;
                case ZombieState.attack:
                    sounds[1].Play();
                    yield return new WaitForSeconds(1.5f);
                    HealthManager.health -= Damage;
                    if (dist > AreaToAttack && dist < zombieArea && animator != null)
                    {
                        state = ZombieState.chasing;
                        sounds[1].Stop();
                        animator.SetBool("isChasing", true);
                        animator.SetBool("isAttacking", false);
                    }
                    else if (dist < AreaToAttack && animator != null)
                    {
                        state = ZombieState.attack;
                        animator.SetBool("isChasing", false);
                        animator.SetBool("isAttacking", true);
                    }
                    nm.SetDestination(transform.position);
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(0f);
        }
    }
}
