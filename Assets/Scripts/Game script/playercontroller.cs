using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class PlayerController : MonoBehaviour
{
    public float speed;
    public float Jump;
    public float Djump;

    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;

    private Vector2 moveInput;
    private bool ismoving;
    private bool canDash = true;
    private bool isDashing;
    private bool isjump;
    public bool isground { get; private set; }
    private bool isFacingRight = true;

    private Rigidbody2D rb;
    private Animator animator;
    public Transform groundcheck;
    public LayerMask groundLayer;
    public TrailRenderer tr;
    public RealmShift realmShift;
    private PlayerInput playerInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>(); // Get the PlayerInput component
    }

    private void Update()
    {
        if (playerInput.enabled) // Only allow input if player input is enabled
        {
            GroundCheck();
            Flip();
            UpdateAnimation();
            jumpaction();
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
            return;

        if (playerInput.enabled) // Only allow movement if player input is enabled
        {
            rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);
            dashaction();
        }
    }

    private void UpdateAnimation()
    {
        if (animator != null)
        {

            animator.SetBool("_isjump", isground);
            animator.SetBool("_isdashing" , isDashing);


        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        ismoving = moveInput != Vector2.zero;
    }

    void dashaction()
    {
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton2)) && canDash && CanUseAbilities())
        {
            StartCoroutine(Dash());
        }
    }

    void jumpaction()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            if (isground)
            {
                jump();
                isjump = false;
            }
            else
            {
                if (!isjump && CanUseAbilities())
                {
                    daublejump();
                    isjump = true;
                }
            }
        }
    }

    private void jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, Jump);
    }

    private void daublejump()
    {
        rb.velocity = new Vector2(rb.velocity.x, Djump);
    }

    private void GroundCheck()
    {
        isground = Physics2D.OverlapCircle(groundcheck.position, 0.2f, groundLayer);
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
        animator.SetBool("_isDashing", true);
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
        animator.SetBool("_isDashing", false);

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private bool CanUseAbilities()
    {
        return realmShift != null && realmShift.isRealmShifted;
    }

    // **Function to Enable/Disable Player Input**
    public void SetPlayerInputActive(bool isActive)
    {
        if (playerInput != null)
        {
            playerInput.enabled = isActive;
        }
    }
}
