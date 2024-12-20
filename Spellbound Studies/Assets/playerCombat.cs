using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour
{
    public GameObject Melee;
    //bool isAttacking = false;
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public float attackDamage = 40;
    private float timer;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (timer > .5)
            {
                timer = 0;
                Attack();
                AudioMgr.Instance.PlaySFX("Attack");
            }
            // Attack();
            // AudioMgr.Instance.PlaySFX("Attack");
        }
    }
    void Attack()
    {
        //plays an attack animation
        animator.SetTrigger("Attack");
        //detects enemies within range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //damages enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            Melee.SetActive(false);
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public IEnumerator IncreaseAttack(float attackBoost, float duration)
    {
        attackDamage += attackBoost; // Increase the attack
        Debug.Log($"Speed increased to {attackDamage} for {duration} seconds!");

        yield return new WaitForSeconds(duration); // Wait for the duration

        attackDamage -= attackBoost; // Revert speed
        Debug.Log($"Speed returned to {attackDamage}");
    }
}
