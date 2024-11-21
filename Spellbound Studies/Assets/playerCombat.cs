using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour
{
    public Animator animator;
    private GameObject weapon;
    // Update is called once per frame
    // void Start()
    // {
    //     weapon = GameObject.FindWithTag("Weapon");
    //     weapon.SetActive(false);
    // }
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Mouse0))
    //     {
    //         Attack();
    //     }
    // }

    // void Attack()
    // {
    //     //plays an attack animation
    //     // weapon.SetActive(true);
    //     animator.SetTrigger("Attack");

    //     //detects enemies within range of attack
    //     //damages enemies
    // }
     //public GameObject Melee;
    bool isAttacking = false;
    float atkDuration = 0.3f;
    float atkTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnAttack();
        }
    }

    void OnAttack()
    {
        if(!isAttacking)
        {
            // Melee.SetActive(true);
            // isAttacking = true;
            animator.SetTrigger("Attack");
        }
    }
    void CheckMeleeTimer()
    {
        atkTimer += Time.deltaTime;
        if(atkTimer >= atkDuration)
        {
            atkTimer = 0;
        }
    }
}
