using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    public float speed;
    public float Jump;
    public float Djump;
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;
    
    bool isjump;
    bool canDash = true;
    bool isDashing;
    private bool isFacingRight = true;
    bool isground;
    
    public Rigidbody2D rb;
    public Transform groundcheck;
    public LayerMask groundLayer;
    public TrailRenderer tr;
    
    public RealmShift realmShift;

    private void Update()
    {
        Flip();
        jumpbutton();
        groundcheckf();

        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && CanUseAbilities())
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    
    void jumpbutton()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isground)
            {
                jump();
                isjump = true;
            }
            else
            {
                // Only allow double jump if realm is shifted
                if (CanUseAbilities())
                {
                    daublejump();
                    isjump = false;
                }
            }
        }
    }

    void groundcheckf()
    {
        
        isground = Physics2D.OverlapCircle(groundcheck.position, 0.2f, groundLayer);
        Debug.Log("groundcheckf");
        
    }

    private void Flip()
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void jump()
    {
        rb.velocity = Vector2.up * Jump;
    }

    void daublejump()
    {
        rb.velocity = Vector2.up * Djump;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(horizontal * dashingPower, vertical * dashingPower);
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
        // Make sure the realmShift reference is assigned and the player is in the realm shifted state
        return realmShift != null && realmShift.isRealmShifted;
    }
}