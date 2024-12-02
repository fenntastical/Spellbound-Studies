using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 moveInput;
    public Animator animator;
    public Transform Aim;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = moveInput * moveSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        animator.SetBool("isWalking", true);
        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("Last_Move_X", moveInput.x);
            animator.SetFloat("Last_Move_Y", moveInput.y);
            Vector3 vector3 = Vector3.left * moveInput.x + Vector3.down * moveInput.y;
            Aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
        }
        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("Horizontal", moveInput.x);
        animator.SetFloat("Vertical", moveInput.y);

        if (moveInput.x != 0 || moveInput.y != 0)
        {
            Vector3 vector3 = Vector3.left * moveInput.x + Vector3.down * moveInput.y;
            Aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
            animator.SetFloat("Last_Move_X", moveInput.x);
            animator.SetFloat("Last_Move_Y", moveInput.y);
        }
   

    }
}
