using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    NavMeshAgent nm;
    public Transform target;
    public enum RobotStates { idle, chasing, attack };
    public float zombieArea = 10f;
    public float AreaToAttack = 2f;
    public Animator animator;
    public int Damage = 1;
    public AudioSource[] sounds;
    private bool flagstate = true;

    public RobotStates state = RobotStates.idle;

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
        while (flagstate)
        {
            if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Dead") == true && animator == null)
            {
                flagstate = false;
            }
            float dist = Vector3.Distance(target.position, transform.position);
            switch (state)
            {
                case RobotStates.idle:
                    sounds[0].Stop();
                    sounds[1].Stop();
                    if (dist < zombieArea && animator != null)
                    {
                        state = RobotStates.chasing;
                        animator.SetBool("Chasing", true);
                    }
                    else if (dist < AreaToAttack && animator != null)
                    {
                        state = RobotStates.attack;
                        animator.SetBool("Attack", true);
                    }
                    nm.SetDestination(transform.position);
                    break;
                case RobotStates.chasing:
                   sounds[0].Play();
                    if (dist > zombieArea && animator != null)
                    {
                        state = RobotStates.idle;
                        animator.SetBool("Chasing", false);
                    }
                    else if (dist < AreaToAttack && animator != null)
                    {
                        sounds[0].Stop();
                        state = RobotStates.attack;
                        animator.SetBool("Attack", true);
                    }
                    nm.SetDestination(target.position);
                    break;
                case RobotStates.attack:
                    sounds[1].Play();
                    yield return new WaitForSeconds(1.3f);
                    HealthManager.health -= Damage;
                    if (dist > AreaToAttack && dist < zombieArea && animator != null)
                    {
                        state = RobotStates.chasing;
                        sounds[1].Stop();
                        animator.SetBool("Chasing", true);
                        animator.SetBool("Attack", false);
                    }
                    else if (dist < AreaToAttack && animator != null)
                    {
                        state = RobotStates.attack;
                        animator.SetBool("Chasing", false);
                        animator.SetBool("Attack", true);
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
