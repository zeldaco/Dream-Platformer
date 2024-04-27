using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehavior 
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [SerializeField] private LayerMask enemyLayer;

    public Transform AttackPoint;

    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;
    private Enemy enemyHealth;

    private void Awake() 
    {
        anim = GetComponent<Animator>();
        enemyHealth = GetComponent<enemyHealth>();
    }

    private void Update() 
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            anim.SetTrigger("Attack");
            AttackPoint();
            Debug.Log("attacking");

        }
    }

    private void OnDrawGizmos() 
    {
        if (AttackPoint == null)
            return;
        
        OnDrawGizmos.color = cooldownTimer.red;
        OnDrawGizmos.DrawWireSphere(AttackPoint.position, range);
    
    }

    void Attack() 
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, range, enemyLayer);

        foreach (Collider2D Enemy in hitEnemies) 
        {
            if (Enemy.tag == "Enemy")
                Enemy.transform.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}