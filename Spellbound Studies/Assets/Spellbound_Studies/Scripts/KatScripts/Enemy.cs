using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class Enemy : MonoBehaviour
{
    public float moveSpeed = 1f;
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;
    public UnityEvent<GameObject> OnHitWithReference;
    public KnockBackFeedback knockBackFeedback;

    public float maxHealth = 200f;
    [HideInInspector] public float health = 200f;

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
        AudioMgr.Instance.PlaySFX("Enemy Damage");
        if (health > 0)
        {
            knockBackFeedback.PlayFeedback();
        }

            //i think knockback would be called here 
            if (health <= 0)
        {
            if(gameObject.tag == "Lilith")
                SceneManager.LoadScene(4);
            if(gameObject.tag == "Callie")
                SceneManager.LoadScene(11);
            else
                Destroy(gameObject);
        }
    }
}
