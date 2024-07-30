using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject dustParticles;
    [SerializeField] private GameObject landingParticles;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundCheckDistance = 1f;
    [SerializeField] private float groundCheckOffsetY = -0.5f;
    [SerializeField] private float landingDelay = 0.5f;

    private Rigidbody2D rb;
    private Animator anim;
    private float moveInput;
    private bool isGrounded;
    private bool wasGrounded;
    private bool canMove = true;
    private bool facingRight = true;
    private int facingDirection = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
        CollisionChecks();
        FlipController();
        AnimationController();
    }

    private void AnimationController()
    {
        bool isMoving = Mathf.Abs(rb.velocity.x) > 0.1f;

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);

        var particleSystem = dustParticles.GetComponent<ParticleSystem>();

        if(isMoving && isGrounded)
        {
            if(!particleSystem.isEmitting)
            {
                particleSystem.Play();
            }
        }
        else
        {
            if(particleSystem.isEmitting)
            {
                particleSystem.Stop(withChildren: true, stopBehavior: ParticleSystemStopBehavior.StopEmitting);
            }
        }
    }

    private void Move()
    {
        if (!canMove)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveSpeed * moveInput, rb.velocity.y);
    }

    private void FlipController()
    {
        if (facingRight && rb.velocity.x < -0.1f)
        {
            Flip();
        }
        else if (!facingRight && rb.velocity.x > 0.1f)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void Jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void CollisionChecks()
    {
        Vector2 checkPosition = new Vector2(transform.position.x, transform.position.y + groundCheckOffsetY);
        wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(checkPosition, groundCheckDistance, whatIsGround);

        if(!wasGrounded && isGrounded)
        {
            TriggerLandingParticles();
            StartCoroutine(LandingDelay());
        }
    }

    private void TriggerLandingParticles()
    {
        var landingParticleSystem = landingParticles.GetComponent<ParticleSystem>();
        landingParticleSystem.Play();
    }

    private IEnumerator LandingDelay()
    {
        canMove = false;
        yield return new WaitForSeconds(landingDelay);
        canMove = true;
    }

    private void OnDrawGizmos()
    {
        Vector2 gizmosPosition = new Vector2(transform.position.x, transform.position.y + groundCheckOffsetY);
        Gizmos.DrawWireSphere(gizmosPosition, groundCheckDistance);
    }
}
