using Systems.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehavior 
{
    [SerializeField] private int startingHealth;

    public int currentHealth;
    public Animator anim;

    void Start() 
    {
        currentHealth = startingHealth; 
    }

    public void TakeDamage(int _damage) 
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            // hurt animation
            // invulnerability
        }
        else 
        {
            // die annimation
            GetComponentInParent<EnemyPatrol>().enabled = false;
            GetComponent<EnemyMelee>().enabled = false;
        }

    }

    void Die() 
    {
        if (currentHealth <= 0)
        {
            Debug.Log("Enemy Dead!");
            Destroy(gameObject);
            //die animation
        }
    }
    
}