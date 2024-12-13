using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    [SerializeField] private Transform waypoint1;
    [SerializeField] private Transform waypoint2; 
    [SerializeField] private float moveSpeed = 2f; 
    [SerializeField] private float detectionRadius = 3f; 
    [SerializeField] private LayerMask playerLayer; 

    private Rigidbody2D rb;

    private Transform currentTarget; 
    private Animator animator; 
    private bool isAttacking = false; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentTarget = waypoint1; 
        animator = GetComponent<Animator>(); 
    }

    private void Update()
    {
        if (PlayerDetected())
        {
            BeginAttack(); 
        }
        else
        {
            Patrol(); 
        }
    }

    private void Patrol()
    {
        if (isAttacking) return; 

        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            currentTarget = currentTarget == waypoint1 ? waypoint2 : waypoint1;
        }

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        Vector2 direction = (currentTarget.position - transform.position).normalized;

        if (direction.x > 0)
        {
            animator.SetBool("isWalkingRight", true);
            animator.SetBool("isWalkingLeft", false);
        }
        else if (direction.x < 0)
        {
            animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingLeft", true);
        }
    }

    private bool PlayerDetected()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);
        return player != null;
    }

    private void BeginAttack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack"); 

        Debug.Log("Enemy is attacking!");
    }

    private void EndAttack()
    {
        isAttacking = false; 
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
