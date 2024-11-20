using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour
{
    public Animator animator;
    public GameObject weapon;
    // Update is called once per frame
    void Start()
    {
        weapon = GameObject.FindWithTag("Weapon");
        weapon.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }

    void Attack()
    {
        //plays an attack animation
        weapon.SetActive(true);
        animator.SetTrigger("Attack");

        //detects enemies within range of attack
        //damages enemies
    }
}
