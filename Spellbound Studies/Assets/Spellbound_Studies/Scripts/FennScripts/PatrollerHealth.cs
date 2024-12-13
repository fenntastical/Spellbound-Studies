using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PatrollerHealth : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public PatrollerMovement enemy;
    public float maxHealth;
    public float health;
    private float lerpSpeed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthSlider.value != enemy.health)
        {
            healthSlider.value = enemy.health;
        }
        
        if(healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, health, lerpSpeed);
        }
    }
}
