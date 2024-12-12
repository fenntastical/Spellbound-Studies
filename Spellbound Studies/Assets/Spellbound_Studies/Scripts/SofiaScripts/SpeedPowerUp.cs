using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpeedUpPowerUp : MonoBehaviour
{
    [SerializeField] private float speedBoost = 3f; 
    [SerializeField] private float duration = 10f; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.StartCoroutine(playerMovement.IncreaseSpeed(speedBoost, duration));
            }

            gameObject.SetActive(false);
        }
    }
}
