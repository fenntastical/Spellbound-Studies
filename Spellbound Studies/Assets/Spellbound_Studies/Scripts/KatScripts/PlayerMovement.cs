using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Base movement speed
    public Rigidbody2D rb;
    private Vector2 moveInput;
    public Animator animator;
    public Animator swordAnimator;
    public Animator swordEffectAnimator;
    public Transform Aim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //swordAnimator = GetComponent<Animator>();
        //swordEffectAnimator = GetComponent<Animator>();
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

            swordAnimator.SetBool("isWalking", false);
            swordAnimator.SetFloat("Last_Move_X", moveInput.x);
            swordAnimator.SetFloat("Last_Move_Y", moveInput.y);

            swordEffectAnimator.SetBool("isWalking", false);
            swordEffectAnimator.SetFloat("Last_Move_X", moveInput.x);
            swordEffectAnimator.SetFloat("Last_Move_Y", moveInput.y);

            Vector3 vector3 = Vector3.left * moveInput.x + Vector3.down * moveInput.y;
            Aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
        }
        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("Horizontal", moveInput.x);
        animator.SetFloat("Vertical", moveInput.y);

        swordAnimator.SetFloat("Horizontal", moveInput.x);
        swordAnimator.SetFloat("Vertical", moveInput.y);

        swordEffectAnimator.SetFloat("Horizontal", moveInput.x);
        swordEffectAnimator.SetFloat("Vertical", moveInput.y);

        if (moveInput.x != 0 || moveInput.y != 0)
        {
            Vector3 vector3 = Vector3.left * moveInput.x + Vector3.down * moveInput.y;
            Aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
            animator.SetFloat("Last_Move_X", moveInput.x);
            animator.SetFloat("Last_Move_Y", moveInput.y);

            swordAnimator.SetFloat("Last_Move_X", moveInput.x);
            swordAnimator.SetFloat("Last_Move_Y", moveInput.y);

            swordEffectAnimator.SetFloat("Last_Move_X", moveInput.x);
            swordEffectAnimator.SetFloat("Last_Move_Y", moveInput.y);
        }
    }

    // Coroutine to increase speed temporarily
    public IEnumerator IncreaseSpeed(float speedBoost, float duration)
    {
        moveSpeed += speedBoost; // Increase the speed
        Debug.Log($"Speed increased to {moveSpeed} for {duration} seconds!");

        yield return new WaitForSeconds(duration); // Wait for the duration

        moveSpeed -= speedBoost; // Revert speed
        Debug.Log($"Speed returned to {moveSpeed}");
    }
}