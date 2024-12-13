using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeEnemyBehavior : MonoBehaviour
{
   public int damage = 1; // Amount of damage dealt to the player

    [Header("Player Color Effect")]
    public float greenDuration = 30.0f; // Duration the player stays green
    public Color primaryGreenColor = Color.blue; // Main green color
    public Color secondaryGreenColor = new Color(32, 32, 200, 1); // Lighter green
    public float colorAlternateDuration = 0.1f; // How quickly colors alternate

    private GameObject player; // Reference to the player GameObject
    private PlayerHealth playerHealth; // Reference to the player's health component
    private SpriteRenderer spriteRenderer; // For flipping the sprite
    public KnockBackFeedback knockBackFeedback;
    [HideInInspector] public PlayerMovement playerMovement;
    private Rigidbody2D rb;
    private bool hitOnce = false;

    void Start()
    {
        // Find the player GameObject and PlayerHealth script
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
            playerMovement = player.GetComponent<PlayerMovement>();
            rb = player.GetComponent<Rigidbody2D>();
        }

        if (player == null || playerHealth == null)
        {
            Debug.LogError("Player or PlayerHealth component not found. Ensure the player has the correct tag and component.");
        }

        // Get the SpriteRenderer to flip the sushi
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on SushiEnemy.");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Deal damage to the player
            // if (playerHealth != null && hitOnce != false)
            // {
            //     playerHealth.TakeDamage(damage);
            //     Debug.Log("Player took damage from sushi.");
            // }

            // Change the player's color to green
            SpriteRenderer playerSprite = collision.gameObject.GetComponent<SpriteRenderer>();
            if (playerSprite != null && hitOnce == false)
            {
                StartCoroutine(ChangePlayerColor(playerSprite));
                hitOnce = true;
            }
        }
    }

    private IEnumerator ChangePlayerColor(SpriteRenderer playerSprite)
    {
        // Save the original color
        Color originalColor = Color.white;

        // Track total time
        float elapsedTime = 0f;
        bool isAlternatingColors = true;
        playerMovement.enabled = false;

        while (elapsedTime < greenDuration)
        {
            // Alternate between primary and secondary green colors
            playerSprite.color = isAlternatingColors ? primaryGreenColor : secondaryGreenColor;
            
            // Wait for a short duration
            yield return new WaitForSeconds(colorAlternateDuration);

            // Toggle between colors
            isAlternatingColors = !isAlternatingColors;
            elapsedTime += colorAlternateDuration;
        }

        playerMovement.enabled = true;

        // Reset the player's color to the original
        playerSprite.color = originalColor;
    }
}
