using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
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
    [SerializeField] private PolygonCollider2D polygonCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        

    }

    private void Update()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.z = 2.91f;  // Lock Z-axis to 1.16
        transform.position = currentPosition;
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


    private void UpdateAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("_ismoving", ismoving);
            animator.SetBool("_isjump", isground);

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

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.started && canDash && CanUseAbilities())
        {
            StartCoroutine(Dash());
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
  
    /*void UpdateColliderShape()
    {
        // Get the current sprite from the SpriteRenderer
        Sprite currentSprite = spriteRenderer.sprite;

        // Clear the current path and set the new path based on the current sprite
        polygonCollider.pathCount = currentSprite.GetPhysicsShapeCount();
        for (int i = 0; i < polygonCollider.pathCount; i++)
        {
            var path = new List<Vector2>();
            currentSprite.GetPhysicsShape(i, path);
            polygonCollider.SetPath(i, path.ToArray());
        }
    }*/
}        