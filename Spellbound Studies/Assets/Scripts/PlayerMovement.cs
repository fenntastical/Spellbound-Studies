using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 moveInput;
    //public Animator animator;
    //public Transform Aim;

    //Vector2 movement;
    //private Vector2 lastMoveDirection;

    bool isWalking = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = moveInput * moveSpeed;
        ////input here
        //movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");

        //animator.SetFloat ("Horizontal", movement.x);
        //animator.SetFloat("Vertical", movement.y);
        //animator.SetFloat("Speed", movement.sqrMagnitude);

        //if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal")==-1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        //{
        //    isWalking = true;
        //    animator.SetFloat("Last_Move_X", Input.GetAxisRaw("Horizontal"));
        //    animator.SetFloat("Last_Move_Y", Input.GetAxisRaw("Vertical"));
        //}
        //else
        //{
        //    isWalking = false;
        //    lastMoveDirection = movement;
        //    Vector3 vector3 = Vector3.left * lastMoveDirection.x + Vector3.down * lastMoveDirection.y;
        //    Aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
        //}
    }

    public void Move(InputAction.CallbackContext context)
    {
     moveInput = context.ReadValue<Vector2>();
    }

    //void FixedUpdate()
    //{
    //    //actual movement here
    //    rb.MovePosition(rb.position + movement.normalized * BaseSpeed * Time.fixedDeltaTime);
    //    if (isWalking)
    //    {
    //        Vector3 vector3 = Vector3.left * movement.x + Vector3.down * movement.y;
    //        Aim.rotation = Quaternion.LookRotation(Vector3.forward,vector3);
    //    }
    //}
}
