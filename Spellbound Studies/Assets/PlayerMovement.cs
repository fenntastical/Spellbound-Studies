using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float BaseSpeed = 5f;
    public Rigidbody2D rb;

    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        //input here
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        //actual movement here
        rb.MovePosition(rb.position + movement * BaseSpeed * Time.fixedDeltaTime);
    }
}
