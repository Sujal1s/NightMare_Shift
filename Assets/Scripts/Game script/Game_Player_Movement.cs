using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class playermovement : MonoBehaviour
{
  
    // floats
    public float speed;
    private float Yspeed;
    public float jumpforce;
    public float djumpforce;
    
    //dash

    public float dashV ;
    public float dashtime ;
    
    // vector2 
    private Vector2 dashDir;
    
    // compoants
    
    Rigidbody2D rb;
    SpriteRenderer sr;
    TrailRenderer tr;
    
    //layes
    
    public LayerMask groundlayer;
    
    //bools
    
    bool isground; 
    bool djump; 
    bool isdash;
    bool candash = true;
    
    //Transform
    public Transform groundcheck;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        jumpmechanic();
      
    }

    void FixedUpdate()
    {
        movement();
        flipsr();
        groundcheckf();
        dashmechanic();
    }

    void movement()
    
    {
        Yspeed =  Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(Yspeed * speed , rb.linearVelocity.y);
      
    }

    void jumpmechanic()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isground)
            {
                jump();
                djump = true;
            }
            else if (djump)

            {
                daublejump();
                djump = false;
            }
             
        }
    }

    void jump()
    {
        rb.linearVelocity = Vector2.up * jumpforce;
    }

    void daublejump()
    {
        rb.linearVelocity = Vector2.up * djumpforce;
    }

    void groundcheckf()
    {
        isground = Physics2D.OverlapCircle(groundcheck.position, 0.2f, groundlayer);
    }

    void flipsr()
    {
        if (rb.linearVelocity.x  < -0.1f)
        {
            sr.flipX = true;
            
        }
        else if (rb.linearVelocity.x > 0.1f)
        {
            sr.flipX = false;
        }
    }

    void dashmechanic()
    {
        var dashinput = Input.GetKeyDown(KeyCode.LeftShift);

        if (dashinput && candash)
        {
            isdash = true;
            candash = false;
            tr.emitting = true; 

            dashDir = new Vector2(Yspeed , Input.GetAxisRaw("Vertical"));
            if (dashDir == Vector2.zero)
            {
                dashDir = new Vector2(transform.localScale.x, 0);
            }

            StartCoroutine(stopdashing());
            

        }
        // animator.setbool("isdash" , isdash);

        if (isdash)
        {
            rb.linearVelocity = dashDir.normalized * dashV;
            return;
        }

        if (isground)
        {
            candash = true;
        }
    }

    private IEnumerator stopdashing()
    {
        yield return new WaitForSeconds(dashtime);
        tr.emitting = false;
        isdash = false;
        
    }
    
}