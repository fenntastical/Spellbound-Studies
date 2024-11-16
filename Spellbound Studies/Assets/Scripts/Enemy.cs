using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;

    float health, maxHealth = 3f;

//    public PlayerHealth playerHealth;
//    public int damage = 2;
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
            Vector2 direction = (target.position-transform.position).normalized;
            moveDirection = direction;
        }
        
    }
    private void FixedUpdate()
    {
        if (target)
        {
            rb.velocity = new Vector2 (moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
 //   private void OnCollisionEnter2D(Collision2D collision)
   // {
    //    if(collision.gameObject.tag == "Player")
    //    {
    //        playerHealth.TakeDamage(damage);
   //     }
  //  }
}
