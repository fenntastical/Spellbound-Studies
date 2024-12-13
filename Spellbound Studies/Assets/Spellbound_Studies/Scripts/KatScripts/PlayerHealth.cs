using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerHealth : MonoBehaviour 
{
    public int health;
    public int maxHealth = 10;
    public GameObject healthPanel;
    public GameMgr gameMgr;

    [Header("Damage Effect")]
    public float damageFlashDuration = 0.5f; // Duration of color flash
    public Color primaryDamageColor = Color.red; // Main damage color
    public Color secondaryDamageColor = new Color(1f, 0.5f, 0.5f); // Lighter red
    public float colorAlternateDuration = 0.1f; // How quickly colors alternate
    public Color primaryGreenColor = Color.green; // Main green color
    public Color secondaryGreenColor = new Color(0.5f, 1f, 0.5f); 
    public float greenDuration = 5f;

    private bool isDead = false;
    private SpriteRenderer spriteRenderer;
    [HideInInspector] public bool posion = false;
    private bool posionDone = false;


    void Awake()
    {
        health = maxHealth;
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        // Start the color alternation coroutine
        if (spriteRenderer != null && posion == false)
        {
            StartCoroutine(AlternateColorTemporarily());
        }

        if (health <= 0 && !isDead)
        {
            isDead = true;
            gameMgr.gameOver();
            gameObject.SetActive(false);
        }
    }


    void Update()
    {
        if(posion == true && posionDone == false)
        {
            StartCoroutine(bePosioned());
            posionDone = true;
        }
    }

    private IEnumerator AlternateColorTemporarily()
    {
        // Store the original color
        Color originalColor = Color.white;

        // Track total time
        float elapsedTime = 0f;
        bool isAlternatingColors = true;

        while (elapsedTime < damageFlashDuration)
        {
            // Alternate between primary and secondary colors
            spriteRenderer.color = isAlternatingColors ? primaryDamageColor : secondaryDamageColor;
            
            // Wait for a short duration
            yield return new WaitForSeconds(colorAlternateDuration);

            // Toggle between colors
            isAlternatingColors = !isAlternatingColors;
            elapsedTime += colorAlternateDuration;
        }

        // Return to original color
        spriteRenderer.color = originalColor;
    }

    private IEnumerator bePosioned()
    {
        // Save the original color
        Color originalColor = Color.white;

        // Track total time
        float elapsedTime = 0f;
        bool isAlternatingColors = true;

        // while(posion == true)
        // {
            while (elapsedTime < greenDuration)
            {
                // Alternate between primary and secondary green colors
                spriteRenderer.color = isAlternatingColors ? primaryGreenColor : secondaryGreenColor;
                
                // Wait for a short duration
                yield return new WaitForSeconds(colorAlternateDuration);

                // Toggle between colors
                isAlternatingColors = !isAlternatingColors;
                elapsedTime += colorAlternateDuration;
                // TakeDamage(1);
            }
            elapsedTime = 0f;
            TakeDamage(1);

        // }

        // Reset the player's color to the original
        spriteRenderer.color = originalColor;
        posionDone = false;
        posion = false;
    }
}