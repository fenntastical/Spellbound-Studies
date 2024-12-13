using System.Collections;
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
    private Rigidbody2D playerRb; // Reference to the player's Rigidbody2D
    private bool hitOnce = false;
    [HideInInspector] public playerCombat weapon;
    [HideInInspector] public GameObject aim;

    void Start()
    {
        // Find the player GameObject and PlayerHealth script
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
            playerMovement = player.GetComponent<PlayerMovement>();
            playerRb = player.GetComponent<Rigidbody2D>();
            weapon = player.GetComponent<playerCombat>();
            aim = GameObject.Find("SwordParent");
        }

        if (player == null || playerHealth == null)
        {
            Debug.LogError("Player or PlayerHealth component not found. Ensure the player has the correct tag and component.");
        }

        // Get the SpriteRenderer to flip the sushi
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on FreezeEnemy.");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hitOnce)
        {
            hitOnce = true; // Prevent multiple hits

            // Deal damage to the player
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Player took damage from freeze enemy.");
            }

            // Stop player's movement immediately
            if (playerRb != null)
            {
                playerRb.velocity = Vector2.zero; // Reset velocity
                playerRb.angularVelocity = 0f; // Reset angular velocity (if any)
            }


            // Change the player's color to green and freeze movement
            SpriteRenderer playerSprite = collision.gameObject.GetComponent<SpriteRenderer>();
            if (playerSprite != null)
            {
                StartCoroutine(ChangePlayerColor(playerSprite));
            }
        }
    }

    private IEnumerator ChangePlayerColor(SpriteRenderer playerSprite)
    {
        // Save the original color
        Color originalColor = playerSprite.color;

        // Disable player movement
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
            weapon.enabled = false;
            aim.SetActive(false);
        }

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

        // Reset player's movement and color
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
            weapon.enabled = true;
            aim.SetActive(true);
        }
        playerSprite.color = originalColor;

        // Allow the enemy to hit again (if required)
        hitOnce = false;
    }
}
