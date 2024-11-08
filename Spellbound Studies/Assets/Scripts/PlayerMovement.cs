using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float BaseSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        //input here
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat ("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal")==-1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            animator.SetFloat("Last_Move_X", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("Last_Move_Y", Input.GetAxisRaw("Vertical"));
        }
    }

    void FixedUpdate()
    {
        //actual movement here
        rb.MovePosition(rb.position + movement.normalized * BaseSpeed * Time.fixedDeltaTime);
    }
}
