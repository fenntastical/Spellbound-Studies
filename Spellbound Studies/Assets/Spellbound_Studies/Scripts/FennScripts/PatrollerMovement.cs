using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollerMovement : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;
    public PlayerHealth playerHealth;
    private GameObject player;
    public int damage = 1;

    public float health = 60f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        anim.SetBool("isWalkingRight", true);
        player = GameObject.FindWithTag("Player");
        if(player != null)
            playerHealth = player.GetComponent<PlayerHealth>();
        health = 60f;
            
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) <1f && currentPoint == pointB.transform)
        {
            anim.SetBool("isWalkingLeft", true);
            anim.SetBool("isWalkingRight", false);
            currentPoint = pointA.transform;
            // speed = -speed;
            
        }
        if (Vector2.Distance(transform.position, currentPoint.position) <1f && currentPoint == pointA.transform)
        {
            anim.SetBool("isWalkingRight", true);
            anim.SetBool("isWalkingLeft", false);
            currentPoint = pointB.transform;
            // speed = -speed;
        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.y *= -1;
        transform.localScale = localScale;
    }

    IEnumerator attackWaiter()
    {

        yield return new WaitForSeconds(2);

    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.tag == "Player"){
            playerHealth.TakeDamage(damage); 
            attackWaiter();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        AudioMgr.Instance.PlaySFX("Enemy Damage");
        if (health > 0)
        {

        }
            //i think knockback would be called here 
        if (health <= 0)
        {         
            Destroy(gameObject);
        }
    }
    
}
