using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 10;
    public GameObject healthPanel;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(this.tag);
        health = maxHealth;
    }
    void Update()
    {
        // print(health);
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        Destroy(healthPanel.transform.GetChild(0).gameObject);
        if (health <= 0)
        {
            Destroy(gameObject);    
        }
    }
}
