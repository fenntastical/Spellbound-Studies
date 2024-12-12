using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SushiEnemy : MonoBehaviour 
{
    public int damage = 1; // Amount of damage dealt to the player
    public float maxHealth = 3f; // Maximum health of the sushi enemy
    [HideInInspector] public float health; // Current health of the enemy

    [Header("Player Color Effect")]
    public float greenDuration = 30.0f; // Duration the player stays green
    public Color primaryGreenColor = Color.green; // Main green color
    public Color secondaryGreenColor = new Color(0.5f, 1f, 0.5f); // Lighter green
    public float colorAlternateDuration = 0.1f; // How quickly colors alternate

    private GameObject player; // Reference to the player GameObject
    private PlayerHealth playerHealth; // Reference to the player's health component
    private SpriteRenderer spriteRenderer; // For flipping the sprite
    public KnockBackFeedback knockBackFeedback;

    void Start()
    {
        // Initialize health to max health
        health = maxHealth;

        // Find the player GameObject and PlayerHealth script
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
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
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Player took damage from sushi.");
            }

            // Change the player's color to green
            SpriteRenderer playerSprite = collision.gameObject.GetComponent<SpriteRenderer>();
            if (playerSprite != null)
            {
                StartCoroutine(ChangePlayerColor(playerSprite));
            }
        }
        else if (collision.gameObject.CompareTag("Lilith"))
        {
            // If hit by Lilith, take damage
            TakeDamage(1);
        }
    }

    public void TakeDamage(float damage)
    {
        // Reduce health
        health -= damage;

        // Play damage sound
        AudioMgr.Instance.PlaySFX("Enemy Damage");

        // Play knockback feedback if still alive
        if (health > 0)
        {
            knockBackFeedback.PlayFeedback();
        }

        // Check if enemy is dead
        if (health <= 0)
        {
            if (gameObject.tag == "Lilith")
                SceneManager.LoadScene(4);
            else
                Destroy(gameObject);
        }
    }

    private IEnumerator ChangePlayerColor(SpriteRenderer playerSprite)
    {
        // Save the original color
        Color originalColor = playerSprite.color;

        // Track total time
        float elapsedTime = 0f;
        bool isAlternatingColors = true;

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

        // Reset the player's color to the original
        playerSprite.color = originalColor;
    }
}