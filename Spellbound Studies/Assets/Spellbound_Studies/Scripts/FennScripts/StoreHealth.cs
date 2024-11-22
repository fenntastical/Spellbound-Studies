using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreHealth : MonoBehaviour
{
    string healthKey = "health";
    public int CurrentHealth {get; set;}

    private void Awake()
    {
        CurrentHealth = PlayerPrefs.GetInt(healthKey);
        DontDestroyOnLoad(gameObject);

    }

    public void SetHealth(int health)
    {
        PlayerPrefs.SetInt(healthKey, health);
    }

}
