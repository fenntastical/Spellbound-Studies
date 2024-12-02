using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 10;
    public GameObject healthPanel;

    public GameMgr gameMgr;
    

    private bool isDead = false;
    // Start is called before the first frame update
    void Awake()
    {
        health = maxHealth;
    }
    void Start()
    {
        // DontDestroyOnLoad(gameObject);
        // health = maxHealth;
    }
    void Update()
    {
        // print(health);
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        AudioMgr.Instance.PlaySFX("Damage");
        Destroy(healthPanel.transform.GetChild(0).gameObject);
        if (health <= 0 && !isDead )
        {
            isDead = true;
            gameMgr.gameOver();
            gameObject.SetActive(false);    
        }
    }
}
