using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public Animator animator;

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log(health.ToString());
        if(health <= 0f)
        {
            StartCoroutine(KillEnemy());
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }

    IEnumerator KillEnemy()
    {
        if (animator != null)
            animator.SetBool("isDead", true);

        yield return new WaitForSeconds(3.0f);
        Die();
    }
}
