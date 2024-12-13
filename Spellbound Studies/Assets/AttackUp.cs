using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AttackUp : MonoBehaviour
{
    [SerializeField] private float attackBoost = 10f;
    [SerializeField] private float duration = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerCombat playerCombat = collision.GetComponent<playerCombat>();
            if (playerCombat != null)
            {
                playerCombat.StartCoroutine(playerCombat.IncreaseAttack(attackBoost, duration));
            }

            gameObject.SetActive(false);
        }
    }
}
