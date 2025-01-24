using System;
using Unity.VisualScripting;
using UnityEngine;

public class playermovement : MonoBehaviour
{
  
    public float speed;
    private float Yspeed;
    public float jumpforce;
    public float djumpforce;

    Rigidbody2D rb;
    SpriteRenderer sr;
    public LayerMask groundlayer;
    bool isground; 
    bool djump;
    public Transform groundcheck;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
       
    }

    private void Update()
    {
        jumpbutton();
      
    }

    void FixedUpdate()
    {
        movement();
        flipsr();
        groundcheckf();
    }

    void jumpbutton()
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
        rb.velocity = Vector2.up * jumpforce;
    }
    void daublejump()
    {
        rb.velocity = Vector2.up * djumpforce;
    }
    void groundcheckf()
    {
        isground = Physics2D.OverlapCircle(groundcheck.position, 0.2f, groundlayer);
    }
    void movement()
    {
        Yspeed =  Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(Yspeed * speed , rb.velocity.y);
      
    }

    void flipsr()
    {
        if (rb.velocity.x  < -0.1f)
        {
            sr.flipX = true;
            
        }
        else if (rb.velocity.x > 0.1f)
        {
            sr.flipX = false;
        }
    }
}