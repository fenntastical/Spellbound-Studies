using UnityEngine;

public class SushiMovement : MonoBehaviour
{
    public float speed = 2.0f; 
    private Transform player; 
    private SpriteRenderer spriteRenderer; 

    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        // Get the SpriteRenderer to flip the sushi
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on SushiEnemy.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            if (direction.x > 0) 
            {
                spriteRenderer.flipX = true;
            }
            else if (direction.x < 0) 
            {
                spriteRenderer.flipX = false;
            }
        }
    }
}
