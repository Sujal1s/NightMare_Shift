using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
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

    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        Flip();
        jumpbutton();
        groundcheckf();
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
            else if (isjump)
            {
                daublejump();
                isjump = false;
            }
             
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

    void groundcheckf()
    {
        isground = Physics2D.OverlapCircle(groundcheck.position, 0.2f, groundLayer);
    }


    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
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
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}