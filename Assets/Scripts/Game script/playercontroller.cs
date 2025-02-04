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
    private bool canDash = true;
    private bool isDashing;

    private Vector2 moveInput;

    private bool isjump;
    public bool isground { get; private set; }
    private bool isFacingRight = true;
    
    private Rigidbody2D rb;
    public Transform groundcheck;
    public LayerMask groundLayer;
    public TrailRenderer tr;  
    public RealmShift realmShift;  

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GroundCheck();
        Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
            return;
        
        rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);
    }

  
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    
    public void OnJump(InputAction.CallbackContext context)
    {
        
        if (context.started)
        {
           
            if (isground)
            {
                JumpAction();
                isjump = true; 
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
        if(dashDirection == Vector2.zero)
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