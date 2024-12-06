using UnityEngine;

public class SpeedUpPowerUp : MonoBehaviour
{
    [SerializeField] private float speedBoost = 3f; 
    [SerializeField] private float duration = 10f; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            KatScripts.PlayerMovement playerMovement = collision.GetComponent<KatScripts.PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.StartCoroutine(playerMovement.IncreaseSpeed(speedBoost, duration));
            }

            Destroy(gameObject);
        }
    }
}
