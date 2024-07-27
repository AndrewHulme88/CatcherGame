using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject dustParticles;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundCheckDistance = 1f;
    [SerializeField] private float groundCheckOffsetY = -0.5f;

    private Rigidbody2D rb;
    private Animator anim;
    private float moveInput;
    private bool isGrounded;
    private bool facingRight = false;
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

        dustParticles.SetActive(isMoving && isGrounded);
    }

    private void Move()
    {
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
        isGrounded = Physics2D.OverlapCircle(checkPosition, groundCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Vector2 gizmosPosition = new Vector2(transform.position.x, transform.position.y + groundCheckOffsetY);
        Gizmos.DrawWireSphere(gizmosPosition, groundCheckDistance);
    }
}
