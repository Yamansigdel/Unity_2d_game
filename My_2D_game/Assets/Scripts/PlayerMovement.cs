using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;
    private float dirX = 0f;
   [SerializeField] private float movespeed = 7f;
   [SerializeField] private float jumpspeed = 14f;

    [SerializeField] private LayerMask jumpableGround;

    private enum MovementState {idle, running, jumping, falling}

    [SerializeField] private AudioSource jumpSoundEffect;

   
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll= GetComponent<BoxCollider2D>();
        sprite= GetComponent<SpriteRenderer>(); 
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * movespeed, rb.velocity.y); 

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpspeed); 
        }

        UpdateAnimationState();
    }
     

    private void UpdateAnimationState()
    {
        MovementState state;
        if (dirX > 0f)
        {
           state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if(rb.velocity.y > .1f)
        {
            state=MovementState.jumping;
        }
        else if(rb.velocity.y < -.1f)
        {
            state=MovementState.falling;
        }
    
        anim.SetInteger("state", (int)state);
    }
    
    private bool IsGrounded()
    {
        //To check if the player is grounded and stop double jump feature
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}