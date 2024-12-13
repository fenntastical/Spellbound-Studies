using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class Callie : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;
    public UnityEvent<GameObject> OnHitWithReference;
    public KnockBackFeedback knockBackFeedback;

    public float maxHealth = 200f;
    [HideInInspector] public float health = 200f;


    //blocking mechanism
    public float blockDuration = 2f; // How long blocking lasts
    public float blockCooldown = 5f; // Cooldown before blocking again
    public float damageReductionFactor = 0.5f; // Reduces damage by 50%
    private bool isBlocking = false;
    private bool canBlock = true;
    private float blockTimer = 0f;
    private float cooldownTimer = 0f;

    // AI logic parameters
    public float blockTriggerHealthThreshold = 0.3f; // Block if health < 30% of maxHealth
    public float blockProbability = 0.5f; // 50% chance to block when triggered
    private bool playerIsAttacking = false; // Simulate player attack detection


    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
        }

        // Handle blocking timers
        if (isBlocking)
        {
            blockTimer -= Time.deltaTime;
            if (blockTimer <= 0f)
            {
                isBlocking = false;
                cooldownTimer = blockCooldown; // Start cooldown
            }
        }
        else if (!canBlock)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                canBlock = true; // Reset block availability
            }
        }
        if (canBlock && ShouldBlock())
        {
            StartBlocking();
        }

}
    private void FixedUpdate()
    {
        if (target && !isBlocking) // Disable movement while blocking
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero; // Stop movement while blocking
        }
    }
    public void TakeDamage(float damage)
    {
        if (isBlocking)
        {
            damage *= damageReductionFactor; // Reduce damage if blocking
        }
        health -= damage;
        AudioMgr.Instance.PlaySFX("Enemy Damage");
        if (health > 0)
        {
            knockBackFeedback.PlayFeedback();
        }

        //i think knockback would be called here 
        if (health <= 0)
        {

                SceneManager.LoadScene(4);
        }
    }

    private void StartBlocking()
    {
        isBlocking = true;
        canBlock = false;
        blockTimer = blockDuration;
    }

    private bool ShouldBlock()
    {
        // Trigger based on player attacking
        if (playerIsAttacking && Random.value < blockProbability)
        {
            return true;
        }

        // Trigger based on health threshold
        if (health / maxHealth < blockTriggerHealthThreshold && Random.value < blockProbability)
        {
            return true;
        }

        return false;
    }

    // Simulate player attack detection
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            playerIsAttacking = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            playerIsAttacking = false;
        }
    }
}