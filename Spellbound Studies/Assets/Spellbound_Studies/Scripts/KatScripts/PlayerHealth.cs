using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour 
{
    public int health;
    public int maxHealth = 10;
    public GameObject healthPanel;
    public GameMgr gameMgr;

    [Header("Damage Effect")]
    public float damageFlashDuration = 0.5f; // Adjustable in the Inspector

    private bool isDead = false;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        health = maxHealth;
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        // Additional initialization if needed
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        AudioMgr.Instance.PlaySFX("Damage");

        // Destroy health panel icons
        for(int i = 0; i <= amount - 1; i++)
        {
            Destroy(healthPanel.transform.GetChild(i).gameObject);
        }

        // Start the color change coroutine
        if (spriteRenderer != null)
        {
            StartCoroutine(FlashRedTemporarily());
        }

        if (health <= 0 && !isDead)
        {
            isDead = true;
            gameMgr.gameOver();
            gameObject.SetActive(false);
        }
    }

    private IEnumerator FlashRedTemporarily()
    {
        // Store the original color
        Color originalColor = spriteRenderer.color;

        // Change to red
        spriteRenderer.color = Color.red;

        // Wait for the inspector-defined duration
        yield return new WaitForSeconds(damageFlashDuration);

        // Return to original color
        spriteRenderer.color = originalColor;
    }
}