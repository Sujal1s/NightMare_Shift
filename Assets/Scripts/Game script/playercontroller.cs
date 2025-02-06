using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float Jump;
    public float Djump;

    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;

    private Vector2 moveInput;
    private Vector2 jumpInput;

    private bool ismoving; // Flag indicating whether the player is moving
    private bool canDash = true;
    private bool isDashing;
    private bool isjump;
    public bool isground { get; private set; }
    private bool isFacingRight = true;

    private Rigidbody2D rb;
    private Animator animator; // Reference to the Animator component

    public Transform groundcheck;
    public LayerMask groundLayer;
    public TrailRenderer tr;
    public RealmShift realmShift;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Gets the Animator component attached to this GameObject
    }

    private void Update()
    {
        GroundCheck();
        Flip();
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        if (isDashing)
            return;

        rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);
    }

    // Update the animator's "isMoving" and "isJumping" parameters
    private void UpdateAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("_ismoving", ismoving);
            animator.SetBool("_isjump", !isground);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        ismoving = moveInput != Vector2.zero;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isground)
            {
                JumpAction();
                isjump = true;
                animator.SetBool("_isjump", true);
            }
            else
            {
                if (CanUseAbilities() && isjump)
                {
                    DoubleJumpAction();
                    isjump = false;
                }
            }
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.started && canDash && CanUseAbilities())
        {
            StartCoroutine(Dash());
        }
    }

    private void JumpAction()
    {
        rb.velocity = new Vector2(rb.velocity.x, Jump);
    }

    private void DoubleJumpAction()
    {
        rb.velocity = new Vector2(rb.velocity.x, Djump);
    }

    private void GroundCheck()
    {
        isground = Physics2D.OverlapCircle(groundcheck.position, 0.2f, groundLayer);
        if (isground)
        {
            isjump = false;
            animator.SetBool("_isjump", false);
        }
    }

    private void Flip()
    {
        if ((isFacingRight && moveInput.x < 0f) || (!isFacingRight && moveInput.x > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
        
            private IEnumerator Dash()
            {
                canDash = false;
                isDashing = true;
                float originalGravity = rb.gravityScale;
                rb.gravityScale = 0f;
        
                Vector2 dashDirection = moveInput;
                if (dashDirection == Vector2.zero)
                    dashDirection = Vector2.right * (isFacingRight ? 1 : -1);
        
                rb.velocity = dashDirection.normalized * dashingPower;
                tr.emitting = true;
        
                yield return new WaitForSeconds(dashingTime);
        
                tr.emitting = false;
                rb.gravityScale = originalGravity;
                isDashing = false;
        
                yield return new WaitForSeconds(dashingCooldown);
                canDash = true;
            }
        
            private bool CanUseAbilities()
            {
                return realmShift != null && realmShift.isRealmShifted;
            }
        }