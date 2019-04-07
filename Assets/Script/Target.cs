using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public Animator animator;
    public int scoreValue = 10;

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
        ScoreManager.score += scoreValue;
        Destroy(gameObject);
    }

    IEnumerator KillEnemy()
    {
        if (animator != null)
            animator.SetBool("isDeath", true);
        yield return new WaitForSeconds(0.5f);
        Die();
    }
}
